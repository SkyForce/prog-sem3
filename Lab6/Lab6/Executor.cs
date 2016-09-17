using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab6
{
    class Executor
    {
        int state = 0, exitState = 0;
        ManualResetEvent ev = new ManualResetEvent(false);
        ITask task;
        Queue<ITask> tasks;
        public Executor(Queue<ITask> q, int i)
        {
            tasks = q;
            new Thread(() =>
            {
                while (true)
                {
                    
                    Monitor.Enter(tasks);
                    if (exitState == 1)
                    {
                        Monitor.Exit(tasks);
                        break;
                    }
                    state = 0;
                    if (tasks.Count > 0)
                    {
                        state = 1;
                        task = tasks.Dequeue();
                    }
                    Monitor.Exit(tasks);

                    if (state == 1)
                    {
                        task.Run();
                        Console.WriteLine("{0} has done the task", i);
                    }
                    else
                    {
                        ev.Reset();
                        ev.WaitOne();
                    }
                }
            }).Start();
            
        }

        public bool IsWait()
        {
            return !ev.WaitOne(0);
        }

        public void Notify()
        {
            ev.Set();
        }

        public void Exit()
        {
            Monitor.Enter(tasks);
            exitState = 1;
            Monitor.Exit(tasks);
            Notify();
        }
    }
}
