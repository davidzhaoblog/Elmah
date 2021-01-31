using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Elmah.WcfContracts
{
#if (XAMARIN)
#else
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Elmah.WcfContracts.IElmahStatusCodeServiceAsyn")]
#endif
    public interface IElmahStatusCodeServiceAsyn
    {

        #region Framework.Repositories.DataAccessLayerContractBase<List<Elmah.DataSourceEntities.ElmahStatusCode>,Elmah.DataSourceEntities.ElmahStatusCode,Elmah.DataSourceEntities.ElmahStatusCodeIdentifier> Members

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.url/Elmah/WcfContracts/IElmahStatusCodeService/DeleteEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahStatusCodeService/DeleteEntityResponse")]
#endif
        Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn DeleteEntity(Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageBuiltIn input);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.url/Elmah/WcfContracts/IElmahStatusCodeService/DeleteByIdentifierEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahStatusCodeService/DeleteByIdentifierEntityResponse")]
#endif
        Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn DeleteByIdentifierEntity(Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageBuiltInOfIdentifier id);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.url/Elmah/WcfContracts/IElmahStatusCodeService/InsertEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahStatusCodeService/InsertEntityResponse")]
#endif
        Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn InsertEntity(Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageBuiltIn input);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.url/Elmah/WcfContracts/IElmahStatusCodeService/UpdateEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahStatusCodeService/UpdateEntityResponse")]
#endif
        Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn UpdateEntity(Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageBuiltIn input);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahStatusCodeService/BatchInsert",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahStatusCodeService/BatchInsertResponse")]
#endif
        Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn BatchInsert(Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageBuiltIn input);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahStatusCodeService/BatchDelete",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahStatusCodeService/BatchDeleteResponse")]
#endif
        Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn BatchDelete(Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageBuiltIn input);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahStatusCodeService/BatchUpdate",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahStatusCodeService/BatchUpdateResponse")]
#endif
        Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn BatchUpdate(Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageBuiltIn input);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahStatusCodeService/DeleteEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahStatusCodeService/DeleteEntityResponse")]
#endif
        System.IAsyncResult BeginDeleteEntity(Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageBuiltIn input, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn EndDeleteEntity(System.IAsyncResult result);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahStatusCodeService/DeleteByIdentifierEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahStatusCodeService/DeleteByIdentifierEntityResponse")]
#endif
        System.IAsyncResult BeginDeleteByIdentifierEntity(Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageBuiltInOfIdentifier id, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn EndDeleteByIdentifierEntity(System.IAsyncResult result);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahStatusCodeService/InsertEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahStatusCodeService/InsertEntityResponse")]
#endif
        System.IAsyncResult BeginInsertEntity(Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageBuiltIn input, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn EndInsertEntity(System.IAsyncResult result);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahStatusCodeService/UpdateEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahStatusCodeService/UpdateEntityResponse")]
#endif
        System.IAsyncResult BeginUpdateEntity(Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageBuiltIn input, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn EndUpdateEntity(System.IAsyncResult result);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahStatusCodeService/BatchInsert",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahStatusCodeService/BatchInsertResponse")]
#endif
        System.IAsyncResult BeginBatchInsert(Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageBuiltIn input, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn EndBatchInsert(System.IAsyncResult result);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahStatusCodeService/BatchDelete",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahStatusCodeService/BatchDeleteResponse")]
#endif
        System.IAsyncResult BeginBatchDelete(Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageBuiltIn inpu, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn EndBatchDelete(System.IAsyncResult result);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahStatusCodeService/BatchUpdate",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahStatusCodeService/BatchUpdateResponse")]
#endif
        System.IAsyncResult BeginBatchUpdate(Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageBuiltIn input, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn EndBatchUpdate(System.IAsyncResult result);

        #endregion

        #region Query Methods Of EntityByCommon

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahStatusCodeService/GetCollectionOfEntityByCommon",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahStatusCodeService/GetCollectionOfEntityByCommonResponse")]
#endif
        Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn GetCollectionOfEntityByCommon(
            Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageUserDefinedOfCommon request);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahStatusCodeService/GetCollectionOfEntityByCommon",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahStatusCodeService/GetCollectionOfEntityByCommonResponse")]
#endif
        System.IAsyncResult BeginGetCollectionOfEntityByCommon(Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageUserDefinedOfCommon request, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn EndGetCollectionOfEntityByCommon(System.IAsyncResult result);

        #endregion Query Methods Of EntityByCommon

        #region Query Methods Of NameValuePairByCommon

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahStatusCodeService/GetCollectionOfNameValuePairByCommon",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahStatusCodeService/GetCollectionOfNameValuePairByCommonResponse")]
#endif
        Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection GetCollectionOfNameValuePairByCommon(
            Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageUserDefinedOfCommon request);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahStatusCodeService/GetCollectionOfNameValuePairByCommon",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahStatusCodeService/GetCollectionOfNameValuePairByCommonResponse")]
#endif
        System.IAsyncResult BeginGetCollectionOfNameValuePairByCommon(Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageUserDefinedOfCommon request, System.AsyncCallback callback, object asyncState);
        Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection EndGetCollectionOfNameValuePairByCommon(System.IAsyncResult result);

        #endregion Query Methods Of NameValuePairByCommon

        #region Query Methods Of EntityByIdentifier

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahStatusCodeService/ExistsOfEntityByIdentifier",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahStatusCodeService/ExistsOfEntityByIdentifierResponse")]
#endif
        Framework.Services.BusinessLogicLayerResponseMessageBoolean ExistsOfEntityByIdentifier(
            Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageUserDefinedOfIdentifier request);
#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahStatusCodeService/ExistsOfEntityByIdentifier",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahStatusCodeService/ExistsOfEntityByIdentifierResponse")]
#endif
        System.IAsyncResult BeginExistsOfEntityByIdentifier(Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageUserDefinedOfIdentifier request, System.AsyncCallback callback, object asyncState);
        Framework.Services.BusinessLogicLayerResponseMessageBoolean EndExistsOfEntityByIdentifier(System.IAsyncResult result);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahStatusCodeService/GetCollectionOfEntityByIdentifier",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahStatusCodeService/GetCollectionOfEntityByIdentifierResponse")]
#endif
        Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn GetCollectionOfEntityByIdentifier(
            Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageUserDefinedOfIdentifier request);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahStatusCodeService/GetCollectionOfEntityByIdentifier",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahStatusCodeService/GetCollectionOfEntityByIdentifierResponse")]
#endif
        System.IAsyncResult BeginGetCollectionOfEntityByIdentifier(Elmah.CommonBLLEntities.ElmahStatusCodeRequestMessageUserDefinedOfIdentifier request, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ElmahStatusCodeResponseMessageBuiltIn EndGetCollectionOfEntityByIdentifier(System.IAsyncResult result);

        #endregion Query Methods Of EntityByIdentifier

    }
}

