using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UdpEcho
{
    internal class Program
    {
        internal static void Usage(int exitCode = 0)
        {
            Console.WriteLine("Usage: UdpEcho.Server.exe <port_number>");
            Console.WriteLine("Establishes an UDP/IP echo server.");
            Console.WriteLine();
            Environment.Exit(exitCode);
        }

        internal static void Main(string[] args)
        {
            if (args.Length < 1)
                Usage(1);

            int port = 0;

            if (!int.TryParse(args[0], out port))
                Usage(2);

            var udp = new UdpClient(port);
            
            while (true)
            {
                IPEndPoint remote = null;

                try
                {
                    var packet = udp.Receive(ref remote);
                    udp.Send(packet, packet.Length, remote);
                    Console.WriteLine($"-> Packet of {packet.Length} bytes received from {remote.Address.ToString()}.");
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exception: {e}.");
                }
            }
        }
    }
}
