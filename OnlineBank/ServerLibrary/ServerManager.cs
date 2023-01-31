using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CommunicationEntityClassLibrary;
using Newtonsoft.Json;
namespace ServerLibrary
{
    public class ServerManager
    {
        private HttpListener _listener;

        public ServerManager()
        {
            _listener = new HttpListener();
            _listener.Prefixes.Add("http://localhost:12345/connection/");
            _listener.Start();
        }

        public HttpListenerContext GetClientContext() => _listener.GetContext();
         public static Request RecieveRequestFromClient(HttpListenerContext clientContext)
        {
            HttpListenerRequest request=clientContext.Request;
            StreamReader requestStream = new StreamReader(request.InputStream);
            string data=requestStream.ReadToEnd();

            requestStream.Close();

            return JsonConvert.DeserializeObject<Request>(data);
        }


        public static void SendResponseToClient(HttpListenerContext clientContext)
        {
            HttpListenerResponse responseContext=clientContext.Response;

            string data=JsonConvert.SerializeObject(responseContext);

            byte[] bytes = Encoding.UTF8.GetBytes(data);
            responseContext.ContentLength64 = bytes.LongLength;
            Stream responseStream = responseContext.OutputStream;
            responseStream.Write(bytes, 0, bytes.Length);

            responseStream.Close();

        }


    }
}
