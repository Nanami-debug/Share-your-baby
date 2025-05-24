using Form.Page.Login;
using Form.Page.MainMenu;
using Stylet;
using StyletIoC;

namespace Form.Config
{
    public class Boot : Bootstrapper<LoginViewModel>
    {
        public static IWindowManager Manager;
        public static IContainer IOC;

        protected override void ConfigureIoC(IStyletIoCBuilder builder)
        {
            builder.Bind<MainMenuViewModel>().To<MainMenuViewModel>().InSingletonScope();
        }

        protected override void Configure()
        {
            // Perform any other configuration before the application starts
            IOC = this.Container;
            Manager = IOC.Get<IWindowManager>();
        }
    }
}