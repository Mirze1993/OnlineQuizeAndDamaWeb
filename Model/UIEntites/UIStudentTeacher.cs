using System;
using System.Collections.Generic;
using System.Text;

namespace Model.UIEntites
{
   public class UIStudentTeacher
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public int GroupId { get; set; }
        public bool Status { get; set; }
        public string StudentName { get; set; }
        public string GroupName { get; set; }
    }
}
