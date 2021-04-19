using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class ResultQuize
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CategoryId { get; set; }
        public int TotalTrueAnswer { get; set; }
        public int TotalFalseAnswer { get; set; }
        public int TotalNoAnswer { get; set; }
        public string FalseAnswers { get; set; }
        public DateTime ResultDate { get; set; }
        public int TotalSecond { get; set; }
    }
}
