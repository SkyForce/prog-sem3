using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab6
{
    class MyThreadPool : IDisposable
    {
        Queue<ITask> tasks = new Queue<ITask>();
        int size;
        Executor[] exec;

        public MyThreadPool(int s)
        {

            size = s;
            exec = new Executor[s];
            for (int i = 0; i < size; i++ )
            {
                exec[i] = new Executor(tasks, i);
            }
        }

        public void Submit(ITask task)
        {
            Monitor.Enter(tasks);
            tasks.Enqueue(task);
            for (int i = 0; i < size; i++)
            {
                if (exec[i].IsWait())
                {
                    exec[i].Notify();
                    break;
                }
            }
            Monitor.Exit(tasks);

        }


        public void Dispose()
        {
            for (int i = 0; i < size; i++)
                exec[i].Exit();
        }
    }
}
