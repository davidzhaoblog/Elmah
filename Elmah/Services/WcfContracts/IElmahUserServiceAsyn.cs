using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Elmah.WcfContracts
{
#if (XAMARIN)
#else
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Elmah.WcfContracts.IElmahUserServiceAsyn")]
#endif
    public interface IElmahUserServiceAsyn
    {

        #region Framework.Repositories.DataAccessLayerContractBase<List<Elmah.DataSourceEntities.ElmahUser>,Elmah.DataSourceEntities.ElmahUser,Elmah.DataSourceEntities.ElmahUserIdentifier> Members

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.url/Elmah/WcfContracts/IElmahUserService/DeleteEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahUserService/DeleteEntityResponse")]
#endif
        Elmah.CommonBLLEntities.ElmahUserResponseMessageBuiltIn DeleteEntity(Elmah.CommonBLLEntities.ElmahUserRequestMessageBuiltIn input);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.url/Elmah/WcfContracts/IElmahUserService/DeleteByIdentifierEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahUserService/DeleteByIdentifierEntityResponse")]
#endif
        Elmah.CommonBLLEntities.ElmahUserResponseMessageBuiltIn DeleteByIdentifierEntity(Elmah.CommonBLLEntities.ElmahUserRequestMessageBuiltInOfIdentifier id);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.url/Elmah/WcfContracts/IElmahUserService/InsertEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahUserService/InsertEntityResponse")]
#endif
        Elmah.CommonBLLEntities.ElmahUserResponseMessageBuiltIn InsertEntity(Elmah.CommonBLLEntities.ElmahUserRequestMessageBuiltIn input);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.url/Elmah/WcfContracts/IElmahUserService/UpdateEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahUserService/UpdateEntityResponse")]
#endif
        Elmah.CommonBLLEntities.ElmahUserResponseMessageBuiltIn UpdateEntity(Elmah.CommonBLLEntities.ElmahUserRequestMessageBuiltIn input);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahUserService/BatchInsert",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahUserService/BatchInsertResponse")]
#endif
        Elmah.CommonBLLEntities.ElmahUserResponseMessageBuiltIn BatchInsert(Elmah.CommonBLLEntities.ElmahUserRequestMessageBuiltIn input);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahUserService/BatchDelete",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahUserService/BatchDeleteResponse")]
#endif
        Elmah.CommonBLLEntities.ElmahUserResponseMessageBuiltIn BatchDelete(Elmah.CommonBLLEntities.ElmahUserRequestMessageBuiltIn input);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahUserService/BatchUpdate",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahUserService/BatchUpdateResponse")]
#endif
        Elmah.CommonBLLEntities.ElmahUserResponseMessageBuiltIn BatchUpdate(Elmah.CommonBLLEntities.ElmahUserRequestMessageBuiltIn input);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahUserService/DeleteEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahUserService/DeleteEntityResponse")]
#endif
        System.IAsyncResult BeginDeleteEntity(Elmah.CommonBLLEntities.ElmahUserRequestMessageBuiltIn input, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ElmahUserResponseMessageBuiltIn EndDeleteEntity(System.IAsyncResult result);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahUserService/DeleteByIdentifierEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahUserService/DeleteByIdentifierEntityResponse")]
#endif
        System.IAsyncResult BeginDeleteByIdentifierEntity(Elmah.CommonBLLEntities.ElmahUserRequestMessageBuiltInOfIdentifier id, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ElmahUserResponseMessageBuiltIn EndDeleteByIdentifierEntity(System.IAsyncResult result);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahUserService/InsertEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahUserService/InsertEntityResponse")]
#endif
        System.IAsyncResult BeginInsertEntity(Elmah.CommonBLLEntities.ElmahUserRequestMessageBuiltIn input, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ElmahUserResponseMessageBuiltIn EndInsertEntity(System.IAsyncResult result);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahUserService/UpdateEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahUserService/UpdateEntityResponse")]
#endif
        System.IAsyncResult BeginUpdateEntity(Elmah.CommonBLLEntities.ElmahUserRequestMessageBuiltIn input, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ElmahUserResponseMessageBuiltIn EndUpdateEntity(System.IAsyncResult result);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahUserService/BatchInsert",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahUserService/BatchInsertResponse")]
#endif
        System.IAsyncResult BeginBatchInsert(Elmah.CommonBLLEntities.ElmahUserRequestMessageBuiltIn input, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ElmahUserResponseMessageBuiltIn EndBatchInsert(System.IAsyncResult result);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahUserService/BatchDelete",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahUserService/BatchDeleteResponse")]
#endif
        System.IAsyncResult BeginBatchDelete(Elmah.CommonBLLEntities.ElmahUserRequestMessageBuiltIn inpu, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ElmahUserResponseMessageBuiltIn EndBatchDelete(System.IAsyncResult result);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahUserService/BatchUpdate",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahUserService/BatchUpdateResponse")]
#endif
        System.IAsyncResult BeginBatchUpdate(Elmah.CommonBLLEntities.ElmahUserRequestMessageBuiltIn input, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ElmahUserResponseMessageBuiltIn EndBatchUpdate(System.IAsyncResult result);

        #endregion

        #region Query Methods Of EntityByCommon

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahUserService/GetCollectionOfEntityByCommon",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahUserService/GetCollectionOfEntityByCommonResponse")]
#endif
        Elmah.CommonBLLEntities.ElmahUserResponseMessageBuiltIn GetCollectionOfEntityByCommon(
            Elmah.CommonBLLEntities.ElmahUserRequestMessageUserDefinedOfCommon request);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahUserService/GetCollectionOfEntityByCommon",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahUserService/GetCollectionOfEntityByCommonResponse")]
#endif
        System.IAsyncResult BeginGetCollectionOfEntityByCommon(Elmah.CommonBLLEntities.ElmahUserRequestMessageUserDefinedOfCommon request, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ElmahUserResponseMessageBuiltIn EndGetCollectionOfEntityByCommon(System.IAsyncResult result);

        #endregion Query Methods Of EntityByCommon

        #region Query Methods Of NameValuePairByCommon

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahUserService/GetCollectionOfNameValuePairByCommon",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahUserService/GetCollectionOfNameValuePairByCommonResponse")]
#endif
        Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection GetCollectionOfNameValuePairByCommon(
            Elmah.CommonBLLEntities.ElmahUserRequestMessageUserDefinedOfCommon request);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahUserService/GetCollectionOfNameValuePairByCommon",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahUserService/GetCollectionOfNameValuePairByCommonResponse")]
#endif
        System.IAsyncResult BeginGetCollectionOfNameValuePairByCommon(Elmah.CommonBLLEntities.ElmahUserRequestMessageUserDefinedOfCommon request, System.AsyncCallback callback, object asyncState);
        Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection EndGetCollectionOfNameValuePairByCommon(System.IAsyncResult result);

        #endregion Query Methods Of NameValuePairByCommon

        #region Query Methods Of EntityByIdentifier

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahUserService/ExistsOfEntityByIdentifier",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahUserService/ExistsOfEntityByIdentifierResponse")]
#endif
        Framework.Services.BusinessLogicLayerResponseMessageBoolean ExistsOfEntityByIdentifier(
            Elmah.CommonBLLEntities.ElmahUserRequestMessageUserDefinedOfIdentifier request);
#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahUserService/ExistsOfEntityByIdentifier",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahUserService/ExistsOfEntityByIdentifierResponse")]
#endif
        System.IAsyncResult BeginExistsOfEntityByIdentifier(Elmah.CommonBLLEntities.ElmahUserRequestMessageUserDefinedOfIdentifier request, System.AsyncCallback callback, object asyncState);
        Framework.Services.BusinessLogicLayerResponseMessageBoolean EndExistsOfEntityByIdentifier(System.IAsyncResult result);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahUserService/GetCollectionOfEntityByIdentifier",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahUserService/GetCollectionOfEntityByIdentifierResponse")]
#endif
        Elmah.CommonBLLEntities.ElmahUserResponseMessageBuiltIn GetCollectionOfEntityByIdentifier(
            Elmah.CommonBLLEntities.ElmahUserRequestMessageUserDefinedOfIdentifier request);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahUserService/GetCollectionOfEntityByIdentifier",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahUserService/GetCollectionOfEntityByIdentifierResponse")]
#endif
        System.IAsyncResult BeginGetCollectionOfEntityByIdentifier(Elmah.CommonBLLEntities.ElmahUserRequestMessageUserDefinedOfIdentifier request, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ElmahUserResponseMessageBuiltIn EndGetCollectionOfEntityByIdentifier(System.IAsyncResult result);

        #endregion Query Methods Of EntityByIdentifier

    }
}

