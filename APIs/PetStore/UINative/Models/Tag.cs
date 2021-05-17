using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Elmah.PetStore.Models
{
    public interface ITagIdentifier
    {
        long Id { get; set; }
    }

    public class TagIdentifier
    {
        public long Id { get; set; }
    }

    public class Tag: Framework.Models.PropertyChangedNotifier, Framework.Models.IClone, ITagIdentifier
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

        public T GetAClone<T>() where T : Tag, new()
        {
            return new T
            {
                Id = Id,
                Name = Name
            };
        }

        public void CopyTo<T>(T destination, bool toDatabase = false, bool isInsert = true) where T : Tag
        {
            destination.Id = Id;
            destination.Name = Name;
        }

        public void CopyFrom<T>(T source) where T : Tag
        {
            Id = source.Id;
            Name = source.Name;
        }
    }
}

