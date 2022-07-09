using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EJobsMarket;
using ProiectColocviu.Droid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(AndroidImplementation))]
namespace ProiectColocviu.Droid
{
    public class AndroidImplementation : IExternalStorage
    {
        public string GetPath()
        {
            Context context = Android.App.Application.Context;
            var filePath = context.GetExternalFilesDir("");
            return filePath.Path;
        }
    }
}