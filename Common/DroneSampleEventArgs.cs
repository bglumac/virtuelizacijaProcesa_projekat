using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class DroneSampleEventArgs : EventArgs
    {
        public int drone_id { get; }
        public int row { get; }
        public string message { get; }

        public DroneSampleEventArgs(int drone_id, int row, string message)
        {
            this.drone_id = drone_id;
            this.row = row;
            this.message = message;
        }
    }
}
