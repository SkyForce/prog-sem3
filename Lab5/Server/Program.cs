using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(Primes), new Uri("http://localhost:1050/TestService"));
            host.AddServiceEndpoint(typeof(IPrimes), new BasicHttpBinding(), "");
            host.Open();
            Console.WriteLine("Сервер запущен");
            Console.ReadLine();

            host.Close();
        }
    }
}
