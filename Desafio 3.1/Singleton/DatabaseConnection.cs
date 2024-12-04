using Microsoft.Data.Sqlite;

namespace Desafio_3._1.Singleton
{
    public sealed class DatabaseConnection
    {
        private static DatabaseConnection _instance;
        private SqliteConnection _connection;

        private DatabaseConnection()
        {
            _connection = new SqliteConnection("Data Source=consultorio.db;Version=3;");
            _connection.Open();
        }

        public static DatabaseConnection GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DatabaseConnection();
            }
            return _instance;
        }

        public SqliteConnection GetConnection()
        {
            return _connection;
        }
    }
}
