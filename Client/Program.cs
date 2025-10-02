using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Microsoft.SqlServer.Server;

namespace Client
{
    internal class Program
    {
        public static string folder = ConfigurationManager.AppSettings["folder"];
        public static ISessionService service;
        public static string path;
        public static int drone_id;
        static void Main(string[] args)
        {
            ChannelFactory<ISessionService> factory = new ChannelFactory<ISessionService>("SessionService");
            Console.WriteLine("Client started !");

            // Sacekaj malo da se poveze
            Thread.Sleep(1000);

            // Izbor fajla?
            string file = SelectFlight();
            //drone_id = int.Parse(file);
            path = Path.Combine(folder, file + ".csv");
            //int drone_id = 26; // Ovo valjda generisemo random ;p. Ili samo uzmi broj fajla?
            // 

            service = factory.CreateChannel();

            try
            {
                //Console.WriteLine("id dron " + drone_id);
                ActionResult res = service.StartSession(drone_id);
                Console.WriteLine($"Server -> {res.message}");

                // Salji samplove
                SendDroneSamples(drone_id, path);
                // Zatvori
                service.EndSession();
            }

            catch (FaultException<DroneServiceException> ex)
            {
                Console.WriteLine($"ERR: {ex.Message}");
            }

            Console.ReadLine();
        }


        public static string SelectFlight()
        {
            var files = Directory.GetFiles(folder, "*.csv");

            Console.WriteLine($"Number of flights found {files.Length}");

            string selected = null;
            int id = 0;

            while(selected == null)
            {
                Console.Write("Enter flight number: ");
                string input = Console.ReadLine();
                
                if(!int.TryParse(input, out int fNum))
                {
                    Console.Write("ERROR: Invalid number. Try again: ");
                    continue;
                }

                selected = files.Select(f => Path.GetFileNameWithoutExtension(f)).FirstOrDefault(name => name == fNum.ToString());

                if (selected == null)
                {
                    Console.Write($"ERROR: No file found for number {fNum}. Try again.");
                }
                else
                {
                    id = fNum;
                }
            }

            Console.WriteLine($"Selected file: {Path.GetFileName(selected)}");
            drone_id = id;

            return selected;
        }

        private static void SendDroneSamples(int id, string filePath)
        {
            try
            {
                using(var reader = new StreamReader(filePath))
                {
                    string header = reader.ReadLine();
                    string line;
                    int row = 1;

                    while((line = reader.ReadLine()) != null)
                    {
                       
                        var parts = line.Split(',');
                        var sample = new DroneSample(id, row, parts[0], parts[1], parts[2], parts[3], parts[4], parts[5], parts[6], parts[7],
                                        parts[8], parts[9], parts[10], parts[11], parts[12], parts[13], parts[14], parts[15], parts[16],
                                        parts[17], parts[18], parts[19], parts[20]);


                        Console.WriteLine($"Drone Sample: {id} | {row} | {sample.battery_c}");
                        var rez = service.PushSample(sample);
                        Console.WriteLine($"Server: {rez.message}");
                        row++;
                    }

                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("ERROR: Failed to load the file: " + ex.Message);
            }
        }
    }
}
