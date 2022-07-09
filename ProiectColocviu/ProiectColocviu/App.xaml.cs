using EJobsMarket.Database;
using EJobsMarket.Models;
using EJobsMarket.ViewModels;
using EJobsMarket.Views;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EJobsMarket
{
    public partial class App : Application
    {
        public static User ActiveUser;
        private EJobsMarketDatabase Database => DependencyService.Get<EJobsMarketDatabase>();

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();

            DependencyService.RegisterSingleton(new EJobsMarketDatabase());
        }

        protected override void OnStart()
        {
            Init();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private async void Init()
        {
            Database.jobService.AddMockDataFromFileAsync();

            try
            {
                if (!await Database.userService.AddMockDataAsync()) 
                    throw new ArgumentNullException();

                ActiveUser = await Database.userService.GetUserAsync(int.Parse(await SecureStorage.GetAsync("LoggedUserId")));

                if (ActiveUser is null) 
                    throw new ArgumentNullException();

                await Shell.Current.GoToAsync("//HomePage");
            }
            catch (ArgumentNullException)
            {
                await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
            }
        }
    }
}
