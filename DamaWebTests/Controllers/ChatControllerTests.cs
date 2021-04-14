using Microsoft.VisualStudio.TestTools.UnitTesting;
using DamaWeb.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using DamaWeb.Repostory;
using System.Threading;
using System.Threading.Tasks;

namespace DamaWeb.Controllers.Tests
{
    [TestClass()]
    public class ChatControllerTests
    {

        public ChatControllerTests()
        {
            MicroORM.ORMConfig.ConnectionString = "Server=.\\SQLExpress;Database=Dama;Integrated Security=true;";
            MicroORM.ORMConfig.DbType = MicroORM.DbType.MSSQL;
            MicroORM.Logging.FileLoggerOptions.FolderPath = "C:/Users/AbbasaliyevMC/source/repos/Mirze1993/DamaWeb/DamaWebTests";
        }
        [TestMethod()]
        public void GetLast1Test()
        {
            for (int i = 0; i < 1000; i++)
            {
                var rep = new ChatRepository();
                var msg = rep.getLastTop(50, 1, 2);
                Console.WriteLine(Thread.CurrentThread.Name);
            }
        }
        [TestMethod()]
        public void GetLast2Test()
        {
            for (int i = 0; i < 1000; i++)
            {
                var rep = new ChatRepository();
                var msg = rep.getLastTop(50, 1, 2);
                Console.WriteLine(Thread.CurrentThread.Name);
            }
        }
        [TestMethod()]
        public void GetLast3Test()
        {
            for (int i = 0; i < 1000; i++)
            {
                var rep = new ChatRepository();
                var msg = rep.getLastTop(50, 1, 2);
                Console.WriteLine(Thread.CurrentThread.Name);
            }
        }
        [TestMethod()]
        public void GetLast4Test()
        {
            var rep = new ChatRepository();
            var msg = rep.getLastTop(50, 1, 2);
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
        }
        [TestMethod()]
        public void GetLast5Test()
        {
            var rep = new ChatRepository();
            var msg = rep.getLastTop(50, 1, 2);
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
        }
        [TestMethod()]
        public void GetLast6Test()
        {
           
        }
        [TestMethod()]
        public void GetLast7Test()
        {
            var rep = new ChatRepository();
            var msg = rep.getLastTop(50, 1, 2);
            Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
        }
        [TestMethod()]
        public void GetLast8Test()
        {
            var rep = new ChatRepository();
            var msg = rep.getLastTop(50, 1, 2);
            Console.WriteLine(msg.Count);
        }
        [TestMethod()]
        public void GetLast9Test()
        {
            var rep = new ChatRepository();
            var msg = rep.getLastTop(50, 1, 2);
            Console.WriteLine(msg.Count);
        }
    }
}