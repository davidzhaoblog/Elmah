using System;
using System.Collections.Generic;
using System.Text;

namespace Elmah.PetStore.Models
{
    public class Pet: Framework.Models.PropertyChangedNotifier
    {

        private long m_id;
        public long id
        {
            get
            {
                return m_id;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    //ValidateProperty(value);
                    Set(nameof(id), ref m_id, value);
                }
                else
                {
                    m_id = value;
                }
            }
        }

        private string m_name;
        public string name
        {
            get
            {
                return m_name;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    //ValidateProperty(value);
                    Set(nameof(name), ref m_name, value);
                }
                else
                {
                    m_name = value;
                }
            }
        }

        private string m_tag;
        public string tag
        {
            get
            {
                return m_tag;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    //ValidateProperty(value);
                    Set(nameof(tag), ref m_tag, value);
                }
                else
                {
                    m_tag = value;
                }
            }
        }

    }
}

