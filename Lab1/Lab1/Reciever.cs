using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab1
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    class Reciever : IMessage
    {
        List<string> ids;
        List<Tuple<IMessage,string>> services;
        string host, port;
        public Reciever(List<Tuple<IMessage, string>> serv, List<string> id, string h, string p)
        {
            services = serv;
            ids = id;
            host = h;
            port = p;
        }

        public void Send(string s)
        {
            
            if(s.StartsWith("-"))
            {
                string h = s.Substring(1, s.IndexOf(':') - 1);
                string p = s.Substring(s.IndexOf(':') + 1);
                Uri tcpUri = new Uri(String.Format("http://{0}:{1}/Chat", h, p));
                EndpointAddress address = new EndpointAddress(tcpUri);
                BasicHttpBinding binding = new BasicHttpBinding();
                ChannelFactory<IMessage> factory = new ChannelFactory<IMessage>(binding, address);
                IMessage service = factory.CreateChannel();
                ((IClientChannel)service).OperationTimeout = TimeSpan.FromSeconds(7);
                services.Add(new Tuple<IMessage, string>(service, h+p));
                return;
            }
            string[] v = s.Split(':');
            string hh = v[0];
            string pp = v[1];
            string num = v[2];
            string nick = v[3];
            string msg="";
            for (int i = 4; i < v.Length; i++) msg += v[i];

            if (ids.Exists(x => x == nick + num)) return;
            ids.Add(nick + num);
            Console.WriteLine(nick+":"+msg);

            List<Tuple<IMessage, string>> del = new List<Tuple<IMessage,string>>();
            foreach (Tuple<IMessage, string> ser in services)
            {
                if (ser.Item2 == hh+pp) continue;
                try
                {
                    ser.Item1.Send(host + ":" + port + ":" + num + ":" + nick + ":" + msg);
                }
                catch (Exception e) { del.Add(ser); }
                foreach (Tuple<IMessage, string> d in del)
                    services.Remove(d);
            }
        }


    }
}
