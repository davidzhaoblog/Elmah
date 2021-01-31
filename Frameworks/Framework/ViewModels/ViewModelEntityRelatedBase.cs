using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.ViewModels
{
    public class ViewModelEntityRelatedBase<TMasterEntity, TCriteriaOfMasterEntity, TOptions>
        : ViewModelEntityRelatedBase<TMasterEntity, TCriteriaOfMasterEntity>
        where TMasterEntity : class, new()
        where TCriteriaOfMasterEntity : class, new()
    {
        public List<ResponseStatus<TOptions>> ResponseStatuses { get; set; } = new List<ResponseStatus<TOptions>>();

        public ViewModelEntityRelatedBase(TCriteriaOfMasterEntity criteriaOfMasterEntity)
            : base(criteriaOfMasterEntity)
        {
        }
    }

    public class ViewModelEntityRelatedBase<TMasterEntity, TCriteriaOfMasterEntity> : Framework.ViewModels.IViewModelEntityRelatedBase<TMasterEntity, TCriteriaOfMasterEntity>
        where TMasterEntity : class, new()
        where TCriteriaOfMasterEntity : class, new()
    {
        public ViewModelEntityRelatedBase(TCriteriaOfMasterEntity criteriaOfMasterEntity)
        {
            this.CriteriaOfMasterEntity = criteriaOfMasterEntity;
            this.QueryPagingSetting = new Framework.Queries.QueryPagingSetting(1, 10);
            this.QueryPagingSettingOneRecord = new Framework.Queries.QueryPagingSetting(1, 1);
        }

        public TMasterEntity MasterEntity { get; set; }
        public TCriteriaOfMasterEntity CriteriaOfMasterEntity { get; set; }
        public ResponseStatus MasterEntityResponseStatus { get; set; }

        // TODO: To Be Removed
        public Framework.Services.BusinessLogicLayerResponseStatus StatusOfMasterEntity { get; set; }
        public string StatusMessageOfMasterEntity { get; set; }

        public Framework.Queries.QueryPagingSetting QueryPagingSetting { get; set; }
        public Framework.Queries.QueryPagingSetting QueryPagingSettingOneRecord { get; set; }
    }
}

