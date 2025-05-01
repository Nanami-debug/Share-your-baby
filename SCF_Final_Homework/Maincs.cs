using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FORM3;
using USER;
using GOOD;
using FORM2;
using Myd;
using Microsoft.EntityFrameworkCore;
using FORM4;
using TRF;



namespace SCF_Final_Homework
{
    public partial class Maincs : Form
    {
        public User user;
        public Maincs()
        {
            InitializeComponent();
            Form2 form2 = new Form2();
            if (form2.ShowDialog() == DialogResult.OK)
            {
                if (form2.user0 == null)
                    this.Close();
                user = form2.user0;
            }
            //检查uesr的buy_orders列表中是否有已发货的订单
            using (var context = new MyDB())
            {
                var specificUser = context.Users
                    .Include(u => u.buy_orders)
                    .ThenInclude(o => o.good)
                    .FirstOrDefault(u => u.Id == user.Id);

                if (specificUser != null)
                {
                    foreach (var order in specificUser.buy_orders)
                    {
                        if (order.orderStatus == ORDER.OrderStatus.NotGet)
                        {
                            MessageBox.Show("您有已发货的订单，请查看");
                            break;
                        }
                    }
                }
            }
            //检查uesr的sell_orders列表中是否有待发货的订单
            using (var context = new MyDB())
            {
                var specificUser = context.Users
                    .Include(u => u.sell_orders)
                    .ThenInclude(o => o.good)
                    .FirstOrDefault(u => u.Id == user.Id);

                if (specificUser != null)
                {
                    foreach (var order in specificUser.sell_orders)
                    {
                        if (order.orderStatus == ORDER.OrderStatus.NotSend)
                        {
                            MessageBox.Show("您有待发货的订单，请查看");
                            break;
                        }
                    }
                }
            }
        }

        private void Maincs_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string name = textBox1.Text;
            int price = int.Parse(textBox2.Text);
            string des = textBox3.Text;
            //检查是否有图片
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("请添加图片");
                return;
            }
            byte[] img = ImageToByteArray(pictureBox1.Image);
            //创建商品对象
            Good good = new Good(name, price, des, false, img);
            AddGoodToUser(user.Id, good);
            MessageBox.Show("添加成功");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // 创建 OpenFileDialog 实例
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // 设置文件过滤器，限制用户只能选择图片文件
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";

            // 显示对话框并检查用户是否选择了文件
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // 获取用户选择的文件路径
                string filePath = openFileDialog.FileName;

                // 将图片加载到 PictureBox 控件中
                pictureBox1.Image = Image.FromFile(filePath);
                //图片自适应picturebox
                pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }
        //需要对图片进行等大小处理
        public Image ResizeImage(int width, int height, Image image)
        {
            // 创建一个新的Bitmap对象，指定大小
            Bitmap resizedImage = new Bitmap(width, height);

            // 使用Graphics对象来绘制调整大小后的图片
            using (Graphics graphics = Graphics.FromImage(resizedImage))
            {
                // 设置插值模式以提高图片质量
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                // 绘制图片
                graphics.DrawImage(image, 0, 0, width, height);
            }

            return resizedImage;
        }
        //图片转二进制
        public byte[] ImageToByteArray(Image image)
        {
            using (var ms = new MemoryStream())
            {
                image.Save(ms, image.RawFormat);
                return ms.ToArray();
            }
        }
        //二进制转图片
        public Image ByteArrayToImage(byte[] byteArray)
        {
            using (var ms = new MemoryStream(byteArray))
            {
                return Image.FromStream(ms);
            }
        }
        //添加关联商品
        public void AddGoodToUser(string userId, Good good)
        {
            using (var context = new MyDB())
            {
                // 查找已存在的 User 对象
                var user = context.Users.Include(u => u.goods)
                    .FirstOrDefault(u => u.Id == userId);

                if (user != null)
                {
                    // 设置 Good 的 UserId
                    good.UserId = user.Id;

                    // 将 Good 添加到 User 的 Goods 列表
                    user.goods.Add(good);

                    // 保存更改到数据库
                    context.SaveChanges();

                }
                else
                {
                    throw new Exception("User not found!");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 form4 = new FORM4.Form4(user);
            form4.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TR tr = new TR(user);
            tr.ShowDialog();
        }
    }
}
