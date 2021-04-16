using System;
using System.Collections.Generic;
using System.Text;

namespace Model.UIEntites
{
    public class UISelectLesson
    {
        public int Id { get; set; }
        public int TeacherId { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }
        public int GroupId { get; set; }
        public string SubjectName { get; set; }
        public string TeacherName { get; set; }
    }
}
