using System;

namespace Framework.Xaml
{
    /// <summary>
    /// must have enum to represent TabItems
    /// </summary>
    /// <typeparam name="TTabItem"></typeparam>
    public class TabSettingVM<TTabItem>: Framework.Xaml.ViewModelTabBase<TTabItem>
        where TTabItem : struct, System.Enum
    {
    }
}

