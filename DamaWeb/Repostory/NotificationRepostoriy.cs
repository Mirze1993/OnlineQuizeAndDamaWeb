using MicroORM;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DamaWeb.Repostory
{
    public class NotificationRepostoriy:CRUD<Notification>
    {
        public List<Notification> getLastTop(int n,int id)
        {
            using (var comannder = DBContext.CreateCommander())
                return comannder.Reader<Notification>(
                    commandText:"GetLastN"
                    ,commandType: System.Data.CommandType.StoredProcedure
                    ,parameters: new List<System.Data.Common.DbParameter> {
                        comannder.SetParametr("n",n),comannder.SetParametr("id",id)}).Item1;
        }
    }
}
