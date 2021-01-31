using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Framework.Xaml
{
    public abstract class ViewModelWizardBase : Framework.Xaml.ViewModelBase
    {
        #region 1. Bindable

        private List<Framework.Xaml.WizardItemVM> _Items = new List<Framework.Xaml.WizardItemVM>();
        public List<Framework.Xaml.WizardItemVM> Items
        {
            get { return _Items; }
            set { Set(nameof(Items), ref _Items, value); }
        }

        private Framework.Xaml.WizardItemVM _CurrentItem;
        public Framework.Xaml.WizardItemVM CurrentItem
        {
            get { return _CurrentItem; }
            set {
                if (_CurrentItem != null && value != null && value.WizardStepItem.WizardStepIndex != _CurrentItem.WizardStepItem.WizardStepIndex)
                {
                    _CurrentItem.WizardStepItem.Active = false;
                }
                if (value != null && (_CurrentItem == null || value.WizardStepItem.WizardStepIndex != _CurrentItem.WizardStepItem.WizardStepIndex))
                {
                    value.WizardStepItem.Active = true;
                }
                if (_CurrentItem != null && value != null && value.WizardItemIndex != _CurrentItem.WizardItemIndex)
                {
                    _CurrentItem.Active = false;
                }
                if (value != null && (_CurrentItem == null || value.WizardItemIndex != _CurrentItem.WizardItemIndex))
                {
                    value.Active = true;
                }
                Set(nameof(CurrentItem), ref _CurrentItem, value);
            }
        }

        private List<Framework.Xaml.WizardStepItem> _WizardStepItems = new List<Framework.Xaml.WizardStepItem>();
        public List<Framework.Xaml.WizardStepItem> WizardStepItems
        {
            get { return _WizardStepItems; }
            set { Set(nameof(WizardStepItems), ref _WizardStepItems, value); }
        }

        private bool _Completed;
        public bool Completed
        {
            get { return _Completed; }
            set { Set(nameof(Completed), ref _Completed, value); }
        }

        private bool _HasSkipButton;
        public bool HasSkipButton
        {
            get { return _HasSkipButton; }
            set { Set(nameof(HasSkipButton), ref _HasSkipButton, value); }
        }

        public bool EnableGoBackwardButton
        {
            get
            {
                return this.Items != null && this.CurrentItem != null && this.Items.IndexOf(this.CurrentItem) != 0;
            }
        }

        public bool EnableGoForwardButton
        {
            get
            {
                return this.CurrentItem == null || this.CurrentItem.EnableGoForward == null ||
                    this.CurrentItem.EnableGoForward();
            }
        }

        protected void On_EnableGoForwardButton_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(nameof(EnableGoBackwardButton));
            RaisePropertyChanged(nameof(EnableGoForwardButton));
        }

        private bool m_HasCancelButton = false;
        public bool HasCancelButton
        {
            get { return m_HasCancelButton; }
            set
            {
                Set(nameof(HasCancelButton), ref m_HasCancelButton, value);
            }
        }

        #endregion 1. Bindable

        #region 2. Commands

        public ICommand Command_GoForward { get; set; }
        public ICommand Command_GoBackward { get; set; }
        public ICommand Command_Cancel { get; set; }
        public ICommand Command_Close { get; set; }
        public ICommand Command_Finish { get; set; }
        public void InitCommands()
        {
            if (Command_GoForward == null) Command_GoForward = new Command(Command_GoForward_Click);
            if (Command_GoBackward == null) Command_GoBackward = new Command(Command_GoBackward_Click);
            if (Command_Cancel == null) Command_Cancel = new Command(() => { Shell.Current.Navigation.PopAsync(); });
            if (Command_Close == null) Command_Close = new Command(() => { Shell.Current.Navigation.PopAsync(); });
            if (Command_Finish == null) Command_Finish = new Command(Command_Finish_Click);
        }

        public void Command_GoBackward_Click()
        {
            var currentIndex = this.Items.IndexOf(this.CurrentItem);
            this.CurrentItem = this.Items[currentIndex - 1];

            RaisePropertyChanged(nameof(EnableGoBackwardButton));
            RaisePropertyChanged(nameof(EnableGoForwardButton));
        }

        public async void Command_GoForward_Click()
        {
            if (EnableGoForwardButton)
            {
                // 1. Exec CurrentItem GoForward Action
                bool goForward = this.CurrentItem.GoForword_Clicked == null;
                if (!goForward)
                    goForward = await this.CurrentItem.GoForword_Clicked();

                if (goForward)
                {
                    // 2. Move to Next Item
                    var currentIndex = this.Items.IndexOf(this.CurrentItem);
                    this.CurrentItem = this.Items[currentIndex + 1];
                    if (this.CurrentItem.DefaultData != null)
                        this.CurrentItem.DefaultData();
                    if (this.CurrentItem.LoadData != null)
                        await this.CurrentItem.LoadData();
                }

                RaisePropertyChanged(nameof(EnableGoBackwardButton));
                RaisePropertyChanged(nameof(EnableGoForwardButton));
            }
        }

        public abstract void Command_Finish_Click();

        #endregion 2. Commands

        public ViewModelWizardBase()
        {
        }

        public void Initialize(Framework.Models.WizardData wizardData, bool useCachedWizardData)
        {
            InitCommands();

            InitializeWizardItems();

            if(Items != null && Items.Count > 0)
            {
                List<Framework.Xaml.WizardStepItem> wizardStepItems = new List<Framework.Xaml.WizardStepItem>();

                foreach (var item in Items)
                {
                    if (!wizardStepItems.Any(t => t.WizardStepIndex == item.WizardStepIndex))
                    {
                        var wizardStepItem = new Framework.Xaml.WizardStepItem { WizardStepIndex = item.WizardStepIndex, Active = false };
                        wizardStepItems.Add(wizardStepItem);
                        item.WizardStepItem = wizardStepItem;
                    }
                    else
                    {
                        var wizardStepItem = wizardStepItems.First(t => t.WizardStepIndex == item.WizardStepIndex);
                        item.WizardStepItem = wizardStepItem;
                    }
                }

                this.WizardStepItems = wizardStepItems;
            }

            this.CurrentItem = Items.FirstOrDefault();

            RaisePropertyChanged(nameof(EnableGoBackwardButton));
            RaisePropertyChanged(nameof(EnableGoForwardButton));

            if (useCachedWizardData)
            {
                // cached data if any
                this.Completed = wizardData.Completed;

                this.CurrentItem = this.Items.FirstOrDefault(t => t.Name.ToString() == wizardData.CurrentItemName);
                if (this.CurrentItem == null)
                {
                    this.CurrentItem = Items.FirstOrDefault();
                }

                if (wizardData.Items != null)
                {
                    foreach (var item in wizardData.Items)
                    {
                        var wizardItemVM = this.Items.FirstOrDefault(t => t.Name.ToString() == item.Name);
                        if (wizardItemVM != null)
                        {
                            wizardItemVM.Completed = item.Completed;
                        }
                    }
                }
            }
        }

        public abstract void InitializeWizardItems();

        public static Framework.Xaml.WizardItemVM BuildOneWizardItemVM(string wizardItemName, int wizardItemIndex, int wizardStepIndex, bool hasSkipButton = true, bool mandatory = true)
        {
            return BuildOneWizardItemVM(null, wizardItemName, wizardItemIndex, wizardStepIndex, hasSkipButton, mandatory);
        }
        public static Framework.Xaml.WizardItemVM BuildOneWizardItemVM(string heading, string wizardItemName, int wizardItemIndex, int wizardStepIndex, bool hasSkipButton = true, bool mandatory = true)
        {
            return new Framework.Xaml.WizardItemVM
            {
                Heading = heading,
                Name = wizardItemName
                                ,
                WizardItemIndex = wizardItemIndex
                                ,
                WizardStepIndex = wizardStepIndex
                                ,
                Mandatory = mandatory
                                ,
                HasFinishButton = false
                                ,
                HasSkipButton = hasSkipButton
                                ,
                HasGoForwardButton = true
                                ,
                HasGoBackwardButton = wizardItemIndex != 1
            };
        }
        public static Framework.Xaml.WizardItemVM BuildFinalWizardItemVM(string wizardItemName, int wizardItemIndex, int wizardStepIndex, bool hasSkipButton = true, bool mandatory = true)
        {
            return BuildFinalWizardItemVM(null, wizardItemName, wizardItemIndex, wizardStepIndex, hasSkipButton, mandatory);
        }

        public static Framework.Xaml.WizardItemVM BuildFinalWizardItemVM(string heading, string wizardItemName, int wizardItemIndex, int wizardStepIndex, bool hasSkipButton = true, bool mandatory = true)
        {
            return new WizardItemVM
            {
                Heading = heading,
                Name = wizardItemName
                                ,
                WizardItemIndex = wizardItemIndex
                                ,
                WizardStepIndex = wizardStepIndex
                                ,
                Mandatory = mandatory
                                ,
                HasFinishButton = true
                                ,
                HasSkipButton = hasSkipButton
                                ,
                HasGoForwardButton = false
                                ,
                HasGoBackwardButton = wizardItemIndex != 1
            };
        }
    }
}

