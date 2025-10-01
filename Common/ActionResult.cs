using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [DataContract]
    public enum Result
    {
        [EnumMember]
        SUCCESS,
        [EnumMember]
        WARNING,
        [EnumMember]
        FAILED
    }

    [DataContract]
    public class ActionResult
    {
        [DataMember]
        public Result res {  get; set; }
        public string message { get; set; }

        public ActionResult(Result res) {
            this.res = res;
        }

        public ActionResult(Result res, String message)
        {
            this.res = res;
            this.message = message;
        }
    }
}
