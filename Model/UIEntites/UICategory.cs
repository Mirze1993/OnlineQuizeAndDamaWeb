using System;
using System.Collections.Generic;
using System.Text;

namespace Model.UIEntites
{
    public class UICategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SubjectId { get; set; }
        public string SubjectName { get; set; }
        public string UserName { get; set; }
    }
}
