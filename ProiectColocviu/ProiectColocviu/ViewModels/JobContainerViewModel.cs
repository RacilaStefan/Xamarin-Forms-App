using EJobsMarket.Models;
using EJobsMarket.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace EJobsMarket.ViewModels
{
    class JobContainerViewModel : BaseViewModel
    {
        public Command LoadJobsCommand { get; set; }
        public Command AddJobCommand { get; }
        public Command<Job> JobTapped { get; }

        public JobContainerViewModel()
        {
            AddJobCommand = new Command(OnAddJob);
            JobTapped = new Command<Job>(OnJobTapped);
        }

        private async void OnAddJob(object obj)
        {
            await Shell.Current.GoToAsync(nameof(NewJobPage));
        }

        private async void OnJobTapped(Job job)
        {
            if (job == null)
                return;

            await Shell.Current.GoToAsync($"{nameof(JobDetailPage)}?{nameof(JobDetailViewModel.JobId)}={job.Id}");
        }

        protected void AddItemsToObservableCollection<T>(ObservableCollection<T> list, List<T> items)
        {
            list.Clear();
            foreach (T item in items)
            {
                list.Add(item);
            }
        }
    }
}
