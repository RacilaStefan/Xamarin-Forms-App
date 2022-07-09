using EJobsMarket.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EJobsMarket.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BrowseJobsPage : ContentPage
    {
        private BrowseJobsViewModel viewModel;

        public BrowseJobsPage()
        {
            InitializeComponent();
            BindingContext = viewModel = new BrowseJobsViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            viewModel.OnAppearing();
        }
    }
}