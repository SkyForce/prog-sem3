using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            AtomicQueue q = new AtomicQueue();
            Manufacturer m = new Manufacturer(q);
            Consumer c = new Consumer(q);

            Thread t1 = new Thread(() => m.StartPushing());
            Thread t2 = new Thread(() => c.StartPulling());
            t1.Start();
            t2.Start();

            Thread.Sleep(15000);
            m.Exit();
            c.Exit();

        }
    }
}
