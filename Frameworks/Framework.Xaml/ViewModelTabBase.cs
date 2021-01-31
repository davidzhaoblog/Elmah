using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Framework.Xaml
{
    public abstract class ViewModelTabBase<TTabItem> : Framework.Xaml.ViewModelBase, Framework.Xaml.IViewModelTabBase
        where TTabItem : struct, System.Enum
    {
        //#region 1. Bindable

        private TTabItem m_TypedSelectedTabItem = default(TTabItem);
        public TTabItem TypedSelectedTabItem
        {
            get { return m_TypedSelectedTabItem; }
            set
            {
                Set(nameof(TypedSelectedTabItem), ref m_TypedSelectedTabItem, value);
                RaisePropertyChanged(nameof(SelectedTabItem));
            }
        }

        public string SelectedTabItem
        {
            get { return m_TypedSelectedTabItem.ToString(); }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    TypedSelectedTabItem = default(TTabItem);
                    RaisePropertyChanged(nameof(SelectedTabItem));
                }
                else
                {
                    TTabItem output;
                    if (Enum.TryParse<TTabItem>(value, out output))
                    {
                        TypedSelectedTabItem = (TTabItem)output;
                        ExecSelectedTabItemChangedAction();
                    }
                    else
                    {
                        RaisePropertyChanged(nameof(SelectedTabItem));
                    }
                }
            }
        }

        //#endregion 1. Bindable

        //#region 2. Commands

        public ICommand Command_SelectTabItem { get; set; }

        public void Command_SelectTabItem_Click(string selectedTabItem)
        {
            SelectedTabItem = selectedTabItem;
        }

        public ICommand Command_SwipeTabItem { get; protected set; }

        public void Command_SwipeTabItem_Click(string direction)
        {
            var names = Enum.GetNames(typeof(TTabItem));

            int nextIndex = 0;
            if (names.Contains(SelectedTabItem))
            {
                nextIndex = names.ToList().IndexOf(SelectedTabItem);
                if (direction == "Left" && nextIndex < names.Length - 1)
                    nextIndex = (nextIndex + 1) % names.Length;
                else if (direction == "Right" && nextIndex > 0)
                    nextIndex = (nextIndex - 1) % names.Length;
            }

            TypedSelectedTabItem = (TTabItem)Enum.Parse(typeof(TTabItem), names[nextIndex]);
            ExecSelectedTabItemChangedAction();
        }

        private void ExecSelectedTabItemChangedAction()
        {
            Action SelectedTabItemChangedAction = null;
            if (SelectedTabItemChangedActionList != null && SelectedTabItemChangedActionList.TryGetValue(TypedSelectedTabItem, out SelectedTabItemChangedAction))
                SelectedTabItemChangedAction();
        }

        public Dictionary<TTabItem, Action> SelectedTabItemChangedActionList { get; set; }
        //#endregion 2. Commands

        public ViewModelTabBase()
            : base()
        {
            if (Command_SelectTabItem == null) Command_SelectTabItem = new Command<string>(Command_SelectTabItem_Click);
            if (Command_SwipeTabItem == null) Command_SwipeTabItem = new Command<string>(Command_SwipeTabItem_Click);
        }
    }
}

