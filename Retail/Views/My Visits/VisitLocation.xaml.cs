using System;
using System.Collections.Generic;
using Retail.Hepler;
using Retail.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Retail.Views.MyVisits
{
    public partial class VisitLocation : ContentPage
    {
        public UserCurrentLocation currentLocation { get; set; }
        public VisitLocation()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            if (currentLocation == null)
                currentLocation = new UserCurrentLocation();
            this.Title = CommonAttribute.VisitLocation.LocationStoreName;
            currentLocation.Area = CommonAttribute.VisitLocation.Area;
            currentLocation.Latitude = Convert.ToDouble(CommonAttribute.VisitLocation.Latitude);
            currentLocation.Longitude = Convert.ToDouble(CommonAttribute.VisitLocation.Longitude);
            if (currentLocation != null)
            {
                MapSpan mapSpan = MapSpan.FromCenterAndRadius
                    (new Position(Convert.ToDouble(currentLocation.Latitude), Convert.ToDouble(currentLocation.Longitude)),
                    Distance.FromKilometers(0.444));
                string labelName = "NA";
                if (!string.IsNullOrEmpty(currentLocation.Area))
                    labelName = currentLocation.Area;
                map.MoveToRegion(mapSpan);
                Pin pin = new Pin
                {
                    Label = labelName,
                    Address = currentLocation.Locality + ", " + currentLocation.CountryName,
                    Type = PinType.Place,
                    Position = new Position(currentLocation.Latitude, currentLocation.Longitude)
                };
                map.Pins.Clear();
                map.Pins.Add(pin);
            }
            base.OnAppearing();          
        }

        async void map_MapClicked(System.Object sender, Xamarin.Forms.Maps.MapClickedEventArgs e)
        {
            try
            {
                currentLocation = await Extensions.GetGeoAddress(e.Position.Latitude, e.Position.Longitude);
                if (currentLocation == null)
                {
                    await DisplayAlert("", "We will not provide service in this location.", "ok");
                    return;
                }
                string labelStr = "NA";// currentLocation.Area;
                if (string.IsNullOrEmpty(currentLocation.Area))
                    labelStr = currentLocation.Locality;
                else
                    labelStr = currentLocation.Area;

                Pin pin = new Pin
                {
                    Label = labelStr,
                    Address = currentLocation.Locality + ", " + currentLocation.CountryName,
                    Type = PinType.Place,
                    Position = new Position(e.Position.Latitude, e.Position.Longitude)
                };

                map.Pins.Clear();
                map.Pins.Add(pin);
            }
            catch (Exception ex)
            {
                await DisplayAlert("", "We will not provide service in this location.", "ok");
            }
        }

        public void direction_Clicked(System.Object sender, System.EventArgs e)
        {
            var location = new Xamarin.Essentials.Location(currentLocation.Latitude, currentLocation.Longitude);
            var placemark = new Placemark
            {
                CountryName = currentLocation.CountryName,
                AdminArea = currentLocation.Area,
                Thoroughfare = currentLocation.Locality,
                Locality = currentLocation.Locality,

            };

            var options = new MapLaunchOptions { Name = CommonAttribute.VisitLocation.LocationStoreName };

             Xamarin.Essentials.Map.OpenAsync(location, options);
        }
    }
}
