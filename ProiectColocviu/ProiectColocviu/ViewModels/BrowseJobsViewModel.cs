using EJobsMarket.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace EJobsMarket.ViewModels
{
    class BrowseJobsViewModel : JobContainerViewModel
    {
        public ObservableCollection<Job> Jobs { get; } = new ObservableCollection<Job>();

        public BrowseJobsViewModel()
        {
            Title = "Browse Jobs";
            LoadJobsCommand = new Command(OnLoadJobs);
        }

        public void OnAppearing()
        {
            if (Jobs.Count == 0)
                OnLoadJobs();
        }

        private async void OnLoadJobs()
        {
            IsBusy = true;

            try
            {
                AddItemsToObservableCollection(Jobs, await Database.jobService.GetJobsAsync());
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
