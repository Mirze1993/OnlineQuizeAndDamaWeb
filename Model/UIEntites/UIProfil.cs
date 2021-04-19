using System;
using System.Collections.Generic;
using System.Text;

namespace Model.UIEntites
{
    public class UIProfil
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateTime BirthDate { get; set; } = DateTime.Now;
        public string Location { get; set; }
        public string Education { get; set; }
        public string Skills { get; set; }
        public string Notes { get; set; }
        public string ProfilPicture { get; set; }       
        public string Role { get; set; }
    }
}
