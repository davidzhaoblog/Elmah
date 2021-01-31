using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.ViewModels
{
    public class ViewModelItemBase<TSearchCriteria, TItem>: Framework.ViewModels.IViewModelItemBase<TSearchCriteria, TItem>
        where TSearchCriteria : class, new()
        where TItem : class, new()
    {
        public const string ViewName_Edit = "Edit";
        public const string ViewName_Create = "Create";
        public const string ViewName_Delete = "Delete";
        public const string ViewName_Details = "Details";

        public ViewModelItemBase()
            : base()
        {
            this.ContentData = new Framework.ViewModels.ContentData();
            this.StatusOfResult = Framework.Services.BusinessLogicLayerResponseStatus.UIProcessReady;
            this.UIActionStatusMessage = new Framework.ViewModels.UIActionStatusMessage();
        }

        public Framework.ViewModels.ContentData ContentData { get; set; }

        public virtual TSearchCriteria Criteria { get; set; }
        public virtual TItem Item { get; set; }
        public Framework.Services.BusinessLogicLayerResponseStatus StatusOfResult { get; set; }
        public string StatusMessageOfResult { get; set; }
        public Framework.ViewModels.UIActionStatusMessage UIActionStatusMessage { get; set; }
    }
}

