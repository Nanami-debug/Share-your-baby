using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace talkServer
{
    class Program
    {
        private static TcpListener _listener;
        private static List<ClientHandler> _clients = new List<ClientHandler>();


        private static void StartServer()
        {
            try
            {
                _listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 8888);
                new Thread(ListenForClients).Start();
                Console.WriteLine("服务器已启动 (按Q退出)...");

                // 监听退出命令
                while (Console.ReadKey(true).Key != ConsoleKey.Q) ;
                Cleanup();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"服务器异常: {ex.Message}");
            }
        }

        private static void ListenForClients()
        {
            _listener.Start();
            while (true)
            {
                TcpClient client = _listener.AcceptTcpClient();
                ClientHandler handler = new ClientHandler(client);
                _clients.Add(handler);
                new Thread(handler.HandleClient).Start();
            }
        }

        public static void RouteMessage(string senderId, string receiverId, string message)
        {
            ClientHandler receiver = _clients.Find(c => c.ClientId == receiverId);
            if (receiver != null)
            {
                receiver.SendMessage($"MESSAGE|{senderId}|{message}");
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] 转发 {senderId} -> {receiverId}: {message}");
            }
        }

        private static void Cleanup()
        {
            _listener?.Stop();
            _clients.ForEach(c => c.Close());
            Console.WriteLine("服务器已关闭");
            Environment.Exit(0);
        }


        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            StartServer();
        }

    }

    class ClientHandler
    {
        public string ClientId { get; private set; }
        private TcpClient _client;
        private NetworkStream _stream;

        public ClientHandler(TcpClient client)
        {
            _client = client;
            _stream = client.GetStream();
        }

        public void HandleClient()
        {
            try
            {
                // 获取客户端ID
                byte[] buffer = new byte[1024];
                int bytesRead = _stream.Read(buffer, 0, buffer.Length);
                ClientId = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] 客户端 {ClientId} 已连接");

                // 持续监听消息
                while (true)
                {
                    bytesRead = _stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;

                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    string[] parts = message.Split('|');
                    if (parts[0] == "SEND" && parts.Length == 3)
                    {
                        string receiverId = parts[1];
                        string msgContent = parts[2];
                        Program.RouteMessage(ClientId, receiverId, msgContent);
                    }
                }
            }
            catch
            {
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] 客户端 {ClientId} 断开连接");
                Close();
            }
        }

        public void SendMessage(string message)
        {
            try
            {
                byte[] data = Encoding.UTF8.GetBytes(message);
                _stream.Write(data, 0, data.Length);
            }
            catch
            {
                Close();
            }
        }

        public void Close()
        {
            _stream?.Close();
            _client?.Close();
        }
    }
}