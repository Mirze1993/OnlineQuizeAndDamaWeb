using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class PlayGame
    {
        public int Id { get; set; }
        public string WhiteCoordinate { get; set; }
        public string BlackCoordinate { get; set; }
        public string Gamer1 { get; set; }
        public string Gamer2 { get; set; }
        public string Queue { get; set; }
        public string Move { get; set; }
        public int GameId { get; set; }
    }
}
