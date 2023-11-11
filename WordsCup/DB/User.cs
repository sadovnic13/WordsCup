using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordsCup.DB
{
    class User
    {
        public int id { get; set; }

        public string login { get; set; }

        public string password { get; set; }

        public string balance { get; set; }

        public string saveWord { get; set; }

        public User() { }

        public User(string login, string password)
        {
            this.id = id;
            this.login = login;
            this.password = password;
        }

        public User(int id, string login, string password, string balance, string saveWord)
        {
            this.id = id;
            this.login = login;
            this.password = password;
            this.balance = balance;
            this.saveWord = saveWord;
        }
    }
}
