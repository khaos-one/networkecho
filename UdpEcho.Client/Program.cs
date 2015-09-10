using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UdpEcho.Client
{
    internal class Program
    {
        internal static void Usage(int exitCode = 0)
        {
            Console.WriteLine("Usage: UdpEcho.Client <hostname> <port>");
            Console.WriteLine("Utility that send arbitary strings to");
            Console.WriteLine("and receives them back over UDP/IP.");
            Console.WriteLine();
            Environment.Exit(exitCode);
        }

        internal static void Main(string[] args)
        {
            if (args.Length < 2)
                Usage(-1);

            var hostname = args[0];
            int port = 0;

            if (!int.TryParse(args[1], out port))
                Usage(-2);

            var udp = new UdpClient();

            while (true)
            {
                IPEndPoint remote = null;

                try
                {
                    Console.Write("< ");
                    var input = Console.ReadLine();
                    var packet = Encoding.UTF8.GetBytes(input);
                    udp.Send(packet, packet.Length, hostname, port);
                    var received = udp.Receive(ref remote);
                    var output = Encoding.UTF8.GetString(received);
                    Console.WriteLine($"> {output}");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"! Exception: {e}.");
                }
            }
        }
    }
}
