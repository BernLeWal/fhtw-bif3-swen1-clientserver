﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.HTTP
{
    public class HttpProcessor
    {
        private TcpClient clientSocket;
        private HttpServer httpServer;

        public HttpProcessor(HttpServer httpServer, TcpClient clientSocket)
        {
            this.httpServer = httpServer;
            this.clientSocket = clientSocket;
        }

        public void run()
        {
            var reader = new StreamReader(clientSocket.GetStream());
            var request = new HttpRequest(reader);
            request.Parse();
            var writer = new StreamWriter(clientSocket.GetStream()) { AutoFlush = true };
            var response = new HttpResponse(writer);

            var endpoint = httpServer.Endpoints[request.Path[1]];
            if ( endpoint!=null )
            {
                endpoint.HandleRequest(request, response);
            }
            else
            {
                //Thread.Sleep(10000);
                response.ResponseCode = 404;
                response.ResponseText = "Not Found";
                response.Content = "<html><body>Not found!</body></html>";
                response.Headers.Add("Content-Length", response.Content.Length.ToString());
                response.Headers.Add("Content-Type", "text/plain"); // application/json
            }
            response.Process();
        }
    }
}
