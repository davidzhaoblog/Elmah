using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Elmah.PetStore.Models
{
    public class Order: Framework.Models.PropertyChangedNotifier
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

        public Order GetAClone()
        {
            return new Order
            {
                Id = Id,
                PetId = PetId,
                Quantity = Quantity,
                ShipDate = ShipDate,
                Status = Status,
                Complete = Complete
            };
        }
    }
}

