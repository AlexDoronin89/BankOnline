using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunicationEntityClassLibrary
{
    public class Response
    {
        public Response(string status, string message)
        {
            Status = status;
            Message = message;
        }

        public string Status { get; private set; }
        public string Message { get; private set; }

        public override string ToString()
        {
            return $"Command: {Status}\nParameters: {Message}";
        }
    }
}
