using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_3
{

    public delegate void EndedEventHandler(object sender, EventArgs e);
    class Program
    {
        static void Main(string[] args)
        {
            for(int i = 0; i < 3; i++)
            {
                PriorityQueue.Push(new Task());
            }
            MainFiber mn = new MainFiber();
            mn.Ended += mn_Ended;
            Fiber main = new Fiber(mn.Run);
            PriorityQueue.mainID = main.Id;
            mn.Run();
        }

        static void mn_Ended(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}
