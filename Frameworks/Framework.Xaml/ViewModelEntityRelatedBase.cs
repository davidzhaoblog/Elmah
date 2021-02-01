using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Framework.Xaml
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TMasterEntity"></typeparam>
    /// <typeparam name="TCriteriaOfMasterEntity"></typeparam>
    /// <typeparam name="TIdentifier"></typeparam>
    /// <typeparam name="TOptions">from xyz.ViewModels.{table name}.DashboardDataOptions</typeparam>
    public abstract class ViewModelEntityRelatedBase<TMasterEntity, TCriteriaOfMasterEntity, TIdentifier, TOptions>
        : ViewModelEntityRelatedBase<TMasterEntity, TCriteriaOfMasterEntity, TIdentifier>
        where TMasterEntity : class, new()
        where TCriteriaOfMasterEntity : class, new()
        where TOptions: Enum
    {
        private List<Framework.ViewModels.ResponseStatus<TOptions>> m_ResponseStatuses;
        public List<Framework.ViewModels.ResponseStatus<TOptions>> ResponseStatuses
        {
            get { return m_ResponseStatuses; }
            set
            {
                Set(nameof(ResponseStatuses), ref m_ResponseStatuses, value);
            }
        }

        private List<Framework.Xaml.DashboardVMSetting<TOptions>> m_DashboardVMSettings;
        public List<Framework.Xaml.DashboardVMSetting<TOptions>> DashboardVMSettings
        {
            get { return m_DashboardVMSettings; }
            set
            {
                Set(nameof(DashboardVMSettings), ref m_DashboardVMSettings, value);
            }
        }

        private Framework.Xaml.DashboardVMUsageOptions m_DashboardVMUsageOption;
        public Framework.Xaml.DashboardVMUsageOptions DashboardVMUsageOption
        {
            get { return m_DashboardVMUsageOption; }
            set
            {
                Set(nameof(DashboardVMUsageOption), ref m_DashboardVMUsageOption, value);
            }
        }

        private Framework.Xaml.IViewModelTabBase m_TabSetting;
        public Framework.Xaml.IViewModelTabBase TabSetting
        {
            get { return m_TabSetting; }
            set
            {
                Set(nameof(TabSetting), ref m_TabSetting, value);
            }
        }

        private ObservableCollection<Framework.Xaml.ActionForm.ActionItemModel> m_SettingTabActionItems;
        public ObservableCollection<Framework.Xaml.ActionForm.ActionItemModel> SettingTabActionItems
        {
            get { return m_SettingTabActionItems; }
            set
            {
                Set(nameof(SettingTabActionItems), ref m_SettingTabActionItems, value);
            }
        }

        public ViewModelEntityRelatedBase()
            : base()
        {
            DashboardVMSettings = DefaultDashboardVMSettings();
        }

        protected override async Task Launch(TIdentifier o)
        {
            PopupVM.ShowPopup(Framework.Resx.UIStringResource.Loading, false);

            CriteriaOfMasterEntity = GetCriteria(o);
            Initialize_SettingTabActionItems(o);

            var options = this.DashboardVMSettings.Where(t => t.CachingOption == Framework.Xaml.CachingOptions.NoCaching || t.CachingOption == Framework.Xaml.CachingOptions.UpdateCacheWhenMasterLoaded).Select(t => t.Option);
            await DoSearch(options.ToArray());

            PopupVM.HidePopup();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Await.Warning", "CS1998:Await.Warning")]
        protected virtual async Task DoSearch(TOptions[] options = null) { }

        protected virtual List<Framework.Xaml.DashboardVMSetting<TOptions>> DefaultDashboardVMSettings()
        {
            throw new NotImplementedException("DefaultDashboardVMSettings");
        }

        protected bool IsDataLoadSuccess(TOptions option)
        {
            return ResponseStatuses != null && ResponseStatuses.Any(t => t.Option.Equals(option));
        }

        public abstract void Initialize_SettingTabActionItems(TIdentifier identifier);
    }

    public abstract class ViewModelEntityRelatedBase<TMasterEntity, TCriteriaOfMasterEntity, TIdentifier>
        : ViewModelBase, Framework.ViewModels.IViewModelEntityRelatedBase<TMasterEntity, TCriteriaOfMasterEntity>
        where TMasterEntity : class, new()
        where TCriteriaOfMasterEntity : class, new()
    {
        #region properties

        public TCriteriaOfMasterEntity m_CriteriaOfMasterEntity;
        public TCriteriaOfMasterEntity CriteriaOfMasterEntity
        {
            get
            {
                return this.m_CriteriaOfMasterEntity;
            }
            set
            {
                if (this.m_CriteriaOfMasterEntity != value)
                {
                    this.m_CriteriaOfMasterEntity = value;
                    RaisePropertyChanged("CriteriaOfMasterEntity");
                }
            }
        }
        public TMasterEntity m_MasterEntity;
        public TMasterEntity MasterEntity
        {
            get
            {
                return this.m_MasterEntity;
            }
            set
            {
                if (this.m_MasterEntity != value)
                {
                    this.m_MasterEntity = value;
                    RaisePropertyChanged("MasterEntity");
                }
            }
        }

        private Framework.ViewModels.ResponseStatus m_MasterEntityResponseStatus;
        public Framework.ViewModels.ResponseStatus MasterEntityResponseStatus
        {
            get { return m_MasterEntityResponseStatus; }
            set
            {
                Set(nameof(MasterEntityResponseStatus), ref m_MasterEntityResponseStatus, value);
            }
        }

        public Framework.Queries.QueryPagingSetting m_QueryPagingSetting;
        public Framework.Queries.QueryPagingSetting QueryPagingSetting
        {
            get
            {
                return this.m_QueryPagingSetting;
            }
            set
            {
                if (this.m_QueryPagingSetting != value)
                {
                    this.m_QueryPagingSetting = value;
                    RaisePropertyChanged("QueryPagingSetting");
                }
            }
        }
        public Framework.Queries.QueryPagingSetting m_QueryPagingSettingOneRecord;
        public Framework.Queries.QueryPagingSetting QueryPagingSettingOneRecord
        {
            get
            {
                return this.m_QueryPagingSettingOneRecord;
            }
            set
            {
                if (this.m_QueryPagingSettingOneRecord != value)
                {
                    this.m_QueryPagingSettingOneRecord = value;
                    RaisePropertyChanged("QueryPagingSettingOneRecord");
                }
            }
        }
        public string m_StatusMessageOfMasterEntity;
        public string StatusMessageOfMasterEntity
        {
            get
            {
                return this.m_StatusMessageOfMasterEntity;
            }
            set
            {
                if (this.m_StatusMessageOfMasterEntity != value)
                {
                    this.m_StatusMessageOfMasterEntity = value;
                    RaisePropertyChanged("StatusMessageOfMasterEntity");
                }
            }
        }
        public Framework.Services.BusinessLogicLayerResponseStatus m_StatusOfMasterEntity;
        public Framework.Services.BusinessLogicLayerResponseStatus StatusOfMasterEntity
        {
            get
            {
                return this.m_StatusOfMasterEntity;
            }
            set
            {
                if (this.m_StatusOfMasterEntity != value)
                {
                    this.m_StatusOfMasterEntity = value;
                    RaisePropertyChanged("StatusOfMasterEntity");
                }
            }
        }

        public Framework.ViewModels.SearchStatus m_SearchStatus;
        public Framework.ViewModels.SearchStatus SearchStatus
        {
            get
            {
                return this.m_SearchStatus;
            }
            set
            {
                if (this.m_SearchStatus != value)
                {
                    this.m_SearchStatus = value;
                    RaisePropertyChanged("SearchStatus");
                }
            }
        }

        #endregion properties

        public ViewModelEntityRelatedBase()
            : base()
        {
            this.m_CriteriaOfMasterEntity =new TCriteriaOfMasterEntity();

            //this.LaunchViewCommand = new Command<TIdentifier>(Launch, CanLaunch);
            this.RefreshViewCommand = new Command<TIdentifier>(Refresh, CanRefresh);
            this.CloseViewCommand = new Command(Close, CanClose);
        }

        //public ICommand LaunchViewCommand { get; protected set; }
        protected virtual async Task Launch(TIdentifier o)
        {
            //Framework.ViewModels.UIAction uiAction = Framework.ViewModels.UIAction.ViewDetails;
            this.CriteriaOfMasterEntity = GetCriteria(o);

            await DoSearch();
        }

        protected virtual bool CanLaunch(TIdentifier o)
        {
            return true;
        }

        public ICommand RefreshViewCommand { get; protected set; }
        protected async void Refresh(TIdentifier o)
        {
            this.CriteriaOfMasterEntity = GetCriteria(o);
            await DoSearch();
        }
        protected virtual bool CanRefresh(TIdentifier o)
        {
            return true;
        }

        public ICommand CloseViewCommand { get; protected set; }
        protected void Close()
        {
            //Framework.ViewModels.UIAction uiAction = Framework.ViewModels.UIAction.Close;

            //if(this.NavigateByRouteKey != null)
            //    this.NavigateByRouteKey.NavigateClose(EntityName, viewName);
        }
        protected virtual bool CanClose()
        {
            return true;
        }

        protected virtual TCriteriaOfMasterEntity GetCriteria(TIdentifier o) { return default(TCriteriaOfMasterEntity); }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Await.Warning", "CS1998:Await.Warning")]
        protected virtual async Task DoSearch() { }

        public override void Cleanup()
        {
            base.Cleanup();
            this.m_MasterEntity = new TMasterEntity();
            this.m_CriteriaOfMasterEntity = new TCriteriaOfMasterEntity();
        }
    }
}

