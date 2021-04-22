using System;
using System.Collections.Generic;
using System.Text;

namespace Elmah.PetStore.Models
{
    public class Error: Framework.Models.PropertyChangedNotifier
    {

        private int m_Code;
        public int Code
        {
            get
            {
                return m_Code;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    //ValidateProperty(value);
                    Set(nameof(Code), ref m_Code, value);
                }
                else
                {
                    m_Code = value;
                }
            }
        }

        private string m_Message;
        public string Message
        {
            get
            {
                return m_Message;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    //ValidateProperty(value);
                    Set(nameof(Message), ref m_Message, value);
                }
                else
                {
                    m_Message = value;
                }
            }
        }

    }
}

