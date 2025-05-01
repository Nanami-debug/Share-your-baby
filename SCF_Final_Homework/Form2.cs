using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Myd;
using Org.BouncyCastle.Bcpg.OpenPgp;
using FORM3;
using USER;

namespace FORM2
{
    public partial class Form2 : Form
    {
        public User user0;
        public Form2()
        { 
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = this.textBox1.Text;
            string password = this.textBox2.Text;
            if (login(name, password))
            {
                //登录成功
                MessageBox.Show("登录成功");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("用户名或密码错误");
            }
        }
        private bool login(string name, string password)
        {
            using (var context = new MyDB())
            {
                var specificUser = context.Users
                    .Where(u => u.Id == name)
                    .FirstOrDefault();

                if (specificUser == null)
                {
                    return false;
                }
                else
                {
                    if (specificUser.password == password)
                    {
                        user0 = specificUser;
                        return true;
                    }
                    else { return false; }
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.ShowDialog();
        }
    }
}
