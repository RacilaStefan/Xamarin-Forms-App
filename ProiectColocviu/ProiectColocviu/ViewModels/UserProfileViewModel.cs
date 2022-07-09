using EJobsMarket.Models;
using EJobsMarket.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace EJobsMarket.ViewModels
{
    class UserProfileViewModel : BaseViewModel
    {
        public Command LoadUserDetailsCommand { get; }
        public Command DeleteUserCommand { get; }

        private string username;
        public string Username
        {
            get => username;
            set => SetProperty(ref username, value);
        }

        private string contact;
        public string Contact
        {
            get => contact;
            set => SetProperty(ref contact, value);
        }

        private string description;
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        private ExperienceTypes experience;
        public ExperienceTypes Experience
        {
            get => experience;
            set => SetProperty(ref experience, value);
        }

        public UserProfileViewModel()
        {
            Title = "User Details";
            LoadUserDetailsCommand = new Command(OnLoadUserDetails);
            DeleteUserCommand = new Command(OnDeleteAccount);
        }

        private async void OnDeleteAccount()
        {
            if (!await Database.userService.DeleteUserAsync(App.ActiveUser.Id))
                return;

            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
        }

        public void OnAppearing()
        {
            OnLoadUserDetails();
        }

        private void OnLoadUserDetails()
        {
            try
            {
                User user = App.ActiveUser;
                Username = user.Username;
                Contact = user.Contact;
                Description = user.Description;
                Experience = user.Experience;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
