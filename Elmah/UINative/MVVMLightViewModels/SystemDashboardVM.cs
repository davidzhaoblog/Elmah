using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Microcharts;
using SkiaSharp;
using Xamarin.Forms;

// Removed OxyPlot, because it is not .Net Standard, it is PCL built on .Net Framework 4.6.1.
// To install OxyPlot.Xamarin.Forms:
// Install-Package OxyPlot.Xamarin.Forms -Version 1.0.0
//using OxyPlot;
//using OxyPlot.Series;

namespace Elmah.MVVMLightViewModels
{
    public class SystemDashboardVM : Framework.Xaml.ViewModelBase
    {
        private bool isBusy = false;
        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                isBusy = value;
                RaisePropertyChanged("IsBusy");
            }
        }

        public ICommand RefreshCommand { get; private set; }

        public SystemDashboardVM()
        {
            RefreshCommand = new Command(async () => await Refresh());
            InitLabelsAndColors();
        }

        #region MicroChart Chart Declaration

        #endregion MicroChart Chart Declaration

        #region OxyPlot PlotModel Declaration -- Commented out

        #endregion OxyPlot PlotModel Declaration -- Commented out

        public async Task Refresh()
        {
            this.IsBusy = true;
            var client = WebApiClientFactory.CreateHomeApiClient();
            await client.GetSystemDashboardVM().ContinueWith(t1 =>
            {
                var result = t1.Result;
                try
                {
                    // MicroCharts

        #region OxyPlot PlotModel Declaration -- Commented out

        #endregion OxyPlot PlotModel Declaration -- Commented out
                }
                catch //(Exception ex)
                {
                }
                this.IsBusy = false;
            });
        }

        private void InitLabelsAndColors()
        {

        }
    }
}

