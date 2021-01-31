// Removed OxyPlot, because it is PCL, not .Net Standard, built on .Net Framework 4.6.1. There are some problem to refresh chart.
// To install OxyPlot.Xamarin.Forms:
// Install-Package OxyPlot.Xamarin.Forms -Version 1.0.0
/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Xamarin.Forms;

namespace Elmah.MVVMLightViewModels
{
    public static class OxyPlotHelper
    {
        public static PlotModel GetPiePlotModel(string title, IEnumerable<PieSlice> pieSlices)
        {
            PlotModel _plotModel = new PlotModel
            {
                Title = title
            };

            var pieSlice = new PieSeries
            {
                StrokeThickness = 2.0,
                InsideLabelPosition = 0.8,
                AngleSpan = 360,
                StartAngle = 0
            };
            pieSlice.InsideLabelFormat = "{1} - {0}";

            foreach (var item in pieSlices)
            {
                pieSlice.Slices.Add(item);
            }
            _plotModel.Series.Add(pieSlice);
            return _plotModel;
        }

        public static PlotModel GetBarPlotModelCategoryAxis(string title, IEnumerable<Tuple<string, OxyColor, OxyColor, BarItem>> series)
        {
            PlotModel _plotModel = new PlotModel
            {
                Title = title
            };

            var xAxis = new LinearAxis
            {
                Key = "X",
                Position = AxisPosition.Bottom,
                IsZoomEnabled = false,
                IsPanEnabled = false,
            };
            _plotModel.Axes.Add(xAxis);

            var yAxis = new CategoryAxis
            {
                Key = "Y",
                Position = AxisPosition.Left,
                IsZoomEnabled = false,
                IsPanEnabled = false,
                IsAxisVisible = false,
            };

            _plotModel.Axes.Add(yAxis);

            foreach (var serie in series)
            {
                var barSeries = new BarSeries
                {
                    Title = serie.Item1,
                    FillColor = serie.Item2,
                    TextColor = serie.Item3,
                    StrokeThickness = 1,
                    LabelPlacement = LabelPlacement.Inside,
                    LabelFormatString = "{0}",
                    TrackerFormatString = "{0}: {2}",
                };
                barSeries.Items.Add(serie.Item4);
                _plotModel.Series.Add(barSeries);
            }

            return _plotModel;
        }

        public static PlotModel GetLinePlotModelDateTimeAxis(string title, IEnumerable<DateValue> data)
        {
            PlotModel _plotModel = new PlotModel
            {
                Title = title
            };

            var yAxis = new LinearAxis
            {
                Key = "Y",
                Position = AxisPosition.Left,
                IsZoomEnabled = false,
                IsPanEnabled = false,
            };
            _plotModel.Axes.Add(yAxis);

            var xAxis = new DateTimeAxis
            {
                Key = "X",
                Position = AxisPosition.Bottom,
                IsZoomEnabled = false,
                IsPanEnabled = false,
            };

            _plotModel.Axes.Add(xAxis);

            var s1 = new LineSeries
            {
                StrokeThickness = 1,
                MarkerSize = 3,
                ItemsSource = data,
                DataFieldX = "Date",
                DataFieldY = "Value",
                MarkerStroke = OxyColors.ForestGreen,
                MarkerType = MarkerType.Plus
            };
            _plotModel.Series.Add(s1);
            return _plotModel;
        }

        public static PlotModel GetColumnPlotModelCategoryAxis(string title, IEnumerable<Tuple<string, double>> data)
        {
            var _plotModel = new PlotModel { Title = title, LegendPlacement = LegendPlacement.Outside, LegendPosition = LegendPosition.RightTop, LegendOrientation = LegendOrientation.Vertical };

            // Add the axes, note that MinimumPadding and AbsoluteMinimum should be set on the value axis.
            _plotModel.Axes.Add(new CategoryAxis { ItemsSource = data, LabelField = "Item1", Angle = -60 });
            _plotModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, MinimumPadding = 0, AbsoluteMinimum = 0 });

            // Add the series, note that the the BarSeries are using the same ItemsSource as the CategoryAxis.
            _plotModel.Series.Add(new ColumnSeries { Title = title, ItemsSource = data, ValueField = "Item2" });
            return _plotModel;
        }
    }

    public class DateValue
    {
        public DateTime Date { get; set; }
        public double Value { get; set; }
    }
}
*/

