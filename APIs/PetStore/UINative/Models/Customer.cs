using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Elmah.PetStore.Models
{
    public class Customer: Framework.Models.PropertyChangedNotifier
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

        private string m_Username;

        [Display(Name = "Username", ResourceType = typeof(Elmah.PetStore.Resx.UIStringResource))]
        [RequiredAttribute(ErrorMessageResourceType = typeof(Elmah.PetStore.Resx.UIStringResource), ErrorMessageResourceName="Username_is_required")]
        public string Username
        {
            get
            {
                return m_Username;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    //ValidateProperty(value);
                    Set(nameof(Username), ref m_Username, value);
                }
                else
                {
                    m_Username = value;
                }
            }
        }

        private Elmah.PetStore.Models.Address[] m_Address;

        [Display(Name = "Address", ResourceType = typeof(Elmah.PetStore.Resx.UIStringResource))]
        [RequiredAttribute(ErrorMessageResourceType = typeof(Elmah.PetStore.Resx.UIStringResource), ErrorMessageResourceName="Address_is_required")]
        public Elmah.PetStore.Models.Address[] Address
        {
            get
            {
                return m_Address;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    //ValidateProperty(value);
                    Set(nameof(Address), ref m_Address, value);
                }
                else
                {
                    m_Address = value;
                }
            }
        }

        public Customer GetAClone()
        {
            return new Customer
            {
                Id = Id,
                Username = Username,
                Address = Address
            };
        }
    }
}

