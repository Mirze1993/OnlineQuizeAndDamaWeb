using System;
using System.Collections.Generic;
using System.Text;

namespace Model.UIEntites
{
    public class UIChatInfo
    {
        public int SenderId { get; set; }
        public int Count { get; set; }
        public int NoReadCount { get; set; }
        public string SenderName { get; set; }
        public string PictureSrc { get; set; }
        public DateTime LastDate { get; set; }
    }
}
