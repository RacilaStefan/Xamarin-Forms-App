using EJobsMarket.Database;
using EJobsMarket.Models;
using EJobsMarket.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EJobsMarket.ViewModels
{
    public class RegisterViewModel : BaseViewModel
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

        private ExperienceTypes experience = (ExperienceTypes)(-1);
        public ExperienceTypes Experience
        {
            get => experience;
            set => SetProperty(ref experience, value);
        }

        public List<string> ExperienceTypes => new List<string>(Enum.GetNames(typeof(ExperienceTypes)));
        public Command RegisterCommand { get; }

        public RegisterViewModel()
        {
            RegisterCommand = new Command(OnRegisterClicked);
        }

        private async void OnRegisterClicked(object obj)
        {
            if (!await Database.userService.SaveUserAsync(username, password, contact, description, experience))
                return;

            await Shell.Current.GoToAsync("..");
        }
    }
}
