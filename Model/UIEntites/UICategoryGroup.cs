using System;
using System.Collections.Generic;
using System.Text;

namespace Model.UIEntites
{
    public class UICategoryGroup
    {
        public List<int> SelectedCategoryIds { get; set; }       
        public int GroupId { get; set; }
        public string GruopeName { get; set; }
        public int TeacherId { get; set; }
        public UICategoryGroup()
        {
            SelectedCategoryIds = new List<int>();
        }
    }
}
