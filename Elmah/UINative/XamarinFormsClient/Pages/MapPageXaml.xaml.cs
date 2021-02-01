using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Elmah.XamarinForms.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPageXaml : ContentPage
    {
        public Elmah.XamarinForms.ViewModels.AppVM AppVM
        {
            get
            {
                return DependencyService.Resolve<Elmah.XamarinForms.ViewModels.AppVM>();
            }
        }

        public int MapSearchDistanceInKM { get; set; } = 20;

        public MapPageXaml()
        {
            InitializeComponent();
        }

        private void Map_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //TODO: need a timer to remember refresh search result.
        }

        private void Map_SizeChanged(object sender, EventArgs e)
        {
        }

        private async void Map_MeasureInvalidated(object sender, EventArgs e)
        {
            await Device.InvokeOnMainThreadAsync(async () => {
                if (sender is Xamarin.Forms.Maps.Map)
                {
                    await AppVM.GetCurrentLocation();
                    await MoveToRegionFromCenterAndRadius(AppVM.CurrentLocation, MapSearchDistanceInKM);
                    await SearchAndAddPins(((Xamarin.Forms.Maps.Map)sender).LastMoveToRegion);
                }
            });
        }

        private async Task SearchAndAddPins(MapSpan region)
        {
            var center = region.Center;
            var halfheightDegrees = region.LatitudeDegrees / 2;
            var halfwidthDegrees = region.LongitudeDegrees / 2;

            var left = center.Longitude - halfwidthDegrees;
            var right = center.Longitude + halfwidthDegrees;
            var top = center.Latitude + halfheightDegrees;
            var bottom = center.Latitude - halfheightDegrees;

            try
            {
                map.Pins.Clear();
                var client = Elmah.MVVMLightViewModels.WebApiClientFactory.CreateMapApiClient();
                var result = await client.GetMapVM(top, left, bottom, right, null);
                foreach (var mapItem in result.Result)
                {
                    var position = new Position(mapItem.SpatialLocation.Latitude, mapItem.SpatialLocation.Longitude); // Latitude, Longitude
                    var pin = new Pin
                    {
                        Type = PinType.Place,
                        Position = position,
                        Label = mapItem.Name,
                        Address = mapItem.Address
                    };
                    pin.MarkerClicked += async (s, ea) => { await DisplayAlert(mapItem.Name, mapItem.Address, "Cancel"); };
                    pin.InfoWindowClicked += async (s, ea) => { await DisplayAlert(mapItem.Name, mapItem.Address, "Cancel"); };
                    map.Pins.Add(pin);
                }
            }
            catch (Exception ex)
            {
            }
        }

        private async Task<MapSpan> MoveToRegionFromCenterAndRadius(Microsoft.Spatial.GeographyPoint currentLocation, int mapSearchDistanceInKM)
        {
            // Testing: Get Map Current Location
            //var currenPosition = new Position(47.760090, -122.205430);

            var currenPosition = new Position(currentLocation.Latitude, currentLocation.Longitude);
            return await MoveToRegionFromCenterAndRadius(currenPosition, mapSearchDistanceInKM);
        }

        private async Task<MapSpan> MoveToRegionFromCenterAndRadius(Position currenPosition, int mapSearchDistanceInKM)
        {
            return await Task.Run(() =>
            {
                MapSpan region = MapSpan.FromCenterAndRadius(currenPosition, Distance.FromKilometers(mapSearchDistanceInKM));
                map.MoveToRegion(region);
                return region;
            });
        }
    }
}

