using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Data;
using System.IO;

namespace CherryTomato.Core.Database
{
    public class DatabaseConnection : IDisposable
    {
        private SQLiteConnection connection;

        public DatabaseConnection(string databaseFile)
        {
            if (!File.Exists(databaseFile))
            {
                var dir = Path.GetDirectoryName(databaseFile);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                SQLiteConnection.CreateFile(databaseFile);
            }

            var connectionString = "data source=" + databaseFile + ";page size=1024";
            this.connection = new SQLiteConnection(connectionString);
            this.connection.Open();
        }

        public void Dispose()
        {
            this.connection.Close();
            this.connection.Dispose();
        }

        /// <summary>
        /// Create a command and call ExecuteScalar.
        /// </summary>
        /// <param name="commandText">The command text</param>
        /// <param name="parameters">Parameters referring to @p1, @p2, ...</param>
        /// <returns></returns>
        public int ExecuteScalar(string commandText, params object[] parameters)
        {
            using (var command = this.CreateCommand(commandText, parameters))
            {
                var dbResult = command.ExecuteScalar();
                if (dbResult == null) throw new DataException("Not found");
                return Convert.ToInt32(dbResult);
            }
        }

        /// <summary>
        /// Create a command and call ExecuteNonQuery.
        /// </summary>
        /// <param name="commandText">The command text</param>
        /// <param name="parameters">Parameters referring to @p1, @p2, ...</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string commandText, params object[] parameters)
        {
            using (var t = this.connection.BeginTransaction())
            using (var command = this.CreateCommand(commandText, parameters))
            {
                var result = command.ExecuteNonQuery();
                t.Commit();
                return result;
            }
        }

        /// <summary>
        /// Create a command and call ExecuteReader. Load the data into a DataTable
        /// </summary>
        /// <param name="commandText">The command text</param>
        /// <param name="parameters">Parameters referring to @p1, @p2, ...</param>
        /// <returns></returns>
        public DataTable ExecuteReader(string commandText, params object[] parameters)
        {
            using (var command = this.CreateCommand(commandText, parameters))
            using (var reader = command.ExecuteReader())
            {
                var result = new DataTable();
                result.BeginLoadData();
                result.Load(reader);
                result.EndLoadData();
                reader.Close();
                return result;
            }
        }

        private SQLiteCommand CreateCommand(string commandText, object[] parameters)
        {
            var command = new SQLiteCommand(commandText, connection);

            var index = 1;
            foreach (var parameter in parameters)
            {
                var sqliteParam = new SQLiteParameter("@p" + index++, parameter);

                if (parameter.GetType() == typeof(int)) sqliteParam.DbType = DbType.Int32;
                if (parameter.GetType() == typeof(long)) sqliteParam.DbType = DbType.Int64;
                if (parameter.GetType() == typeof(byte[])) sqliteParam.DbType = DbType.Binary;
                if (parameter.GetType() == typeof(DateTime)) sqliteParam.DbType = DbType.Time;

                command.Parameters.Add(sqliteParam);
            }
            
            return command;
        }

        public bool TableExists(string tableName)
        {
            var rows = this.connection.GetSchema("Tables").Select("Table_Name='" + tableName + "'");
            return rows.Length != 0;
        }
    }
}
