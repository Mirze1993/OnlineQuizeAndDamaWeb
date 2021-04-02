using MicroORM;
using MicroORM.Tools;
using Model.Models;
using Model.UIEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DamaWeb.Repostory
{
    public class UserRepository : CRUD<AppUser>
    {
        public (bool, AppUser) ValidateUserCredentials(string username, string password)
        {
            var (u, b) = GetByColumNameFist("UserName", username);

            if (!b || u == default(AppUser)) return (false, default(AppUser));
            b = new HashCreate().VerfiyPassword(password, u.Password);
            return (b, b ? u : null);
        }

        public List<AppUser> getRandomUsers()
        {
            using (var comannder = DBContext.CreateCommander())
                return comannder.Reader<AppUser>("GetRandomUsers", commandType: System.Data.CommandType.StoredProcedure).Item1;
        }

        public UINotifAndChatCount GetNotifAndChatCount(int id)
        {
            using (var comannder = DBContext.CreateCommander())
            {
                return comannder.ReaderFist<UINotifAndChatCount>(
                    commandText: "getNotifAndChatCount"
                    , commandType: System.Data.CommandType.StoredProcedure
                    , parameters: new List<System.Data.Common.DbParameter> {
                       comannder.SetParametr("id", id)
                    }).Item1;
            }
        }
    }
}
