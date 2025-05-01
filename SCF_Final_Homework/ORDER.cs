using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOOD;
using USER;

namespace ORDER
{
    public enum OrderStatus//依据资金流转状态区分
    {
        NotPay,//尚未支付
        NotSend,//支付成功，尚未发货
        NotGet,//发货成功，尚未收货
        Get//已收货
    }
    public class Order
    {
        [Key]
        public string Id { get; set; }//订单号
        public string? goodId { get; set; } 
        public Good? good { get; set; }
        public OrderStatus orderStatus { get; set; }
        public User buyer { get; set; }//买家
        public string buyerId { get; set; } // 外键，关联User
        public User seller { get; set; }//卖家
        public string sellerId { get; set; } // 外键，关联User
        public Order()
        {
            this.orderStatus = OrderStatus.NotPay;
        }
    }
}
