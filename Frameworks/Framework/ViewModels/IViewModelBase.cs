using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Framework.ViewModels
{
    public interface IViewModelBase
    {
        Framework.Queries.QueryOrderBySettingCollection QueryOrderBySettingCollection { get; set; }
        Framework.Queries.QueryPagingSetting QueryPagingSetting { get; set; }

        Framework.Queries.QueryPagingSetting GetDefaultQueryPagingSetting();

        Framework.Services.BusinessLogicLayerResponseStatus StatusOfResult { get; set; }
        string StatusMessageOfResult { get; set; }
    }

    public interface IViewModelBase<TSearchCriteria>: IViewModelBase
        where TSearchCriteria : class, new()
    {
        TSearchCriteria Criteria { get; set; }
    }

    public interface IViewModelBase<TSearchCriteria, TSearchResult> : IViewModelBase<TSearchCriteria>
     where TSearchCriteria : class, new()
    {
        TSearchResult Result { get; set; }
    }
}

