using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;

namespace Elmah.WcfContracts
{
#if (XAMARIN)
#else
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Elmah.WcfContracts.IELMAH_ErrorServiceAsyn")]
#endif
    public interface IELMAH_ErrorServiceAsyn
    {

        #region Framework.Repositories.DataAccessLayerContractBase<List<Elmah.DataSourceEntities.ELMAH_Error>,Elmah.DataSourceEntities.ELMAH_Error,Elmah.DataSourceEntities.ELMAH_ErrorIdentifier> Members

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/DeleteEntity",
            ReplyAction = "http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/DeleteEntityResponse")]
#endif
        Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn DeleteEntity(Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageBuiltIn input);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/DeleteByIdentifierEntity",
            ReplyAction = "http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/DeleteByIdentifierEntityResponse")]
#endif
        Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn DeleteByIdentifierEntity(Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageBuiltInOfIdentifier id);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/InsertEntity",
            ReplyAction = "http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/InsertEntityResponse")]
#endif
        Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn InsertEntity(Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageBuiltIn input);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/UpdateEntity",
            ReplyAction = "http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/UpdateEntityResponse")]
#endif
        Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn UpdateEntity(Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageBuiltIn input);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action="http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/BatchInsert",
            ReplyAction="http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/BatchInsertResponse")]
#endif
        Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn BatchInsert(Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageBuiltIn input);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action="http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/BatchDelete",
            ReplyAction="http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/BatchDeleteResponse")]
#endif
        Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn BatchDelete(Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageBuiltIn input);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action="http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/BatchUpdate",
            ReplyAction="http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/BatchUpdateResponse")]
#endif
        Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn BatchUpdate(Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageBuiltIn input);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/DeleteEntity",
            ReplyAction = "http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/DeleteEntityResponse")]
#endif
        System.IAsyncResult BeginDeleteEntity(Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageBuiltIn input, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn EndDeleteEntity(System.IAsyncResult result);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/DeleteByIdentifierEntity",
            ReplyAction = "http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/DeleteByIdentifierEntityResponse")]
#endif
        System.IAsyncResult BeginDeleteByIdentifierEntity(Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageBuiltInOfIdentifier id, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn EndDeleteByIdentifierEntity(System.IAsyncResult result);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/InsertEntity",
            ReplyAction = "http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/InsertEntityResponse")]
#endif
        System.IAsyncResult BeginInsertEntity(Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageBuiltIn input, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn EndInsertEntity(System.IAsyncResult result);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/UpdateEntity",
            ReplyAction = "http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/UpdateEntityResponse")]
#endif
        System.IAsyncResult BeginUpdateEntity(Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageBuiltIn input, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn EndUpdateEntity(System.IAsyncResult result);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/BatchInsert",
            ReplyAction="http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/BatchInsertResponse")]
#endif
        System.IAsyncResult BeginBatchInsert(Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageBuiltIn input, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn EndBatchInsert(System.IAsyncResult result);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/BatchDelete",
            ReplyAction="http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/BatchDeleteResponse")]
#endif
        System.IAsyncResult BeginBatchDelete(Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageBuiltIn inpu, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn EndBatchDelete(System.IAsyncResult result);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/BatchUpdate",
            ReplyAction="http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/BatchUpdateResponse")]
#endif
        System.IAsyncResult BeginBatchUpdate(Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageBuiltIn input, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn EndBatchUpdate(System.IAsyncResult result);

        #endregion

        #region Query Methods Of NameValuePairByCommon

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action="http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/GetCollectionOfNameValuePairByCommon",
            ReplyAction="http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/GetCollectionOfNameValuePairByCommonResponse")]
#endif
        Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection GetCollectionOfNameValuePairByCommon(
            Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageUserDefinedOfCommon request);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/GetCollectionOfNameValuePairByCommon",
            ReplyAction="http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/GetCollectionOfNameValuePairByCommonResponse")]
#endif
        System.IAsyncResult BeginGetCollectionOfNameValuePairByCommon(Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageUserDefinedOfCommon request, System.AsyncCallback callback, object asyncState);
        Framework.Services.BusinessLogicLayerResponseMessageNameValuePairCollection EndGetCollectionOfNameValuePairByCommon(System.IAsyncResult result);

        #endregion Query Methods Of NameValuePairByCommon

        #region Query Methods Of DefaultByCommon

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action="http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/GetCollectionOfDefaultByCommon",
            ReplyAction="http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/GetCollectionOfDefaultByCommonResponse")]
#endif
        Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default GetCollectionOfDefaultByCommon(
            Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageUserDefinedOfCommon request);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/GetCollectionOfDefaultByCommon",
            ReplyAction="http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/GetCollectionOfDefaultByCommonResponse")]
#endif
        System.IAsyncResult BeginGetCollectionOfDefaultByCommon(Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageUserDefinedOfCommon request, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default EndGetCollectionOfDefaultByCommon(System.IAsyncResult result);

        #endregion Query Methods Of DefaultByCommon

        #region Query Methods Of EntityByIdentifier

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action="http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/ExistsOfEntityByIdentifier",
            ReplyAction="http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/ExistsOfEntityByIdentifierResponse")]
#endif
        Framework.Services.BusinessLogicLayerResponseMessageBoolean ExistsOfEntityByIdentifier(
            Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageUserDefinedOfIdentifier request);
#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/ExistsOfEntityByIdentifier",
            ReplyAction="http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/ExistsOfEntityByIdentifierResponse")]
#endif
        System.IAsyncResult BeginExistsOfEntityByIdentifier(Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageUserDefinedOfIdentifier request, System.AsyncCallback callback, object asyncState);
        Framework.Services.BusinessLogicLayerResponseMessageBoolean EndExistsOfEntityByIdentifier(System.IAsyncResult result);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action="http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/GetCollectionOfEntityByIdentifier",
            ReplyAction="http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/GetCollectionOfEntityByIdentifierResponse")]
#endif
        Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn GetCollectionOfEntityByIdentifier(
            Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageUserDefinedOfIdentifier request);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/GetCollectionOfEntityByIdentifier",
            ReplyAction="http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/GetCollectionOfEntityByIdentifierResponse")]
#endif
        System.IAsyncResult BeginGetCollectionOfEntityByIdentifier(Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageUserDefinedOfIdentifier request, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn EndGetCollectionOfEntityByIdentifier(System.IAsyncResult result);

        #endregion Query Methods Of EntityByIdentifier

        #region Query Methods Of DefaultByIdentifier

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action="http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/ExistsOfDefaultByIdentifier",
            ReplyAction="http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/ExistsOfDefaultByIdentifierResponse")]
#endif
        Framework.Services.BusinessLogicLayerResponseMessageBoolean ExistsOfDefaultByIdentifier(
            Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageUserDefinedOfIdentifier request);
#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/ExistsOfDefaultByIdentifier",
            ReplyAction="http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/ExistsOfDefaultByIdentifierResponse")]
#endif
        System.IAsyncResult BeginExistsOfDefaultByIdentifier(Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageUserDefinedOfIdentifier request, System.AsyncCallback callback, object asyncState);
        Framework.Services.BusinessLogicLayerResponseMessageBoolean EndExistsOfDefaultByIdentifier(System.IAsyncResult result);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(Action="http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/GetCollectionOfDefaultByIdentifier",
            ReplyAction="http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/GetCollectionOfDefaultByIdentifierResponse")]
#endif
        Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default GetCollectionOfDefaultByIdentifier(
            Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageUserDefinedOfIdentifier request);

#if (XAMARIN)
#else
        [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/GetCollectionOfDefaultByIdentifier",
            ReplyAction="http://www.Elmah.com/Elmah/WcfContracts/IELMAH_ErrorService/GetCollectionOfDefaultByIdentifierResponse")]
#endif
        System.IAsyncResult BeginGetCollectionOfDefaultByIdentifier(Elmah.CommonBLLEntities.ELMAH_ErrorRequestMessageUserDefinedOfIdentifier request, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.ELMAH_ErrorResponseMessageBuiltIn.Default EndGetCollectionOfDefaultByIdentifier(System.IAsyncResult result);

        #endregion Query Methods Of DefaultByIdentifier

    }
}

