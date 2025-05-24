using Dapper;
using Form.Page.Dialog;
using Form.Page.Login;
using HandyControl.Controls;
using Model;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace Form.Page.Good
{
    /// <summary>
    /// AddModiGood.xaml 的交互逻辑
    /// </summary>
    public partial class AddModiGood : System.Windows.Controls.Page
    {
        public Goods Good { get; set; }
        public string Mode { get; set; }
        private async void AiReq()
        {
            GrdMain.IsEnabled = false;
            BrdLoad.Visibility = Visibility.Visible;
            string apikey = "sk-f17e0a1077a1413487109e740b924928"; // 确保API Key正确
            var baseurl = "https://api.deepseek.com";

            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apikey}"); // 添加请求头

            var requestData = new
            {
                model = "deepseek-chat", // 模型名称
                messages = new[]
                {
                new
                {
                    role = "user",
                    content ="请为我的商品提供建议报价，商品描述为"+txtDes.Text
                    +"你的回答格式应为“建议价格：价格。理由：理由（不超过30字）"
                }
            }
            };

            var jsonContent = JsonConvert.SerializeObject(requestData); // 将请求数据转换为JSON格式
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            string advise = "抱歉，出错了";//记录回答
            try
            {
                var response = await client.PostAsync($"{baseurl}/v1/chat/completions", content); // 发送POST请求
                var responseContent = await response.Content.ReadAsStringAsync(); // 读取响应内容


                dynamic result = JsonConvert.DeserializeObject(responseContent); // 解析响应内容

                // 检查响应结构，确保choices和message存在
                if (result != null)
                {
                    if (result.choices != null && result.choices.Count > 0)
                    {
                        if (result.choices[0].message != null)
                        {
                            advise = result.choices[0].message.content.ToString();
                        }
                        else
                        {
                            advise = "Error: 'message' field is missing in the API response.";
                        }
                    }
                    else
                    {
                        advise = "Error: 'choices' field is missing or empty in the API response.";
                    }
                }
                else
                {
                    advise = "Error: API response is null.";
                }
            }
            catch (Exception ex)
            {
                // 处理异常情况
                advise = $"Error: {ex.Message}";
            }
            GrdMain.IsEnabled = true;
            BrdLoad.Visibility = Visibility.Collapsed;
            Growl.InfoGlobal(advise);
        }
        public AddModiGood(Goods good, string mode)
        {
            InitializeComponent();
            Good = good;
            Mode = mode;
            if (Mode == "EDIT")
            {
                txtId.IsReadOnly = true;
                txtId.Text = Good.Id;
                txtPrice.Text = Convert.ToString(Good.price);
                txtDes.Text = Good.des;
                LoadImageFromBytes(Good.img, Img);
            }
        }

        private string FilePath { get; set; }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (Mode == "ADD")
            {
                Good = new Goods();
                Good.Id = txtId.Text;
                Good.price = Convert.ToInt32(txtPrice.Text);
                Good.des = txtDes.Text;
                Good.img = File.ReadAllBytes(FilePath);
                Good.UserId = LoginViewModel.User.Id;
                using (var db = DBHelper.GetConn())
                {
                    var id = db.Insert<string, Goods>(Good);
                    Growl.InfoGlobal("添加成功!");
                    var dg = this.Tag as DialogView;
                    dg.Close();
                }
            }
            else
            {
                Good.Id = txtId.Text;
                Good.price = Convert.ToInt32(txtPrice.Text);
                Good.des = txtDes.Text;
                if (!string.IsNullOrEmpty(FilePath))
                {
                    Good.img = File.ReadAllBytes(FilePath);
                }
                Good.UserId = LoginViewModel.User.Id;
                using (var db = DBHelper.GetConn())
                {
                    var id = db.Update(Good);
                    Growl.InfoGlobal("修改成功!");
                    var dg = this.Tag as DialogView;
                    dg.Close();
                }
            }
        }

        private void ChangePic(object sender, RoutedEventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // 设置文件筛选器和标题
                openFileDialog.Filter = "图片文件|*.jpg;*.jpeg;*.png;*.gif;*.bmp|所有文件|*.*";
                openFileDialog.Title = "选择图片文件";

                // 显示对话框并处理结果
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FilePath = openFileDialog.FileName;
                    Img.Source = new BitmapImage(new Uri(openFileDialog.FileName));
                }
            }
        }

        public static void LoadImageFromBytes(byte[] bytes, System.Windows.Controls.Image imageControl)
        {
            if (bytes == null || bytes.Length == 0)
            {
                throw new ArgumentException("输入的字节数组为空或无效。");
            }

            try
            {
                using (MemoryStream stream = new MemoryStream(bytes))
                {
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad; // 确保流关闭后图像仍可用
                    bitmap.StreamSource = stream;
                    bitmap.EndInit();
                    bitmap.Freeze(); // 跨线程安全（可选）

                    imageControl.Source = bitmap;
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("无法解析字节数组为有效图片。", ex);
            }
        }

        private void Ai_Click(object sender, RoutedEventArgs e)
        {
            AiReq();
        }
    }
}