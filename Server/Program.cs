using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("A simple server running...\n");

            TcpListener serverListener = new TcpListener(IPAddress.Loopback, 8000);
            serverListener.Start(5);

            while (true)
            {
                TcpClient clientSocket = serverListener.AcceptTcpClient();
                new Task(() =>
               {
                   try
                   {
                       var writer = new StreamWriter(clientSocket.GetStream());
                       var reader = new StreamReader(clientSocket.GetStream());

                       writer.WriteLine("Welcome to my server!");
                       writer.WriteLine("Please enter your commands...");
                       writer.Flush();

                       string message;
                       do
                       {
                           message = reader.ReadLine();
                           Console.WriteLine($"received: {message}");
                       } while (message != "quit");
                   }
                   catch (Exception e)
                   {
                       Console.WriteLine($"error occured {e.Message}");
                   }
               }).Start();
            }
        }
    }
}
