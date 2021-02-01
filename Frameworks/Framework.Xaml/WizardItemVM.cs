using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Xaml
{
    public class WizardItemVM : ViewModelBase
    {
        private Framework.Xaml.WizardStepItem _WizardStepItem;
        public Framework.Xaml.WizardStepItem WizardStepItem
        {
            get { return _WizardStepItem; }
            set { Set(nameof(WizardStepItem), ref _WizardStepItem, value); }
        }

        private string m_Heading;
        public string Heading
        {
            get { return m_Heading; }
            set
            {
                Set(nameof(Heading), ref m_Heading, value);
            }
        }

        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { Set(nameof(Name), ref _Name, value); }
        }

        private int _WizardItemIndex;
        public int WizardItemIndex
        {
            get { return _WizardItemIndex; }
            set { Set(nameof(WizardItemIndex), ref _WizardItemIndex, value); }
        }

        private int _WizardStepIndex;
        public int WizardStepIndex
        {
            get { return _WizardStepIndex; }
            set { Set(nameof(WizardStepIndex), ref _WizardStepIndex, value); }
        }

        private bool _Mandatory;
        public bool Mandatory
        {
            get { return _Mandatory; }
            set { Set(nameof(Mandatory), ref _Mandatory, value); }
        }

        private bool _Completed;
        public bool Completed
        {
            get { return _Completed; }
            set { Set(nameof(Completed), ref _Completed, value); }
        }

        private bool _Active;
        public bool Active
        {
            get { return _Active; }
            set { Set(nameof(Active), ref _Active, value); }
        }

        // navigation
        private bool _HasGoBackwardButton;
        public bool HasGoBackwardButton
        {
            get { return _HasGoBackwardButton; }
            set { Set(nameof(HasGoBackwardButton), ref _HasGoBackwardButton, value); }
        }

        private bool _HasGoForwardButton;
        public bool HasGoForwardButton
        {
            get { return _HasGoForwardButton; }
            set { Set(nameof(HasGoForwardButton), ref _HasGoForwardButton, value); }
        }

        private bool _HasSkipButton;
        public bool HasSkipButton
        {
            get { return _HasSkipButton; }
            set { Set(nameof(HasSkipButton), ref _HasSkipButton, value); }
        }

        private bool _HasFinishButton;
        public bool HasFinishButton
        {
            get { return _HasFinishButton; }
            set { Set(nameof(HasFinishButton), ref _HasFinishButton, value); }
        }

        private bool m_HasCloseButton = false;
        public bool HasCloseButton
        {
            get { return m_HasCloseButton; }
            set
            {
                Set(nameof(HasCloseButton), ref m_HasCloseButton, value);
            }
        }

        private bool m_EnableCancelButton = false;
        public bool EnableCancelButton
        {
            get { return m_EnableCancelButton; }
            set
            {
                Set(nameof(EnableCancelButton), ref m_EnableCancelButton, value);
            }
        }

        public Func<Task<bool>> LoadData;
        public Func<Task<bool>> GoForword_Clicked;
        public Action DefaultData;
        public Func<bool> EnableGoForward;
    }
}

