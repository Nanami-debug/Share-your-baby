using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Myd;
using System.Xml.Linq;
using USER;
using System.Net;
using System.Net.Mail;

namespace FORM3
{
    public partial class Form3 : Form
    {
        private string _verificationCode;
        public Form3()
        {
            InitializeComponent();
        }
        //添加函数textBox1_TextChanged
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }
        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            string id = textBox1.Text;
            string password = textBox2.Text;
            string sure = textBox3.Text;
            string call = textBox4.Text;
            string verificationCode = textBox5.Text;
            using (var context = new MyDB())
            {
                var specificUser = context.Users
                    .Where(u => u.Id == id)
                    .FirstOrDefault();
                var specificUser2 = context.Users
                    .Where(u => u.call == call)
                    .FirstOrDefault();

                if (specificUser != null)
                {
                    MessageBox.Show("用户名重复");
                }
                else if (password != sure)
                {
                    MessageBox.Show("两次密码不一致");
                }
                else if (specificUser2 != null)
                {
                    MessageBox.Show("邮箱已注册");
                }
                else if(string.IsNullOrEmpty(verificationCode))
                {
                    MessageBox.Show("请输入验证码");
                }
                else if (!VerifyCode(verificationCode, _verificationCode))
                {
                    MessageBox.Show("验证码错误");
                }
                else
                {
                    User user = new User(id, password, call);
                    context.Users.Add(user);
                    context.SaveChanges();
                    MessageBox.Show("注册成功");
                    this.Close();
                }
            }
        }
        private string GenerateVerificationCode()
        {
            Random random = new Random();
            return random.Next(1000, 9999).ToString(); // 生成 4 位验证码
        }

        private void SendVerificationCode(string email, string verificationCode)
        {
            try
            {
                // 配置 SMTP 客户端
                SmtpClient smtpClient = new SmtpClient("smtp.qq.com")
                {
                    Port = 587, // QQ 邮箱的 SMTP 端口
                    Credentials = new NetworkCredential("3541996247@qq.com", "aafoiznjyhqkdbhh"), // 发件人邮箱和授权码
                    EnableSsl = true, // 启用 SSL/TLS
                };

                // 创建邮件内容
                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("3541996247@qq.com"),
                    Subject = "邮箱验证码",
                    Body = $"您的验证码是：{verificationCode}，请在 5 分钟内完成验证。",
                    IsBodyHtml = false,
                };
                mailMessage.To.Add(email);

                // 发送邮件
                smtpClient.Send(mailMessage);
                MessageBox.Show("验证码已发送，请查收邮箱！");
            }
            catch (Exception ex)
            {
                MessageBox.Show("发送验证码失败：" + ex.Message);
            }
        }

        private bool VerifyCode(string userInput, string serverCode)
        {
            return userInput == serverCode;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string email = textBox4.Text.Trim();
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("请输入邮箱地址！");
                return;
            }

            // 生成验证码
            _verificationCode = GenerateVerificationCode();

            // 发送验证码到邮箱
            SendVerificationCode(email, _verificationCode);
        }
    }
}
