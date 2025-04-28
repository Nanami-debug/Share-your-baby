using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace talkClient
{
    class Program
    {
        private static TcpClient _client;
        private static NetworkStream _stream;
        private static Thread _listenThread;
        private static string _clientId;


        private static void StartClient()
        {
            try
            {
                // 初始化连接
                Console.Write("请输入服务器IP (默认127.0.0.1): ");
                string ip = Console.ReadLine();
                ip = string.IsNullOrEmpty(ip) ? "127.0.0.1" : ip;

                Console.Write("请输入服务器端口 (默认8888): ");
                string portInput = Console.ReadLine();
                int port = string.IsNullOrEmpty(portInput) ? 8888 : int.Parse(portInput);

                Console.Write("请输入用户ID: ");
                _clientId = Console.ReadLine();

                // 建立TCP连接
                _client = new TcpClient(ip, port);
                _stream = _client.GetStream();

                // 发送用户ID
                byte[] idData = Encoding.UTF8.GetBytes(_clientId);
                _stream.Write(idData, 0, idData.Length);

                // 启动消息监听线程
                _listenThread = new Thread(ListenForMessages);
                _listenThread.IsBackground = true;
                _listenThread.Start();

                Console.WriteLine($"已连接到服务器({ip}:{port})，输入消息格式：接收者ID>消息内容");

                // 消息发送循环
                while (true)
                {
                    Console.Write("> ");
                    var input = Console.ReadLine();
                    if (string.IsNullOrEmpty(input)) continue;

                    // 解析接收者和消息内容
                    var splitIndex = input.IndexOf('>');
                    if (splitIndex == -1)
                    {
                        Console.WriteLine("输入格式错误");
                        continue;
                    }

                    string receiverId = input.Substring(0, splitIndex).Trim();
                    string message = input.Substring(splitIndex + 1).Trim();
                    SendMessage(receiverId, message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"发生错误：{ex.Message}");
            }
            finally
            {
                Cleanup();
            }
        }

        private static void SendMessage(string receiverId, string message)
        {
            try
            {
                // 构建协议：SEND|接收者ID|消息内容
                string protocol = $"SEND|{receiverId}|{message}";
                byte[] data = Encoding.UTF8.GetBytes(protocol);
                _stream.Write(data, 0, data.Length);

                // 本地显示已发送消息
                Console.WriteLine($"[{DateTime.Now:HH:mm}] 我 -> {receiverId}: {message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"发送失败：{ex.Message}");
            }
        }

        private static void ListenForMessages()
        {
            try
            {
                byte[] buffer = new byte[1024];
                while (true)
                {
                    int bytesRead = _stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0) break;

                    string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    string[] parts = message.Split('|');
                    if (parts[0] == "MESSAGE" && parts.Length == 3)
                    {
                        string senderId = parts[1];
                        string msgContent = parts[2];
                        Console.WriteLine($"[{DateTime.Now:HH:mm}] {senderId} -> 我: {msgContent}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"接收异常：{ex.Message}");
            }
        }

        private static void Cleanup()
        {
            _stream?.Close();
            _client?.Close();
            _listenThread?.Join(1000);
            Console.WriteLine("连接已关闭");
            Environment.Exit(0);
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            StartClient();
        }
    }
}