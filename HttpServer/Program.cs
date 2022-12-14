// See https://aka.ms/new-console-template for more information
using HttpServer.MTCG;
using System;
using System.Net;
using System.Net.Sockets;

Console.WriteLine("Simple http-server! http://localhost:10001/");
Console.WriteLine();

var server = new HttpServer.HTTP.HttpServer(IPAddress.Any, 10001);
server.RegisterEndpoint("/users", new UsersEndpoint());
server.run();
