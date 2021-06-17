using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class Article
    {
        public int Id { get; set; }
        public int Writer  { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string Body { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
