using EJobsMarket.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace EJobsMarket.ViewModels
{
    class HomeViewModel : JobContainerViewModel
    {
        public ObservableCollection<Job> RecommendedJobs { get; set; } = new ObservableCollection<Job>();
        public ObservableCollection<Job> OpenJobs { get; } = new ObservableCollection<Job>();
        public ObservableCollection<Job> OpenOrders { get; } = new ObservableCollection<Job>();

        public HomeViewModel()
        {
            Title = "Home";
            LoadJobsCommand = new Command(OnLoadJobs);
        }

        public void OnAppearing()
        {
            if (RecommendedJobs.Count == 0 && OpenJobs.Count == 0 && OpenOrders.Count == 0)
                OnLoadJobs();
        }

        private async void OnLoadJobs()
        {
            IsBusy = true;

            try
            {
                List<Job> jobs = await Database.jobService.GetRecommendedJobsAsync(App.ActiveUser);
                AddItemsToObservableCollection(RecommendedJobs, jobs);
                jobs = await Database.jobService.GetJobsByTakerIdAsync(App.ActiveUser.Id);
                AddItemsToObservableCollection(OpenJobs, jobs);
                jobs = await Database.jobService.GetJobsByMakerIdAsync(App.ActiveUser.Id);
                AddItemsToObservableCollection(OpenOrders, jobs);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
