using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_3
{

    public delegate void EndEventHandler(object sender, EventArgs e);
    class MainFiber
    {
        public event EndEventHandler Ended;

        public void Run()
        {
            
            while(PriorityQueue.GetSize() > 0)
            {
                Task t = PriorityQueue.Get();
                if (t == null) continue;

                if (PriorityQueue.GetCurrent() != null && !PriorityQueue.GetCurrent().IsTrash)
                    PriorityQueue.Push(PriorityQueue.GetCurrent());

                PriorityQueue.SetCurrent(t);

                if(t.IsRun)
                {
                    Fiber.Switch(t.fiberID);
                }
                else
                {
                    Fiber f = new Fiber(t.p.Run);
                    t.fiberID = f.Id;
                    t.IsRun = true;
                    Fiber.Switch(f.Id);
                }

                if(PriorityQueue.GetCurrent() != null && PriorityQueue.GetCurrent().IsTrash)
                {
                    Fiber.Delete(PriorityQueue.GetCurrent().fiberID);
                }

            }
            Console.WriteLine("done");
            Console.ReadKey();
            //System.Environment.Exit(0);
            Ended(this, EventArgs.Empty);

        }
    }
}
