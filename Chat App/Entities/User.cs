using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_App.Entities
{
    [Table(Name = "Users")]
    public class User // User class
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, Name = "Id")]
        public int Id { get; set; }
        [Column]
        public string Name { get; set; }
        [Column]
        public string Password { get; set; }
        [Column]
        public byte[] Avatar { get; set; }
        [Column]
        public string Contacts_String_Users { get; set; }
        [Column]
        public string Contacts_String_Groups { get; set; }

        public User() { }

        public User(int id, string name, string password)
        {
            Id = id;
            Name = name;
            Password = password;
            Avatar = null;
        }
    }
}
