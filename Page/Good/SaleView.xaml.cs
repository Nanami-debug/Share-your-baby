using Dapper;
using Form.Page.Dialog;
using Form.Page.Home;
using Form.Page.Login;
using Model;
using Newtonsoft.Json;
using System;
using System.Windows;
using Aop.Api.Request;
using Aop.Api.Response;
using Aop.Api;
using Newtonsoft.Json;

namespace Form.Page.Good
{
    /// <summary>
    /// SaleView.xaml 的交互逻辑
    /// </summary>
    public partial class SaleView : System.Windows.Controls.Page
    {
        public Goods Good { get; }

        public SaleView(Goods good)
        {
            InitializeComponent();
            Good = good;
            RegView.LoadImageFromBytes(good.img, Img);
            txtAddress.Text = LoginViewModel.User.address;
            txtName.Text = good.Id;
            txtPrice.Text = $@"¥ {good.price}";
            txtTotal.Text = $@"¥ {good.price}";
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            Orders orders = new Orders();
            orders.Id = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            orders.goodId = Good.Id;
            orders.orderStatus = "001";
            orders.buyerId = LoginViewModel.User.Id;
            orders.sellerId = Good.UserId;
            Good.solded = true;
            using (var db = DBHelper.GetConn())
            {
                var tr = db.BeginTransaction();
                var id = db.Insert<string, Orders>(orders, tr);
                db.Update(Good, tr);
                tr.Commit();
            }
            Buy(orders.Id + Good.Id);
            var dg = this.Tag as DialogView;
            dg.Close();
        }
        public async void Buy(string no)
        {
            string privateKey = "MIIEowIBAAKCAQEA5YSy/vivl6LOpWdG9RA0PDCP8JhVZxM2tklGjkOpw9wfIR917ud1lvUZKV3xkXMwAyXafVMdXEQV6rVZsrmySJY+S4gPrcOXio3jq00R43q8cvIr2WnT8twcDp/gm+qiP1M7UggxBVtL0zjEum5ENaEDJ1fx6ep88pk38/L7eJQyJmzEfWGTypdJdbaZO/1mBJZ47Ht2Yl4alL9LqHuL/cukJoGk8vlzvY2BERnfziMWZ0ZzA2i+EysYXa0qazt6YtKc817O5qI4YYAJv/LLF/PsjrmUrYjT/k80M7HzrKVoqntWM35h9BG0+t1y4zmP/vss2+V39hp60QQHaCwtCwIDAQABAoIBAFL+QwNzxXrdgj/JMp1W+KxzGvly/B7DkdskcADtcqvCIveFOs3ioGCvzCNrNvjTYB8jRvheX5WHgS1X2lGIWHFq8qe5UYIR4fBWEmdU/Qz2ysH90+FQJTYCZZrcMQnwNC8V17N4BtHbC3YbZvfK+5sw18UYRf/BrODEirkJuekFbixz4OrhQI5DflF3j++D9x+lKL3tatfS5+/l2hW/hkbQom3L2Iht7TuY3ak4TX1LMnTcAJLyxDmTIqKaTgMVm0XhZKqUKCAe64FO+lIb37X1olh1hDyDNq3FJwVrhmfWlceR8+6ukRtHxG+x18Ce9zsEZSry7mgNUlL1KMj++4ECgYEA98kea85YJZfG+SKh/sm9wAiSXoHdWA88JB5Pk8kmpodDmJlTiAIYsZZ+1X8LjtRHdfRIWJtQtSwFOuQ67mlhiFiLKJfoUO4OELVVRE4V24IViXo6X46j+mdPeyHmHEjR2MHj+k/BZWZ7LKDUEgyru6BXKep8ftRu68ywg16hbGECgYEA7SCNOW82l2ejTclOMAdvAAR7cjVQ3x26MhANOGfyDWLAuUY0PtCdzrtgPOLcGSGEhG04E3YJTxgy2Pu8reUTzOJ/Kx2vSNdAkYkYvCSOuh1Gitqu0V6Bbnt0QLjtnLeUPc6f64Ocfp427sQjAg0T6bzHT/j+DlZTMHgzU6lbsOsCgYA74ZDGgOFwhFODlckMZ/jBVdD5oey6AUSJfgHBN1Vt3TaKxFMZhVguYq5YLhG+/LIgYV6yeyZwAilpaAqN303Aw5UtxDhShpVDmG74yN4jZDaMOMuumCwssZbVrQWNRFPiTEkqDugv9ypBSMS3b/R4rXEjIhXgGLqNpbPqq43bQQKBgQCZEZc4SQsjZxoqf1UCefn0N/B8A+CldioYYPY3wBmm/GVN8/yLw9zqc1gnVYYgjttdJKe7IEnYDdXc7XuZgTNr4ZWXfn0E9zZN/ALEiJC9oWJFBe8tZeUFLZoYoYd4z6RY1YWz4Oxna7goHuI+U7TUADFk9WUCWkUvF5iCW+O9awKBgGRgTYGll9bupyWdMRjXrQyGZtJQjYzEe7mg5ZOpTf4Wzv/j9KVsROLEOECKdpmNO444QN3EURJeQTORWc/XkCIfNrMMRCqla6ek13ywa5fxZpZY7h3aWUvTRpsIixUUtm/rt2cJcRmdahKz9WAB5j32vmCZupxSHg4XXs+uI7Ud";



            string alipayPublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAgoTY8+gbC5KGD7Mv88UHPALaptwiCMKpiF99rvy9HUqHNZjyIz5JeYdW46Vvv7Kj8dxIVvFG6jx0mAJ82D/H9o/raokKbOICVrUSy0JLMGRB9BYJVJQZo2DzSl+6Be67sN5Ev6wfEhXjqSuqPC96R8aGvwEmqFXRgNOeLa7ySw47y4+Q1PLEiyl9q1Mnnzu1iJKTYYSVQRNk93uSuJ/opd/yQvu1kHITV4MWMfNjZB32zJPtHfyZ5R/ePMau23ftaQquaUP9WvjGUdkjJ5dJfGALKJNfBKL5xsEMXp3K3aZk3fg20UhlLeDW7IB60wrSEau/wdRCzgLvQsE7bOWJjQIDAQAB";

            string serverUrl = "https://openapi-sandbox.dl.alipaydev.com/gateway.do";
            string appId = "2021000148639898";
            IAopClient client = new DefaultAopClient(
                serverUrl,
                appId,
                privateKey,
                "json",
                "1.0",
                "RSA2",
                alipayPublicKey,
                "utf-8",
                false
            );

            AlipayTradePagePayRequest request = new AlipayTradePagePayRequest();
            request.SetReturnUrl("http://localhost:5005");
            request.SetNotifyUrl("http://p62cba28.natappfree.cc/notify");

            var bizObj = new
            {
                out_trade_no = no,
                product_code = "FAST_INSTANT_TRADE_PAY",
                total_amount = Convert.ToDecimal(Good.price).ToString("0.00"),
                subject = "购买" + Good.Id + "的订单",
            };
            request.BizContent = JsonConvert.SerializeObject(bizObj);

            try
            {
                AlipayTradePagePayResponse response = client.pageExecute(request);
                string formHtml = response.Body;

                // 启动本地 HTTP Server 显示页面
                var server = new LightweightHttpServer(formHtml);
                server.OnPaymentNotify += (string notifyData) =>
                {
                    // TODO：可以添加验签逻辑
                    //Invoke(new Action(() => { }));
                };

                await server.StartAsync(); // 启动本地服务器并打开支付页面
                bool paid = await server.WaitForPaymentResultAsync();

                if (paid)
                {
                    //User.Pay(order);//付款
                    MessageBox.Show("用户已支付成功");
                }
                else
                {
                    MessageBox.Show("支付未完成或失败");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("支付失败：" + ex.Message);
            }
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            var dg = this.Tag as DialogView;
            dg.Close();
        }
    }
}