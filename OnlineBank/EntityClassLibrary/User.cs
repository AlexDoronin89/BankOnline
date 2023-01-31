using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankEntityClassLibrary
{
    public class User
    {

        public int Id { get; private set; }
        public string Name{ get; private set; }
        public string Login { get; private set; }
        public string Password { get; private set; }
        public User(int id, string fullName, string login, string password)
        {
            Id = id;
            Name = fullName;
            Login = login;
            Password = password;
        }
    }
}
