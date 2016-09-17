using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab4
{
    class Consumer
    {
        AtomicQueue queue;
        bool exit = false;
        public Consumer(AtomicQueue q)
        {
            queue = q;
        }

        public void StartPulling()
        {
            while (true)
            {
                if (exit) break;
                if(queue.size > 0)
                {
                    Interlocked.Decrement(ref queue.size);
                    Console.WriteLine("Object removed");
                }
            }
        }

        public void Exit()
        {
            exit = true;
        }
    }
}
