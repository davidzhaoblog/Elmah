using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elmah.EntityFrameworkDAL
{
    /// <summary>
    /// a factory class, can create all data access layer class instances for each entity.
    /// </summary>
    public class EFCoreDALFactory
        : Elmah.DALContracts.DataAccessLayerFactoryContract
    {
        internal Elmah.EntityFrameworkContext.ElmahModelEntities DbContext { get; set; }

        public EFCoreDALFactory()
        {
        }

        public EFCoreDALFactory(Elmah.EntityFrameworkContext.ElmahModelEntities dbContext)
        {
            this.DbContext = dbContext;
        }

        public Elmah.DALContracts.IMapRepository CreateDALInstanceOfMap____()
        {
            return new Elmah.EntityFrameworkDAL.MapRepository(this.DbContext);
        }

        /// <summary>
        /// method to create an data access layer class instance of <see cref="Elmah.DALContracts.IELMAH_ErrorRepository"/>
        /// </summary>
        /// <returns>a new instance of <see cref="Elmah.DALContracts.IELMAH_ErrorRepository"/>.</returns>
        public Elmah.DALContracts.IELMAH_ErrorRepository CreateDALInstanceOfELMAH_Error()
        {
            return new Elmah.EntityFrameworkDAL.ELMAH_ErrorRepository(this.DbContext);
        }

        /// <summary>
        /// method to create an data access layer class instance of <see cref="Elmah.DALContracts.IElmahApplicationRepository"/>
        /// </summary>
        /// <returns>a new instance of <see cref="Elmah.DALContracts.IElmahApplicationRepository"/>.</returns>
        public Elmah.DALContracts.IElmahApplicationRepository CreateDALInstanceOfElmahApplication()
        {
            return new Elmah.EntityFrameworkDAL.ElmahApplicationRepository(this.DbContext);
        }

        /// <summary>
        /// method to create an data access layer class instance of <see cref="Elmah.DALContracts.IElmahHostRepository"/>
        /// </summary>
        /// <returns>a new instance of <see cref="Elmah.DALContracts.IElmahHostRepository"/>.</returns>
        public Elmah.DALContracts.IElmahHostRepository CreateDALInstanceOfElmahHost()
        {
            return new Elmah.EntityFrameworkDAL.ElmahHostRepository(this.DbContext);
        }

        /// <summary>
        /// method to create an data access layer class instance of <see cref="Elmah.DALContracts.IElmahSourceRepository"/>
        /// </summary>
        /// <returns>a new instance of <see cref="Elmah.DALContracts.IElmahSourceRepository"/>.</returns>
        public Elmah.DALContracts.IElmahSourceRepository CreateDALInstanceOfElmahSource()
        {
            return new Elmah.EntityFrameworkDAL.ElmahSourceRepository(this.DbContext);
        }

        /// <summary>
        /// method to create an data access layer class instance of <see cref="Elmah.DALContracts.IElmahStatusCodeRepository"/>
        /// </summary>
        /// <returns>a new instance of <see cref="Elmah.DALContracts.IElmahStatusCodeRepository"/>.</returns>
        public Elmah.DALContracts.IElmahStatusCodeRepository CreateDALInstanceOfElmahStatusCode()
        {
            return new Elmah.EntityFrameworkDAL.ElmahStatusCodeRepository(this.DbContext);
        }

        /// <summary>
        /// method to create an data access layer class instance of <see cref="Elmah.DALContracts.IElmahTypeRepository"/>
        /// </summary>
        /// <returns>a new instance of <see cref="Elmah.DALContracts.IElmahTypeRepository"/>.</returns>
        public Elmah.DALContracts.IElmahTypeRepository CreateDALInstanceOfElmahType()
        {
            return new Elmah.EntityFrameworkDAL.ElmahTypeRepository(this.DbContext);
        }

        /// <summary>
        /// method to create an data access layer class instance of <see cref="Elmah.DALContracts.IElmahUserRepository"/>
        /// </summary>
        /// <returns>a new instance of <see cref="Elmah.DALContracts.IElmahUserRepository"/>.</returns>
        public Elmah.DALContracts.IElmahUserRepository CreateDALInstanceOfElmahUser()
        {
            return new Elmah.EntityFrameworkDAL.ElmahUserRepository(this.DbContext);
        }

    }

    /// <summary>
    /// singlton class of <see cref="EFCoreDALFactory"/>
    /// </summary>
    public class EFCoreDALFactorySingleton : Framework.Singleton<EFCoreDALFactory>
    {
    }
}

