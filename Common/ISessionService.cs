using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    [ServiceContract]
    internal interface ISessionService
    {
        [OperationContract]
        void StartSession();
        [OperationContract]
        void PushSample();
        [OperationContract]
        void EndSession();
    }
}
