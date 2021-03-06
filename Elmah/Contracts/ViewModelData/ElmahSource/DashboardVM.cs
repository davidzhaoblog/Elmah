using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elmah.ViewModelData.ElmahSource
{
    public enum DashboardVMDataOptions
    {

        /// <summary>
        /// 4.1 ListTable-UpdateCacheWhenMasterLoaded: ELMAH_Error, FK_ELMAH_Error_ElmahSource
        /// </summary>
        ELMAH_Error,

    }

    public partial class DashboardVM
        : Framework.ViewModels.ViewModelEntityRelatedBase<Elmah.DataSourceEntities.ElmahSource, Elmah.CommonBLLEntities.ElmahSourceChainedQueryCriteriaIdentifier, DashboardVMDataOptions>
    {
        public DashboardVM()
            : this(new Elmah.CommonBLLEntities.ElmahSourceChainedQueryCriteriaIdentifier())
        { }

        public DashboardVM(Elmah.CommonBLLEntities.ElmahSourceChainedQueryCriteriaIdentifier criteriaOfMasterEntity)
            : base(criteriaOfMasterEntity)
        {
            //1. ELMAH_Error
            this.CriteriaOfELMAH_Error = new Elmah.CommonBLLEntities.ELMAH_ErrorChainedQueryCriteriaCommon();
        }

        #region 1. ELMAH_Error
        public Elmah.CommonBLLEntities.ELMAH_ErrorChainedQueryCriteriaCommon CriteriaOfELMAH_Error { get; set; }
        public Framework.Services.BusinessLogicLayerResponseStatus StatusOfELMAH_Error { get; set; }
        public string StatusMessageOfELMAH_Error { get; set; }
        public bool LoadELMAH_Error { get; set; } = true;
        public List<Elmah.DataSourceEntities.ELMAH_Error.Default> ELMAH_Error { get; set; }
        #endregion 1. ElmahModel.ELMAH_Error  as Child
    }
}

