using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Framework.Xaml.ActionForm
{
    public class ActionItemModel: Framework.Models.PropertyChangedNotifier
    {
        private Framework.Xaml.ActionForm.ActionFormItemTypes _ActionFormItemType = Framework.Xaml.ActionForm.ActionFormItemTypes.NavigationItem;
        public Framework.Xaml.ActionForm.ActionFormItemTypes ActionFormItemType
        {
            get { return _ActionFormItemType; }
            set { Set(nameof(ActionFormItemType), ref _ActionFormItemType, value); }
        }

        private byte m_Position;
        public byte Position
        {
            get { return m_Position; }
            set
            {
                Set(nameof(Position), ref m_Position, value);
            }
        }

        private string m_AutomationId;
        public string AutomationId
        {
            get { return m_AutomationId; }
            set
            {
                Set(nameof(AutomationId), ref m_AutomationId, value);
            }
        }

        private bool m_ShowTitle = true;
        public bool ShowTitle
        {
            get { return m_ShowTitle; }
            set
            {
                Set(nameof(ShowTitle), ref m_ShowTitle, value);
            }
        }

        private string _Title;
        public string Title
        {
            get { return _Title; }
            set { Set(nameof(Title), ref _Title, value); }
        }

        private bool m_ShowDescription = true;
        public bool ShowDescription
        {
            get { return m_ShowDescription; }
            set
            {
                Set(nameof(ShowDescription), ref m_ShowDescription, value);
            }
        }

        private string _Description;
        public string Description
        {
            get { return _Description; }
            set { Set(nameof(Description), ref _Description, value); }
        }

        private bool m_ShowFontIcon = true;
        public bool ShowFontIcon
        {
            get { return m_ShowFontIcon; }
            set
            {
                Set(nameof(ShowFontIcon), ref m_ShowFontIcon, value);
            }
        }

        private Framework.Xaml.FontIconSettings m_FontIconSettings;
        public Framework.Xaml.FontIconSettings FontIconSettings
        {
            get { return m_FontIconSettings; }
            set
            {
                Set(nameof(FontIconSettings), ref m_FontIconSettings, value);
            }
        }

        private string m_InfoFontIconUpdateMessageTitle;
        public string InfoFontIconUpdateMessageTitle
        {
            get { return m_InfoFontIconUpdateMessageTitle; }
            set
            {
                Set(nameof(InfoFontIconUpdateMessageTitle), ref m_InfoFontIconUpdateMessageTitle, value);
            }
        }

        private ICommand m_Command;
        public ICommand Command
        {
            get { return m_Command; }
            set
            {
                Set(nameof(Command), ref m_Command, value);
            }
        }

        private object m_CommandParameter;
        public object CommandParameter
        {
            get { return m_CommandParameter; }
            set
            {
                Set(nameof(CommandParameter), ref m_CommandParameter, value);
            }
        }

        private ICommand _NavigationCommand;
        public ICommand NavigationCommand
        {
            get { return _NavigationCommand; }
            set { Set(nameof(NavigationCommand), ref _NavigationCommand, value); }
        }

        private object _NavigationCommandParam;
        public object NavigationCommandParam
        {
            get { return _NavigationCommandParam; }
            set { Set(nameof(NavigationCommandParam), ref _NavigationCommandParam, value); }
        }

        private bool m_ShowRightArrow = true;
        public bool ShowRightArrow
        {
            get { return m_ShowRightArrow; }
            set
            {
                Set(nameof(ShowRightArrow), ref m_ShowRightArrow, value);
            }
        }

        public ContentView CustomControlInstance { get; set; }
    }
}

