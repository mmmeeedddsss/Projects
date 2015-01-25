using System;
using System.Net.Sockets;
using System.Text;
using System.IO;
using System.Net;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket sck = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234);
            sck.Bind(ep);
            sck.Listen(5);
            Socket accepted = sck.Accept();
            Console.WriteLine( "Accepted !" );
            byte[] buffer = new byte[sck.SendBufferSize];
            int bytesRead = accepted.Receive(buffer);
            byte[] data = new byte[bytesRead];
            for (int i = 0; i < bytesRead; i++)
            {
                data[i] = buffer[i];
            }
            Console.WriteLine(Encoding.ASCII.GetString(data));
            Console.Read();
        }
    }
}