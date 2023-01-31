using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DbConnectionLibrary.Tables;


namespace DbConnectionLibrary
{
    public class DbManager
    {
        

        public TableCards TableCards { get; set; }
        public TableUserCards TableUserCards { get; set; }

        public TableUsers TableUsers { get; set; }

        public DbManager()
        {
            TableCards = new TableCards();
            TableUserCards = new TableUserCards();
            TableUsers = new TableUsers();
        }
    }
}
