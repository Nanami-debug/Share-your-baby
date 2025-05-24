using HandyControl.Controls;
using System.Windows;
using System.Windows.Threading;

namespace Form
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Growl.InfoGlobal(e.Exception.Message);
            e.Handled = true;
        }
    }
}