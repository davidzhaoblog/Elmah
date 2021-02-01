namespace Framework
{
    public interface IBusinessLogicLayerResponseMessageBase
    {
        string BusinessLogicLayerRequestID { get; set; }
        string BusinessLogicLayerResponseID { get; set; }
        Framework.Services.BusinessLogicLayerResponseStatus BusinessLogicLayerResponseStatus { get; set; }
        Framework.Models.DataStreamServiceResult DataStreamServiceResult { get; set; }
        Framework.Queries.QueryPagingResult QueryPagingResult { get; set; }
        string ServerErrorMessage { get; set; }

        string GetStatusMessage();
    }
}

