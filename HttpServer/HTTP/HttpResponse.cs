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

        public Dictionary<string, string> Headers { get; } = new();
        public string Content { get; set; }
        public string ContentType {
            get
            {
                return Headers["Content-Type"];
            } 
            set
            {
                Headers["Content-Type"] = value;
            }
        }

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
            if ( Content!= null && Content.Length>0 )
            {
                Headers["Content-Length"] = Content.Length.ToString();
            }
            foreach( var kvp in Headers )
            {
                writer.WriteLine(kvp.Key + ": " + kvp.Value);
                Console.WriteLine(kvp.Key + ": " + kvp.Value);
            }
            writer.WriteLine();
            Console.WriteLine();

            // Content
            if (Content != null && Content.Length > 0)
            {
                writer.WriteLine(Content);
                Console.WriteLine(Content);
            }

            writer.Flush();
            writer.Close();

            Console.WriteLine();
        }
    }
}
