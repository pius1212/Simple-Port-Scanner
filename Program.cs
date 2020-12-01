using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Port_Scanner
{
    class Program
    {
        static List<int> openPort = new List<int>();

        public static void PingHost(string hostUri, int portNumber)
        {
            try
            {
                using (var client = new TcpClient(hostUri, portNumber))
                {
                    client.ReceiveTimeout = 5000;
                    openPort.Add(portNumber);
                }

                Console.WriteLine("port open at: " + hostUri + ":" + portNumber);
            }
            catch (SocketException)
            {
                Console.WriteLine("cannot ping host at " + hostUri + ":" + portNumber);
            }
        }

        static void Main(string[] args)
        {
            Console.Write("IP to scan: ");
            String ip = Console.ReadLine();
            String build = "Ports open: ";

            //ping host
            for(int i = 1; i < 65535; i++)
            {
                //be less dumb and fix this
                Thread t = new Thread(() => PingHost(ip, i));

                t.Start();
            }

            //buildString
            foreach (int i in openPort)
            {
                build += i + ", ";
            }

            Console.WriteLine(build);
            Console.WriteLine("Finished");
            Thread.Sleep(-1);

        }
    }
}
