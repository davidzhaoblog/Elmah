using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Elmah.DataSourceEntities
{
    /// <summary>
    /// Entity class, used across the solution. <see cref="ELMAH_Error"/>
    /// </summary>
    //[DataContract]
    public partial class ELMAH_Error : Framework.Models.PropertyChangedNotifier, Elmah.EntityContracts.IELMAH_Error, Framework.Models.IClone<Elmah.EntityContracts.IELMAH_Error>
    {

        #region Storage Fields

        System.Guid m_ErrorId;

        System.String m_Application;

        System.String m_Host;

        System.String m_Type;

        System.String m_Source;

        System.String m_Message;

        System.String m_User;

        System.Int32 m_StatusCode;

        System.DateTime m_TimeUtc = System.DateTime.Now;

        System.Int32 m_Sequence;

        System.String m_AllXml;

        #endregion Storage Fields

        #region properties

                //[DataMember]
        [Display(Name = "ErrorId", ResourceType = typeof(Elmah.Resx.UIStringResourcePerEntity))]
        [RequiredAttribute(ErrorMessageResourceType = typeof(Elmah.Resx.UIStringResourcePerEntity), ErrorMessageResourceName="ErrorId_is_required")]
        public System.Guid ErrorId
        {
            get
            {
                return m_ErrorId;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    //ValidateProperty(value);
                    Set(nameof(ErrorId), ref m_ErrorId, value);
                }
                else
                {
                    m_ErrorId = value;
                }
            }
        }

                //[DataMember]
        [Display(Name = "ElmahApplication", ResourceType = typeof(Elmah.Resx.UIStringResourcePerEntity))]
        [StringLengthAttribute(60, ErrorMessageResourceType = typeof(Elmah.Resx.UIStringResourcePerEntity), ErrorMessageResourceName="The_length_of_Application_should_be_1_to_60", MinimumLength = 1)]
        public System.String Application
        {
            get
            {
                return m_Application;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    ValidateProperty(value);
                    Set(nameof(Application), ref m_Application, value);
                }
                else
                {
                    m_Application = value;
                }
            }
        }

                //[DataMember]
        [Display(Name = "ElmahHost", ResourceType = typeof(Elmah.Resx.UIStringResourcePerEntity))]
        [StringLengthAttribute(50, ErrorMessageResourceType = typeof(Elmah.Resx.UIStringResourcePerEntity), ErrorMessageResourceName="The_length_of_Host_should_be_1_to_50", MinimumLength = 1)]
        public System.String Host
        {
            get
            {
                return m_Host;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    ValidateProperty(value);
                    Set(nameof(Host), ref m_Host, value);
                }
                else
                {
                    m_Host = value;
                }
            }
        }

                //[DataMember]
        [Display(Name = "ElmahType", ResourceType = typeof(Elmah.Resx.UIStringResourcePerEntity))]
        [StringLengthAttribute(100, ErrorMessageResourceType = typeof(Elmah.Resx.UIStringResourcePerEntity), ErrorMessageResourceName="The_length_of_Type_should_be_1_to_100", MinimumLength = 1)]
        public System.String Type
        {
            get
            {
                return m_Type;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    ValidateProperty(value);
                    Set(nameof(Type), ref m_Type, value);
                }
                else
                {
                    m_Type = value;
                }
            }
        }

                //[DataMember]
        [Display(Name = "ElmahSource", ResourceType = typeof(Elmah.Resx.UIStringResourcePerEntity))]
        [StringLengthAttribute(60, ErrorMessageResourceType = typeof(Elmah.Resx.UIStringResourcePerEntity), ErrorMessageResourceName="The_length_of_Source_should_be_1_to_60", MinimumLength = 1)]
        public System.String Source
        {
            get
            {
                return m_Source;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    ValidateProperty(value);
                    Set(nameof(Source), ref m_Source, value);
                }
                else
                {
                    m_Source = value;
                }
            }
        }

                //[DataMember]
        [Display(Name = "Message", ResourceType = typeof(Elmah.Resx.UIStringResourcePerEntity))]
        [StringLengthAttribute(500, ErrorMessageResourceType = typeof(Elmah.Resx.UIStringResourcePerEntity), ErrorMessageResourceName="The_length_of_Message_should_be_1_to_500", MinimumLength = 1)]
        public System.String Message
        {
            get
            {
                return m_Message;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    ValidateProperty(value);
                    Set(nameof(Message), ref m_Message, value);
                }
                else
                {
                    m_Message = value;
                }
            }
        }

                //[DataMember]
        [Display(Name = "ElmahUser", ResourceType = typeof(Elmah.Resx.UIStringResourcePerEntity))]
        [StringLengthAttribute(50, ErrorMessageResourceType = typeof(Elmah.Resx.UIStringResourcePerEntity), ErrorMessageResourceName="The_length_of_User_should_be_1_to_50", MinimumLength = 1)]
        public System.String User
        {
            get
            {
                return m_User;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    ValidateProperty(value);
                    Set(nameof(User), ref m_User, value);
                }
                else
                {
                    m_User = value;
                }
            }
        }

                //[DataMember]
        [Display(Name = "ElmahStatusCode", ResourceType = typeof(Elmah.Resx.UIStringResourcePerEntity))]
        [RequiredAttribute(ErrorMessageResourceType = typeof(Elmah.Resx.UIStringResourcePerEntity), ErrorMessageResourceName="StatusCode_is_required")]
        public System.Int32 StatusCode
        {
            get
            {
                return m_StatusCode;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    ValidateProperty(value);
                    Set(nameof(StatusCode), ref m_StatusCode, value);
                }
                else
                {
                    m_StatusCode = value;
                }
            }
        }

                //[DataMember]
        [Display(Name = "TimeUtc", ResourceType = typeof(Elmah.Resx.UIStringResourcePerEntity))]
        [RequiredAttribute(ErrorMessageResourceType = typeof(Elmah.Resx.UIStringResourcePerEntity), ErrorMessageResourceName="TimeUtc_is_required")]
        public System.DateTime TimeUtc
        {
            get
            {
                return m_TimeUtc;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    ValidateProperty(value);
                    Set(nameof(TimeUtc), ref m_TimeUtc, value);
                }
                else
                {
                    m_TimeUtc = value;
                }
            }
        }

                //[DataMember]
        [Display(Name = "Sequence", ResourceType = typeof(Elmah.Resx.UIStringResourcePerEntity))]
        public System.Int32 Sequence
        {
            get
            {
                return m_Sequence;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    //ValidateProperty(value);
                    Set(nameof(Sequence), ref m_Sequence, value);
                }
                else
                {
                    m_Sequence = value;
                }
            }
        }

                //[DataMember]
        [Display(Name = "AllXml", ResourceType = typeof(Elmah.Resx.UIStringResourcePerEntity))]
        public System.String AllXml
        {
            get
            {
                return m_AllXml;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    ValidateProperty(value);
                    Set(nameof(AllXml), ref m_AllXml, value);
                }
                else
                {
                    m_AllXml = value;
                }
            }
        }

        #endregion properties

        #region override methods
/*
        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            throw new NotImplementedExecption();
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            bool _retval = obj is ELMAH_Error;
            if (_retval == true)
            {
                throw new NotImplementedExecption();
            }
            return _retval;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
*/
        #endregion override methods

        public T GetAClone<T>()
            where T: Elmah.EntityContracts.IELMAH_Error, new()
        {
            var cloned = new T();
            CopyTo<T>(cloned);
            return cloned;
        }

        public void CopyTo<T>(T destination, bool toDatabase = false, bool isInsert = true)
            where T: Elmah.EntityContracts.IELMAH_Error
        {
            destination.ErrorId = ErrorId;

            destination.Application = Application;

            destination.Host = Host;

            destination.Type = Type;

            destination.Source = Source;

            destination.Message = Message;

            destination.User = User;

            destination.StatusCode = StatusCode;

            destination.TimeUtc = TimeUtc;

            destination.Sequence = Sequence;

            destination.AllXml = AllXml;

        }

        public void CopyFrom<T>(T source)
            where T: Elmah.EntityContracts.IELMAH_Error
        {
            ErrorId = source.ErrorId;

            Application = source.Application;

            Host = source.Host;

            Type = source.Type;

            Source = source.Source;

            Message = source.Message;

            User = source.User;

            StatusCode = source.StatusCode;

            TimeUtc = source.TimeUtc;

            Sequence = source.Sequence;

            AllXml = source.AllXml;

        }

        #region Nested Views classes and their collection classes 1

        /// <summary>
        /// View "Default" class of <see cref="ELMAH_Error"/>, used across the solution.
        /// </summary>
        public partial class Default :Framework.Models.PropertyChangedNotifier, Elmah.EntityContracts.IELMAH_Error, Framework.Models.IClone<Default>
        {

            #region Storage Fields

        System.String m_ElmahApplication_Name;

        System.Guid m_ErrorId;

        System.String m_ElmahHost_Name;

        System.String m_ElmahSource_Name;

        System.String m_ElmahStatusCode_Name;

        System.String m_ElmahType_Name;

        System.String m_ElmahUser_Name;

        System.String m_Application;

        System.String m_Host;

        System.String m_Type;

        System.String m_Source;

        System.String m_Message;

        System.String m_User;

        System.Int32 m_StatusCode;

        System.DateTime m_TimeUtc = System.DateTime.Now;

        System.Int32 m_Sequence;

        System.String m_AllXml;

            #endregion Storage Fields

            #region properties

                    //[DataMember]
        [Display(Name = "Application", ResourceType = typeof(Elmah.Resx.UIStringResourcePerEntity))]
        public System.String ElmahApplication_Name
        {
            get
            {
                return m_ElmahApplication_Name;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    //ValidateProperty(value);
                    Set(nameof(ElmahApplication_Name), ref m_ElmahApplication_Name, value);
                }
                else
                {
                    m_ElmahApplication_Name = value;
                }
            }
        }

                    //[DataMember]
        [Display(Name = "ErrorId", ResourceType = typeof(Elmah.Resx.UIStringResourcePerEntity))]
        [RequiredAttribute(ErrorMessageResourceType = typeof(Elmah.Resx.UIStringResourcePerEntity), ErrorMessageResourceName="ErrorId_is_required")]
        public System.Guid ErrorId
        {
            get
            {
                return m_ErrorId;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    //ValidateProperty(value);
                    Set(nameof(ErrorId), ref m_ErrorId, value);
                }
                else
                {
                    m_ErrorId = value;
                }
            }
        }

                    //[DataMember]
        [Display(Name = "Host", ResourceType = typeof(Elmah.Resx.UIStringResourcePerEntity))]
        public System.String ElmahHost_Name
        {
            get
            {
                return m_ElmahHost_Name;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    //ValidateProperty(value);
                    Set(nameof(ElmahHost_Name), ref m_ElmahHost_Name, value);
                }
                else
                {
                    m_ElmahHost_Name = value;
                }
            }
        }

                    //[DataMember]
        [Display(Name = "Source", ResourceType = typeof(Elmah.Resx.UIStringResourcePerEntity))]
        public System.String ElmahSource_Name
        {
            get
            {
                return m_ElmahSource_Name;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    //ValidateProperty(value);
                    Set(nameof(ElmahSource_Name), ref m_ElmahSource_Name, value);
                }
                else
                {
                    m_ElmahSource_Name = value;
                }
            }
        }

                    //[DataMember]
        [Display(Name = "Name", ResourceType = typeof(Elmah.Resx.UIStringResourcePerEntity))]
        public System.String ElmahStatusCode_Name
        {
            get
            {
                return m_ElmahStatusCode_Name;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    //ValidateProperty(value);
                    Set(nameof(ElmahStatusCode_Name), ref m_ElmahStatusCode_Name, value);
                }
                else
                {
                    m_ElmahStatusCode_Name = value;
                }
            }
        }

                    //[DataMember]
        [Display(Name = "Type", ResourceType = typeof(Elmah.Resx.UIStringResourcePerEntity))]
        public System.String ElmahType_Name
        {
            get
            {
                return m_ElmahType_Name;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    //ValidateProperty(value);
                    Set(nameof(ElmahType_Name), ref m_ElmahType_Name, value);
                }
                else
                {
                    m_ElmahType_Name = value;
                }
            }
        }

                    //[DataMember]
        [Display(Name = "User", ResourceType = typeof(Elmah.Resx.UIStringResourcePerEntity))]
        public System.String ElmahUser_Name
        {
            get
            {
                return m_ElmahUser_Name;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    //ValidateProperty(value);
                    Set(nameof(ElmahUser_Name), ref m_ElmahUser_Name, value);
                }
                else
                {
                    m_ElmahUser_Name = value;
                }
            }
        }

                    //[DataMember]
        [Display(Name = "ElmahApplication", ResourceType = typeof(Elmah.Resx.UIStringResourcePerEntity))]
        public System.String Application
        {
            get
            {
                return m_Application;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    //ValidateProperty(value);
                    Set(nameof(Application), ref m_Application, value);
                }
                else
                {
                    m_Application = value;
                }
            }
        }

                    //[DataMember]
        [Display(Name = "ElmahHost", ResourceType = typeof(Elmah.Resx.UIStringResourcePerEntity))]
        public System.String Host
        {
            get
            {
                return m_Host;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    //ValidateProperty(value);
                    Set(nameof(Host), ref m_Host, value);
                }
                else
                {
                    m_Host = value;
                }
            }
        }

                    //[DataMember]
        [Display(Name = "ElmahType", ResourceType = typeof(Elmah.Resx.UIStringResourcePerEntity))]
        public System.String Type
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

                    //[DataMember]
        [Display(Name = "ElmahSource", ResourceType = typeof(Elmah.Resx.UIStringResourcePerEntity))]
        public System.String Source
        {
            get
            {
                return m_Source;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    //ValidateProperty(value);
                    Set(nameof(Source), ref m_Source, value);
                }
                else
                {
                    m_Source = value;
                }
            }
        }

                    //[DataMember]
        [Display(Name = "Message", ResourceType = typeof(Elmah.Resx.UIStringResourcePerEntity))]
        [StringLengthAttribute(500, ErrorMessageResourceType = typeof(Elmah.Resx.UIStringResourcePerEntity), ErrorMessageResourceName="The_length_of_Message_should_be_1_to_500", MinimumLength = 1)]
        public System.String Message
        {
            get
            {
                return m_Message;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    ValidateProperty(value);
                    Set(nameof(Message), ref m_Message, value);
                }
                else
                {
                    m_Message = value;
                }
            }
        }

                    //[DataMember]
        [Display(Name = "ElmahUser", ResourceType = typeof(Elmah.Resx.UIStringResourcePerEntity))]
        public System.String User
        {
            get
            {
                return m_User;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    //ValidateProperty(value);
                    Set(nameof(User), ref m_User, value);
                }
                else
                {
                    m_User = value;
                }
            }
        }

                    //[DataMember]
        [Display(Name = "ElmahStatusCode", ResourceType = typeof(Elmah.Resx.UIStringResourcePerEntity))]
        public System.Int32 StatusCode
        {
            get
            {
                return m_StatusCode;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    //ValidateProperty(value);
                    Set(nameof(StatusCode), ref m_StatusCode, value);
                }
                else
                {
                    m_StatusCode = value;
                }
            }
        }

                    //[DataMember]
        [Display(Name = "TimeUtc", ResourceType = typeof(Elmah.Resx.UIStringResourcePerEntity))]
        [RequiredAttribute(ErrorMessageResourceType = typeof(Elmah.Resx.UIStringResourcePerEntity), ErrorMessageResourceName="TimeUtc_is_required")]
        public System.DateTime TimeUtc
        {
            get
            {
                return m_TimeUtc;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    ValidateProperty(value);
                    Set(nameof(TimeUtc), ref m_TimeUtc, value);
                }
                else
                {
                    m_TimeUtc = value;
                }
            }
        }

                    //[DataMember]
        [Display(Name = "Sequence", ResourceType = typeof(Elmah.Resx.UIStringResourcePerEntity))]
        public System.Int32 Sequence
        {
            get
            {
                return m_Sequence;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    //ValidateProperty(value);
                    Set(nameof(Sequence), ref m_Sequence, value);
                }
                else
                {
                    m_Sequence = value;
                }
            }
        }

                    //[DataMember]
        [Display(Name = "AllXml", ResourceType = typeof(Elmah.Resx.UIStringResourcePerEntity))]
        public System.String AllXml
        {
            get
            {
                return m_AllXml;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    ValidateProperty(value);
                    Set(nameof(AllXml), ref m_AllXml, value);
                }
                else
                {
                    m_AllXml = value;
                }
            }
        }

            #endregion properties

            public T GetAClone<T>()
                where T: Default, new()
            {
                var cloned = new T();
                CopyTo<T>(cloned);
                return cloned;
            }

            public void CopyTo<T>(T destination, bool toDatabase = false, bool isInsert = true)
                where T : Default
            {
                destination.ElmahApplication_Name = ElmahApplication_Name;

            destination.ErrorId = ErrorId;

            destination.ElmahHost_Name = ElmahHost_Name;

            destination.ElmahSource_Name = ElmahSource_Name;

            destination.ElmahStatusCode_Name = ElmahStatusCode_Name;

            destination.ElmahType_Name = ElmahType_Name;

            destination.ElmahUser_Name = ElmahUser_Name;

            destination.Application = Application;

            destination.Host = Host;

            destination.Type = Type;

            destination.Source = Source;

            destination.Message = Message;

            destination.User = User;

            destination.StatusCode = StatusCode;

            destination.TimeUtc = TimeUtc;

            destination.Sequence = Sequence;

            destination.AllXml = AllXml;

            }

            public void CopyFrom<T>(T source)
                where T : Default
            {
                ElmahApplication_Name = source.ElmahApplication_Name;

            ErrorId = source.ErrorId;

            ElmahHost_Name = source.ElmahHost_Name;

            ElmahSource_Name = source.ElmahSource_Name;

            ElmahStatusCode_Name = source.ElmahStatusCode_Name;

            ElmahType_Name = source.ElmahType_Name;

            ElmahUser_Name = source.ElmahUser_Name;

            Application = source.Application;

            Host = source.Host;

            Type = source.Type;

            Source = source.Source;

            Message = source.Message;

            User = source.User;

            StatusCode = source.StatusCode;

            TimeUtc = source.TimeUtc;

            Sequence = source.Sequence;

            AllXml = source.AllXml;

            }
        }
/*
        /// <summary>
        /// View "Default" class of <see cref="ELMAH_Error"/>, used across the solution.
        /// </summary>
        public partial class DefaultCollection
            :  List<Default>
        {
        }
*/
        /// <summary>
        /// message definition of "Default", pass a collection of instances, from database, to business logic layer. <see cref="ELMAH_Error"/> and <see cref="Framework.DataAccessLayerMessageBase&lt;T&gt;"/>
        /// </summary>
        public class DataAccessLayerMessageOfDefaultCollection
            : Framework.Models.DataAccessLayerMessageBase<List<Default>>
        {
        }

        #endregion Nested Views classes and their collection classes 1
    }

/*
    /// <summary>
    /// message definition, pass single entry, pulled from database, to business logic layer. <see cref="ELMAH_Error"/> and <see cref="Framework.DataAccessLayerMessageBase&lt;T&gt;"/>
    /// </summary>
    public class DataAccessLayerMessageOfEntityELMAH_Error
        : Framework.Models.DataAccessLayerMessageBase<ELMAH_Error>
    {
    }
*/
    public partial class DataAccessLayerMessageOfEntityCollectionELMAH_Error
        : Framework.Models.DataAccessLayerMessageBase<List<ELMAH_Error>>
    {
    }
}

