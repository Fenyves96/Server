using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Communication
{
    [Serializable]
    public class CommObject
    {
        public string Sender { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }

        public CommObject() { }
        public CommObject(string sender,string msg)
        {
            this.Sender = sender;
            this.Message = msg;
            this.Date = DateTime.Now;
        }

        public override string ToString()
        {
            return Sender + " " + Message;
          
        }
    }
}
