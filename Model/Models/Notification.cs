using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class Notification
    {
        public int Id  { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public NotificationType Type { get; set; }
        public DateTime NoteDate { get; set; } = DateTime.Now;
    }

    public enum NotificationType
    {
        GameRequest,
        GameAccept,
        GameReject,
        System,
        Follow
    }
}
