using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Elmah.XamarinForms.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        public Elmah.XamarinForms.ViewModels.AppVM AppVM
        {
            get
            {
                return DependencyService.Resolve<Elmah.XamarinForms.ViewModels.AppVM>();
            }
        }

        public int MapSearchDistanceInKM { get; set; } = 20;

        Xamarin.Forms.Maps.Map map;
        Entry searchEntry;

        Geocoder geoCoder;

        public MapPage()
        {
            geoCoder = new Geocoder();

            map = new Xamarin.Forms.Maps.Map
            {
                //IsShowingUser = true,
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            map.MeasureInvalidated += Map_MeasureInvalidated;
            map.SizeChanged += Map_SizeChanged;
            map.PropertyChanged += Map_PropertyChanged;

            var searchBar = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
            };

            searchEntry = new Entry { HorizontalOptions = LayoutOptions.FillAndExpand };
            var searchButton = new Button { Text = "Search" };
            searchButton.Clicked += SearchButton_Clicked;

            searchBar.Children.Add(searchEntry);
            searchBar.Children.Add(searchButton);

            // put the page together
            var stack = new StackLayout { Spacing = 0 };
            stack.Children.Add(searchBar);
            stack.Children.Add(map);
            Content = stack;

            // TODO: if you want to track current GPS location
            //_TypeFullNameOfFrameworkXamarinerHelpersCrossGeolocatorHelper .locationChangedHandler += OnLocationChanged;
        }

        private async void SearchButton_Clicked(object sender, EventArgs e)
        {
            var xamarinAddress = this.searchEntry.Text;
            var approximateLocation = await geoCoder.GetPositionsForAddressAsync(xamarinAddress);
            if (approximateLocation.Any())
            {
                var region = await MoveToRegionFromCenterAndRadius(approximateLocation.First(), this.MapSearchDistanceInKM);
                await SearchAndAddPins(region);
            }
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
            if (sender is Xamarin.Forms.Maps.Map)
            {
                await AppVM.GetCurrentLocation();
                await MoveToRegionFromCenterAndRadius(AppVM.CurrentLocation, MapSearchDistanceInKM);
                await SearchAndAddPins(((Xamarin.Forms.Maps.Map)sender).LastMoveToRegion);
            }
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

        /// <summary>
        /// Example of locationChangedHandler
        /// Called when [location changed].
        /// </summary>
        public void OnLocationChanged(Microsoft.Spatial.GeographyPoint geographyPoint)
        {
            //either map or person location changes.
            //App.CurrentLocation = geographyPoint;
        }
    }
}

