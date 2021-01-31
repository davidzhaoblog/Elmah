using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Xaml
{
    public enum NavigationActionTypes
    {
        CloseButtonCloseItemControlPopup,
        CloseButtonNavBack,
        ListFooterActionSheet,
        ListSelectionActions, //DoneSelection, ClearSelections
        ItemFooterActionSheet,
        //WizardActions,
    }

    public class NavigationButtonsVM: _ViewModelBase
    {
        private Framework.Xaml.ActionForm.ActionSheetVM m_ListFooterActionSheet;
        public Framework.Xaml.ActionForm.ActionSheetVM ListFooterActionSheet
        {
            get { return m_ListFooterActionSheet; }
            set
            {
                Set(nameof(ListFooterActionSheet), ref m_ListFooterActionSheet, value);
            }
        }

        public void ShowGeneralListActions(Framework.Xaml.ActionForm.ActionSheetVM footerActionSheet)
        {
            ListFooterActionSheet = footerActionSheet;
        }

        public void ShowListSelectionActions()
        {
            ListFooterActionSheet = ListFooterActionSheet;
        }

        public void ShowItemActions(Framework.Xaml.NavigationActionTypes closeAction = NavigationActionTypes.CloseButtonCloseItemControlPopup, Framework.Xaml.ActionForm.ActionSheetVM footerActionSheet = null)
        {
            ListFooterActionSheet = ListFooterActionSheet;
        }
    }
}

