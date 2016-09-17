using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_3
{
    class Task
    {

        public Process p;
        public uint fiberID;
        public bool IsTrash = false;
        public bool IsRun = false;

        public Task()
        {
            p = new Process();
        }
    }
}
