using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Elmah.WcfContracts
{
#if (XAMARIN)
#else
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Elmah.WcfContracts.IElmahApplicationServiceAsyn")]
#endif
    public interface IElmahApplicationServiceAsyn
    {

        #region Framework.Repositories.DataAccessLayerContractBase<List<Elmah.DataSourceEntities.ElmahApplication>,Elmah.DataSourceEntities.ElmahApplication,Elmah.DataSourceEntities.ElmahApplicationIdentifier> Members

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.url/Elmah/WcfContracts/IElmahApplicationService/DeleteEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahApplicationService/DeleteEntityResponse")]
#endif
        Elmah.CommonBLLEntities.ElmahApplicationResponseMessageBuiltIn DeleteEntity(Elmah.CommonBLLEntities.ElmahApplicationRequestMessageBuiltIn input);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.url/Elmah/WcfContracts/IElmahApplicationService/DeleteByIdentifierEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahApplicationService/DeleteByIdentifierEntityResponse")]
#endif
        Elmah.CommonBLLEntities.ElmahApplicationResponseMessageBuiltIn DeleteByIdentifierEntity(Elmah.CommonBLLEntities.ElmahApplicationRequestMessageBuiltInOfIdentifier id);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.url/Elmah/WcfContracts/IElmahApplicationService/InsertEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahApplicationService/InsertEntityResponse")]
#endif
        Elmah.CommonBLLEntities.ElmahApplicationResponseMessageBuiltIn InsertEntity(Elmah.CommonBLLEntities.ElmahApplicationRequestMessageBuiltIn input);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.url/Elmah/WcfContracts/IElmahApplicationService/UpdateEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahApplicationService/UpdateEntityResponse")]
#endif
        Elmah.CommonBLLEntities.ElmahApplicationResponseMessageBuiltIn UpdateEntity(Elmah.CommonBLLEntities.ElmahApplicationRequestMessageBuiltIn input);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahApplicationService/BatchInsert",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahApplicationService/BatchInsertResponse")]
#endif
        Elmah.CommonBLLEntities.ElmahApplicationResponseMessageBuiltIn BatchInsert(Elmah.CommonBLLEntities.ElmahApplicationRequestMessageBuiltIn input);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahApplicationService/BatchDelete",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahApplicationService/BatchDeleteResponse")]
#endif
        Elmah.CommonBLLEntities.ElmahApplicationResponseMessageBuiltIn BatchDelete(Elmah.CommonBLLEntities.ElmahApplicationRequestMessageBuiltIn input);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahApplicationService/BatchUpdate",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahApplicationService/BatchUpdateResponse")]
#endif
        Elmah.CommonBLLEntities.ElmahApplicationResponseMessageBuiltIn BatchUpdate(Elmah.CommonBLLEntities.ElmahApplicationRequestMessageBuiltIn input);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahApplicationService/DeleteEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahApplicationService/DeleteEntityResponse")]
#endif
        System.IAsyncResult BeginDeleteEntity(Elmah.CommonBLLEntities.ElmahApplicationRequestMessageBuiltIn input, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ElmahApplicationResponseMessageBuiltIn EndDeleteEntity(System.IAsyncResult result);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahApplicationService/DeleteByIdentifierEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahApplicationService/DeleteByIdentifierEntityResponse")]
#endif
        System.IAsyncResult BeginDeleteByIdentifierEntity(Elmah.CommonBLLEntities.ElmahApplicationRequestMessageBuiltInOfIdentifier id, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ElmahApplicationResponseMessageBuiltIn EndDeleteByIdentifierEntity(System.IAsyncResult result);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahApplicationService/InsertEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahApplicationService/InsertEntityResponse")]
#endif
        System.IAsyncResult BeginInsertEntity(Elmah.CommonBLLEntities.ElmahApplicationRequestMessageBuiltIn input, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ElmahApplicationResponseMessageBuiltIn EndInsertEntity(System.IAsyncResult result);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahApplicationService/UpdateEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahApplicationService/UpdateEntityResponse")]
#endif
        System.IAsyncResult BeginUpdateEntity(Elmah.CommonBLLEntities.ElmahApplicationRequestMessageBuiltIn input, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ElmahApplicationResponseMessageBuiltIn EndUpdateEntity(System.IAsyncResult result);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahApplicationService/BatchInsert",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahApplicationService/BatchInsertResponse")]
#endif
        System.IAsyncResult BeginBatchInsert(Elmah.CommonBLLEntities.ElmahApplicationRequestMessageBuiltIn input, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ElmahApplicationResponseMessageBuiltIn EndBatchInsert(System.IAsyncResult result);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahApplicationService/BatchDelete",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahApplicationService/BatchDeleteResponse")]
#endif
        System.IAsyncResult BeginBatchDelete(Elmah.CommonBLLEntities.ElmahApplicationRequestMessageBuiltIn inpu, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ElmahApplicationResponseMessageBuiltIn EndBatchDelete(System.IAsyncResult result);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahApplicationService/BatchUpdate",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahApplicationService/BatchUpdateResponse")]
#endif
        System.IAsyncResult BeginBatchUpdate(Elmah.CommonBLLEntities.ElmahApplicationRequestMessageBuiltIn input, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ElmahApplicationResponseMessageBuiltIn EndBatchUpdate(System.IAsyncResult result);

        #endregion

        #region Query Methods Of EntityByCommon

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahApplicationService/GetCollectionOfEntityByCommon",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahApplicationService/GetCollectionOfEntityByCommonResponse")]
#endif
        Elmah.CommonBLLEntities.ElmahApplicationResponseMessageBuiltIn GetCollectionOfEntityByCommon(
            Elmah.CommonBLLEntities.ElmahApplicationRequestMessageUserDefinedOfCommon request);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahApplicationService/GetCollectionOfEntityByCommon",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahApplicationService/GetCollectionOfEntityByCommonResponse")]
#endif
        System.IAsyncResult BeginGetCollectionOfEntityByCommon(Elmah.CommonBLLEntities.ElmahApplicationRequestMessageUserDefinedOfCommon request, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ElmahApplicationResponseMessageBuiltIn EndGetCollectionOfEntityByCommon(System.IAsyncResult result);

        #endregion Query Methods Of EntityByCommon

        #region Query Methods Of NameValuePairByCommon

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahApplicationService/GetCollectionOfNameValuePairByCommon",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahApplicationService/GetCollectionOfNameValuePairByCommonResponse")]
#endif
        Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection GetCollectionOfNameValuePairByCommon(
            Elmah.CommonBLLEntities.ElmahApplicationRequestMessageUserDefinedOfCommon request);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahApplicationService/GetCollectionOfNameValuePairByCommon",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahApplicationService/GetCollectionOfNameValuePairByCommonResponse")]
#endif
        System.IAsyncResult BeginGetCollectionOfNameValuePairByCommon(Elmah.CommonBLLEntities.ElmahApplicationRequestMessageUserDefinedOfCommon request, System.AsyncCallback callback, object asyncState);
        Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection EndGetCollectionOfNameValuePairByCommon(System.IAsyncResult result);

        #endregion Query Methods Of NameValuePairByCommon

        #region Query Methods Of EntityByIdentifier

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahApplicationService/ExistsOfEntityByIdentifier",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahApplicationService/ExistsOfEntityByIdentifierResponse")]
#endif
        Framework.Services.BusinessLogicLayerResponseMessageBoolean ExistsOfEntityByIdentifier(
            Elmah.CommonBLLEntities.ElmahApplicationRequestMessageUserDefinedOfIdentifier request);
#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahApplicationService/ExistsOfEntityByIdentifier",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahApplicationService/ExistsOfEntityByIdentifierResponse")]
#endif
        System.IAsyncResult BeginExistsOfEntityByIdentifier(Elmah.CommonBLLEntities.ElmahApplicationRequestMessageUserDefinedOfIdentifier request, System.AsyncCallback callback, object asyncState);
        Framework.Services.BusinessLogicLayerResponseMessageBoolean EndExistsOfEntityByIdentifier(System.IAsyncResult result);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahApplicationService/GetCollectionOfEntityByIdentifier",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahApplicationService/GetCollectionOfEntityByIdentifierResponse")]
#endif
        Elmah.CommonBLLEntities.ElmahApplicationResponseMessageBuiltIn GetCollectionOfEntityByIdentifier(
            Elmah.CommonBLLEntities.ElmahApplicationRequestMessageUserDefinedOfIdentifier request);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahApplicationService/GetCollectionOfEntityByIdentifier",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahApplicationService/GetCollectionOfEntityByIdentifierResponse")]
#endif
        System.IAsyncResult BeginGetCollectionOfEntityByIdentifier(Elmah.CommonBLLEntities.ElmahApplicationRequestMessageUserDefinedOfIdentifier request, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ElmahApplicationResponseMessageBuiltIn EndGetCollectionOfEntityByIdentifier(System.IAsyncResult result);

        #endregion Query Methods Of EntityByIdentifier

    }
}

