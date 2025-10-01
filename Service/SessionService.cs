using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Service
{
    public class SessionService : ISessionService
    {   
        // Events
        public static event EventHandler<DroneSampleEventArgs> VoltageSpikeEvent;
        public static event EventHandler<DroneSampleEventArgs> CurrentSpikeEvent;

        public static event EventHandler<DroneSampleEventArgs> OnTransferStarted;
        public static event EventHandler<DroneSampleEventArgs> OnTransferCompleted;
        public static event EventHandler<DroneSampleEventArgs> OnSampleReceived;
        public static event EventHandler<DroneSampleEventArgs> OnWarningRaised;

        // Variables
        private static StreamWriter sw;
        private static StreamWriter rejects;
        int rejectCount = 0;
        int drone_id;
        bool first = true;
        public static string path;
        private static DroneSample prev_sample = null;

        // Constants
        const float VoltageSpikeTresh = 0.1f;
        const float CurrentSpikeTresh = 0.1f;

        public ActionResult StartSession(int drone_id) { 
            Random rnd = new Random();
            string directory = "Data/" + drone_id  + "/" + DateTime.Now.ToString("yyyy-MM-dd") + "/";
            path = directory + "session.csv";

            try
            {
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                sw = new StreamWriter((new FileStream(path, FileMode.Create, FileAccess.Write)));
                rejects = new StreamWriter(new FileStream(directory + "rejects.csv", FileMode.Create, FileAccess.Write));


                if (File.Exists(path))
                {
                    Console.WriteLine("Session established!");

                    if (OnTransferStarted != null)
                    {
                        OnTransferStarted(this, new DroneSampleEventArgs(drone_id, 0, "Session established!"));
                    }

                    return new ActionResult(Result.SUCCESS);
                }

                throw new FaultException<DroneServiceException>(
                        new DroneServiceException("File error"),
                        new FaultReason("File error")
                    );


            }

            catch
            {
                throw new FaultException<DroneServiceException>(
                    new DroneServiceException("Couldn't create session file"),
                    new FaultReason("Couldn't create session file")
                    );
            }
        }

        public ActionResult PushSample(DroneSample sample)
        {
            try
            {
                if (String.IsNullOrEmpty(path))
                {
                    throw new FaultException<DroneServiceException>(
                            new DroneServiceException("Session not started"),
                            new FaultReason("Session not started")
                        );
                }

                if (sw == null || rejects == null)
                {
                    throw new FaultException<DroneServiceException>(
                            new DroneServiceException("Writers not initialized"),
                            new FaultReason("Writers not initialized")
                        );
                }

                ActionResult res = sample.Validate();

                if (res.res == Result.SUCCESS)
                {
                    sample.row = sample.row - rejectCount;
                    sw.WriteLine(sample.ToString());
                    sw.Flush();

                    if (prev_sample != null)
                    {
                        // Proveri stvari spikes errori sta god vetar
                    }

                    prev_sample = sample;

                    // Analiza?

                    if (OnSampleReceived != null)
                    {
                        OnSampleReceived(this, new DroneSampleEventArgs(sample.drone_id, sample.row, "Good sample!"));
                    }

                    return new ActionResult(Result.SUCCESS);
                }

                else
                {
                    sample.row = ++rejectCount;
                    rejects.WriteLine($"Bad sample: {sample.ToString()} ({res.message})");
                    rejects.Flush();

                    if (OnWarningRaised != null)
                    {
                        OnWarningRaised(this, new DroneSampleEventArgs(sample.drone_id, sample.row, "Sample rejected"));
                    }

                    return new ActionResult(Result.FAILED, res.message);
                }

            }

            catch (Exception ex)
            {
                {
                    throw new FaultException<DroneServiceException>(
                                new DroneServiceException($"Exception while adding sample ({ex.Message})"),
                                new FaultReason($"Exception while adding sample ({ex.Message})")
                            );
                }
            }
        }

        public void EndSession()
        {
            if (sw != null)
            {
                sw.Dispose();
                sw = null;
            }

            if (rejects != null)
            {
                rejects.Dispose();
                rejects = null;
            }

            Console.WriteLine("Process finished! Memory cleaned!");
            if (OnTransferCompleted != null)
            {
                OnTransferCompleted(this, new DroneSampleEventArgs(drone_id, 0, "Porcess finished!"));
            }
        }
    }
}
