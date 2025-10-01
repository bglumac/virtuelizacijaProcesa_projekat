using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
    public interface ISessionService
    {
        [OperationContract]
        ActionResult StartSession(int drone_id);
        [OperationContract]
        ActionResult PushSample(DroneSample sample);
        [OperationContract]
        void EndSession();
    }
}
