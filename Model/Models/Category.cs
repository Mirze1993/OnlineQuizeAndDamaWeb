using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SubjectId { get; set; }
        public int UserId { get; set; }

    }
}
