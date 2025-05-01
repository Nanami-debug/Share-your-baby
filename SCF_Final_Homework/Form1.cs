using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Windows.Forms;

namespace SCF_Final_Homework
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 这里可以添加按钮点击事件的逻辑
        }

        private void aloneTextBox1_TextChanged(object sender, EventArgs e)
        {
            // 这里可以添加文本框内容变化时的逻辑
        }

        private async void airButton1_Click(object sender, EventArgs e)
        {
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
                content = textBox2.Text // 使用bigTextBox1中的内容作为用户输入
            }
        }
            };

            var jsonContent = JsonConvert.SerializeObject(requestData); // 将请求数据转换为JSON格式
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync($"{baseurl}/v1/chat/completions", content); // 发送POST请求
                var responseContent = await response.Content.ReadAsStringAsync(); // 读取响应内容

                // 打印响应内容到控制台，方便调试
                Console.WriteLine("API Response: " + responseContent);

                dynamic result = JsonConvert.DeserializeObject(responseContent); // 解析响应内容

                // 检查响应结构，确保choices和message存在
                if (result != null)
                {
                    if (result.choices != null && result.choices.Count > 0)
                    {
                        if (result.choices[0].message != null)
                        {
                            textBox1.Text = result.choices[0].message.content.ToString();
                        }
                        else
                        {
                            textBox1.Text = "Error: 'message' field is missing in the API response.";
                        }
                    }
                    else
                    {
                        textBox1.Text = "Error: 'choices' field is missing or empty in the API response.";
                    }
                }
                else
                {
                    textBox1.Text = "Error: API response is null.";
                }
            }
            catch (Exception ex)
            {
                // 处理异常情况
                textBox1.Text = $"Error: {ex.Message}";
            }
        }

        private void airButton2_Click(object sender, EventArgs e)
        {
            this.Close(); // 关闭窗体
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //设置为只读
            textBox1.ReadOnly = true;
        }
    }
}


