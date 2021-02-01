using System;
using System.Windows.Input;

namespace Framework.Xaml
{
    public interface IViewModelTabBase
    {
        ICommand Command_SelectTabItem { get; set; }
        ICommand Command_SwipeTabItem { get; }
        string SelectedTabItem { get; set; }
    }
}

