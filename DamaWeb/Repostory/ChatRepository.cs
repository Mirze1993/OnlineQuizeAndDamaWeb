using MicroORM;
using Model.Models;
using Model.UIEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DamaWeb.Repostory
{
    public class ChatRepository:CRUD<Chat>
    {
        public List<Chat> getLastTop(int n, int id,int otherId)
        {
            using (var comannder = DBContext.CreateCommander())
                return comannder.Reader<Chat>(
                    commandText: "GetLastChatN"
                    , commandType: System.Data.CommandType.StoredProcedure
                    , parameters: new List<System.Data.Common.DbParameter> {
                        comannder.SetParametr("n",n),
                        comannder.SetParametr("ReciveId",id),
                        comannder.SetParametr("SenderId",otherId) }
                    ).Item1;
        }

        public List<UIChatInfo> GetMesageGrup(int id)
        {
            using (var comannder = DBContext.CreateCommander())
                return comannder.Reader<UIChatInfo>(
                    commandText: "GetMesageGrup"
                    , commandType: System.Data.CommandType.StoredProcedure
                    , parameters: new List<System.Data.Common.DbParameter> {
                       comannder.SetParametr("Id",id) 
                    }).Item1;
        }

        public List<Chat> GetLastIsNoReadMsg(int reciveId,int senderId )
        {
            using (var comannder = DBContext.CreateCommander())
                return comannder.Reader<Chat>(
                    commandText: "GetLastIsNoReadMsg"
                    , commandType: System.Data.CommandType.StoredProcedure
                    , parameters: new List<System.Data.Common.DbParameter> {
                        comannder.SetParametr("ReciveId",reciveId),
                        comannder.SetParametr("SenderId",senderId)
                    }).Item1;
        }
    }
}
