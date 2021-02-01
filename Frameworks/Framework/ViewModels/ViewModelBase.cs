using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.ViewModels
{
    public class ViewModelBase<TSearchCriteria> : Framework.ViewModels.IViewModelBase<TSearchCriteria>
        where TSearchCriteria : class, new()
    {
        public ViewModelBase()
        {
            this.Criteria = new TSearchCriteria();
        }

        public TSearchCriteria Criteria { get; set; }

        public Framework.Queries.QueryPagingSetting QueryPagingSetting { get; set; }
        public string QueryOrderBySettingCollecionInString { get; set; }
        public string OriginalQueryOrderBySettingCollecionInString { get; set; }
        public List<Framework.Models.NameValuePair> ListOfQueryOrderBySettingCollecionInString { get; set; }
        public List<Framework.Models.NameValuePair> ListOfDataExport { get; set; }
        public Framework.Queries.QueryOrderBySettingCollection QueryOrderBySettingCollection { get; set; }
        public Framework.Services.BusinessLogicLayerResponseStatus StatusOfResult { get; set; }
        public string StatusMessageOfResult { get; set; }

        public Framework.ViewModels.IViewModelBase<TSearchCriteria> GetPrimaryInformationEntity()
        {
            ViewModelBase<TSearchCriteria> vm;
            vm = new ViewModelBase<TSearchCriteria>
            {
                Criteria = this.Criteria,
                ListOfQueryOrderBySettingCollecionInString = this.ListOfQueryOrderBySettingCollecionInString,
                QueryOrderBySettingCollecionInString = this.QueryOrderBySettingCollecionInString,
                OriginalQueryOrderBySettingCollecionInString = this.OriginalQueryOrderBySettingCollecionInString,
                QueryPagingSetting = this.QueryPagingSetting
            };

            return vm;
        }

        public virtual void PopulateAllUIElements(ViewModelBase<TSearchCriteria> vmFromTempData, int currentPage, bool useTempData)
        {
             var hascachedVM = vmFromTempData != null;
             this.ListOfQueryOrderBySettingCollecionInString = GetDefaultListOfQueryOrderBySettingCollecionInString();

            if (!hascachedVM)
            {
                this.Criteria = new TSearchCriteria();
                this.QueryPagingSetting = GetDefaultQueryPagingSetting();
                this.QueryPagingSetting.CurrentPage = 1;
            }
            else
            {
                if (useTempData)
                {
                    this.Criteria = vmFromTempData.Criteria;
                    this.QueryPagingSetting = vmFromTempData.QueryPagingSetting;
                }
                this.QueryPagingSetting.CurrentPage = currentPage;
            }

            // 4. QueryOrderBySettingCollecionInString
            if (string.IsNullOrWhiteSpace(this.QueryOrderBySettingCollecionInString))
            {
                if (vmFromTempData != null && !string.IsNullOrWhiteSpace(vmFromTempData.QueryOrderBySettingCollecionInString))
                // first time, pagination
                {
                    this.QueryOrderBySettingCollecionInString = vmFromTempData.QueryOrderBySettingCollecionInString;
                }
            }

            // 4.1. QueryOrderBySettingCollecion
            if (!string.IsNullOrWhiteSpace(this.QueryOrderBySettingCollecionInString))
            {
                this.QueryOrderBySettingCollection = new Framework.Queries.QueryOrderBySettingCollection(this.QueryOrderBySettingCollecionInString);
            }

            // 5. GetDefaultPerViewModel
            _ = this.GetDefaultPerViewModel();

            if (this.ListOfDataExport == null)
            {
                this.ListOfDataExport = new List<Framework.Models.NameValuePair>();
                this.ListOfDataExport.Add(new Framework.Models.NameValuePair("Csv", "Csv"));
                this.ListOfDataExport.Add(new Framework.Models.NameValuePair("Excel2010", "Excel2010"));
            }
        }

        public virtual List<Framework.Models.NameValuePair> GetDefaultListOfQueryOrderBySettingCollecionInString()
        {
            return null;
        }

        public virtual Framework.Queries.QueryPagingSetting GetDefaultQueryPagingSetting()
        {
            Framework.Queries.QueryPagingSetting queryPagingSetting = new Framework.Queries.QueryPagingSetting();
            return queryPagingSetting;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Await.Warning", "CS1998:Await.Warning")]
        public virtual async Task GetDefaultPerViewModel()
        {
        }
    }

    public class ViewModelBase<TSearchCriteria, TSearchResult> : ViewModelBase<TSearchCriteria>, Framework.ViewModels.IViewModelBase<TSearchCriteria, TSearchResult>
        where TSearchCriteria : class, new()
    {
        public ViewModelBase()
            : base()
        {
            this.StatusOfResult = Framework.Services.BusinessLogicLayerResponseStatus.UIProcessReady;
        }
        public TSearchResult Result {get;set;}
    }
}

