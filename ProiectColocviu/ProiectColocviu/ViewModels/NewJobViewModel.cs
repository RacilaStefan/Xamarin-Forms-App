using EJobsMarket.Database;
using EJobsMarket.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EJobsMarket.ViewModels
{
    class NewJobViewModel : JobViewModel
    {
        public List<string> ExperienceTypesList => new List<string>(Enum.GetNames(typeof(ExperienceTypes)));
        public List<string> JobTypesList => new List<string>(Enum.GetNames(typeof(JobTypes)));
        public List<string> CompensationTypesList => new List<string>(Enum.GetNames(typeof(CompensationTypes)));

        public Command AddJobCommand { get; }
        public Command CancelCommand { get; }

        public NewJobViewModel()
        {
            AddJobCommand = new Command(OnAddJob);
            CancelCommand = new Command(OnCancel);
        }

        private async void OnAddJob()
        {
            if (!await Database.jobService.SaveJobAsync(JobTitle, Description, ExperienceRequired, JobType, CompensationType, Compensation, Remote))
                return;

            await Shell.Current.Navigation.PopAsync();
        }

        private async void OnCancel()
        {
            await Shell.Current.Navigation.PopAsync();
        }
    }
}
