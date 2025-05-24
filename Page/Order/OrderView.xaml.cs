using Dapper;
using Form.Models;
using Form.Page.Login;
using System.Linq;
using System.Windows;

namespace Form.Page.Order
{
    /// <summary>
    /// OrderView.xaml 的交互逻辑
    /// </summary>
    public partial class OrderView : System.Windows.Controls.Page
    {
        public OrderView()
        {
            InitializeComponent();
        }

        public void LoadData(string orderId)
        {
            using (var db = DBHelper.GetConn())
            {
                var sql = $@"
                    SELECT
	                    A.*,
	                    B.address,
	                    C.img,
	                    C.des,
	                    CASE WHEN A.sellerId = '{LoginViewModel.User.Id}' THEN '我卖出的' ELSE '我买入的' END AS transactionType
                    FROM
	                    Orders A
	                    LEFT JOIN Users B ON B.Id = A.buyerId
	                    LEFT JOIN Goods C ON C.Id = A.goodId
                    WHERE
                    ( A.sellerId = '{LoginViewModel.User.Id}' OR A.buyerId = '{LoginViewModel.User.Id}' ) ";

                if (!string.IsNullOrEmpty(orderId))
                {
                    sql += $"and A.Id like '%{orderId}%'";
                }
                var list = db.Query<OrderViewModel>(sql).ToList();
                this.dgdData.ItemsSource = list;
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            LoadData(txtId.Text);
        }
    }
}