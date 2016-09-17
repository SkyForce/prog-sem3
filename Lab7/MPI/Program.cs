using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPI
{
    class Program
    {
        static bool IsPrime(int n)
        {
            for (int i = 2; i <= Math.Sqrt(n); i++)
                if (n % i == 0) return false;
            return true;
        }
        public static void Main(String[] args)
        {
            
            int n = int.Parse(args[args.Length - 1]);
            using (new MPI.Environment(ref args))
            {
                Intracommunicator comm = Communicator.world;

                if(comm.Rank == 0)
                {
                    int s = n / comm.Size;
                    int t = s;
                    for(int i = 1; i < comm.Size - 1; i++)
                    {
                        comm.Send<Tuple<int, int>>(new Tuple<int, int>(t + 1, t + s), i, 0);
                        t += s;
                    }
                    if(comm.Size > 1) comm.Send<Tuple<int, int>>(new Tuple<int, int>(t + 1, n), comm.Size - 1, 0);

                    ArrayList temp = new ArrayList();
                    for (int i = 2; i <= s; i++)
                    {
                        if (IsPrime(i)) temp.Add(i);
                    }

                    comm.Barrier();

                    for (int i = 0; i < temp.Count; i++)
                        Console.WriteLine(temp[i]);

                    ArrayList r;
                    for (int i = 1; i < comm.Size; i++)
                    {
                        r = comm.Receive<ArrayList>(i, 0);
                        for (int j = 0; j < r.Count; j++)
                            Console.WriteLine(r[j]);
                    }
                }
                else
                {
                    ArrayList temp = new ArrayList();
                    Tuple<int,int> t = comm.Receive<Tuple<int, int>>(0, 0);
                    for(int i = t.Item1; i <= t.Item2; i++)
                    {
                        if (IsPrime(i)) temp.Add(i);
                    }
                    comm.Send<ArrayList>(temp, 0, 0);
                    comm.Barrier();
                    
                }
                

            }
        }
        
    }
}
