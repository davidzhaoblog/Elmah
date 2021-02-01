namespace Framework.ViewModels
{
    public interface IViewModelItemBase
    {
        string StatusMessageOfResult { get; set; }
        Framework.Services.BusinessLogicLayerResponseStatus StatusOfResult { get; set; }
        Framework.ViewModels.UIActionStatusMessage UIActionStatusMessage { get; set; }
    }

    public interface IViewModelItemBase<TSearchCriteria, TItem> : IViewModelItemBase
        where TSearchCriteria : class, new()
        where TItem : class, new()
    {
        TSearchCriteria Criteria { get; set; }
        TItem Item { get; set; }
    }
}

