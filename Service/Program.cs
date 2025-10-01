using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace Service
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(SessionService));
            DroneSampleListener listener = new DroneSampleListener();

            // Transfer
            SessionService.OnTransferStarted += listener.OnSampleEvent;
            SessionService.OnTransferCompleted += listener.OnSampleEvent;

            // Sample
            SessionService.OnSampleReceived += listener.OnSampleEvent;

            // Spike
            SessionService.VoltageSpikeEvent += listener.OnSampleEvent;
            SessionService.CurrentSpikeEvent += listener.OnSampleEvent;

            // Warning
            SessionService.OnWarningRaised += listener.OnSampleEvent;

            Console.WriteLine("Server started!");

            host.Open();
            Console.ReadLine();
            host.Close();
            // Dispose???
        }
    }
}
