using EJobsMarket.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace EJobsMarket.ViewModels
{
    [QueryProperty(nameof(JobId), nameof(JobId))]
    class JobDetailViewModel : JobViewModel
    {
        private int jobId;
        public int JobId
        {
            get => jobId;
            set
            {
                jobId = value;
                LoadJob();
            }
        }

        private bool takeBtnVisibility;
        public bool TakeBtnVisibility
        {
            get => takeBtnVisibility;
            set => SetProperty(ref takeBtnVisibility, value);
        }

        private bool cancelOrderBtnVisibility;
        public bool CancelOrderBtnVisibility
        {
            get => cancelOrderBtnVisibility;
            set => SetProperty(ref cancelOrderBtnVisibility, value);
        }

        private bool cancelJobBtnVisibility;
        public bool CancelJobBtnVisibility
        {
            get => cancelJobBtnVisibility;
            set => SetProperty(ref cancelJobBtnVisibility, value);
        }

        private string taker;
        public string Taker
        {
            get => taker;
            set => SetProperty(ref taker, value);
        }

        public Command TakeJobCommand { get; }
        public Command CancelOrderCommand { get; }
        public Command CancelJobCommand { get; }

        public JobDetailViewModel()
        {
            Title = "Job Details";
            TakeJobCommand = new Command(OnTakeJob);
            CancelJobCommand = new Command(OnCancelJob);
            CancelOrderCommand = new Command(OnCancelOrder);
        }

        public async void LoadJob()
        {
            try
            {
                Job job = await Database.jobService.GetJobAsync(JobId);
                JobTitle = job.Title;
                Description = job.Description;
                ExperienceRequired = job.ExperienceRequired;
                JobType = job.Type;
                CompensationType = job.CompensationType;
                Compensation = job.Compensation;
                Remote = job.Remote;
                RemoteString = Remote ? "Yes" : "No";
                Taker = job.TakerId != 0 ? (await Database.userService.GetUserAsync(job.TakerId)).Username : "No one";

                TakeBtnVisibility = job.MakerId != App.ActiveUser.Id && job.TakerId == 0;
                CancelJobBtnVisibility = job.TakerId == App.ActiveUser.Id;
                CancelOrderBtnVisibility = job.MakerId == App.ActiveUser.Id;
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Job");
            }
        }

        private async void OnTakeJob()
        {
            if (!await Database.jobService.UpdateJobTakerAsync(App.ActiveUser.Id, JobId))
                return;

            LoadJob();
        }

        private async void OnCancelJob()
        {
            if (!await Database.jobService.UpdateJobTakerAsync(0, JobId))
                return;

            LoadJob();
        }

        private async void OnCancelOrder()
        {
            if (!await Database.jobService.DeleteJobAsync(JobId))
                return;

            await Shell.Current.Navigation.PopAsync();
        }
    }
}
