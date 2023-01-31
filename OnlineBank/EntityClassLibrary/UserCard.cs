using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankEntityClassLibrary
{
    public class UserCard
    {

        public int IdUser { get; private set; }
        public List<Card> Cards { get; private set; }
        public UserCard(int idUser, List<Card> cards)
        {
            IdUser = idUser;
            Cards = cards;
        }
    }
}
