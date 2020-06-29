using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MON.LT.Services;
using MON.LT.Views;

namespace MON.LT
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
