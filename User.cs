using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GOOD;

namespace USER
{
    public class User
    {
        public string Id { get; set; }//用户名
        public string password { get; set; }//密码
        public List<Good> goods { get; set; }//商品列表
        public List<Good> stared { get; set; }//购物车
        public string call { get; set; }//联系方式
        public User(string id, string password, string call)
        {
            this.Id = id;
            this.password = password;
            this.call = call;
            this.goods = new List<Good>();
            this.stared = new List<Good>();
        }
        //添加商品的方法
        public void AddGood(Good good)
        {
            goods.Add(good);
        }
        //添加购物车的方法  
        public void AddStared(Good good)
        {
            stared.Add(good);
        }
        //查找商品的方法，依据关键字查找
        public List<Good> FindGood(string key)
        {
            List<Good> result = new List<Good>();
            foreach (var good in goods)
            {
                if (good.Id.Contains(key) || good.des.Contains(key))
                {
                    result.Add(good);
                }
            }
            return result;
        }
        //待添加

    }
}
