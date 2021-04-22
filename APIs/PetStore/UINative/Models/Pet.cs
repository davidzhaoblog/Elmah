using System;
using System.Collections.Generic;
using System.Text;

namespace Elmah.PetStore.Models
{
    public class Pet: Framework.Models.PropertyChangedNotifier
    {

        private long m_Id;

        [RequiredAttribute(ErrorMessageResourceType = typeof(abcdefg.wwww), ErrorMessageResourceName="Id_is_required")]
        public long Id
        {
            get
            {
                return m_Id;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    //ValidateProperty(value);
                    Set(nameof(Id), ref m_Id, value);
                }
                else
                {
                    m_Id = value;
                }
            }
        }

        private string m_Name;

        [RequiredAttribute(ErrorMessageResourceType = typeof(abcdefg.wwww), ErrorMessageResourceName="Name_is_required")]
        public string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    //ValidateProperty(value);
                    Set(nameof(Name), ref m_Name, value);
                }
                else
                {
                    m_Name = value;
                }
            }
        }

        private string m_Tag;

        [RequiredAttribute(ErrorMessageResourceType = typeof(abcdefg.wwww), ErrorMessageResourceName="Tag_is_required")]
        public string Tag
        {
            get
            {
                return m_Tag;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    //ValidateProperty(value);
                    Set(nameof(Tag), ref m_Tag, value);
                }
                else
                {
                    m_Tag = value;
                }
            }
        }

    }
}

