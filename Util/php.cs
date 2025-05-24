using System.Net;
using System.Text;
using System.Diagnostics;
using System;
using System.Threading.Tasks;
using System.IO;

public class LightweightHttpServer : IDisposable
{
    private readonly string _htmlContent;
    private readonly int _port;
    private HttpListener _listener;
    private TaskCompletionSource<bool> _paymentTcs;
    public event Action<string> OnPaymentNotify;

    public LightweightHttpServer(string htmlContent, int port = 5005)
    {
        _htmlContent = htmlContent;
        _port = port;
    }

    public async Task StartAsync()
    {
        _listener = new HttpListener();
        _listener.Prefixes.Add($"http://localhost:{_port}/");
        _listener.Start();

        // 自动打开浏览器
        Process.Start(new ProcessStartInfo($"http://localhost:{_port}")
        {
            UseShellExecute = true
        });

        _ = Task.Run(async () =>
        {
            while (_listener.IsListening)
            {
                var context = await _listener.GetContextAsync();
                ProcessRequest(context);
            }
        });
    }

    private void ProcessRequest(HttpListenerContext context)
    {
        try
        {
            if (context.Request.HttpMethod == "GET")
            {
                // 处理页面请求
                if (context.Request.Url.AbsolutePath == "/" ||
                    context.Request.Url.AbsolutePath == "/index")
                {
                    var buffer = Encoding.UTF8.GetBytes(_htmlContent);
                    context.Response.ContentType = "text/html; charset=utf-8";
                    context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                }
                else
                {
                    context.Response.StatusCode = 404;
                }
            }
            else if (context.Request.HttpMethod == "POST" &&
                    context.Request.Url.AbsolutePath == "/notify")
            {
                // 处理支付回调
                using (var reader = new StreamReader(context.Request.InputStream))
                {
                    var body = reader.ReadToEnd();
                    var query = System.Web.HttpUtility.ParseQueryString(body);

                    var status = query["trade_status"];
                    var orderId = query["out_trade_no"];
                    var isSuccess = status == "TRADE_SUCCESS" || status == "TRADE_FINISHED";

                    OnPaymentNotify?.Invoke(isSuccess ?
                        $"支付成功，订单号：{orderId}" :
                        $"支付失败，状态：{status}");

                    _paymentTcs?.TrySetResult(isSuccess);
                }

                var response = Encoding.UTF8.GetBytes("success");
                context.Response.OutputStream.Write(response, 0, response.Length);
            }

            context.Response.Close();
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = 500;
            Console.WriteLine($"处理请求出错: {ex}");
        }
    }

    public Task<bool> WaitForPaymentResultAsync()
    {
        _paymentTcs = new TaskCompletionSource<bool>();
        return _paymentTcs.Task;
    }

    public void Dispose()
    {
        _listener?.Stop();
        _listener?.Close();
    }
}
