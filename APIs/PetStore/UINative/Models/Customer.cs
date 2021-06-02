using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Elmah.PetStore.Models
{
    public interface ICustomerIdentifier
    {
        long Id { get; set; }
    }

    public class CustomerIdentifier
    {
        public long Id { get; set; }
    }

    public class Customer: Framework.Models.PropertyChangedNotifier, Framework.Models.IClone<Customer>, ICustomerIdentifier
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

        public T GetAClone<T>() where T : Customer, new()
        {
            return new T
            {
                Id = Id,
                Username = Username,
                Address = Address
            };
        }

        public void CopyTo<T>(T destination, bool toDatabase = false, bool isInsert = true) where T : Customer
        {
            destination.Id = Id;
            destination.Username = Username;
            destination.Address = Address;
        }

        public void CopyFrom<T>(T source) where T : Customer
        {
            Id = source.Id;
            Username = source.Username;
            Address = source.Address;
        }
    }
}

