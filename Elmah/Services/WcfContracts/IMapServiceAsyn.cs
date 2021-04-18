namespace Elmah.WcfContracts
{
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName = "Elmah.WcfContracts.IMapServiceAsyn")]
    public interface IMapServiceAsyn
    {
        [System.ServiceModel.OperationContractAttribute(Action = "http://www.Elmah.com/Elmah/WcfContracts/IMapService/GetMapItems",
            ReplyAction = "http://www.Elmah.com/Elmah/WcfContracts/IMapService/GetMapItemsResponse")]
        Elmah.CommonBLLEntities.BusinessLogicLayerResponseMessageMapItemCollection GetMapItems(
            Elmah.CommonBLLEntities.GeoSearchRequestMessage request);

        [System.ServiceModel.OperationContractAttribute(AsyncPattern = true, Action = "http://www.Elmah.com/Elmah/WcfContracts/IMapService/GetMapItems",
            ReplyAction = "http://www.Elmah.com/Elmah/WcfContracts/IMapService/GetMapItemsResponse")]
        System.IAsyncResult BeginGetCollectionOfDefaultByCommon(Elmah.CommonBLLEntities.GeoSearchRequestMessage request, System.AsyncCallback callback, object asyncState);
        Elmah.CommonBLLEntities.BusinessLogicLayerResponseMessageMapItemCollection EndGetMapItems(System.IAsyncResult result);
    }
}

