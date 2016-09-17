using MPI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Lab7
{
    class Program
    {
        static void Main(string[] args)
        {
            using (new MPI.Environment(ref args))
            {
                Intracommunicator comm = Communicator.world;
                if (comm.Rank == 0)
                {
                    ServiceHost host = new ServiceHost(typeof(Primes), new Uri("http://localhost:1239/MPI"));
                    host.AddServiceEndpoint(typeof(IPrimes), new BasicHttpBinding(), "");
                    host.Open();
                    Console.WriteLine("Сервер запущен");
                    Console.ReadLine();
                    for (int i = 1; i < comm.Size; i++)
                    {
                        comm.Send<Tuple<int, int>>(new Tuple<int, int>(-1, -1), i, 0);
                    }
                    host.Close();
                }
                else
                {
                    Primes p = new Primes();
                    while(true)
                    {
                        ArrayList r = p.GetPrimes(0);
                        if (r.Count > 0 && r.IndexOf(0) == -1)
                        {
                            break;
                        }
                    }
                }
            }
        }
    }
}
