using Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.UIEntites
{
    public class UIUserClaims
    {
        public AppUser User { get; set; }
        public List<UserClaims> Claims { get; set; }
    }
}
