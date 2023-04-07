using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_App.Entities
{
    public class MessagesList
    {
        public ObservableCollection<Message> Messages { get; set; } = new ObservableCollection<Message>();
        public int ReceiverPort { get; set; }
        public int SenderPort { get; set; }

        public MessagesList() { }

        public MessagesList(int receiverPort, int senderPort)
        {
            ReceiverPort = receiverPort;
            SenderPort = senderPort;
        }
    }
}
