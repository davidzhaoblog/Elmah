using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Xaml
{
    public class SaveDataRequest<TMessage>
    {
        public Framework.ViewModels.UIAction UIAction { get; set; }
        public TMessage Message { get; set; }
        public bool EnablePopup { get; set; }
        public bool LoadDropDownContents { get; set; }
    }
}

