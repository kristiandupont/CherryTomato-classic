using System;
using System.IO;
using System.Reflection;
using CherryTomato.Core.Database;

namespace CherryTomato.Core.Tests
{
    public class DatabaseTest
    {
        protected DatabaseConnection dbCon;

        public DatabaseTest()
        {
            var r = DateTime.Now.Ticks;
            var testDatabaseFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\test_" + r + ".sqlite";
            dbCon = new DatabaseConnection(testDatabaseFile);
        }
    }
}
