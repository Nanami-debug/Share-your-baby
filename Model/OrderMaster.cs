using System;
using System.Collections.Generic;

namespace Model
{
    // 订单主表实体类
    [Table("OrderMaster")]
    public class OrderMaster
    {
        [Key]
        public int OrderID { get; set; }

        // 员工ID
        public int EmployeeID { get; set; }

        // 订单日期
        public DateTime OrderDate { get; set; }

        // 订单总金额
        public decimal TotalAmount { get; set; }

        // 支付方式
        public string PaymentMethod { get; set; }

        // 支付状态
        public string PaymentStatus { get; set; }

        // 订单编号
        public string OrderNo { get; set; }

        [NotMapped]
        public int BranchID { get; set; }

        [NotMapped]
        public List<OrderDetail> OrderDetail { get; set; }
    }
}