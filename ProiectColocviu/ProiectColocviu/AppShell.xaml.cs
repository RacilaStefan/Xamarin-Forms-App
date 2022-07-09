using EJobsMarket.ViewModels;
using EJobsMarket.Views;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EJobsMarket
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(JobDetailPage), typeof(JobDetailPage));
            Routing.RegisterRoute(nameof(NewJobPage), typeof(NewJobPage));
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            SecureStorage.Remove("LoggedUserId");
            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }
    }
}
