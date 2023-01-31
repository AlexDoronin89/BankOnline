using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunicationEntityClassLibrary
{
    public  class Request
    {
        public Request(string command, string parameters, string token)
        {
            Command = command;
            Parameters = parameters;
            Token = token;
        }

        public string Command { get; private set; }
        public string Parameters { get; private set; }
        public string Token { get; private set; }

        public override string ToString()
        {
            return $"Command: {Command}\nParameters: {Parameters}";
        }
    }
}
