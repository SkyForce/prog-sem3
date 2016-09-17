using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter your nick, host and port: ");
            string nick = Console.ReadLine();
            string h = Console.ReadLine();
            string myport = Console.ReadLine();

            List<Tuple<IMessage, string>> serv = new List<Tuple<IMessage, string>>();
            List<string> ids = new List<string>();

            Reciever rec = new Reciever(serv, ids, h, myport);
            ServiceHost host = new ServiceHost(rec, new Uri(String.Format("http://localhost:{0}/Chat", myport)));
            host.AddServiceEndpoint(typeof(IMessage), new BasicHttpBinding(), "");
            host.Open();

            Sender send = new Sender(serv, h, myport, ids);
            Console.WriteLine("Connect to somebody? (y/n): ");
            string ans = Console.ReadLine();
            if(ans == "y")
            {
                Console.WriteLine("Enter host and port: ");
                string hst = Console.ReadLine();
                string port =(Console.ReadLine());
                send.Connect(hst, port);
            }
            int count = 0;
            while (true)
            {
                ConsoleKey key = ConsoleKey.NoName;
                if (Console.KeyAvailable) key = Console.ReadKey().Key;
                if (key == ConsoleKey.Enter)
                {
                    Console.Write(nick + ": ");
                    string msg = Console.ReadLine();
                    count++;
                    ids.Add(nick + count);
                    msg = h + ":" + myport + ":" + count + ":" + nick + ": " + msg;
                    send.Send(msg);
                }
                else if(key == ConsoleKey.F3)
                {
                    Console.WriteLine("Enter host and port: ");
                    string hst = Console.ReadLine();
                    string port = (Console.ReadLine());
                    send.Connect(hst, port);
                }
            }
        }
    }
}
