using Autofac;
using Prism.Autofac;
using Prism.Autofac.Forms;
using MTMobile.Views;
using Xamarin.Forms;

namespace MTMobile
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync("MasterView/NavigationPage/HomeView");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<NavigationPage>();
            Container.RegisterTypeForNavigation<HomeView>();
            Container.RegisterTypeForNavigation<MasterView>();
            Container.RegisterTypeForNavigation<MenuView>();
        }
    }
}
