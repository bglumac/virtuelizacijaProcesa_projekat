using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [DataContract]
    public class DroneServiceException
    {
        [DataMember]
        string message { get; set; }

        public DroneServiceException(string message)
        {
            this.message = message;
        }

    }
}
