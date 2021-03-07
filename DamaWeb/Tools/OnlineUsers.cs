using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DamaWeb.Tools
{
    public class OnlineUsers
    {
        public static int Count { get; set; } = 0;
        
        public static List<OnlieUsersEntity> Users { get; set; }

        private static readonly object obj = new object();

        public static void RemoveUsers()
        {
            lock (obj)
            {
                if (Users == null) return;
                var ss = Users.RemoveAll(x => (DateTime.Now - x.Date).TotalMinutes > 1);
                Count = Users.Count;
            }
        }

        public static void AddUser(string userName,int id)
        { 
            lock (obj)
            {
                if (Users == null) Users =new List<OnlieUsersEntity>();
                if (Users.Any(x=>x.Name==userName)) Users.FirstOrDefault(x=> x.Name == userName).Date = DateTime.Now;
                else Users.Add(new OnlieUsersEntity { Date=DateTime.Now,Id=id,Name=userName});
            }
        }
    }

    public class OnlieUsersEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}
