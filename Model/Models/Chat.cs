using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReciveId { get; set; }
        public string Message { get; set; }
        public string FileName { get; set; }
        public DateTime Date { get; set; }
        public bool IsRead { get; set; }
    }
}
