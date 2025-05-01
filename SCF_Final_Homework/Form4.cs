using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.Devices;
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
using GOOD;
using ORDER;
using FORM5;

namespace FORM4
{
    public partial class Form4 : Form
    {
        private User user;
        public Form4(User user)
        {
            this.user = user;
            InitializeComponent();
        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            string key = textBox1.Text;
            using (var context = new MyDB())
            {
                var results = context.Goods
                    .Where(g => EF.Functions.Like(g.Id, $"%{key}%")) // 使用 Like 进行模糊查询
                    .ToList();
                //数据绑定
                dataGridView1.DataSource = new BindingList<Good>(results);
                //设置DataGridView的readonly属性
                dataGridView1.ReadOnly = true;
                DataGridViewCheckBoxColumn checkBoxColumn = new DataGridViewCheckBoxColumn();
                checkBoxColumn.HeaderText = "Select";
                checkBoxColumn.Name = "CheckBoxColumn";
                checkBoxColumn.Width = 50;

                // 将CheckBox列添加到DataGridView中
                dataGridView1.Columns.Add(checkBoxColumn);
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
        //返回选中行的商品
        public List<Good> GetSelectedGoods()
        {
            List<Good> selectedGoods = new List<Good>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (Convert.ToBoolean(row.Cells["CheckBoxColumn"].Value))
                {
                    Good good = row.DataBoundItem as Good;
                    if (good != null)
                    {
                        selectedGoods.Add(good);
                    }
                }
            }
            return selectedGoods;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //获取选中的商品
            List<Good> selectedGoods = GetSelectedGoods();
            if (selectedGoods.Count == 0)
            {
                MessageBox.Show("请至少选择一个商品");
                return;
            }
            //将商品加入该用户购物车
            using (var context = new MyDB())
            {
                var specificUser = context.Users
                    .Include(u => u.stared)

                    .FirstOrDefault(u => u.Id == user.Id);
                if (specificUser != null)
                {
                    foreach (var good in selectedGoods)
                    {
                        //检查购物车中是否已经有该商品
                        if (!specificUser.stared.Contains(good))
                        {
                            specificUser.stared.Add(good);
                        }
                    }
                    context.SaveChanges();
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //获取选中的商品,只允许选中一个商品
            List<Good> selectedGoods = GetSelectedGoods();
            if (selectedGoods.Count == 0)
            {
                MessageBox.Show("请至少选择一个商品");
                return;
            }
            else if (selectedGoods.Count > 1)
            {
                MessageBox.Show("只能选择一个商品");
                return;
            }
            //获取选中的商品
            Good selectedGood = selectedGoods[0];
            //创建订单
            Form5 form5 = new Form5(selectedGood, user);
            form5.ShowDialog();
        }
    }
}
