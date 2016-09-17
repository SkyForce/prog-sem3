using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{

    [ServiceContract]
    interface IMessage
    {
        [OperationContract]
        void Send(string s);

    }
}
