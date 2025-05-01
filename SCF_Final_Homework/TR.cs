using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using USER;
using Myd;
using Microsoft.EntityFrameworkCore;
using GOOD;
using ORDER;

namespace TRF
{
    public partial class TR : Form
    {
        private DataGridView dataGridViewMain = new DataGridView { Dock = DockStyle.Fill, AutoGenerateColumns = true };
        private Panel panelLeft = new Panel { Width = 150, Dock = DockStyle.Left };
        private Panel panelRight = new Panel { Dock = DockStyle.Fill };
        private Button btn1 = new Button { Text = "购物车", Dock = DockStyle.Top };
        private Button btn2 = new Button { Text = "已发布", Dock = DockStyle.Top };
        private Button btn3 = new Button { Text = "购买订单", Dock = DockStyle.Top };
        private Button btn4 = new Button { Text = "售出订单", Dock = DockStyle.Top };
        private Button btn5 = new Button { Text = "退出登录", Dock = DockStyle.Top };
        private Button btn6 = new Button { Text = "删除", Dock = DockStyle.Top };
        public User user;
        public TR(User user)
        {
            this.user = user;
            Width = 800;
            Height = 600;

            panelLeft.Controls.Add(btn1);
            panelLeft.Controls.Add(btn2);
            panelLeft.Controls.Add(btn3);
            panelLeft.Controls.Add(btn4);
            panelLeft.Controls.Add(btn5);
            panelRight.Controls.Add(dataGridViewMain);
            Controls.Add(panelRight);
            Controls.Add(panelLeft);

            btn1.Click += Btn1_Click;
            btn3.Click += Btn3_Click;
            btn4.Click += Btn4_Click;
            btn5.Click += Btn5_Click;

            btn2.Click += Btn2_Click;
            btn6.Click += Btn6_Click;
            dataGridViewMain.CellValueChanged += DataGridViewMain_CellValueChanged;
            InitializeComponent();
        }
        private void LoadGrid<T>() where T : class
        {
            using (var context = new MyDB())
            {
                var set = context.Set<T>().ToList();
                dataGridViewMain.DataSource = new BindingList<T>(set);
                dataGridViewMain.ReadOnly = false;
            }
        }

        private void DataGridViewMain_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewMain.DataSource is not IList<object> list) return;

            var updatedItem = dataGridViewMain.Rows[e.RowIndex].DataBoundItem;
            using (var context = new MyDB())
            {
                context.Update(updatedItem);
                context.SaveChanges();
            }
        }
        private void Btn1_Click(object sender, EventArgs e)
        {
            // Load the shopping cart
            using (var context = new MyDB())
            {
                var goods = context.Users
                   .Where(u => u.Id == user.Id)
                   .SelectMany(u => u.stared)
                   .ToList();


                dataGridViewMain.DataSource = new BindingList<Good>(goods);
            }
        }
        private void Btn2_Click(object sender, EventArgs e)
        {
            // 加载发布商品
            using (var context = new MyDB())
            {
                var goods = context.Users
                   .Where(u => u.Id == user.Id)
                   .SelectMany(u => u.goods)
                   .ToList();


                dataGridViewMain.DataSource = new BindingList<Good>(goods);
            }
        }
        private void Btn3_Click(object sender, EventArgs e)
        {
            // 加载购买商品
            using (var context = new MyDB())
            {
                var orders = context.Users
                   .Where(u => u.Id == user.Id)
                   .SelectMany(u => u.buy_orders)
                   .ToList();


                dataGridViewMain.DataSource = new BindingList<Order>(orders);
            }
        }
        private void Btn4_Click(object sender, EventArgs e)
        {
            // 加载售出商品
            using (var context = new MyDB())
            {
                var orders = context.Users
                   .Where(u => u.Id == user.Id)
                   .SelectMany(u => u.sell_orders)
                   .ToList();


                dataGridViewMain.DataSource = new BindingList<Order>(orders);
            }
        }
        private void Btn5_Click(object sender, EventArgs e)
        {
            // 退出登录
            DialogResult result = MessageBox.Show("您确定要退出登录吗？", "退出登录", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                this.Close();
            }
        }
        private void Btn6_Click(object sender, EventArgs e)
        {
            if (dataGridViewMain.CurrentRow == null) return;
            var item = dataGridViewMain.CurrentRow.DataBoundItem;
            using (var context = new MyDB())
            {
                context.Remove(item);
                context.SaveChanges();
            }
            dataGridViewMain.Rows.Remove(dataGridViewMain.CurrentRow);
        }
        
        private void TR_Load(object sender, EventArgs e)
        {

        }
    }
    
        
    
}
