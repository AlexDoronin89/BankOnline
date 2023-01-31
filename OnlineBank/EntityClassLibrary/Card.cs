using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankEntityClassLibrary
{
    public class Card
    {
        public Card(int id, int number, int balance)
        {
            Id = id;
            Number = number;
            Balance = balance;
        }

        public int Id { get; private set; }
        public int Number { get; private set; }
        public int Balance { get; private set; }

        public override string ToString() => $"{Number} ({Balance} p.) ";
    }
}
