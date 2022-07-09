using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EJobsMarket.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        public Command ValueChangedCommand { get; }

        private double rotationValue = 0;
        public double RotationValue
        {
            get => rotationValue;
            set => SetProperty(ref rotationValue, value);
        }

        private double scaleValue = 1;
        public double ScaleValue
        {
            get => scaleValue;
            set => SetProperty(ref scaleValue, value);
        }

        private double xTranslationValue = 0;
        public double XTranslationValue
        {
            get => xTranslationValue;
            set => SetProperty(ref xTranslationValue, value);
        }

        private double yTranslationValue = 0;
        public double YTranslationValue
        {
            get => yTranslationValue;
            set => SetProperty(ref yTranslationValue, value);
        }

        public string Location => GetLocation().Result;

        public AboutViewModel()
        {
            Title = "About us";
            ValueChangedCommand = new Command(OnValueChanged);
        }

        private void OnValueChanged()
        {

        }

        private async Task<string> GetLocation()
        {
            try
            {
                Location location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    return $"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}";
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                Debug.WriteLine("Unable to get location1");
            }
            catch (FeatureNotEnabledException fneEx)
            {
                Debug.WriteLine("Unable to get location2");
            }
            catch (PermissionException pEx)
            {
                Debug.WriteLine("Unable to get location3");
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to get location");
            }

            return "the location was not accesible.";
        }

    }
}