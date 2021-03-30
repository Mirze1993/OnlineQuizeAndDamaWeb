using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DamaWeb.Tools
{
    public class OnlineUsers
    {
        public static int Count { get =>Users!=null? Users.Count:0; }
        
        public static HashSet<OnlieUsersEntity> Users { get; set; }

        private static readonly object obj = new object();

        public static void RemoveUsers(string connectionID)
        {
            lock (obj)
            {
                if (Users == null) return;
                var d = Users.FirstOrDefault(x => x.ConnectionId == connectionID);
                if(d!=null)Users.Remove(d);
            }
        }

        public static void AddUser(string userName,int id,string connectionId)
        { 
            lock (obj)
            {
                if (Users == null) Users =new HashSet<OnlieUsersEntity>();
                if (Users.Any(x => x.Name == userName)) Users.RemoveWhere(x=>x.Name==userName);
                Users.Add(new OnlieUsersEntity {Id=id,Name=userName,ConnectionId=connectionId});
            }
        }
    }

    public class OnlieUsersEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string  ConnectionId { get; set; }
    }
}
