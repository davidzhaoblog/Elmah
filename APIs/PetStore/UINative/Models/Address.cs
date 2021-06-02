using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Elmah.PetStore.Models
{
    public interface IAddressIdentifier
    {
        string Street { get; set; }
    }

    public class AddressIdentifier
    {
        public string Street { get; set; }
    }

    public class Address: Framework.Models.PropertyChangedNotifier, Framework.Models.IClone<Address>, IAddressIdentifier
    {

        private string m_Street;

        [Display(Name = "Street", ResourceType = typeof(Elmah.PetStore.Resx.UIStringResource))]
        [RequiredAttribute(ErrorMessageResourceType = typeof(Elmah.PetStore.Resx.UIStringResource), ErrorMessageResourceName="Street_is_required")]
        public string Street
        {
            get
            {
                return m_Street;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    //ValidateProperty(value);
                    Set(nameof(Street), ref m_Street, value);
                }
                else
                {
                    m_Street = value;
                }
            }
        }

        private string m_City;

        [Display(Name = "City", ResourceType = typeof(Elmah.PetStore.Resx.UIStringResource))]
        [RequiredAttribute(ErrorMessageResourceType = typeof(Elmah.PetStore.Resx.UIStringResource), ErrorMessageResourceName="City_is_required")]
        public string City
        {
            get
            {
                return m_City;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    //ValidateProperty(value);
                    Set(nameof(City), ref m_City, value);
                }
                else
                {
                    m_City = value;
                }
            }
        }

        private string m_State;

        [Display(Name = "State", ResourceType = typeof(Elmah.PetStore.Resx.UIStringResource))]
        [RequiredAttribute(ErrorMessageResourceType = typeof(Elmah.PetStore.Resx.UIStringResource), ErrorMessageResourceName="State_is_required")]
        public string State
        {
            get
            {
                return m_State;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    //ValidateProperty(value);
                    Set(nameof(State), ref m_State, value);
                }
                else
                {
                    m_State = value;
                }
            }
        }

        private string m_Zip;

        [Display(Name = "Zip", ResourceType = typeof(Elmah.PetStore.Resx.UIStringResource))]
        [RequiredAttribute(ErrorMessageResourceType = typeof(Elmah.PetStore.Resx.UIStringResource), ErrorMessageResourceName="Zip_is_required")]
        public string Zip
        {
            get
            {
                return m_Zip;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    //ValidateProperty(value);
                    Set(nameof(Zip), ref m_Zip, value);
                }
                else
                {
                    m_Zip = value;
                }
            }
        }

        public T GetAClone<T>() where T : Address, new()
        {
            return new T
            {
                Street = Street,
                City = City,
                State = State,
                Zip = Zip
            };
        }

        public void CopyTo<T>(T destination, bool toDatabase = false, bool isInsert = true) where T : Address
        {
            destination.Street = Street;
            destination.City = City;
            destination.State = State;
            destination.Zip = Zip;
        }

        public void CopyFrom<T>(T source) where T : Address
        {
            Street = source.Street;
            City = source.City;
            State = source.State;
            Zip = source.Zip;
        }
    }
}

