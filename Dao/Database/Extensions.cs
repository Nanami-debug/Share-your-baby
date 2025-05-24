using MySql.Data.MySqlClient;
using System.Data;

namespace Dapper
{
    public static class Extensions
    {
        public static DataTable QueryTable(this IDbConnection cnn, string sql)
        {
#if SQLITE
                using (SQLiteCommand command = new SQLiteCommand(sql, (SQLiteConnection)cnn))
                {
                    using (SQLiteDataAdapter adapter = new SQLiteDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
#elif SQLSERVER
                using (SqlCommand command = new SqlCommand(sql, (SqlConnection)cnn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        return dataTable;
                    }
                }
#elif MYSQL
            using (MySqlCommand command = new MySqlCommand(sql, (MySqlConnection)cnn))
            {
                using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
#endif
        }
    }
}