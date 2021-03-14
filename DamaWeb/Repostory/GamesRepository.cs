using MicroORM;
using Model.Models;
using Model.UIEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DamaWeb.Repostory
{
    public class GamesRepository: CRUD<Games>
    {
        public int RequestGame(int requestUserId,int accespUserId)
        {
            using (var commander =DBContext.CreateCommander())
            {
                var outParam = commander.SetOutputParametr();
                var sqlparams = new List<System.Data.Common.DbParameter> {
                    commander.SetParametr("RequestUser",requestUserId),
                    commander.SetParametr("AcceptUser",accespUserId),
                    outParam
                };

                var b = commander.NonQuery("RequsetGame", commandType: System.Data.CommandType.StoredProcedure,
                    parameters: sqlparams);
                if (outParam.Value == null) return 0;                
                return (int)outParam.Value;
            }
        }

        public int AcceptGame(int id)
        {
            using (var commander = DBContext.CreateCommander())
            {
                var outParam = commander.SetOutputParametr();
                var sqlparams = new List<System.Data.Common.DbParameter> {
                    commander.SetParametr("Id",id),
                    outParam
                };

                var b = commander.NonQuery("AcceptGame", commandType: System.Data.CommandType.StoredProcedure,
                    parameters: sqlparams);
                if (outParam.Value == null) return 0;
                return (int)outParam.Value;
            }
        } 

        public List<UIGames> GetUserGames(int id)
        {

            using (CommanderBase commander = DBContext.CreateCommander())
            {
                 var (g,b)=commander.Reader<UIGames>("GetUserGames", commandType: System.Data.CommandType.StoredProcedure,
                    parameters: new List<System.Data.Common.DbParameter> {
                        commander.SetParametr("Id",id)}
                    );
                return g;
            }
        }

    }
}
