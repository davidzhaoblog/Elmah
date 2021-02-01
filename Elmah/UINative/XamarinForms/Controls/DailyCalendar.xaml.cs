using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Elmah.XamarinForms.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DailyCalendar : ContentView
    {
        #region BindablePropert List

        public static readonly BindableProperty SelectedDateProperty =
            BindableProperty.Create(nameof(SelectedDate), typeof(DateTime), typeof(DailyCalendar), null, BindingMode.OneWay
                , propertyChanged: (sender, oldValue, newValue) => {
                    if (newValue != null)
                    {
                        var newValueInt = (DateTime)newValue;
                        var control = (DailyCalendar)sender;
                        control.WeekDayLabel.Text = newValueInt.ToString("ddd");
                        control.MonthDayLabel.Text = newValueInt.ToString("MMM dd");
                    }
                });

        public DateTime SelectedDate
        {
            get { return (DateTime)GetValue(SelectedDateProperty); }
            set { SetValue(SelectedDateProperty, value); }
        }

        public static readonly BindableProperty OfficeHourColorProperty =
            BindableProperty.Create(nameof(OfficeHourColor), typeof(Xamarin.Forms.Color), typeof(DailyCalendar), null, BindingMode.OneWay
                , propertyChanged: (sender, oldValue, newValue) => {
                    var control = (DailyCalendar)sender;
                    control.Calendar.BackgroundColor = (Color)newValue;
                });

        public Xamarin.Forms.Color OfficeHourColor
        {
            get { return (Xamarin.Forms.Color)GetValue(OfficeHourColorProperty); }
            set { SetValue(OfficeHourColorProperty, value); }
        }

        public static readonly BindableProperty NonOfficeHourMorningStartProperty =
                    BindableProperty.Create(nameof(NonOfficeHourMorningStart), typeof(double), typeof(DailyCalendar), null, BindingMode.OneWay
                        , propertyChanged: (sender, oldValue, newValue) => {
                            double newValueInt = (double)newValue;
                            var control = (DailyCalendar)sender;
                            if (newValueInt < control.NonOfficeHourMorningEnd)
                            {
                                control.NonOfficeHourMorningBoxView.Margin = new Thickness(50, control.HourHeight * newValueInt, 0, 0);
                                control.NonOfficeHourMorningBoxView.HeightRequest = (control.NonOfficeHourMorningEnd - newValueInt) * control.HourHeight;
                            }
                        });

        public double NonOfficeHourMorningStart
        {
            get { return (double)GetValue(NonOfficeHourMorningStartProperty); }
            set { SetValue(NonOfficeHourMorningStartProperty, value); }
        }

        public static readonly BindableProperty NonOfficeHourMorningEndProperty =
            BindableProperty.Create(nameof(NonOfficeHourMorningEnd), typeof(double), typeof(DailyCalendar), null, BindingMode.OneWay
                , propertyChanged: (sender, oldValue, newValue) => {
                    double newValueInt = (double)newValue;
                    var control = (DailyCalendar)sender;
                    if (control.NonOfficeHourMorningStart < newValueInt)
                    {
                        control.NonOfficeHourMorningBoxView.Margin = new Thickness(50, control.HourHeight * control.NonOfficeHourMorningStart, 0, 0);
                        control.NonOfficeHourMorningBoxView.HeightRequest = (newValueInt - control.NonOfficeHourMorningStart) * control.HourHeight;
                    }
                });

        public double NonOfficeHourMorningEnd
        {
            get { return (double)GetValue(NonOfficeHourMorningEndProperty); }
            set { SetValue(NonOfficeHourMorningEndProperty, value); }
        }

        public static readonly BindableProperty NonOfficeHourAfternoonStartProperty =
            BindableProperty.Create(nameof(NonOfficeHourAfternoonStart), typeof(double), typeof(DailyCalendar), null, BindingMode.OneWay
                , propertyChanged: (sender, oldValue, newValue) => {
                    double newValueInt = (double)newValue;
                    var control = (DailyCalendar)sender;
                    if (newValueInt < control.NonOfficeHourAfternoonEnd)
                    {
                        control.NonOfficeHourAfternoonBoxView.Margin = new Thickness(50, control.HourHeight * newValueInt, 0, 0);
                        control.NonOfficeHourAfternoonBoxView.HeightRequest = (control.NonOfficeHourAfternoonEnd - newValueInt) * control.HourHeight;
                    }
                });

        public double NonOfficeHourAfternoonStart
        {
            get { return (double)GetValue(NonOfficeHourAfternoonStartProperty); }
            set { SetValue(NonOfficeHourAfternoonStartProperty, value); }
        }

        public static readonly BindableProperty NonOfficeHourAfternoonEndProperty =
            BindableProperty.Create(nameof(NonOfficeHourAfternoonEnd), typeof(double), typeof(DailyCalendar), null, BindingMode.OneWay
                , propertyChanged: (sender, oldValue, newValue) => {
                    double newValueInt = (double)newValue;
                    var control = (DailyCalendar)sender;
                    if (control.NonOfficeHourAfternoonStart < newValueInt)
                    {
                        control.NonOfficeHourAfternoonBoxView.Margin = new Thickness(50, control.HourHeight * control.NonOfficeHourAfternoonStart, 0, 0);
                        control.NonOfficeHourAfternoonBoxView.HeightRequest = (newValueInt - control.NonOfficeHourAfternoonStart) * control.HourHeight;
                    }
                });

        public double NonOfficeHourAfternoonEnd
        {
            get { return (double)GetValue(NonOfficeHourAfternoonEndProperty); }
            set { SetValue(NonOfficeHourAfternoonEndProperty, value); }
        }

        public static readonly BindableProperty NonOfficeHourColorProperty =
            BindableProperty.Create(nameof(NonOfficeHourColor), typeof(Xamarin.Forms.Color), typeof(DailyCalendar), null, BindingMode.OneWay
                , propertyChanged: (sender, oldValue, newValue) => {
                    var control = (DailyCalendar)sender;
                    control.NonOfficeHourMorningBoxView.BackgroundColor = (Color)newValue;
                    control.NonOfficeHourAfternoonBoxView.BackgroundColor = (Color)newValue;
                });

        public Xamarin.Forms.Color NonOfficeHourColor
        {
            get { return (Xamarin.Forms.Color)GetValue(NonOfficeHourColorProperty); }
            set { SetValue(NonOfficeHourColorProperty, value); }
        }

        public static readonly BindableProperty LunchHourStartProperty =
            BindableProperty.Create(nameof(LunchHourStart), typeof(double), typeof(DailyCalendar), null, BindingMode.OneWay
                , propertyChanged: (sender, oldValue, newValue) => {
                    double newValueInt = (double)newValue;
                    var control = (DailyCalendar)sender;
                    if (newValueInt < control.LunchHourEnd)
                    {
                        control.LunchHourBoxView.Margin = new Thickness(50, control.HourHeight * newValueInt, 0, 0);
                        control.LunchHourBoxView.HeightRequest = (control.LunchHourEnd - newValueInt) * control.HourHeight;
                    }
                });

        public double LunchHourStart
        {
            get { return (double)GetValue(LunchHourStartProperty); }
            set { SetValue(LunchHourStartProperty, value); }
        }

        public static readonly BindableProperty LunchHourEndProperty =
            BindableProperty.Create(nameof(LunchHourEnd), typeof(double), typeof(DailyCalendar), null, BindingMode.OneWay
                , propertyChanged: (sender, oldValue, newValue) => {
                    double newValueInt = (double)newValue;
                    var control = (DailyCalendar)sender;
                    if (control.LunchHourStart < newValueInt)
                    {
                        control.LunchHourBoxView.Margin = new Thickness(50, control.HourHeight * control.LunchHourStart, 0, 0);
                        control.LunchHourBoxView.HeightRequest = (newValueInt - control.LunchHourStart) * control.HourHeight;
                    }
                });

        public double LunchHourEnd
        {
            get { return (double)GetValue(LunchHourEndProperty); }
            set { SetValue(LunchHourEndProperty, value); }
        }

        public static readonly BindableProperty LunchHourColorProperty =
            BindableProperty.Create(nameof(LunchHourColor), typeof(Xamarin.Forms.Color), typeof(DailyCalendar), null, BindingMode.OneWay
                , propertyChanged: (sender, oldValue, newValue) => {
                    var control = (DailyCalendar)sender;
                    control.LunchHourBoxView.BackgroundColor = (Color)newValue;
                });

        public Xamarin.Forms.Color LunchHourColor
        {
            get { return (Xamarin.Forms.Color)GetValue(LunchHourColorProperty); }
            set { SetValue(LunchHourColorProperty, value); }
        }

        public static readonly BindableProperty DatesProperty =
            BindableProperty.Create(nameof(Dates), typeof(DateTime?), typeof(DailyCalendar), null, BindingMode.TwoWay);

        public DateTime? Dates
        {
            get { return (DateTime?)GetValue(DatesProperty); }
            set { SetValue(DatesProperty, value); }
        }

        public static readonly BindableProperty HourHeightProperty =
            BindableProperty.Create(nameof(HourHeight), typeof(byte), typeof(DailyCalendar), null, BindingMode.OneWay
                , propertyChanged: (sender, oldValue, newValue) => {
                    double newValueInt = (byte)newValue;
                    var control = (DailyCalendar)sender;
                    if (control.LunchHourStart < control.LunchHourEnd)
                    {
                        control.NonOfficeHourMorningBoxView.Margin = new Thickness(50, control.HourHeight * control.NonOfficeHourMorningStart, 0, 0);
                        control.NonOfficeHourMorningBoxView.HeightRequest = (control.NonOfficeHourMorningEnd - control.NonOfficeHourMorningStart) * newValueInt;
                        control.NonOfficeHourAfternoonBoxView.Margin = new Thickness(50, control.HourHeight * control.NonOfficeHourAfternoonStart, 0, 0);
                        control.NonOfficeHourAfternoonBoxView.HeightRequest = (control.NonOfficeHourAfternoonEnd - control.NonOfficeHourAfternoonStart) * newValueInt;
                        control.LunchHourBoxView.Margin = new Thickness(50, control.HourHeight * control.LunchHourStart, 0, 0);
                        control.LunchHourBoxView.HeightRequest = (control.LunchHourEnd - control.LunchHourStart) * newValueInt;
                    }
                });

        public byte HourHeight
        {
            get { return (byte)GetValue(HourHeightProperty); }
            set { SetValue(HourHeightProperty, value); }
        }

        public static readonly BindableProperty ShowQuarterGridProperty =
            BindableProperty.Create(nameof(ShowQuarterGrid), typeof(bool), typeof(DailyCalendar), null, BindingMode.TwoWay);

        public bool ShowQuarterGrid
        {
            get { return (bool)GetValue(ShowQuarterGridProperty); }
            set { SetValue(ShowQuarterGridProperty, value); }
        }

        #endregion BindablePropert List

        // Grayout Areas: e.g. Office hours, 3, morning(0-8am), afternoon(20,23), optional: luch(12-13),
        //
        public DailyCalendar()
        {
            InitializeComponent();
            HourHeight = 120;
        }

        public async Task ScrollToHourAsync(int hour)
        {
            await Task.Delay(300);
            Device.BeginInvokeOnMainThread(async () =>
            {
                //Calendar.SetScrolledPosition(0, hour * 120);
                await Calendar.ScrollToAsync(0, hour * HourHeight, false);
                //await Calendar.ScrollToAsync(this.StartMarker, ScrollToPosition.Start, false);
            });
        }
    }
}

