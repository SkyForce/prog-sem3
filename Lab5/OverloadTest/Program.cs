using System;
using System.ServiceModel;
using System.Threading;
using System.Diagnostics;

namespace Overloads
{
    public class UnitTest1
    {
        public static void Main()
        {
            const int NUM_OF_QUERIES = 100000;
            Random random = new Random(239);
            Object exceptionCounterLock = new Object();

            //			ServerProgram.OpenServer();

            Thread[] threads;
            threads = new Thread[NUM_OF_QUERIES];
            int exceptionCounter = 0;

            for (int i = 0; i < NUM_OF_QUERIES; i++)
            {
                try
                {

                    threads[i] = new Thread(new ParameterizedThreadStart(
                        (Object num) =>
                        {
                            try
                            {
                                Uri tcpUri = new Uri("http://localhost:1050/TestService");
                                EndpointAddress address = new EndpointAddress(tcpUri);
                                BasicHttpBinding binding = new BasicHttpBinding();
                                ChannelFactory<IPrimes> factory = new ChannelFactory<IPrimes>(binding, address);
                                //IPrimes service = factory.CreateChannel();
                                //Console.WriteLine("Вызываю метод сервиса...?");
                                //for (int i = 0; i < 1000000; i++)
                                //{
                                    IPrimes service = factory.CreateChannel();
                                    service.GetPrimes(100);
                                //Assert.AreEqual(answer, true, "Wrong Answer");
                            }
                            catch(Exception e)
                            {
                                lock (exceptionCounterLock)
                                {
                                    Console.WriteLine(e.Message + " on thread " + num);
                                    exceptionCounter++;
                                }
                            }
                        }
                    ));
                }
                catch
                {
                    Console.WriteLine("Execption");
                    //Assert.IsFalse(true, "exception");
                }
            }

            for (int i = 0; i < NUM_OF_QUERIES; i++)
            {
                //Assert.AreNotEqual(threads[i], null, "Thread doesn't exsist");
                threads[i].Start(i);
            }

            for (int i = 0; i < NUM_OF_QUERIES; i++)
            {
                threads[i].Join();
            }

            Trace.WriteLine(("Num of exceptions = " + (exceptionCounter).ToString()));
            //			Assert.IsTrue(exceptionCounter == 0, "With exceptions");

            //proxy = ChannelFactory<IPrimes>.CreateChannel(new NetTcpBinding(),
              //  new EndpointAddress("http://localhost:1050/TestService"));
            //int ans = proxy.GetMaxNumOfThreads();
            //Trace.WriteLine("Max num of threads = " + (ans).ToString());
            //Assert.IsTrue(ans <= 4, "More than 4 thread");
            //Assert.IsTrue(ans >= 0, "Less than zero thread");

            //			ServerProgram.CloseServer();
        }

        
    }
}
