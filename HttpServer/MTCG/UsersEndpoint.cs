﻿using HttpServer.HTTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using HttpMethod = HttpServer.HTTP.HttpMethod;

namespace HttpServer.MTCG
{
    public class UsersEndpoint : IHttpEndpoint
    {

        public void HandleRequest(HttpRequest rq, HttpResponse rs)
        {
            switch (rq.Method)
            {
                case HttpMethod.POST:
                    CreateUser(rq, rs);
                    break;
                case HttpMethod.GET:
                    GetUsers(rq, rs);
                    break;
            }
        }

        private void CreateUser(HttpRequest rq, HttpResponse rs)
        {
            try
            {
                var user = JsonSerializer.Deserialize<User>(rq.Content);

                // call BL

                rs.ResponseCode = 201;
                rs.ResponseText = "OK";
            }
            catch (Exception)
            {
                rs.ResponseCode = 400;
                rs.Content = "Failed to create User! ";
            }
        }

        private void GetUsers(HttpRequest rq, HttpResponse rs)
        {
            List<User> users = new List<User>();
            users.Add(new User("Rudi Ratlos", "1234"));
            users.Add(new User("Susi Sorglos", "0000"));

            rs.Content = JsonSerializer.Serialize(users);
            rs.ResponseCode = 200;
            rs.ResponseText= "OK";
        }

    }
}
