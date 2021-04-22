using System;
using System.Collections.Generic;
using System.Text;

namespace Elmah.PetStore.Models
{
    public class Error: Framework.Models.PropertyChangedNotifier
    {

        private int m_code;
        public int code
        {
            get
            {
                return m_code;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    //ValidateProperty(value);
                    Set(nameof(code), ref m_code, value);
                }
                else
                {
                    m_code = value;
                }
            }
        }

        private string m_message;
        public string message
        {
            get
            {
                return m_message;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    //ValidateProperty(value);
                    Set(nameof(message), ref m_message, value);
                }
                else
                {
                    m_message = value;
                }
            }
        }

    }
}

