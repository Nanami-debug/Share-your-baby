using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Mysqlx.Crud;
using System.ComponentModel.DataAnnotations;
using USER;

namespace GOOD
{
    public class Good
    {
        [Key]
        public string Id { get; set; }//商品名
        public int price { get; set; }//商品价格
        public string des { get; set; }//商品描述
        public bool solded { get; set; }//商品类型
        //添加商品的一个属性，这个属性用于存储商品的图片
        public Image img { get; set; }
        public int UserId { get; set; } // 外键，关联Order
        public User user { get; set; } // 导航属性
        public Good(string id, int price, string des, bool solded, Image img)
        {
            this.Id = id;
            this.price = price;
            this.des = des;
            this.solded = solded;
            this.img = ResizeImage(150,150,img);
        }
        //需要对图片进行等大小处理
        public Image ResizeImage(int width, int height,Image image)
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
    }
}
