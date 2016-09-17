using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_3
{
    class PriorityQueue
    {
        static List<Task> queue = new List<Task>();
        static Task current = null;
        public static uint mainID;

        public static void Push(Task f)
        {
            queue.Add(f);
            queue = new List<Task>(queue.OrderBy(x => x.p.Priority));
        }

        public static Task Get()
        {
            Task t = queue.Last();
            queue.RemoveAt(queue.Count - 1);
            return t;
        }

        public static Task Peek()
        {
            return queue.Last();
        }

        public static int GetSize()
        {
            return queue.Count;
        }

        public static Task GetCurrent()
        {
            return current;
        }

        public static void SetCurrent(Task t)
        {
            current = t;
        }

        public static void DeleteCurrent()
        {
            current.IsTrash = true;
        }

     
    }
}
