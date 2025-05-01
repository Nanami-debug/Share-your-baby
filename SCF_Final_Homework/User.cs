using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOOD;
using ORDER;
using Myd;

namespace USER
{
    public class User
    {
        public string Id { get; set; }//用户名
        public string password { get; set; }//密码
        public List<Good> goods { get; set; }//发布商品列表
        public List<Good> stared { get; set; }//购物车
        public List<Order> buy_orders { get; set; }//作为买家订单列表
        public List<Order> sell_orders { get; set; }//作为卖家订单列表
        public string call { get; set; }//联系方式

        //添加默认构造函数
        public User() { }
        public User(string id, string password, string call)
        {
            this.Id = id;
            this.password = password;
            this.call = call;
            this.goods = new List<Good>();
            this.stared = new List<Good>();
        }
        //发布商品
        public void Publish(Good good)
        {
            //将商品添加到数据库
            using (var context = new MyDB())
            {
                //先查询用户，再把商品添加到数据库
                var user = context.Users.FirstOrDefault(u => u.Id == this.Id);
                user.goods.Add(good);
            }
        }
        //下架商品
        public void OffShelf(Good good)
        {
            //将商品从数据库中删除
            using (var context = new MyDB())
            {
                //先查询用户，再把商品从数据库中删除
                var user = context.Users.FirstOrDefault(u => u.Id == this.Id);
                var goodToRemove = user.goods.FirstOrDefault(g => g.Id == good.Id);
                //查询商品是否已创建订单
                var order = context.Orders.FirstOrDefault(o => o.good.Id == good.Id);
                //如果商品已创建订单，则不能删除
                if (order != null)
                {
                    Console.WriteLine("商品已创建订单，不能删除！");
                    return;
                }
                if (goodToRemove != null)
                {
                    user.goods.Remove(goodToRemove);
                    context.SaveChanges();
                }
            }
        }
        //添加商品到购物车
        public void AddToStared(Good good)
        {
            //将商品添加到数据库
            using (var context = new MyDB())
            {
                //先查询用户，再把商品添加到数据库
                var user = context.Users.FirstOrDefault(u => u.Id == this.Id);
                user.stared.Add(good);
                context.SaveChanges();
            }
        }
        //从购物车中删除商品
        public void RemoveFromStared(Good good)
        {
            //将商品从数据库中删除
            using (var context = new MyDB())
            {
                //先查询用户，再把商品从数据库中删除
                var user = context.Users.FirstOrDefault(u => u.Id == this.Id);
                var goodToRemove = user.stared.FirstOrDefault(g => g.Id == good.Id);
                if (goodToRemove != null)
                {
                    user.stared.Remove(goodToRemove);
                    context.SaveChanges();
                }
            }
        }




        //下面为用户的订单处理操作
        //购买商品，即创建订单
        public Order Buy(Good good)
        {
            Order order = new Order();
            order.good = good;
            order.buyer = this;
            order.seller = good.user;;
            //获得当前时间并转为string,如2025年4月5日12：30则生成字符串202504051230
            string time = DateTime.Now.ToString("yyyyMMddHHmm");
            Random random = new Random();
            //生成订单号
            string orderId = time + random.Next(1000, 9999).ToString();
            order.Id = orderId;
            //存入数据库
            using (var context = new MyDB())
            {
                //先查询用户，再把订单添加到数据库
                var buyer = context.Users.FirstOrDefault(u => u.Id == this.Id);
                var seller = context.Users.FirstOrDefault(u => u.Id == good.user.Id);
                //将订单添加到买家和卖家的订单列表中
                buyer.buy_orders.Add(order);
                seller.sell_orders.Add(order);
                context.SaveChanges();
            }
            return order;
        }
        //付款
        public void Pay(Order order)
        {
            //修改数据库中的订单状态为已支付
            using (var context = new MyDB())
            {
                var orderToUpdate = context.Orders.FirstOrDefault(o => o.Id == order.Id);
                if (orderToUpdate != null)
                {
                    orderToUpdate.orderStatus = OrderStatus.NotSend;
                    context.SaveChanges();
                }
            }
        }
        //发货
        public Order Send(Order order)
        {
            //修改数据库中的订单状态为已发货
            using (var context = new MyDB())
            {
                var orderToUpdate = context.Orders.FirstOrDefault(o => o.Id == order.Id);
                if (orderToUpdate != null)
                {
                    orderToUpdate.orderStatus = OrderStatus.NotGet;
                    context.SaveChanges();
                }
            }
            return order;
        }
        //收货
        public Order Get(Order order)
        {
            //修改数据库中的订单状态为已收货
            using (var context = new MyDB())
            {
                var orderToUpdate = context.Orders.FirstOrDefault(o => o.Id == order.Id);
                if (orderToUpdate != null)
                {
                    orderToUpdate.orderStatus = OrderStatus.Get;
                    context.SaveChanges();
                }
            }
            return order;
        }
    }
}
