using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Elmah.WcfContracts
{
#if (XAMARIN)
#else
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Elmah.WcfContracts.IElmahTypeServiceAsyn")]
#endif
    public interface IElmahTypeServiceAsyn
    {

        #region Framework.Repositories.DataAccessLayerContractBase<List<Elmah.DataSourceEntities.ElmahType>,Elmah.DataSourceEntities.ElmahType,Elmah.DataSourceEntities.ElmahTypeIdentifier> Members

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.url/Elmah/WcfContracts/IElmahTypeService/DeleteEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahTypeService/DeleteEntityResponse")]
#endif
        Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn DeleteEntity(Elmah.CommonBLLEntities.ElmahTypeRequestMessageBuiltIn input);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.url/Elmah/WcfContracts/IElmahTypeService/DeleteByIdentifierEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahTypeService/DeleteByIdentifierEntityResponse")]
#endif
        Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn DeleteByIdentifierEntity(Elmah.CommonBLLEntities.ElmahTypeRequestMessageBuiltInOfIdentifier id);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.url/Elmah/WcfContracts/IElmahTypeService/InsertEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahTypeService/InsertEntityResponse")]
#endif
        Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn InsertEntity(Elmah.CommonBLLEntities.ElmahTypeRequestMessageBuiltIn input);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.url/Elmah/WcfContracts/IElmahTypeService/UpdateEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahTypeService/UpdateEntityResponse")]
#endif
        Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn UpdateEntity(Elmah.CommonBLLEntities.ElmahTypeRequestMessageBuiltIn input);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahTypeService/BatchInsert",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahTypeService/BatchInsertResponse")]
#endif
        Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn BatchInsert(Elmah.CommonBLLEntities.ElmahTypeRequestMessageBuiltIn input);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahTypeService/BatchDelete",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahTypeService/BatchDeleteResponse")]
#endif
        Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn BatchDelete(Elmah.CommonBLLEntities.ElmahTypeRequestMessageBuiltIn input);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahTypeService/BatchUpdate",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahTypeService/BatchUpdateResponse")]
#endif
        Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn BatchUpdate(Elmah.CommonBLLEntities.ElmahTypeRequestMessageBuiltIn input);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahTypeService/DeleteEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahTypeService/DeleteEntityResponse")]
#endif
        System.IAsyncResult BeginDeleteEntity(Elmah.CommonBLLEntities.ElmahTypeRequestMessageBuiltIn input, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn EndDeleteEntity(System.IAsyncResult result);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahTypeService/DeleteByIdentifierEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahTypeService/DeleteByIdentifierEntityResponse")]
#endif
        System.IAsyncResult BeginDeleteByIdentifierEntity(Elmah.CommonBLLEntities.ElmahTypeRequestMessageBuiltInOfIdentifier id, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn EndDeleteByIdentifierEntity(System.IAsyncResult result);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahTypeService/InsertEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahTypeService/InsertEntityResponse")]
#endif
        System.IAsyncResult BeginInsertEntity(Elmah.CommonBLLEntities.ElmahTypeRequestMessageBuiltIn input, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn EndInsertEntity(System.IAsyncResult result);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahTypeService/UpdateEntity",
            ReplyAction = "http://www.url/Elmah/WcfContracts/IElmahTypeService/UpdateEntityResponse")]
#endif
        System.IAsyncResult BeginUpdateEntity(Elmah.CommonBLLEntities.ElmahTypeRequestMessageBuiltIn input, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn EndUpdateEntity(System.IAsyncResult result);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahTypeService/BatchInsert",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahTypeService/BatchInsertResponse")]
#endif
        System.IAsyncResult BeginBatchInsert(Elmah.CommonBLLEntities.ElmahTypeRequestMessageBuiltIn input, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn EndBatchInsert(System.IAsyncResult result);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahTypeService/BatchDelete",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahTypeService/BatchDeleteResponse")]
#endif
        System.IAsyncResult BeginBatchDelete(Elmah.CommonBLLEntities.ElmahTypeRequestMessageBuiltIn inpu, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn EndBatchDelete(System.IAsyncResult result);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahTypeService/BatchUpdate",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahTypeService/BatchUpdateResponse")]
#endif
        System.IAsyncResult BeginBatchUpdate(Elmah.CommonBLLEntities.ElmahTypeRequestMessageBuiltIn input, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn EndBatchUpdate(System.IAsyncResult result);

        #endregion

        #region Query Methods Of EntityByCommon

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahTypeService/GetCollectionOfEntityByCommon",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahTypeService/GetCollectionOfEntityByCommonResponse")]
#endif
        Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn GetCollectionOfEntityByCommon(
            Elmah.CommonBLLEntities.ElmahTypeRequestMessageUserDefinedOfCommon request);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahTypeService/GetCollectionOfEntityByCommon",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahTypeService/GetCollectionOfEntityByCommonResponse")]
#endif
        System.IAsyncResult BeginGetCollectionOfEntityByCommon(Elmah.CommonBLLEntities.ElmahTypeRequestMessageUserDefinedOfCommon request, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn EndGetCollectionOfEntityByCommon(System.IAsyncResult result);

        #endregion Query Methods Of EntityByCommon

        #region Query Methods Of NameValuePairByCommon

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahTypeService/GetCollectionOfNameValuePairByCommon",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahTypeService/GetCollectionOfNameValuePairByCommonResponse")]
#endif
        Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection GetCollectionOfNameValuePairByCommon(
            Elmah.CommonBLLEntities.ElmahTypeRequestMessageUserDefinedOfCommon request);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahTypeService/GetCollectionOfNameValuePairByCommon",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahTypeService/GetCollectionOfNameValuePairByCommonResponse")]
#endif
        System.IAsyncResult BeginGetCollectionOfNameValuePairByCommon(Elmah.CommonBLLEntities.ElmahTypeRequestMessageUserDefinedOfCommon request, System.AsyncCallback callback, object asyncState);
        Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection EndGetCollectionOfNameValuePairByCommon(System.IAsyncResult result);

        #endregion Query Methods Of NameValuePairByCommon

        #region Query Methods Of EntityByIdentifier

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahTypeService/ExistsOfEntityByIdentifier",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahTypeService/ExistsOfEntityByIdentifierResponse")]
#endif
        Framework.Services.BusinessLogicLayerResponseMessageBoolean ExistsOfEntityByIdentifier(
            Elmah.CommonBLLEntities.ElmahTypeRequestMessageUserDefinedOfIdentifier request);
#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahTypeService/ExistsOfEntityByIdentifier",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahTypeService/ExistsOfEntityByIdentifierResponse")]
#endif
        System.IAsyncResult BeginExistsOfEntityByIdentifier(Elmah.CommonBLLEntities.ElmahTypeRequestMessageUserDefinedOfIdentifier request, System.AsyncCallback callback, object asyncState);
        Framework.Services.BusinessLogicLayerResponseMessageBoolean EndExistsOfEntityByIdentifier(System.IAsyncResult result);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action="http://www.url/Elmah/WcfContracts/IElmahTypeService/GetCollectionOfEntityByIdentifier",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahTypeService/GetCollectionOfEntityByIdentifierResponse")]
#endif
        Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn GetCollectionOfEntityByIdentifier(
            Elmah.CommonBLLEntities.ElmahTypeRequestMessageUserDefinedOfIdentifier request);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.url/Elmah/WcfContracts/IElmahTypeService/GetCollectionOfEntityByIdentifier",
            ReplyAction="http://www.url/Elmah/WcfContracts/IElmahTypeService/GetCollectionOfEntityByIdentifierResponse")]
#endif
        System.IAsyncResult BeginGetCollectionOfEntityByIdentifier(Elmah.CommonBLLEntities.ElmahTypeRequestMessageUserDefinedOfIdentifier request, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ElmahTypeResponseMessageBuiltIn EndGetCollectionOfEntityByIdentifier(System.IAsyncResult result);

        #endregion Query Methods Of EntityByIdentifier

    }
}

