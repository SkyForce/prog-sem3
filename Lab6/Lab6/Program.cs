using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab6
{
    class Program
    {
        static void Main(string[] args)
        {
            MyThreadPool pool = new MyThreadPool(5);
            for (int i = 0; i < 10; i++)
                pool.Submit(new TestTask());
            Thread.Sleep(15000);
            pool.Dispose();
        }

    }
}
