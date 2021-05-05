using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Elmah.PetStore.Models
{
    public class Category: Framework.Models.PropertyChangedNotifier
    {

        private long m_Id;

        [Display(Name = "Id", ResourceType = typeof(Elmah.PetStore.Resx.UIStringResource))]
        [RequiredAttribute(ErrorMessageResourceType = typeof(Elmah.PetStore.Resx.UIStringResource), ErrorMessageResourceName="Id_is_required")]
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

        [Display(Name = "Name", ResourceType = typeof(Elmah.PetStore.Resx.UIStringResource))]
        [RequiredAttribute(ErrorMessageResourceType = typeof(Elmah.PetStore.Resx.UIStringResource), ErrorMessageResourceName="Name_is_required")]
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

        public Category GetAClone()
        {
            return new Category
            {
                Id = Id,
                Name = Name
            };
        }
    }
}

