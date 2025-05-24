using System;

namespace Model
{
    public class Users
    {
        [Key, Required]
        public string Id { get; set; }//用户名

        public string password { get; set; }//密码
        public string call { get; set; }//联系方式
        public byte[] img { get; set; }
        public string address { get; set; }
    }

    public class Goods
    {
        [Key, Required]
        public string Id { get; set; }//商品名

        public int price { get; set; }//商品价格
        public string des { get; set; }//商品描述
        public bool solded { get; set; }//商品类型

        //添加商品的一个属性，这个属性用于存储商品的图片
        public byte[] img { get; set; }

        public string UserId { get; set; } // 外键，关联发布者
    }

    public class Orders
    {
        [Key, Required]
        public string Id { get; set; }//订单号

        public string goodId { get; set; }
        public string orderStatus { get; set; }
        public string buyerId { get; set; } // 外键，关联User
        public string sellerId { get; set; } // 外键，关联User
    }

    public class Chat
    {
        [Key, Required]
        public string ChatNo { get; set; }

        public string Id1 { get; set; }
        public string Id2 { get; set; }
    }

    public class ChatDetail
    {
        [Key, Required]
        public string ChatDetailNo { get; set; }

        public string ChatNo { get; set; }
        public string Msg { get; set; }
        public string UserId { get; set; }
        public DateTime SendTime { get; set; }
    }
}