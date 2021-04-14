using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class StudentTeacher
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public int GroupId { get; set; }
        public bool Status { get; set; }
    }
}
