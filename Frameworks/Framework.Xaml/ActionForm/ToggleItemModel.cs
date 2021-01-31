using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Xaml.ActionForm
{
    public class ToggleItemModel : ActionItemModel
    {
        private Framework.Models.ToggleStatus m_ToggleStatus;
        public Framework.Models.ToggleStatus ToggleStatus
        {
            get { return m_ToggleStatus; }
            set
            {
                Set(nameof(ToggleStatus), ref m_ToggleStatus, value);
            }
        }

        public void Toggle()
        {
            ToggleStatus = ToggleStatus == Framework.Models.ToggleStatus.On ? Framework.Models.ToggleStatus.Off : Framework.Models.ToggleStatus.On;
        }
    }
}

