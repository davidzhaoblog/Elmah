using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Elmah.MVVMLightViewModels
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </para>
    /// </summary>
    public class MainViewModel : Framework.Xaml.ViewModelBase
    {
        public bool IsBusy { get; set; }

        public string Welcome
        {
            get
            {
                return Framework.Resx.UIStringResource.Common_WelcomeToNTIERONTIME;
            }
        }

        public ICommand GoHomeCommand { get; private set; }
        public void GoHome()
        {
            //Messenger.Default.Send<Framework.ViewModels.UIActionStatusMessage>(new Framework.ViewModels.UIActionStatusMessage(null, null, Framework.ViewModels.UIAction.Home, Framework.ViewModels.UIActionStatus.Launch));
        }

        public ICommand  GoMapCommand { get; private set; }
        public void GoMap()
        {
        }

        public ICommand  GoSystemDashboardCommand { get; private set; }
        public void GoSystemDashboard()
        {
        }

        public ICommand GoBackCommand { get; private set; }
        public void GoBack()
        {
        }

        public List<Framework.Models.NameValuePair> PreDefinedDateTimeRangeList { get; }
        public List<Framework.Models.NameValuePair> PredefinedBooleanSelectedValueList { get; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            // 2. Command
            this.GoHomeCommand = new Command(GoHome);
            this.GoMapCommand = new Command(GoMap);
            this.GoSystemDashboardCommand = new Command(GoSystemDashboard);
            this.GoBackCommand = new Command(GoBack);

            // 3. PreDefinedDateTimeRangeList and PredefinedBooleanSelectedValueList

            this.PreDefinedDateTimeRangeList = Framework.Queries.QuerySystemDateTimeRangeCriteria.GetList(null);
            this.PredefinedBooleanSelectedValueList = Framework.Queries.QuerySystemBooleanEqualsCriteria.GetList(null);

            this.IsBusy = false;
        }

        //public override void Cleanup()
        //{
        //    // Clean up if needed
        //
        //    base.Cleanup();
        //}
    }
}

