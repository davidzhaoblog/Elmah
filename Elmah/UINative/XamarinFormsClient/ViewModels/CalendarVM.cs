using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamForms.Controls;

namespace Elmah.XamarinForms.ViewModels
{
    /// <summary>
    /// Features:
    /// 1. Initial: Display today/this week/this month at header
    /// 2. Click header can choose date/week/month
    /// 3. Swipe left/right header/body can change date/week/month, left -1, right + 1
    /// 4. Display when daily/weekly/month
    /// 5.1. Add
    /// 5.2. Delelte
    /// 5.3. Update
    /// </summary>
    public class CalendarVM : Framework.Xaml.ViewModelBase
    {
        #region 1. CalendarType, properties and command

        private Framework.Queries.CalendarTypes _CalendarType;
        public Framework.Queries.CalendarTypes CalendarType
        {
            get { return _CalendarType; }
            set
            {
                Set(nameof(CalendarType), ref _CalendarType, value);
                RaisePropertyChanged(nameof(DisplayDaily));
                RaisePropertyChanged(nameof(DisplayWeekly));
                RaisePropertyChanged(nameof(DisplayMonthly));
                RaisePropertyChanged(nameof(DisplayAnnually));
            }
        }

        public bool DisplayDaily
        {
            get { return this._CalendarType == Framework.Queries.CalendarTypes.Daily; }
        }

        public bool DisplayWeekly
        {
            get { return this._CalendarType == Framework.Queries.CalendarTypes.Weekly; }
        }

        public bool DisplayMonthly
        {
            get { return this._CalendarType == Framework.Queries.CalendarTypes.Monthly; }
        }

        public bool DisplayAnnually
        {
            get { return this._CalendarType == Framework.Queries.CalendarTypes.Annually; }
        }
        public ICommand Command_ChangeCalendarType { get; protected set; }

        #endregion 1. CalendarType, properties and command

        #region 2. Date / Date Range

        /// <summary>
        /// Daily: this day
        /// Weekly: this week
        /// Monthly: this month
        /// </summary>

        private DateTime? _date;
        public DateTime? Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                this._SelectedDate = _date;
                RaisePropertyChanged(nameof(Date));
                RaisePropertyChanged(nameof(SelectedDate));
                this._WeeklySunday = value.Value.AddDays(-(int)value.Value.DayOfWeek);
                this._WeeklySaturday = this.WeeklySunday.Value.AddDays(6);
                RaisePropertyChanged(nameof(WeeklySunday));
                RaisePropertyChanged(nameof(WeeklySaturday));
                CalculateActualStartDateTimeRange();
            }
        }

        private DateTime? _SelectedDate;
        public DateTime? SelectedDate
        {
            get { return _SelectedDate; }
            set { Set(nameof(SelectedDate), ref _SelectedDate, value); }
        }

        private DateTime? _ActualStartDateTimeRangeLowerBound;
        public DateTime? ActualStartDateTimeRangeLowerBound
        {
            get { return _ActualStartDateTimeRangeLowerBound; }
            set { Set(nameof(ActualStartDateTimeRangeLowerBound), ref _ActualStartDateTimeRangeLowerBound, value); }
        }

        private DateTime? _ActualStartDateTimeRangeUpperBound;
        public DateTime? ActualStartDateTimeRangeUpperBound
        {
            get { return _ActualStartDateTimeRangeUpperBound; }
            set { Set(nameof(ActualStartDateTimeRangeUpperBound), ref _ActualStartDateTimeRangeUpperBound, value); }
        }

        public ICommand Command_IncrementalChangeDate { get; protected set; }

        public ICommand Command_MonthDayTap { get; protected set; }

        #endregion 2. Date / Date Range

        #region 3. Weekly DatePicker

        private DateTime? _WeeklySunday;
        public DateTime? WeeklySunday
        {
            get { return _WeeklySunday; }
            set { Set(nameof(WeeklySunday), ref _WeeklySunday, value); this._WeeklySaturday = value.Value.AddDays(6); RaisePropertyChanged(nameof(WeeklySaturday)); this._date = value; RaisePropertyChanged(nameof(Date)); this._SelectedDate = _date; RaisePropertyChanged(nameof(SelectedDate)); }
        }

        private DateTime? _WeeklySaturday;
        public DateTime? WeeklySaturday
        {
            get { return _WeeklySaturday; }
            set { Set(nameof(WeeklySaturday), ref _WeeklySaturday, value); this._WeeklySunday = value.Value.AddDays(-6); RaisePropertyChanged(nameof(WeeklySunday)); this._date = value; RaisePropertyChanged(nameof(Date)); this._SelectedDate = _date; RaisePropertyChanged(nameof(SelectedDate)); }
        }

        #endregion 3. Weekly DatePicker

        #region  4. Command_Toggle_Calendar_List, change icon not working

        // Xamarin.forms 4.1. can't change ToolBarItem icon. https://stackoverflow.com/questions/57449234/cannot-change-toolbar-icon-in-xamarin-forms-v4-1

        //private bool _IsList = false;
        //public bool IsList
        //{
        //    get { return _IsList; }
        //    set { Set(nameof(IsList), ref _IsList, value); }
        //}

        //private string _Icon_Toggle_Calendar_List;
        //public string Icon_Toggle_Calendar_List
        //{
        //    get { return _Icon_Toggle_Calendar_List; }
        //    set { Set(nameof(Icon_Toggle_Calendar_List), ref _Icon_Toggle_Calendar_List, value); }
        //}

        //public ICommand Command_Toggle_Calendar_List { get; protected set; }

        #endregion  4. Command_Toggle_Calendar_List

        #region 5. Binding Data/CalendarItem list

        public Framework.Services.BusinessLogicLayerResponseStatus StatusOfResult { get; set; }
        public string StatusMessageOfResult { get; set; }
        public Framework.Queries.QueryPagingSetting QueryPagingSetting { get; set; }
        //public ObservableCollection<NTierOnTime.DataSourceEntities.CalendarItem> Result { get; set; } = new ObservableCollection<DataSourceEntities.CalendarItem>();

        #endregion 5. Binding Data

        #region original properties

        public ICommand DateChosen
        {
            get
            {
                return new Command((obj) => {
                    System.Diagnostics.Debug.WriteLine(obj as DateTime?);
                });
            }
        }

        private ObservableCollection<XamForms.Controls.SpecialDate> attendances;
        public ObservableCollection<XamForms.Controls.SpecialDate> Attendances
        {
            get { return attendances; }
            set { attendances = value; RaisePropertyChanged(nameof(Attendances));}
        }

        #endregion original properties

        public CalendarVM()
        {
            // 1. Choose CalendarType
            CalendarType = Framework.Queries.CalendarTypes.Daily;
            this.Command_ChangeCalendarType = new Command<string>(
                execute: (t) =>
                {
                    Framework.Queries.CalendarTypes parsed = Framework.Queries.CalendarTypes.Daily;
                    if (Enum.TryParse<Framework.Queries.CalendarTypes>(t, out parsed))
                    {
                        CalendarType = parsed;
                        //if (this.CalendarType == Framework.Queries.CalendarTypes.Weekly)
                        //{
                        //    PopulateWeeklyMonthDayLabelBinding();
                        //}
                    }
                });

            // 2. Initial
            this.Date = DateTime.Now.Date;
            if (this.CalendarType == Framework.Queries.CalendarTypes.Daily)
            {
                this.SelectedDate = this.Date;
            }

            this.Command_IncrementalChangeDate = new Command(
            execute: (t) =>
            {
                int offset = 0;
                if (t.ToString().ToLower() == "left")
                {
                    offset = 1;
                }
                else if (t.ToString().ToLower() == "right")
                {
                    offset = -1;
                }

                if (offset != 0)
                {
                    if (!this.Date.HasValue)
                    {
                        this._date = DateTime.Now.Date;
                    }

                    if (this.CalendarType == Framework.Queries.CalendarTypes.Daily)
                    {
                        this.Date = this.Date.Value.AddDays(offset);
                    }
                    else if (this.CalendarType == Framework.Queries.CalendarTypes.Weekly)
                    {
                        this.Date = this.Date.Value.AddDays(offset * 7);
                    }
                    else if (this.CalendarType == Framework.Queries.CalendarTypes.Monthly)
                    {
                        this.Date = this.Date.Value.AddMonths(offset);
                    }
                    else if (this.CalendarType == Framework.Queries.CalendarTypes.Annually)
                    {
                        this.Date = this.Date.Value.AddYears(offset);
                    }
                }
            });

            this.Command_MonthDayTap = new Command(
                execute: (t) =>
                {
                    if (t != null)
                    {
                        try
                        {
                            var index = int.Parse(t.ToString());

                            if (this.Date.HasValue)
                            {
                                var firstDayOfThisMonth = new DateTime(this.Date.Value.Year, this.Date.Value.Month, 1);
                                var firstSunday = firstDayOfThisMonth.AddDays(-(int)this.Date.Value.DayOfWeek);
                                this.SelectedDate = firstSunday.AddDays(index);
                            }
                        }
                        catch //(Exception ex)
                        {
                        }
                    }
                });

            //this.Command_Toggle_Calendar_List = new Command(
            //    execute: (t) =>
            //    {
            //        this.IsList = !this.IsList;

            //        this.Icon_Toggle_Calendar_List = this.IsList ? "ic_date_range.png" : "ic_list.png";
            //    });

        }

        public async Task LoadData()
        {
            try
            {
                /*
                var vmData = new NTierOnTime.ViewModelData.CalendarItem.IndexVM();
                vmData.Criteria = new CommonBLLEntities.CalendarItemChainedQueryCriteriaCommon();
                vmData.Criteria.CanQueryWhenNoQuery = true;
                vmData.Criteria.Common.ActualStartDateTimeRange.NullableLowerBound = this.ActualStartDateTimeRangeLowerBound;
                vmData.Criteria.Common.ActualStartDateTimeRange.NullableUpperBound = this.ActualStartDateTimeRangeUpperBound;
                //vmData.Criteria.Common.IsFullDay.NullableValueToCompare = true;
                vmData.QueryPagingSetting = new Framework.Queries.QueryPagingSetting(-1, -1);
                vmData.QueryOrderBySettingCollection = new Framework.Queries.QueryOrderBySettingCollection("ActualStartDateTime~ASC");

                var client = new NTierOnTime.WebApiClient.CalendarItemApiClient(Elmah.MVVMLightViewModels.WebServiceConfig.WebApiRootUrl, Elmah.MVVMLightViewModels.WebServiceConfig.UseToken, Elmah.MVVMLightViewModels.WebServiceConfig.Token);
                var result = await client.GetIndexVMAsync(vmData);
                Device.BeginInvokeOnMainThread(() =>
                {
                    this.StatusOfResult = result.StatusOfResult;
                    if (result.StatusOfResult == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
                    {
                        if (this.Result == null)
                        {
                            this.Result = new ObservableCollection<NTierOnTime.DataSourceEntities.CalendarItem>();
                        }

                        foreach (var item in result.Result)
                        {
                            this.Result.Add(item);
                        }

                        this.QueryPagingSetting = result.QueryPagingSetting;

                    }
                    else
                    {
                        this.StatusMessageOfResult = result.StatusMessageOfResult;
                    }
                });
                */
            }
            catch //(Exception ex)
            {
            }
            //// TODO: #1: Holiday, e.g.  not selectable, Selectable = false by default
            //this.Attendances = new ObservableCollection<SpecialDate>();
            //this.Attendances.Add(new SpecialDate(DateTime.Now.AddDays(5)) { BackgroundColor = Color.Fuchsia, TextColor = Color.Accent, BorderColor = Color.Maroon, BorderWidth = 8 });
            //this.Attendances.Add(new SpecialDate(DateTime.Now.AddDays(6)) { BackgroundColor = Color.Fuchsia, TextColor = Color.Accent, BorderColor = Color.Maroon, BorderWidth = 8 });

            //// TODO: #2: Available date, can add appointment, Selectable = true
            //this.Attendances.Add(new SpecialDate(DateTime.Now.AddDays(7)) { BackgroundColor = Color.YellowGreen, TextColor = Color.Black, BorderColor = Color.Maroon, BorderWidth = 0, Selectable = true });

            //// TODO: #3: Not available date, can't add appointment, Selectable = false by default
            //var specialDate = new SpecialDate(DateTime.Now.AddDays(-1));
            //specialDate.BackgroundColor = Color.Green;
            //specialDate.TextColor = Color.White;

            //this.Attendances.Add(specialDate);

            //// #4: Show today
            //this.Date = DateTime.Now.Date;
        }

        public void CalculateActualStartDateTimeRange()
        {
            if(this.Date.HasValue)
            {
                if (this.CalendarType == Framework.Queries.CalendarTypes.Daily)
                {
                    this.ActualStartDateTimeRangeLowerBound = this.Date.Value.Date;
                    this.ActualStartDateTimeRangeUpperBound = this.ActualStartDateTimeRangeLowerBound.Value.AddDays(1);
                }
                else if (this.CalendarType == Framework.Queries.CalendarTypes.Weekly)
                {
                    this.ActualStartDateTimeRangeLowerBound = this.Date.Value.AddDays(-(int)this.Date.Value.DayOfWeek).Date; // Sunday is first day
                    this.ActualStartDateTimeRangeUpperBound = this.ActualStartDateTimeRangeLowerBound.Value.AddDays(7);
                }
                else if (this.CalendarType == Framework.Queries.CalendarTypes.Monthly)
                {
                    var firstDayOfThisMonth = new DateTime(this.Date.Value.Year, this.Date.Value.Month, 1);
                    this.ActualStartDateTimeRangeLowerBound = firstDayOfThisMonth.AddDays(-(int)firstDayOfThisMonth.DayOfWeek);
                    this.ActualStartDateTimeRangeUpperBound = this.ActualStartDateTimeRangeLowerBound.Value.AddDays(42); // 6 weeks
                }
                else if (this.CalendarType == Framework.Queries.CalendarTypes.Annually)
                {
                    this.ActualStartDateTimeRangeLowerBound = new DateTime(this.Date.Value.Year, 1, 1, 0, 0, 0);
                    this.ActualStartDateTimeRangeUpperBound = this.ActualStartDateTimeRangeLowerBound.Value.AddYears(1);
                }
            }
        }
    }
}

