using MySql.Data.MySqlClient;
using static Dapper.SimpleCRUD;

namespace Dapper
{
    public class DBHelper
    {
        private static string Conn;

        public static void Init(string conn)
        {
            Conn = conn;
            using (var db = DBHelper.GetConn())
            {
                db.Query("SELECT 1");
            }
        }

        public static bool IsInit()
        {
            return !string.IsNullOrEmpty(Conn);
        }

#if SQLITE
        public static SQLiteConnection GetConn()
        {
            var connection = new SQLiteConnection(Conn);
            SimpleCRUD.SetDialect(Dialect.SQLite);
            connection.Open();
            return connection;
        }
#elif SQLSERVER
        public static SqlConnection GetConn()
        {
            var connection = new SqlConnection(Conn);
            SimpleCRUD.SetDialect(Dialect.SQLite);
            connection.Open();
            return connection;
        }
#elif MYSQL

        public static MySqlConnection GetConn()
        {
            var connection = new MySqlConnection(Conn);
            SimpleCRUD.SetDialect(Dialect.MySQL);
            connection.Open();
            return connection;
        }

#endif
    }
}