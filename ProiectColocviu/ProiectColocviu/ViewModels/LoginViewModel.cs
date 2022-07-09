using EJobsMarket.Database;
using EJobsMarket.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EJobsMarket.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string username;
        public string Username
        {
            get => username;
            set => SetProperty(ref username, value);
        }

        private string password;
        public string Password
        {
            get => password;
            set => SetProperty(ref password, value);
        }

        private string error;
        public string Error
        {
            get => error;
            set => SetProperty(ref error, value);
        }

        public Command LoginCommand { get; }
        public Command SignUpCommand { get; }
        
        public LoginViewModel()
        {
            LoginCommand = new Command(OnLoginClicked);
            SignUpCommand = new Command(OnSignUpClicked);
        }

        private async void OnSignUpClicked(object obj)
        {
            await Shell.Current.GoToAsync(nameof(RegisterPage));
        }

        private async void OnLoginClicked(object obj)
        {
            if (!await Database.userService.LoginUserAsync(Username, Password))
            {
                Error = "Bad Credentials";
                return;
            }

            Error = "";
            // Prefixing with `//` switches to a different navigation stack instead of pushing to the active one
            await Shell.Current.GoToAsync("//HomePage");
        }

        public void OnDisappearing()
        {
            Username = "";
            Password = "";
        }
    }
}
