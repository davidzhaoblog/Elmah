using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Elmah.PetStore.Models
{
    public interface IOrderIdentifier
    {
        long Id { get; set; }
    }

    public class OrderIdentifier
    {
        public long Id { get; set; }
    }

    public class Order: Framework.Models.PropertyChangedNotifier, Framework.Models.IClone, IOrderIdentifier
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

        private long m_PetId;

        [Display(Name = "PetId", ResourceType = typeof(Elmah.PetStore.Resx.UIStringResource))]
        [RequiredAttribute(ErrorMessageResourceType = typeof(Elmah.PetStore.Resx.UIStringResource), ErrorMessageResourceName="PetId_is_required")]
        public long PetId
        {
            get
            {
                return m_PetId;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    //ValidateProperty(value);
                    Set(nameof(PetId), ref m_PetId, value);
                }
                else
                {
                    m_PetId = value;
                }
            }
        }

        private int m_Quantity;

        [Display(Name = "Quantity", ResourceType = typeof(Elmah.PetStore.Resx.UIStringResource))]
        [RequiredAttribute(ErrorMessageResourceType = typeof(Elmah.PetStore.Resx.UIStringResource), ErrorMessageResourceName="Quantity_is_required")]
        public int Quantity
        {
            get
            {
                return m_Quantity;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    //ValidateProperty(value);
                    Set(nameof(Quantity), ref m_Quantity, value);
                }
                else
                {
                    m_Quantity = value;
                }
            }
        }

        private System.DateTime m_ShipDate;

        [Display(Name = "ShipDate", ResourceType = typeof(Elmah.PetStore.Resx.UIStringResource))]
        [RequiredAttribute(ErrorMessageResourceType = typeof(Elmah.PetStore.Resx.UIStringResource), ErrorMessageResourceName="ShipDate_is_required")]
        public System.DateTime ShipDate
        {
            get
            {
                return m_ShipDate;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    //ValidateProperty(value);
                    Set(nameof(ShipDate), ref m_ShipDate, value);
                }
                else
                {
                    m_ShipDate = value;
                }
            }
        }

        private string m_Status;

        [Display(Name = "Status", ResourceType = typeof(Elmah.PetStore.Resx.UIStringResource))]
        [RequiredAttribute(ErrorMessageResourceType = typeof(Elmah.PetStore.Resx.UIStringResource), ErrorMessageResourceName="Status_is_required")]
        public string Status
        {
            get
            {
                return m_Status;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    //ValidateProperty(value);
                    Set(nameof(Status), ref m_Status, value);
                }
                else
                {
                    m_Status = value;
                }
            }
        }

        private bool m_Complete;

        [Display(Name = "Complete", ResourceType = typeof(Elmah.PetStore.Resx.UIStringResource))]
        [RequiredAttribute(ErrorMessageResourceType = typeof(Elmah.PetStore.Resx.UIStringResource), ErrorMessageResourceName="Complete_is_required")]
        public bool Complete
        {
            get
            {
                return m_Complete;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    //ValidateProperty(value);
                    Set(nameof(Complete), ref m_Complete, value);
                }
                else
                {
                    m_Complete = value;
                }
            }
        }

        public T GetAClone<T>() where T : Order, new()
        {
            return new T
            {
                Id = Id,
                PetId = PetId,
                Quantity = Quantity,
                ShipDate = ShipDate,
                Status = Status,
                Complete = Complete
            };
        }

        public void CopyTo<T>(T destination, bool toDatabase = false, bool isInsert = true) where T : Order
        {
            destination.Id = Id;
            destination.PetId = PetId;
            destination.Quantity = Quantity;
            destination.ShipDate = ShipDate;
            destination.Status = Status;
            destination.Complete = Complete;
        }

        public void CopyFrom<T>(T source) where T : Order
        {
            Id = source.Id;
            PetId = source.PetId;
            Quantity = source.Quantity;
            ShipDate = source.ShipDate;
            Status = source.Status;
            Complete = source.Complete;
        }
    }
}

