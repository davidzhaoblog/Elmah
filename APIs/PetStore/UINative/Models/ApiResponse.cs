using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Elmah.PetStore.Models
{
    public interface IApiResponseIdentifier
    {
        int Code { get; set; }
    }

    public class ApiResponseIdentifier
    {
        public int Code { get; set; }
    }

    public class ApiResponse: Framework.Models.PropertyChangedNotifier, Framework.Models.IClone<ApiResponse>, IApiResponseIdentifier
    {

        private int m_Code;

        [Display(Name = "Code", ResourceType = typeof(Elmah.PetStore.Resx.UIStringResource))]
        [RequiredAttribute(ErrorMessageResourceType = typeof(Elmah.PetStore.Resx.UIStringResource), ErrorMessageResourceName="Code_is_required")]
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

        private string m_Type;

        [Display(Name = "Type", ResourceType = typeof(Elmah.PetStore.Resx.UIStringResource))]
        [RequiredAttribute(ErrorMessageResourceType = typeof(Elmah.PetStore.Resx.UIStringResource), ErrorMessageResourceName="Type_is_required")]
        public string Type
        {
            get
            {
                return m_Type;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    //ValidateProperty(value);
                    Set(nameof(Type), ref m_Type, value);
                }
                else
                {
                    m_Type = value;
                }
            }
        }

        private string m_Message;

        [Display(Name = "Message", ResourceType = typeof(Elmah.PetStore.Resx.UIStringResource))]
        [RequiredAttribute(ErrorMessageResourceType = typeof(Elmah.PetStore.Resx.UIStringResource), ErrorMessageResourceName="Message_is_required")]
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

        public T GetAClone<T>() where T : ApiResponse, new()
        {
            return new T
            {
                Code = Code,
                Type = Type,
                Message = Message
            };
        }

        public void CopyTo<T>(T destination, bool toDatabase = false, bool isInsert = true) where T : ApiResponse
        {
            destination.Code = Code;
            destination.Type = Type;
            destination.Message = Message;
        }

        public void CopyFrom<T>(T source) where T : ApiResponse
        {
            Code = source.Code;
            Type = source.Type;
            Message = source.Message;
        }
    }
}

