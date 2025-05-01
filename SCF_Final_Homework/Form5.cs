using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GOOD;
using USER;
using Myd;
using ORDER;

namespace FORM5
{
    public partial class Form5 : Form
    {
        public Good Good { get; set; } // 商品对象
        public User User { get; set; } // 用户对象
        public Order order;
        public Form5(Good good, User user)
        {
            InitializeComponent();
            Good = good;
            User = user;
            order=User.Buy(good);//创建订单
        }
        private void Form5_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            User.Pay(order);//付款
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("订单尚未支付，您确定退出吗？", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

            if (result != DialogResult.Cancel)
            {
                 this.Close();
            }
        }
    }
}
