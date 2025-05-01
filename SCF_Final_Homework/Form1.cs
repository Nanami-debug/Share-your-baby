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
            // ���������Ӱ�ť����¼����߼�
        }

        private void aloneTextBox1_TextChanged(object sender, EventArgs e)
        {
            // �����������ı������ݱ仯ʱ���߼�
        }

        private async void airButton1_Click(object sender, EventArgs e)
        {
            string apikey = "sk-f17e0a1077a1413487109e740b924928"; // ȷ��API Key��ȷ
            var baseurl = "https://api.deepseek.com";

            var client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apikey}"); // �������ͷ

            var requestData = new
            {
                model = "deepseek-chat", // ģ������
                messages = new[]
                {
            new
            {
                role = "user",
                content = textBox2.Text // ʹ��bigTextBox1�е�������Ϊ�û�����
            }
        }
            };

            var jsonContent = JsonConvert.SerializeObject(requestData); // ����������ת��ΪJSON��ʽ
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            try
            {
                var response = await client.PostAsync($"{baseurl}/v1/chat/completions", content); // ����POST����
                var responseContent = await response.Content.ReadAsStringAsync(); // ��ȡ��Ӧ����

                // ��ӡ��Ӧ���ݵ�����̨���������
                Console.WriteLine("API Response: " + responseContent);

                dynamic result = JsonConvert.DeserializeObject(responseContent); // ������Ӧ����

                // �����Ӧ�ṹ��ȷ��choices��message����
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
                // �����쳣���
                textBox1.Text = $"Error: {ex.Message}";
            }
        }

        private void airButton2_Click(object sender, EventArgs e)
        {
            this.Close(); // �رմ���
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            //����Ϊֻ��
            textBox1.ReadOnly = true;
        }
    }
}


