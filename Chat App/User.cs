using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_App
{
    public class User // User class
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        public User() {}

        public User(int id, string name, string password)
        {
            Id = id;
            Name = name;
            Password = password;
        }
    }
}
