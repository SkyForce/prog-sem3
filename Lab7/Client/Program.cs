using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Uri tcpUri = new Uri("http://localhost:1239/MPI");
            EndpointAddress address = new EndpointAddress(tcpUri);
            BasicHttpBinding binding = new BasicHttpBinding();
            ChannelFactory<IPrimes> factory = new ChannelFactory<IPrimes>(binding, address);
            IPrimes service = factory.CreateChannel();

            Console.WriteLine("Введите число, до которого искать простые числа: ");
            ArrayList l = service.GetPrimes(int.Parse(Console.ReadLine()));
            for (int i = 0; i < l.Count; i++)
                Console.WriteLine(l[i]);
            Console.ReadLine();
        }
    }
}
