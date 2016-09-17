using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Lab1
{
    class Sender
    {
        string port, host;
        List<Tuple<IMessage, string>> services = new List<Tuple<IMessage, string>>();
        List<string> ids;
        public Sender(List<Tuple<IMessage, string>> serv, string h, string p, List<string> l)
        {
            services = serv;
            host = h;
            port = p;
            ids = l;
        }

        public void Send(string msg)
        {
            List<Tuple<IMessage, string>> del = new List<Tuple<IMessage, string>>();
            foreach (Tuple<IMessage,string> s in services)
            {
                try
                {
                    s.Item1.Send(msg);
                }
                catch (Exception e) { del.Add(s); }
            }
            foreach (Tuple<IMessage, string> d in del)
                services.Remove(d);
        }

        public void Connect(string h, string p)
        {
            Uri tcpUri = new Uri(String.Format("http://{0}:{1}/Chat", h, p));
            EndpointAddress address = new EndpointAddress(tcpUri);
            BasicHttpBinding binding = new BasicHttpBinding();
            ChannelFactory<IMessage> factory = new ChannelFactory<IMessage>(binding, address);
            IMessage service = factory.CreateChannel();
            ((IClientChannel)service).OperationTimeout = TimeSpan.FromSeconds(7);
            services.Add(new Tuple<IMessage, string>(service, h+p));
            service.Send("-"+host+":"+port);
        }

    }
}
