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
    public partial class JobDetailPage : ContentPage
    {
        public JobDetailPage()
        {
            InitializeComponent();
            BindingContext = new JobDetailViewModel();
        }
    }
}