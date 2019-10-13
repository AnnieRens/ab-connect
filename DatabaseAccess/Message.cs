using System;

namespace DatabaseAccess
{
    public class Message
    {
        public int Id { get; set; }

        public DateTime DateTime { get; set; }

        public string Text { get; set; }

        public bool IsReceived { get; set; }

        // used by ef core creating poco class
        public Message() { }

        public Message(string text, DateTime dateTime, bool isReceived)
        {
            Text = text;
            DateTime = dateTime;
            IsReceived = isReceived;
        }
    }
}