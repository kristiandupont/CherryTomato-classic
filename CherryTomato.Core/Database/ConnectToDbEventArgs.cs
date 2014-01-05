using CherryTomato.Core.EventsModel;

namespace CherryTomato.Core.Database
{
    public class ConnectToDbEventArgs : CherryEventArgs
    {
        public DatabaseConnection DbConnection { get; protected set; }

        public ConnectToDbEventArgs(DatabaseConnection dbConnection)
        {
            this.DbConnection = dbConnection;
        }
    }
}
