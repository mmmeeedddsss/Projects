using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Client___T
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);
            try
            {
                sck.Connect(ep);
            }
            catch
            {
                Console.WriteLine("FCK !");
                Console.Read();
                Environment.Exit(Environment.ExitCode);
            }
            Console.WriteLine( "Conn Succes !" );
            Console.WriteLine("Write Something !");
            byte[] data = Encoding.ASCII.GetBytes(Console.ReadLine());
            sck.Send(data);
        }
    }
}
