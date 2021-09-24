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
            var r = GetByColumNameFist("UserName", username);

            if (!r.Success || r.t == default(AppUser)) return (false, default(AppUser));
            var b = new HashCreate().VerfiyPassword(password, r.t.Password);
            return (b, b ? r.t : null);
        }

        public List<AppUser> getRandomUsers()
        {
            using (var comannder = DBContext.CreateCommander())
                return comannder.Reader<AppUser>("GetRandomUsers", commandType: System.Data.CommandType.StoredProcedure).t;
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
                    }).t;
            }
        }

        public bool UpdateOrCreateClaim(int userId,string type,string value)
        {
            using (var comannder = DBContext.CreateCommander())
            {
                return comannder.NonQuery(
                    commandText: "UpdateOrCreateClaim"
                    , commandType: System.Data.CommandType.StoredProcedure
                    , parameters: new List<System.Data.Common.DbParameter> {
                       comannder.SetParametr("UserId", userId),
                       comannder.SetParametr("Type", type),
                       comannder.SetParametr("Value", value)
                    });
            }
        }
    }
}
