using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Models
{
    public class Quize
    {
        public int Id { get; set; }
        public string Question { get; set; }
        public string QuestionImgSrc { get; set; }

        public string A { get; set; }
        public string AImgSrc { get; set; }

        public string B { get; set; }
        public string BImgSrc { get; set; }

        public string C { get; set; }
        public string CImgSrc { get; set; }

        public string D { get; set; }
        public string DImgSrc { get; set; }

        public string E { get; set; }
        public string EImgSrc { get; set; }

        public string Answer { get; set; }

        public string PictureSrc { get; set; }
        public string VideoSrc { get; set; }

        public string AnswerDecrpt { get; set; }

        public int CategoryId { get; set; }
    }
}
