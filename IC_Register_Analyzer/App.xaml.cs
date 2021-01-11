using IC_Register_Analyzer.Views;
using IC_Register_Analyzer.ViewModels;
using Prism.Ioc;
using Prism.Modularity;
using System.Windows;

namespace IC_Register_Analyzer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Viewの登録
            containerRegistry.RegisterDialog<UserControlSelectIC, UserControlSelectICViewModel>();
            containerRegistry.RegisterForNavigation<UserControlNone>();
            containerRegistry.RegisterForNavigation<UserControlADF4111>();
            containerRegistry.RegisterForNavigation<UserControlADF4111_None>();
            containerRegistry.RegisterForNavigation<UserControlADF4111_Reference>();
            containerRegistry.RegisterForNavigation<UserControlADF4111_AB>();
            containerRegistry.RegisterForNavigation<UserControlADF4111_Function>();
            containerRegistry.RegisterForNavigation<UserControlADF4111_Initialize>();
            containerRegistry.RegisterForNavigation<UserControlR2A20178NP>();
        }
    }
}
