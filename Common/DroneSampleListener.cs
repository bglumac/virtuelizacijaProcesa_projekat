using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class DroneSampleListener
    {
       public void OnSampleEvent(object sender, DroneSampleEventArgs e)
        {
            Console.WriteLine($"Event -> {e.drone_id} | {e.row} | {e.message}");
            // Logger?
        }
    }
}
