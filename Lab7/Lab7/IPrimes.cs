﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;


namespace Lab7
{
    [ServiceContract]
    interface IPrimes
    {
        [OperationContract]
        ArrayList GetPrimes(int n);
    }
}
