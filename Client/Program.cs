using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace Client
{
    internal class Program
    {
        public static ISessionService service;
        static void Main(string[] args)
        {
            ChannelFactory<ISessionService> factory = new ChannelFactory<ISessionService>("SessionService");
            Console.WriteLine("Client started !");

            // Sacekaj malo da se poveze
            Thread.Sleep(1000);

            // Izbor fajla?
            int drone_id = 26; // Ovo valjda generisemo random ;p. Ili samo uzmi broj fajla?
            // 

            service = factory.CreateChannel();

            try
            {
                ActionResult res = service.StartSession(drone_id);
                Console.WriteLine($"Server -> ${res.message}");

                // Salji samplove

                // Zatvori
                service.EndSession();
            }

            catch (FaultException<DroneServiceException> ex)
            {
                Console.WriteLine($"ERR: {ex.Message}");
            }

            Console.ReadLine();
        }
    }
}
