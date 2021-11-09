using HttpServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

// see also https://www.codeproject.com/Articles/137979/Simple-HTTP-Server-in-C
namespace HttpServer
{
    public class HttpServer
    {
        protected int port;
        TcpListener listener;

        public HttpServer(int port)
        {
            this.port = port;
        }

        public void Run()
        {
            listener = new TcpListener(IPAddress.Loopback, port);
            listener.Start(5);
            while (true)
            {
                TcpClient s = listener.AcceptTcpClient();
                HttpProcessor processor = new HttpProcessor(s, this);
                new Thread(processor.Process).Start();
                Thread.Sleep(1);
            }
        }
    }
}
