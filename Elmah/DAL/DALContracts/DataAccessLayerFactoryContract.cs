using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;

namespace Elmah.DALContracts
{
    /// <summary>
    /// There is one method for each entity class, which creates data access layer instance.
    /// </summary>
    public partial interface DataAccessLayerFactoryContract
//        <

//        TDALContractOfELMAH_Error, TCollectionOfELMAH_Error, TOfELMAH_Error, TIdentifierOfELMAH_Error

//        , TDALContractOfElmahApplication, TCollectionOfElmahApplication, TOfElmahApplication, TIdentifierOfElmahApplication

//        , TDALContractOfElmahHost, TCollectionOfElmahHost, TOfElmahHost, TIdentifierOfElmahHost

//        , TDALContractOfElmahSource, TCollectionOfElmahSource, TOfElmahSource, TIdentifierOfElmahSource

//        , TDALContractOfElmahStatusCode, TCollectionOfElmahStatusCode, TOfElmahStatusCode, TIdentifierOfElmahStatusCode

//        , TDALContractOfElmahType, TCollectionOfElmahType, TOfElmahType, TIdentifierOfElmahType

//        , TDALContractOfElmahUser, TCollectionOfElmahUser, TOfElmahUser, TIdentifierOfElmahUser

//        >

//            where TDALContractOfELMAH_Error : IELMAH_ErrorRepository<TCollectionOfELMAH_Error, TOfELMAH_Error, TIdentifierOfELMAH_Error>
//            where TCollectionOfELMAH_Error : IList<TOfELMAH_Error>, new()
//            where TOfELMAH_Error : Elmah.EntityContracts.IELMAH_Error, new()
//            where TIdentifierOfELMAH_Error : Elmah.EntityContracts.IELMAH_ErrorIdentifier, new()

//            where TDALContractOfElmahApplication : IElmahApplicationRepository<TCollectionOfElmahApplication, TOfElmahApplication, TIdentifierOfElmahApplication>
//            where TCollectionOfElmahApplication : IList<TOfElmahApplication>, new()
//            where TOfElmahApplication : Elmah.EntityContracts.IElmahApplication, new()
//            where TIdentifierOfElmahApplication : Elmah.EntityContracts.IElmahApplicationIdentifier, new()

//            where TDALContractOfElmahHost : IElmahHostRepository<TCollectionOfElmahHost, TOfElmahHost, TIdentifierOfElmahHost>
//            where TCollectionOfElmahHost : IList<TOfElmahHost>, new()
//            where TOfElmahHost : Elmah.EntityContracts.IElmahHost, new()
//            where TIdentifierOfElmahHost : Elmah.EntityContracts.IElmahHostIdentifier, new()

//            where TDALContractOfElmahSource : IElmahSourceRepository<TCollectionOfElmahSource, TOfElmahSource, TIdentifierOfElmahSource>
//            where TCollectionOfElmahSource : IList<TOfElmahSource>, new()
//            where TOfElmahSource : Elmah.EntityContracts.IElmahSource, new()
//            where TIdentifierOfElmahSource : Elmah.EntityContracts.IElmahSourceIdentifier, new()

//            where TDALContractOfElmahStatusCode : IElmahStatusCodeRepository<TCollectionOfElmahStatusCode, TOfElmahStatusCode, TIdentifierOfElmahStatusCode>
//            where TCollectionOfElmahStatusCode : IList<TOfElmahStatusCode>, new()
//            where TOfElmahStatusCode : Elmah.EntityContracts.IElmahStatusCode, new()
//            where TIdentifierOfElmahStatusCode : Elmah.EntityContracts.IElmahStatusCodeIdentifier, new()

//            where TDALContractOfElmahType : IElmahTypeRepository<TCollectionOfElmahType, TOfElmahType, TIdentifierOfElmahType>
//            where TCollectionOfElmahType : IList<TOfElmahType>, new()
//            where TOfElmahType : Elmah.EntityContracts.IElmahType, new()
//            where TIdentifierOfElmahType : Elmah.EntityContracts.IElmahTypeIdentifier, new()

//            where TDALContractOfElmahUser : IElmahUserRepository<TCollectionOfElmahUser, TOfElmahUser, TIdentifierOfElmahUser>
//            where TCollectionOfElmahUser : IList<TOfElmahUser>, new()
//            where TOfElmahUser : Elmah.EntityContracts.IElmahUser, new()
//            where TIdentifierOfElmahUser : Elmah.EntityContracts.IElmahUserIdentifier, new()

    {
        Elmah.DALContracts.IMapRepository CreateDALInstanceOfMap____();

        /// <summary>
        /// Creates the DAL instance of Elmah.DALContracts.IELMAH_ErrorRepository for entity TDALContractOfELMAH_Error .
        /// </summary>
        /// <returns>one instance of data access layer class</returns>
        //TDALContractOfELMAH_Error CreateDALInstanceOfELMAH_Error();
        Elmah.DALContracts.IELMAH_ErrorRepository CreateDALInstanceOfELMAH_Error();

        /// <summary>
        /// Creates the DAL instance of Elmah.DALContracts.IElmahApplicationRepository for entity TDALContractOfElmahApplication .
        /// </summary>
        /// <returns>one instance of data access layer class</returns>
        //TDALContractOfElmahApplication CreateDALInstanceOfElmahApplication();
        Elmah.DALContracts.IElmahApplicationRepository CreateDALInstanceOfElmahApplication();

        /// <summary>
        /// Creates the DAL instance of Elmah.DALContracts.IElmahHostRepository for entity TDALContractOfElmahHost .
        /// </summary>
        /// <returns>one instance of data access layer class</returns>
        //TDALContractOfElmahHost CreateDALInstanceOfElmahHost();
        Elmah.DALContracts.IElmahHostRepository CreateDALInstanceOfElmahHost();

        /// <summary>
        /// Creates the DAL instance of Elmah.DALContracts.IElmahSourceRepository for entity TDALContractOfElmahSource .
        /// </summary>
        /// <returns>one instance of data access layer class</returns>
        //TDALContractOfElmahSource CreateDALInstanceOfElmahSource();
        Elmah.DALContracts.IElmahSourceRepository CreateDALInstanceOfElmahSource();

        /// <summary>
        /// Creates the DAL instance of Elmah.DALContracts.IElmahStatusCodeRepository for entity TDALContractOfElmahStatusCode .
        /// </summary>
        /// <returns>one instance of data access layer class</returns>
        //TDALContractOfElmahStatusCode CreateDALInstanceOfElmahStatusCode();
        Elmah.DALContracts.IElmahStatusCodeRepository CreateDALInstanceOfElmahStatusCode();

        /// <summary>
        /// Creates the DAL instance of Elmah.DALContracts.IElmahTypeRepository for entity TDALContractOfElmahType .
        /// </summary>
        /// <returns>one instance of data access layer class</returns>
        //TDALContractOfElmahType CreateDALInstanceOfElmahType();
        Elmah.DALContracts.IElmahTypeRepository CreateDALInstanceOfElmahType();

        /// <summary>
        /// Creates the DAL instance of Elmah.DALContracts.IElmahUserRepository for entity TDALContractOfElmahUser .
        /// </summary>
        /// <returns>one instance of data access layer class</returns>
        //TDALContractOfElmahUser CreateDALInstanceOfElmahUser();
        Elmah.DALContracts.IElmahUserRepository CreateDALInstanceOfElmahUser();

    }
}

