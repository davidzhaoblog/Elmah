using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Framework.Xaml.ActionForm
{
    public class ActionSheetVM: _ViewModelBase
    {
        private string m_Title;
        public string Title
        {
            get { return m_Title; }
            set
            {
                Set(nameof(Title), ref m_Title, value);
            }
        }

        private int m_DisplayOrder;
        public int DisplayOrder
        {
            get { return m_DisplayOrder; }
            set
            {
                Set(nameof(DisplayOrder), ref m_DisplayOrder, value);
            }
        }

        private bool m_HasIcon;
        public bool HasIcon
        {
            get { return m_HasIcon; }
            set
            {
                Set(nameof(HasIcon), ref m_HasIcon, value);
            }
        }

        private ObservableCollection<Framework.Xaml.ActionForm.ActionItemModel> m_ActionItems;
        public ObservableCollection<Framework.Xaml.ActionForm.ActionItemModel> ActionItems
        {
            get { return m_ActionItems; }
            set
            {
                Set(nameof(ActionItems), ref m_ActionItems, value);
            }
        }

        private ObservableCollection<Framework.Xaml.ActionForm.ActionItemModel> m_StartActionItems;
        public ObservableCollection<Framework.Xaml.ActionForm.ActionItemModel> StartActionItems
        {
            get { return m_StartActionItems; }
            set
            {
                Set(nameof(StartActionItems), ref m_StartActionItems, value);
            }
        }

        private ObservableCollection<Framework.Xaml.ActionForm.ActionItemModel> m_CenterActionItems;
        public ObservableCollection<Framework.Xaml.ActionForm.ActionItemModel> CenterActionItems
        {
            get { return m_CenterActionItems; }
            set
            {
                Set(nameof(CenterActionItems), ref m_CenterActionItems, value);
            }
        }

        private ObservableCollection<Framework.Xaml.ActionForm.ActionItemModel> m_EndActionItems;
        public ObservableCollection<Framework.Xaml.ActionForm.ActionItemModel> EndActionItems
        {
            get { return m_EndActionItems; }
            set
            {
                Set(nameof(EndActionItems), ref m_EndActionItems, value);
            }
        }

        public override void Cleanup()
        {
            ActionItems?.Clear();
            CenterActionItems?.Clear();
            EndActionItems?.Clear();
            StartActionItems?.Clear();
        }
    }
}

