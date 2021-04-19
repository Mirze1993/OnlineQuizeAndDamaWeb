using System;
using System.Collections.Generic;
using System.Text;

namespace Model.UIEntites
{
    public class UIResultQuize
    {
        public string CategoryName { get; set; }
        public int TotalTrueAnswer { get; set; }
        public int TotalFalseAnswer { get; set; }
        public int TotalNoAnswer { get; set; }
        public string FalseAnswers { get; set; }
        public DateTime ResultDate { get; set; }
        public int TotalSecond { get; set; }
    }
}
