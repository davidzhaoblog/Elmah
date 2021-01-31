using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elmah.ViewModelData.ELMAH_Error
{
    public enum DashboardVMDataOptions
    {

        /// <summary>
        /// 2.1 AncestorTable-UpdateCacheWhenViewLoaded: ElmahApplication, \Application
        /// </summary>
        ElmahApplication,

        /// <summary>
        /// 2.2 AncestorTable-UpdateCacheWhenViewLoaded: ElmahHost, \Host
        /// </summary>
        ElmahHost,

        /// <summary>
        /// 2.3 AncestorTable-UpdateCacheWhenViewLoaded: ElmahSource, \Source
        /// </summary>
        ElmahSource,

        /// <summary>
        /// 2.4 AncestorTable-UpdateCacheWhenViewLoaded: ElmahStatusCode, \StatusCode
        /// </summary>
        ElmahStatusCode,

        /// <summary>
        /// 2.5 AncestorTable-UpdateCacheWhenViewLoaded: ElmahType, \Type
        /// </summary>
        ElmahType,

        /// <summary>
        /// 2.6 AncestorTable-UpdateCacheWhenViewLoaded: ElmahUser, \User
        /// </summary>
        ElmahUser,

    }

    public partial class DashboardVM
        : Framework.ViewModels.ViewModelEntityRelatedBase<Elmah.DataSourceEntities.ELMAH_Error.Default, Elmah.CommonBLLEntities.ELMAH_ErrorChainedQueryCriteriaIdentifier, DashboardVMDataOptions>
    {
        public DashboardVM()
            : this(new Elmah.CommonBLLEntities.ELMAH_ErrorChainedQueryCriteriaIdentifier())
        { }

        public DashboardVM(Elmah.CommonBLLEntities.ELMAH_ErrorChainedQueryCriteriaIdentifier criteriaOfMasterEntity)
            : base(criteriaOfMasterEntity)
        {
            //2. ElmahApplication
            this.CriteriaOfElmahApplication = new Elmah.CommonBLLEntities.ElmahApplicationChainedQueryCriteriaIdentifier();
            //3. ElmahHost
            this.CriteriaOfElmahHost = new Elmah.CommonBLLEntities.ElmahHostChainedQueryCriteriaIdentifier();
            //4. ElmahSource
            this.CriteriaOfElmahSource = new Elmah.CommonBLLEntities.ElmahSourceChainedQueryCriteriaIdentifier();
            //5. ElmahStatusCode
            this.CriteriaOfElmahStatusCode = new Elmah.CommonBLLEntities.ElmahStatusCodeChainedQueryCriteriaIdentifier();
            //6. ElmahType
            this.CriteriaOfElmahType = new Elmah.CommonBLLEntities.ElmahTypeChainedQueryCriteriaIdentifier();
            //7. ElmahUser
            this.CriteriaOfElmahUser = new Elmah.CommonBLLEntities.ElmahUserChainedQueryCriteriaIdentifier();
        }

        #region 2. ElmahApplication
        public Elmah.CommonBLLEntities.ElmahApplicationChainedQueryCriteriaIdentifier CriteriaOfElmahApplication { get; set; }
        public Framework.Services.BusinessLogicLayerResponseStatus StatusOfElmahApplication { get; set; }
        public string StatusMessageOfElmahApplication { get; set; }
        public bool LoadElmahApplication { get; set; } = true;
        public Elmah.DataSourceEntities.ElmahApplication ElmahApplication { get; set; }
        #endregion 2. ElmahApplication
        #region 3. ElmahHost
        public Elmah.CommonBLLEntities.ElmahHostChainedQueryCriteriaIdentifier CriteriaOfElmahHost { get; set; }
        public Framework.Services.BusinessLogicLayerResponseStatus StatusOfElmahHost { get; set; }
        public string StatusMessageOfElmahHost { get; set; }
        public bool LoadElmahHost { get; set; } = true;
        public Elmah.DataSourceEntities.ElmahHost ElmahHost { get; set; }
        #endregion 3. ElmahHost
        #region 4. ElmahSource
        public Elmah.CommonBLLEntities.ElmahSourceChainedQueryCriteriaIdentifier CriteriaOfElmahSource { get; set; }
        public Framework.Services.BusinessLogicLayerResponseStatus StatusOfElmahSource { get; set; }
        public string StatusMessageOfElmahSource { get; set; }
        public bool LoadElmahSource { get; set; } = true;
        public Elmah.DataSourceEntities.ElmahSource ElmahSource { get; set; }
        #endregion 4. ElmahSource
        #region 5. ElmahStatusCode
        public Elmah.CommonBLLEntities.ElmahStatusCodeChainedQueryCriteriaIdentifier CriteriaOfElmahStatusCode { get; set; }
        public Framework.Services.BusinessLogicLayerResponseStatus StatusOfElmahStatusCode { get; set; }
        public string StatusMessageOfElmahStatusCode { get; set; }
        public bool LoadElmahStatusCode { get; set; } = true;
        public Elmah.DataSourceEntities.ElmahStatusCode ElmahStatusCode { get; set; }
        #endregion 5. ElmahStatusCode
        #region 6. ElmahType
        public Elmah.CommonBLLEntities.ElmahTypeChainedQueryCriteriaIdentifier CriteriaOfElmahType { get; set; }
        public Framework.Services.BusinessLogicLayerResponseStatus StatusOfElmahType { get; set; }
        public string StatusMessageOfElmahType { get; set; }
        public bool LoadElmahType { get; set; } = true;
        public Elmah.DataSourceEntities.ElmahType ElmahType { get; set; }
        #endregion 6. ElmahType
        #region 7. ElmahUser
        public Elmah.CommonBLLEntities.ElmahUserChainedQueryCriteriaIdentifier CriteriaOfElmahUser { get; set; }
        public Framework.Services.BusinessLogicLayerResponseStatus StatusOfElmahUser { get; set; }
        public string StatusMessageOfElmahUser { get; set; }
        public bool LoadElmahUser { get; set; } = true;
        public Elmah.DataSourceEntities.ElmahUser ElmahUser { get; set; }
        #endregion 7. ElmahUser
    }
}

