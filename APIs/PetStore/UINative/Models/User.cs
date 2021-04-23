using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Elmah.PetStore.Models
{
    public class User: Framework.Models.PropertyChangedNotifier
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

        private string m_FirstName;

        [Display(Name = "FirstName", ResourceType = typeof(Elmah.PetStore.Resx.UIStringResource))]
        [RequiredAttribute(ErrorMessageResourceType = typeof(Elmah.PetStore.Resx.UIStringResource), ErrorMessageResourceName="FirstName_is_required")]
        public string FirstName
        {
            get
            {
                return m_FirstName;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    //ValidateProperty(value);
                    Set(nameof(FirstName), ref m_FirstName, value);
                }
                else
                {
                    m_FirstName = value;
                }
            }
        }

        private string m_LastName;

        [Display(Name = "LastName", ResourceType = typeof(Elmah.PetStore.Resx.UIStringResource))]
        [RequiredAttribute(ErrorMessageResourceType = typeof(Elmah.PetStore.Resx.UIStringResource), ErrorMessageResourceName="LastName_is_required")]
        public string LastName
        {
            get
            {
                return m_LastName;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    //ValidateProperty(value);
                    Set(nameof(LastName), ref m_LastName, value);
                }
                else
                {
                    m_LastName = value;
                }
            }
        }

        private string m_Email;

        [Display(Name = "Email", ResourceType = typeof(Elmah.PetStore.Resx.UIStringResource))]
        [RequiredAttribute(ErrorMessageResourceType = typeof(Elmah.PetStore.Resx.UIStringResource), ErrorMessageResourceName="Email_is_required")]
        public string Email
        {
            get
            {
                return m_Email;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    //ValidateProperty(value);
                    Set(nameof(Email), ref m_Email, value);
                }
                else
                {
                    m_Email = value;
                }
            }
        }

        private string m_Password;

        [Display(Name = "Password", ResourceType = typeof(Elmah.PetStore.Resx.UIStringResource))]
        [RequiredAttribute(ErrorMessageResourceType = typeof(Elmah.PetStore.Resx.UIStringResource), ErrorMessageResourceName="Password_is_required")]
        public string Password
        {
            get
            {
                return m_Password;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    //ValidateProperty(value);
                    Set(nameof(Password), ref m_Password, value);
                }
                else
                {
                    m_Password = value;
                }
            }
        }

        private string m_Phone;

        [Display(Name = "Phone", ResourceType = typeof(Elmah.PetStore.Resx.UIStringResource))]
        [RequiredAttribute(ErrorMessageResourceType = typeof(Elmah.PetStore.Resx.UIStringResource), ErrorMessageResourceName="Phone_is_required")]
        public string Phone
        {
            get
            {
                return m_Phone;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    //ValidateProperty(value);
                    Set(nameof(Phone), ref m_Phone, value);
                }
                else
                {
                    m_Phone = value;
                }
            }
        }

        private int m_UserStatus;

        [Display(Name = "UserStatus", ResourceType = typeof(Elmah.PetStore.Resx.UIStringResource))]
        [RequiredAttribute(ErrorMessageResourceType = typeof(Elmah.PetStore.Resx.UIStringResource), ErrorMessageResourceName="UserStatus_is_required")]
        public int UserStatus
        {
            get
            {
                return m_UserStatus;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    //ValidateProperty(value);
                    Set(nameof(UserStatus), ref m_UserStatus, value);
                }
                else
                {
                    m_UserStatus = value;
                }
            }
        }

    }
}

