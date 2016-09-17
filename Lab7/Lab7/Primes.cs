using MPI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Lab7
{
    class Primes : IPrimes
    {
        
        bool IsPrime(int n)
        {
            for (int i = 2; i <= Math.Sqrt(n); i++)
                if (n % i == 0) return false;
            return true;
        }

        public ArrayList GetPrimes(int n)
        {
            ArrayList result = new ArrayList();

            Intracommunicator comm = Communicator.world;

            if (comm.Rank == 0)
            {
                int s = n / comm.Size;
                int t = s;
                for (int i = 1; i < comm.Size - 1; i++)
                {
                    comm.Send<Tuple<int, int>>(new Tuple<int, int>(t + 1, t + s), i, 0);
                    t += s;
                }
                if (comm.Size > 1) comm.Send<Tuple<int, int>>(new Tuple<int, int>(t + 1, n), comm.Size - 1, 0);

                for (int i = 2; i <= s; i++)
                {
                    if (IsPrime(i)) result.Add(i);
                }

                ArrayList r;
                for (int i = 1; i < comm.Size; i++)
                {
                    r = comm.Receive<ArrayList>(i, 0);
                    for (int j = 0; j < r.Count; j++)
                        result.Add(r[j]);
                }
                comm.Barrier();
            }
            else
            {
                
                ArrayList temp = new ArrayList();
                Tuple<int, int> t = comm.Receive<Tuple<int, int>>(0, 0);
                if (t.Item1 == -1)
                {
                    result.Add(-1);
                    return result;
                }
                for (int i = t.Item1; i <= t.Item2; i++)
                {
                    if (IsPrime(i)) temp.Add(i);
                }
                comm.Send<ArrayList>(temp, 0, 0);
                comm.Barrier();

            }

            return result;
        }
    }
}
