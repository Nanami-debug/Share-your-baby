using MySql.Data.MySqlClient;
using System.Data;
using System.Text;

public static class talkHistory
{
    private static string connectionString = "Server=localhost;Database=ChatDB;User=cswork;Password=cswork;";

    // 存储聊天记录
    public static void SaveMessage(string sender, string receiver, string message)
    {
        using (var conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            var sql = @"INSERT INTO ChatHistory (Sender, Receiver, Message) 
                        VALUES (@sender, @receiver, @message)";

            using (var cmd = new MySqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("@sender", sender);
                cmd.Parameters.AddWithValue("@receiver", receiver);
                cmd.Parameters.AddWithValue("@message", message);
                cmd.ExecuteNonQuery();
            }
        }
    }

    // 查询所有聊天记录
    public static DataTable GetChatHistory()
    {
        using (var conn = new MySqlConnection(connectionString))
        {
            conn.Open();
            var sql = "SELECT Sender, Receiver, Message, Timestamp FROM ChatHistory";

            using (var cmd = new MySqlCommand(sql, conn))
            using (var adapter = new MySqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }
    }

    static void Main()
    {
        DataTable history = GetChatHistory();
        StringBuilder sb = new StringBuilder();

        foreach (DataRow row in history.Rows)
        {
            Console.WriteLine($"[{row["Timestamp"]}] {row["Sender"]} -> {row["Receiver"]}: {row["Message"]}");
        }
    }
}