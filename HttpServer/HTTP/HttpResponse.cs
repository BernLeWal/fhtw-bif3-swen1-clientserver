using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.HTTP
{
    public class HttpResponse
    {
        private StreamWriter writer;
        public int ResponseCode { get; set; }
        public string ResponseText { get; set; }

        public Dictionary<string, string> Headers { get; }
        public string Content { get; set; }

        public HttpResponse(StreamWriter writer)
        {
            this.writer = writer;
            Headers = new Dictionary<string, string>();
        }

        public void Process()
        {
            writer.WriteLine($"HTTP/1.1 {ResponseCode} {ResponseText}");
            Console.WriteLine($"HTTP/1.1 {ResponseCode} {ResponseText}");
            // headers... (skipped)
            writer.WriteLine();
            Console.WriteLine();
            writer.WriteLine(Content);
            Console.WriteLine(Content);
            writer.Flush();
            writer.Close();

            Console.WriteLine();
        }
    }
}
