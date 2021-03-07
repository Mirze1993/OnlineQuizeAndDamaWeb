using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class Games
    {
        public int Id { get; set; }
        public int RequestUser { get; set; }
        public int AcceptUser { get; set; }
        public string Status { get; set; }
        public int WinUser { get; set; }
    }
}
