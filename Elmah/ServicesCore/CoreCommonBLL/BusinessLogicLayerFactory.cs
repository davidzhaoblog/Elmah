/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elmah.CoreCommonBLL
{
    /// <summary>
    /// is to factory class to create instances of business layer classes
    /// </summary>
    public class BusinessLogicLayerFactory : Elmah.WcfContracts.IBusinessLogicLayerFactory
    {
        public Elmah.WcfContracts.IMapService CreateBLLInstanceOfMap(Framework.CommonBLLEntities.BusinessLogicLayerContext businessLogicLayerContext)
        {
            return new MapService(businessLogicLayerContext);
        }

        #region Entity related

        /// <summary>
        /// Creates the BLL instance of entity <see cref="ElmahModel.ELMAH_Error"/> tables for change notification.
        /// </summary>
        /// <param name="businessLogicLayerContext">The business logic layer context.</param>
        /// <returns>Instance of ELMAH_ErrorService</returns>
        public Elmah.WcfContracts.IELMAH_ErrorService CreateBLLInstanceOfEntityELMAH_Error(Framework.CommonBLLEntities.BusinessLogicLayerContext businessLogicLayerContext)
        {
            return new ELMAH_ErrorService(businessLogicLayerContext);
        }

        /// <summary>
        /// Creates the BLL instance of entity <see cref="ElmahModel.ElmahApplication"/> tables for change notification.
        /// </summary>
        /// <param name="businessLogicLayerContext">The business logic layer context.</param>
        /// <returns>Instance of ElmahApplicationService</returns>
        public Elmah.WcfContracts.IElmahApplicationService CreateBLLInstanceOfEntityElmahApplication(Framework.CommonBLLEntities.BusinessLogicLayerContext businessLogicLayerContext)
        {
            return new ElmahApplicationService(businessLogicLayerContext);
        }

        /// <summary>
        /// Creates the BLL instance of entity <see cref="ElmahModel.ElmahHost"/> tables for change notification.
        /// </summary>
        /// <param name="businessLogicLayerContext">The business logic layer context.</param>
        /// <returns>Instance of ElmahHostService</returns>
        public Elmah.WcfContracts.IElmahHostService CreateBLLInstanceOfEntityElmahHost(Framework.CommonBLLEntities.BusinessLogicLayerContext businessLogicLayerContext)
        {
            return new ElmahHostService(businessLogicLayerContext);
        }

        /// <summary>
        /// Creates the BLL instance of entity <see cref="ElmahModel.ElmahSource"/> tables for change notification.
        /// </summary>
        /// <param name="businessLogicLayerContext">The business logic layer context.</param>
        /// <returns>Instance of ElmahSourceService</returns>
        public Elmah.WcfContracts.IElmahSourceService CreateBLLInstanceOfEntityElmahSource(Framework.CommonBLLEntities.BusinessLogicLayerContext businessLogicLayerContext)
        {
            return new ElmahSourceService(businessLogicLayerContext);
        }

        /// <summary>
        /// Creates the BLL instance of entity <see cref="ElmahModel.ElmahStatusCode"/> tables for change notification.
        /// </summary>
        /// <param name="businessLogicLayerContext">The business logic layer context.</param>
        /// <returns>Instance of ElmahStatusCodeService</returns>
        public Elmah.WcfContracts.IElmahStatusCodeService CreateBLLInstanceOfEntityElmahStatusCode(Framework.CommonBLLEntities.BusinessLogicLayerContext businessLogicLayerContext)
        {
            return new ElmahStatusCodeService(businessLogicLayerContext);
        }

        /// <summary>
        /// Creates the BLL instance of entity <see cref="ElmahModel.ElmahType"/> tables for change notification.
        /// </summary>
        /// <param name="businessLogicLayerContext">The business logic layer context.</param>
        /// <returns>Instance of ElmahTypeService</returns>
        public Elmah.WcfContracts.IElmahTypeService CreateBLLInstanceOfEntityElmahType(Framework.CommonBLLEntities.BusinessLogicLayerContext businessLogicLayerContext)
        {
            return new ElmahTypeService(businessLogicLayerContext);
        }

        /// <summary>
        /// Creates the BLL instance of entity <see cref="ElmahModel.ElmahUser"/> tables for change notification.
        /// </summary>
        /// <param name="businessLogicLayerContext">The business logic layer context.</param>
        /// <returns>Instance of ElmahUserService</returns>
        public Elmah.WcfContracts.IElmahUserService CreateBLLInstanceOfEntityElmahUser(Framework.CommonBLLEntities.BusinessLogicLayerContext businessLogicLayerContext)
        {
            return new ElmahUserService(businessLogicLayerContext);
        }

        #endregion Entity related
    }

    /// <summary>
    /// Singleton of <see cref="BusinessLogicLayerFactory"/>
    /// </summary>
    public class BusinessLogicLayerFactorySingleton
        : Framework.Singleton<BusinessLogicLayerFactory>
    {
    }
}
*/

