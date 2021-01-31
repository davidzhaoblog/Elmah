using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Elmah.WcfContracts
{
#if (XAMARIN)
#else
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Elmah.WcfContracts.IElmahHostServiceAsyn")]
#endif
    public interface IElmahHostServiceAsyn
    {

        #region Framework.Repositories.DataAccessLayerContractBase<List<Elmah.DataSourceEntities.ElmahHost>,Elmah.DataSourceEntities.ElmahHost,Elmah.DataSourceEntities.ElmahHostIdentifier> Members

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.url/Elmah/WcfContracts/IElmahHostService/DeleteEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahHostService/DeleteEntityResponse")]
#endif
        Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn DeleteEntity(Elmah.CommonBLLEntities.ElmahHostRequestMessageBuiltIn input);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.url/Elmah/WcfContracts/IElmahHostService/DeleteByIdentifierEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahHostService/DeleteByIdentifierEntityResponse")]
#endif
        Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn DeleteByIdentifierEntity(Elmah.CommonBLLEntities.ElmahHostRequestMessageBuiltInOfIdentifier id);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.url/Elmah/WcfContracts/IElmahHostService/InsertEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahHostService/InsertEntityResponse")]
#endif
        Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn InsertEntity(Elmah.CommonBLLEntities.ElmahHostRequestMessageBuiltIn input);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.url/Elmah/WcfContracts/IElmahHostService/UpdateEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahHostService/UpdateEntityResponse")]
#endif
        Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn UpdateEntity(Elmah.CommonBLLEntities.ElmahHostRequestMessageBuiltIn input);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahHostService/BatchInsert",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahHostService/BatchInsertResponse")]
#endif
        Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn BatchInsert(Elmah.CommonBLLEntities.ElmahHostRequestMessageBuiltIn input);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahHostService/BatchDelete",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahHostService/BatchDeleteResponse")]
#endif
        Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn BatchDelete(Elmah.CommonBLLEntities.ElmahHostRequestMessageBuiltIn input);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahHostService/BatchUpdate",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahHostService/BatchUpdateResponse")]
#endif
        Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn BatchUpdate(Elmah.CommonBLLEntities.ElmahHostRequestMessageBuiltIn input);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahHostService/DeleteEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahHostService/DeleteEntityResponse")]
#endif
        System.IAsyncResult BeginDeleteEntity(Elmah.CommonBLLEntities.ElmahHostRequestMessageBuiltIn input, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn EndDeleteEntity(System.IAsyncResult result);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahHostService/DeleteByIdentifierEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahHostService/DeleteByIdentifierEntityResponse")]
#endif
        System.IAsyncResult BeginDeleteByIdentifierEntity(Elmah.CommonBLLEntities.ElmahHostRequestMessageBuiltInOfIdentifier id, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn EndDeleteByIdentifierEntity(System.IAsyncResult result);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahHostService/InsertEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahHostService/InsertEntityResponse")]
#endif
        System.IAsyncResult BeginInsertEntity(Elmah.CommonBLLEntities.ElmahHostRequestMessageBuiltIn input, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn EndInsertEntity(System.IAsyncResult result);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahHostService/UpdateEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahHostService/UpdateEntityResponse")]
#endif
        System.IAsyncResult BeginUpdateEntity(Elmah.CommonBLLEntities.ElmahHostRequestMessageBuiltIn input, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn EndUpdateEntity(System.IAsyncResult result);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahHostService/BatchInsert",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahHostService/BatchInsertResponse")]
#endif
        System.IAsyncResult BeginBatchInsert(Elmah.CommonBLLEntities.ElmahHostRequestMessageBuiltIn input, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn EndBatchInsert(System.IAsyncResult result);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahHostService/BatchDelete",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahHostService/BatchDeleteResponse")]
#endif
        System.IAsyncResult BeginBatchDelete(Elmah.CommonBLLEntities.ElmahHostRequestMessageBuiltIn inpu, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn EndBatchDelete(System.IAsyncResult result);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahHostService/BatchUpdate",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahHostService/BatchUpdateResponse")]
#endif
        System.IAsyncResult BeginBatchUpdate(Elmah.CommonBLLEntities.ElmahHostRequestMessageBuiltIn input, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn EndBatchUpdate(System.IAsyncResult result);

        #endregion

        #region Query Methods Of EntityByCommon

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahHostService/GetCollectionOfEntityByCommon",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahHostService/GetCollectionOfEntityByCommonResponse")]
#endif
        Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn GetCollectionOfEntityByCommon(
            Elmah.CommonBLLEntities.ElmahHostRequestMessageUserDefinedOfCommon request);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahHostService/GetCollectionOfEntityByCommon",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahHostService/GetCollectionOfEntityByCommonResponse")]
#endif
        System.IAsyncResult BeginGetCollectionOfEntityByCommon(Elmah.CommonBLLEntities.ElmahHostRequestMessageUserDefinedOfCommon request, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn EndGetCollectionOfEntityByCommon(System.IAsyncResult result);

        #endregion Query Methods Of EntityByCommon

        #region Query Methods Of NameValuePairByCommon

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahHostService/GetCollectionOfNameValuePairByCommon",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahHostService/GetCollectionOfNameValuePairByCommonResponse")]
#endif
        Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection GetCollectionOfNameValuePairByCommon(
            Elmah.CommonBLLEntities.ElmahHostRequestMessageUserDefinedOfCommon request);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahHostService/GetCollectionOfNameValuePairByCommon",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahHostService/GetCollectionOfNameValuePairByCommonResponse")]
#endif
        System.IAsyncResult BeginGetCollectionOfNameValuePairByCommon(Elmah.CommonBLLEntities.ElmahHostRequestMessageUserDefinedOfCommon request, System.AsyncCallback callback, object asyncState);
        Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection EndGetCollectionOfNameValuePairByCommon(System.IAsyncResult result);

        #endregion Query Methods Of NameValuePairByCommon

        #region Query Methods Of EntityByIdentifier

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahHostService/ExistsOfEntityByIdentifier",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahHostService/ExistsOfEntityByIdentifierResponse")]
#endif
        Framework.Services.BusinessLogicLayerResponseMessageBoolean ExistsOfEntityByIdentifier(
            Elmah.CommonBLLEntities.ElmahHostRequestMessageUserDefinedOfIdentifier request);
#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahHostService/ExistsOfEntityByIdentifier",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahHostService/ExistsOfEntityByIdentifierResponse")]
#endif
        System.IAsyncResult BeginExistsOfEntityByIdentifier(Elmah.CommonBLLEntities.ElmahHostRequestMessageUserDefinedOfIdentifier request, System.AsyncCallback callback, object asyncState);
        Framework.Services.BusinessLogicLayerResponseMessageBoolean EndExistsOfEntityByIdentifier(System.IAsyncResult result);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahHostService/GetCollectionOfEntityByIdentifier",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahHostService/GetCollectionOfEntityByIdentifierResponse")]
#endif
        Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn GetCollectionOfEntityByIdentifier(
            Elmah.CommonBLLEntities.ElmahHostRequestMessageUserDefinedOfIdentifier request);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahHostService/GetCollectionOfEntityByIdentifier",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahHostService/GetCollectionOfEntityByIdentifierResponse")]
#endif
        System.IAsyncResult BeginGetCollectionOfEntityByIdentifier(Elmah.CommonBLLEntities.ElmahHostRequestMessageUserDefinedOfIdentifier request, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ElmahHostResponseMessageBuiltIn EndGetCollectionOfEntityByIdentifier(System.IAsyncResult result);

        #endregion Query Methods Of EntityByIdentifier

    }
}

