using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Elmah.MVVMLightViewModels.ELMAH_Error
{
    public class ExtendedVM: Framework.Xaml.ViewModelBase
    {
        public ListsHelper ListsHelper
        {
            get
            {
                return DependencyService.Resolve<ListsHelper>();
            }
        }

        public ExtendedVM()
            : base()
        {

            // 1.2. ElmahApplication GetDropDownContentsOfElmahApplicationSelectionChanged and its command
            GetDropDownContentsOfElmahApplicationCommand = new Command(async() => { await this.GetDropDownContentsOfElmahApplication(); });
            GetDropDownContentsOfElmahApplicationCommand.Execute(null);

            // 2.2. ElmahHost GetDropDownContentsOfElmahHostSelectionChanged and its command
            GetDropDownContentsOfElmahHostCommand = new Command(async() => { await this.GetDropDownContentsOfElmahHost(); });
            GetDropDownContentsOfElmahHostCommand.Execute(null);

            // 3.2. ElmahSource GetDropDownContentsOfElmahSourceSelectionChanged and its command
            GetDropDownContentsOfElmahSourceCommand = new Command(async() => { await this.GetDropDownContentsOfElmahSource(); });
            GetDropDownContentsOfElmahSourceCommand.Execute(null);

            // 4.2. ElmahStatusCode GetDropDownContentsOfElmahStatusCodeSelectionChanged and its command
            GetDropDownContentsOfElmahStatusCodeCommand = new Command(async() => { await this.GetDropDownContentsOfElmahStatusCode(); });
            GetDropDownContentsOfElmahStatusCodeCommand.Execute(null);

            // 5.2. ElmahType GetDropDownContentsOfElmahTypeSelectionChanged and its command
            GetDropDownContentsOfElmahTypeCommand = new Command(async() => { await this.GetDropDownContentsOfElmahType(); });
            GetDropDownContentsOfElmahTypeCommand.Execute(null);

            // 6.2. ElmahUser GetDropDownContentsOfElmahUserSelectionChanged and its command
            GetDropDownContentsOfElmahUserCommand = new Command(async() => { await this.GetDropDownContentsOfElmahUser(); });
            GetDropDownContentsOfElmahUserCommand.Execute(null);

            // 1.1. ElmahApplication LaunchElmahApplicationDetailsView and its command
            //this.LaunchElmahApplicationDetailsViewCommand = new Command<Elmah.DataSourceEntities.ELMAH_Error.Default>(async t => { await this.LaunchElmahApplicationDetailsView(t); });

            // 2.1. ElmahHost LaunchElmahHostDetailsView and its command
            //this.LaunchElmahHostDetailsViewCommand = new Command<Elmah.DataSourceEntities.ELMAH_Error.Default>(async t => { await this.LaunchElmahHostDetailsView(t); });

            // 3.1. ElmahSource LaunchElmahSourceDetailsView and its command
            //this.LaunchElmahSourceDetailsViewCommand = new Command<Elmah.DataSourceEntities.ELMAH_Error.Default>(async t => { await this.LaunchElmahSourceDetailsView(t); });

            // 4.1. ElmahStatusCode LaunchElmahStatusCodeDetailsView and its command
            //this.LaunchElmahStatusCodeDetailsViewCommand = new Command<Elmah.DataSourceEntities.ELMAH_Error.Default>(async t => { await this.LaunchElmahStatusCodeDetailsView(t); });

            // 5.1. ElmahType LaunchElmahTypeDetailsView and its command
            //this.LaunchElmahTypeDetailsViewCommand = new Command<Elmah.DataSourceEntities.ELMAH_Error.Default>(async t => { await this.LaunchElmahTypeDetailsView(t); });

            // 6.1. ElmahUser LaunchElmahUserDetailsView and its command
            //this.LaunchElmahUserDetailsViewCommand = new Command<Elmah.DataSourceEntities.ELMAH_Error.Default>(async t => { await this.LaunchElmahUserDetailsView(t); });

        }

        #region 1.1. ElmahApplication LaunchElmahApplicationDetailsView and its command, Commented out
        /*
        public ICommand<Elmah.DataSourceEntities.ELMAH_Error.Default> LaunchElmahApplicationDetailsViewCommand { get; protected set; }
        protected async Task LaunchElmahApplicationDetailsView(Elmah.DataSourceEntities.ELMAH_Error.Default item)
        {
            var itemViewModel = DependencyService.Resolve<ElmahApplication.ItemVM>();
            await itemViewModel.LoadItem(new Elmah.DataSourceEntities.ElmahApplicationIdentifier{ Application = item.Application});
            itemViewModel.LaunchDetailsViewCommand.Execute(itemViewModel.Item);
        }
        */
        #endregion 1. ElmahApplication

        #region 2.1. ElmahHost LaunchElmahHostDetailsView and its command, Commented out
        /*
        public ICommand<Elmah.DataSourceEntities.ELMAH_Error.Default> LaunchElmahHostDetailsViewCommand { get; protected set; }
        protected async Task LaunchElmahHostDetailsView(Elmah.DataSourceEntities.ELMAH_Error.Default item)
        {
            var itemViewModel = DependencyService.Resolve<ElmahHost.ItemVM>();
            await itemViewModel.LoadItem(new Elmah.DataSourceEntities.ElmahHostIdentifier{ Host = item.Host});
            itemViewModel.LaunchDetailsViewCommand.Execute(itemViewModel.Item);
        }
        */
        #endregion 2. ElmahHost

        #region 3.1. ElmahSource LaunchElmahSourceDetailsView and its command, Commented out
        /*
        public ICommand<Elmah.DataSourceEntities.ELMAH_Error.Default> LaunchElmahSourceDetailsViewCommand { get; protected set; }
        protected async Task LaunchElmahSourceDetailsView(Elmah.DataSourceEntities.ELMAH_Error.Default item)
        {
            var itemViewModel = DependencyService.Resolve<ElmahSource.ItemVM>();
            await itemViewModel.LoadItem(new Elmah.DataSourceEntities.ElmahSourceIdentifier{ Source = item.Source});
            itemViewModel.LaunchDetailsViewCommand.Execute(itemViewModel.Item);
        }
        */
        #endregion 3. ElmahSource

        #region 4.1. ElmahStatusCode LaunchElmahStatusCodeDetailsView and its command, Commented out
        /*
        public ICommand<Elmah.DataSourceEntities.ELMAH_Error.Default> LaunchElmahStatusCodeDetailsViewCommand { get; protected set; }
        protected async Task LaunchElmahStatusCodeDetailsView(Elmah.DataSourceEntities.ELMAH_Error.Default item)
        {
            var itemViewModel = DependencyService.Resolve<ElmahStatusCode.ItemVM>();
            await itemViewModel.LoadItem(new Elmah.DataSourceEntities.ElmahStatusCodeIdentifier{ StatusCode = item.StatusCode});
            itemViewModel.LaunchDetailsViewCommand.Execute(itemViewModel.Item);
        }
        */
        #endregion 4. ElmahStatusCode

        #region 5.1. ElmahType LaunchElmahTypeDetailsView and its command, Commented out
        /*
        public ICommand<Elmah.DataSourceEntities.ELMAH_Error.Default> LaunchElmahTypeDetailsViewCommand { get; protected set; }
        protected async Task LaunchElmahTypeDetailsView(Elmah.DataSourceEntities.ELMAH_Error.Default item)
        {
            var itemViewModel = DependencyService.Resolve<ElmahType.ItemVM>();
            await itemViewModel.LoadItem(new Elmah.DataSourceEntities.ElmahTypeIdentifier{ Type = item.Type});
            itemViewModel.LaunchDetailsViewCommand.Execute(itemViewModel.Item);
        }
        */
        #endregion 5. ElmahType

        #region 6.1. ElmahUser LaunchElmahUserDetailsView and its command, Commented out
        /*
        public ICommand<Elmah.DataSourceEntities.ELMAH_Error.Default> LaunchElmahUserDetailsViewCommand { get; protected set; }
        protected async Task LaunchElmahUserDetailsView(Elmah.DataSourceEntities.ELMAH_Error.Default item)
        {
            var itemViewModel = DependencyService.Resolve<ElmahUser.ItemVM>();
            await itemViewModel.LoadItem(new Elmah.DataSourceEntities.ElmahUserIdentifier{ User = item.User});
            itemViewModel.LaunchDetailsViewCommand.Execute(itemViewModel.Item);
        }
        */
        #endregion 6. ElmahUser

        #region 1. ElmahApplication

        // 1.2. ElmahApplication DropDownContentsOfElmahApplication and its commands

        public Framework.Models.NameValuePair<string> m_DropDownContentsOfElmahApplicationSelectedItem;
        public Framework.Models.NameValuePair<string> DropDownContentsOfElmahApplicationSelectedItem
        {
            get
            {
                return this.m_DropDownContentsOfElmahApplicationSelectedItem;
            }
            set
            {
                Set(nameof(DropDownContentsOfElmahApplicationSelectedItem), ref this.m_DropDownContentsOfElmahApplicationSelectedItem, value);
            }
        }

        public ObservableCollection<Framework.Models.NameValuePair<string>> m_DropDownContentsOfElmahApplication = new ObservableCollection<Framework.Models.NameValuePair<string>>();
        public ObservableCollection<Framework.Models.NameValuePair<string>> DropDownContentsOfElmahApplication
        {
            get
            {
                return this.m_DropDownContentsOfElmahApplication;
            }
            set
            {
                Set(nameof(DropDownContentsOfElmahApplication), ref this.m_DropDownContentsOfElmahApplication, value);
            }
        }

        public ICommand GetDropDownContentsOfElmahApplicationSelectionChangedCommand { get; private set; }

        public ICommand GetDropDownContentsOfElmahApplicationCommand { get; private set; }
        public async Task GetDropDownContentsOfElmahApplication()
        {
            try
            {
                var client = WebApiClientFactory.CreateListsApiClient();
                var result = await client.ElmahApplicationList(currentIndex : -1, pageSize : -1, queryOrderByExpression : null);

                var originalDropDownContentsOfElmahApplicationSelectedItem = this.m_DropDownContentsOfElmahApplicationSelectedItem;
                var dropDownContentsOfElmahApplication = new List<Framework.Models.NameValuePair<string>>();

                if (result != null)
                {
                    foreach (var item in result)
                    {
                        if(item != null)
                        {
                            string value = item.ParseToSystemString(null);
                            dropDownContentsOfElmahApplication.Add(new Framework.Models.NameValuePair<string>(value, item.Name));
                        }
                    }
                    this.DropDownContentsOfElmahApplication = new ObservableCollection<Framework.Models.NameValuePair<string>>(dropDownContentsOfElmahApplication);
                    DropDownContentsOfElmahApplicationSelectedItem = originalDropDownContentsOfElmahApplicationSelectedItem;
                }
            }
            catch //(Exception ex)
            {
            }
        }

        #endregion 1. ElmahApplication

        #region 2. ElmahHost

        // 2.2. ElmahHost DropDownContentsOfElmahHost and its commands

        public Framework.Models.NameValuePair<string> m_DropDownContentsOfElmahHostSelectedItem;
        public Framework.Models.NameValuePair<string> DropDownContentsOfElmahHostSelectedItem
        {
            get
            {
                return this.m_DropDownContentsOfElmahHostSelectedItem;
            }
            set
            {
                Set(nameof(DropDownContentsOfElmahHostSelectedItem), ref this.m_DropDownContentsOfElmahHostSelectedItem, value);
            }
        }

        public ObservableCollection<Framework.Models.NameValuePair<string>> m_DropDownContentsOfElmahHost = new ObservableCollection<Framework.Models.NameValuePair<string>>();
        public ObservableCollection<Framework.Models.NameValuePair<string>> DropDownContentsOfElmahHost
        {
            get
            {
                return this.m_DropDownContentsOfElmahHost;
            }
            set
            {
                Set(nameof(DropDownContentsOfElmahHost), ref this.m_DropDownContentsOfElmahHost, value);
            }
        }

        public ICommand GetDropDownContentsOfElmahHostSelectionChangedCommand { get; private set; }

        public ICommand GetDropDownContentsOfElmahHostCommand { get; private set; }
        public async Task GetDropDownContentsOfElmahHost()
        {
            try
            {
                var client = WebApiClientFactory.CreateListsApiClient();
                var result = await client.ElmahHostList(currentIndex : -1, pageSize : -1, queryOrderByExpression : null);

                var originalDropDownContentsOfElmahHostSelectedItem = this.m_DropDownContentsOfElmahHostSelectedItem;
                var dropDownContentsOfElmahHost = new List<Framework.Models.NameValuePair<string>>();

                if (result != null)
                {
                    foreach (var item in result)
                    {
                        if(item != null)
                        {
                            string value = item.ParseToSystemString(null);
                            dropDownContentsOfElmahHost.Add(new Framework.Models.NameValuePair<string>(value, item.Name));
                        }
                    }
                    this.DropDownContentsOfElmahHost = new ObservableCollection<Framework.Models.NameValuePair<string>>(dropDownContentsOfElmahHost);
                    DropDownContentsOfElmahHostSelectedItem = originalDropDownContentsOfElmahHostSelectedItem;
                }
            }
            catch //(Exception ex)
            {
            }
        }

        #endregion 2. ElmahHost

        #region 3. ElmahSource

        // 3.2. ElmahSource DropDownContentsOfElmahSource and its commands

        public Framework.Models.NameValuePair<string> m_DropDownContentsOfElmahSourceSelectedItem;
        public Framework.Models.NameValuePair<string> DropDownContentsOfElmahSourceSelectedItem
        {
            get
            {
                return this.m_DropDownContentsOfElmahSourceSelectedItem;
            }
            set
            {
                Set(nameof(DropDownContentsOfElmahSourceSelectedItem), ref this.m_DropDownContentsOfElmahSourceSelectedItem, value);
            }
        }

        public ObservableCollection<Framework.Models.NameValuePair<string>> m_DropDownContentsOfElmahSource = new ObservableCollection<Framework.Models.NameValuePair<string>>();
        public ObservableCollection<Framework.Models.NameValuePair<string>> DropDownContentsOfElmahSource
        {
            get
            {
                return this.m_DropDownContentsOfElmahSource;
            }
            set
            {
                Set(nameof(DropDownContentsOfElmahSource), ref this.m_DropDownContentsOfElmahSource, value);
            }
        }

        public ICommand GetDropDownContentsOfElmahSourceSelectionChangedCommand { get; private set; }

        public ICommand GetDropDownContentsOfElmahSourceCommand { get; private set; }
        public async Task GetDropDownContentsOfElmahSource()
        {
            try
            {
                var client = WebApiClientFactory.CreateListsApiClient();
                var result = await client.ElmahSourceList(currentIndex : -1, pageSize : -1, queryOrderByExpression : null);

                var originalDropDownContentsOfElmahSourceSelectedItem = this.m_DropDownContentsOfElmahSourceSelectedItem;
                var dropDownContentsOfElmahSource = new List<Framework.Models.NameValuePair<string>>();

                if (result != null)
                {
                    foreach (var item in result)
                    {
                        if(item != null)
                        {
                            string value = item.ParseToSystemString(null);
                            dropDownContentsOfElmahSource.Add(new Framework.Models.NameValuePair<string>(value, item.Name));
                        }
                    }
                    this.DropDownContentsOfElmahSource = new ObservableCollection<Framework.Models.NameValuePair<string>>(dropDownContentsOfElmahSource);
                    DropDownContentsOfElmahSourceSelectedItem = originalDropDownContentsOfElmahSourceSelectedItem;
                }
            }
            catch //(Exception ex)
            {
            }
        }

        #endregion 3. ElmahSource

        #region 4. ElmahStatusCode

        // 4.2. ElmahStatusCode DropDownContentsOfElmahStatusCode and its commands

        public Framework.Models.NameValuePair<int> m_DropDownContentsOfElmahStatusCodeSelectedItem;
        public Framework.Models.NameValuePair<int> DropDownContentsOfElmahStatusCodeSelectedItem
        {
            get
            {
                return this.m_DropDownContentsOfElmahStatusCodeSelectedItem;
            }
            set
            {
                Set(nameof(DropDownContentsOfElmahStatusCodeSelectedItem), ref this.m_DropDownContentsOfElmahStatusCodeSelectedItem, value);
            }
        }

        public ObservableCollection<Framework.Models.NameValuePair<int>> m_DropDownContentsOfElmahStatusCode = new ObservableCollection<Framework.Models.NameValuePair<int>>();
        public ObservableCollection<Framework.Models.NameValuePair<int>> DropDownContentsOfElmahStatusCode
        {
            get
            {
                return this.m_DropDownContentsOfElmahStatusCode;
            }
            set
            {
                Set(nameof(DropDownContentsOfElmahStatusCode), ref this.m_DropDownContentsOfElmahStatusCode, value);
            }
        }

        public ICommand GetDropDownContentsOfElmahStatusCodeSelectionChangedCommand { get; private set; }

        public ICommand GetDropDownContentsOfElmahStatusCodeCommand { get; private set; }
        public async Task GetDropDownContentsOfElmahStatusCode()
        {
            try
            {
                var client = WebApiClientFactory.CreateListsApiClient();
                var result = await client.ElmahStatusCodeList(currentIndex : -1, pageSize : -1, queryOrderByExpression : null);

                var originalDropDownContentsOfElmahStatusCodeSelectedItem = this.m_DropDownContentsOfElmahStatusCodeSelectedItem;
                var dropDownContentsOfElmahStatusCode = new List<Framework.Models.NameValuePair<int>>();

                if (result != null)
                {
                    foreach (var item in result)
                    {
                        if(item != null)
                        {
                            int value = item.ParseToSystemInt32(null);
                            dropDownContentsOfElmahStatusCode.Add(new Framework.Models.NameValuePair<int>(value, item.Name));
                        }
                    }
                    this.DropDownContentsOfElmahStatusCode = new ObservableCollection<Framework.Models.NameValuePair<int>>(dropDownContentsOfElmahStatusCode);
                    DropDownContentsOfElmahStatusCodeSelectedItem = originalDropDownContentsOfElmahStatusCodeSelectedItem;
                }
            }
            catch //(Exception ex)
            {
            }
        }

        #endregion 4. ElmahStatusCode

        #region 5. ElmahType

        // 5.2. ElmahType DropDownContentsOfElmahType and its commands

        public Framework.Models.NameValuePair<string> m_DropDownContentsOfElmahTypeSelectedItem;
        public Framework.Models.NameValuePair<string> DropDownContentsOfElmahTypeSelectedItem
        {
            get
            {
                return this.m_DropDownContentsOfElmahTypeSelectedItem;
            }
            set
            {
                Set(nameof(DropDownContentsOfElmahTypeSelectedItem), ref this.m_DropDownContentsOfElmahTypeSelectedItem, value);
            }
        }

        public ObservableCollection<Framework.Models.NameValuePair<string>> m_DropDownContentsOfElmahType = new ObservableCollection<Framework.Models.NameValuePair<string>>();
        public ObservableCollection<Framework.Models.NameValuePair<string>> DropDownContentsOfElmahType
        {
            get
            {
                return this.m_DropDownContentsOfElmahType;
            }
            set
            {
                Set(nameof(DropDownContentsOfElmahType), ref this.m_DropDownContentsOfElmahType, value);
            }
        }

        public ICommand GetDropDownContentsOfElmahTypeSelectionChangedCommand { get; private set; }

        public ICommand GetDropDownContentsOfElmahTypeCommand { get; private set; }
        public async Task GetDropDownContentsOfElmahType()
        {
            try
            {
                var client = WebApiClientFactory.CreateListsApiClient();
                var result = await client.ElmahTypeList(currentIndex : -1, pageSize : -1, queryOrderByExpression : null);

                var originalDropDownContentsOfElmahTypeSelectedItem = this.m_DropDownContentsOfElmahTypeSelectedItem;
                var dropDownContentsOfElmahType = new List<Framework.Models.NameValuePair<string>>();

                if (result != null)
                {
                    foreach (var item in result)
                    {
                        if(item != null)
                        {
                            string value = item.ParseToSystemString(null);
                            dropDownContentsOfElmahType.Add(new Framework.Models.NameValuePair<string>(value, item.Name));
                        }
                    }
                    this.DropDownContentsOfElmahType = new ObservableCollection<Framework.Models.NameValuePair<string>>(dropDownContentsOfElmahType);
                    DropDownContentsOfElmahTypeSelectedItem = originalDropDownContentsOfElmahTypeSelectedItem;
                }
            }
            catch //(Exception ex)
            {
            }
        }

        #endregion 5. ElmahType

        #region 6. ElmahUser

        // 6.2. ElmahUser DropDownContentsOfElmahUser and its commands

        public Framework.Models.NameValuePair<string> m_DropDownContentsOfElmahUserSelectedItem;
        public Framework.Models.NameValuePair<string> DropDownContentsOfElmahUserSelectedItem
        {
            get
            {
                return this.m_DropDownContentsOfElmahUserSelectedItem;
            }
            set
            {
                Set(nameof(DropDownContentsOfElmahUserSelectedItem), ref this.m_DropDownContentsOfElmahUserSelectedItem, value);
            }
        }

        public ObservableCollection<Framework.Models.NameValuePair<string>> m_DropDownContentsOfElmahUser = new ObservableCollection<Framework.Models.NameValuePair<string>>();
        public ObservableCollection<Framework.Models.NameValuePair<string>> DropDownContentsOfElmahUser
        {
            get
            {
                return this.m_DropDownContentsOfElmahUser;
            }
            set
            {
                Set(nameof(DropDownContentsOfElmahUser), ref this.m_DropDownContentsOfElmahUser, value);
            }
        }

        public ICommand GetDropDownContentsOfElmahUserSelectionChangedCommand { get; private set; }

        public ICommand GetDropDownContentsOfElmahUserCommand { get; private set; }
        public async Task GetDropDownContentsOfElmahUser()
        {
            try
            {
                var client = WebApiClientFactory.CreateListsApiClient();
                var result = await client.ElmahUserList(currentIndex : -1, pageSize : -1, queryOrderByExpression : null);

                var originalDropDownContentsOfElmahUserSelectedItem = this.m_DropDownContentsOfElmahUserSelectedItem;
                var dropDownContentsOfElmahUser = new List<Framework.Models.NameValuePair<string>>();

                if (result != null)
                {
                    foreach (var item in result)
                    {
                        if(item != null)
                        {
                            string value = item.ParseToSystemString(null);
                            dropDownContentsOfElmahUser.Add(new Framework.Models.NameValuePair<string>(value, item.Name));
                        }
                    }
                    this.DropDownContentsOfElmahUser = new ObservableCollection<Framework.Models.NameValuePair<string>>(dropDownContentsOfElmahUser);
                    DropDownContentsOfElmahUserSelectedItem = originalDropDownContentsOfElmahUserSelectedItem;
                }
            }
            catch //(Exception ex)
            {
            }
        }

        #endregion 6. ElmahUser

/*
        public override void Cleanup()
        {
            base.Cleanup();
        }
*/
    }
}

