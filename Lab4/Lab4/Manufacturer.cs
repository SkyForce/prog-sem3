using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab4
{
    class Manufacturer
    {
        AtomicQueue queue;
        bool exit = false;
        public Manufacturer(AtomicQueue q)
        {
            queue = q;
        }

        public void StartPushing()
        {
            while (true)
            {
                if (exit) break;
                Interlocked.Increment(ref queue.size);
                Console.WriteLine("Object pushed");
                Thread.Sleep(100);
            }
        }

        public void Exit()
        {
            exit = true;
        }
    }
}
