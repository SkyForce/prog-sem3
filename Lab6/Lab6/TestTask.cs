using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab6
{
    class TestTask : ITask
    {
        public void Run()
        {
            Thread.Sleep(5000);
        }
    }
}
