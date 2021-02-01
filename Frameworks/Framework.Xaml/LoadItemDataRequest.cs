using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Xaml
{
    public class LoadItemDataRequest<TCriteria>
    {
        public TCriteria Criteria { get; set; }
        public Framework.ViewModels.UIAction UIAction { get; set; }
        public bool Refresh { get; set; }
        public bool ShowLoadingPopup { get; set; } = true;
        public bool ShowSavingPopup { get; set; } = true;
        public bool ShowSuccessfullySavedPopup { get; set; } = true;
        public bool ShowSaveFailedPopup { get; set; } = true;
        public bool LoadExtraDataIfNeeded { get; set; }
        public bool LoadDropDownContents { get; set; }
        public bool SetDropDownSelectedItems { get; set; }

        public Framework.Xaml.ControlParentOptions ControlParentOption { get; set; } = Framework.Xaml.ControlParentOptions.InPage;

        // This property is only used in Details control
        public bool IsContentEnable { get; set; }
    }
}

