using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_App.Entities
{
    public class Message // Message class
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public string Timestring { get; set; }

        public Message(){}

        public Message(string content, string sender, string receiver)
        {
            Timestring = DateTime.Now.ToShortTimeString();
            this.Content = content + "\n\n" + Timestring;
            this.Sender = sender;
            this.Receiver = receiver;
            
        }

        public Message(int id, string content, string sender, string receiver)
        {
            Timestring = DateTime.Now.ToShortTimeString();
            this.Id = id;
            this.Content = content + "\n\n" + Timestring;
            this.Sender = sender;
            this.Receiver = receiver;
            
        }
    }
}
