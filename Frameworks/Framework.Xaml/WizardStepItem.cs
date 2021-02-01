using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Xaml
{
    public class WizardStepItem : _ViewModelBase
    {
        private int _WizardStepIndex;
        public int WizardStepIndex
        {
            get { return _WizardStepIndex; }
            set { Set(nameof(WizardStepIndex), ref _WizardStepIndex, value); }
        }

        private bool _Active;
        public bool Active
        {
            get { return _Active; }
            set { Set(nameof(Active), ref _Active, value); }
        }
    }
}

