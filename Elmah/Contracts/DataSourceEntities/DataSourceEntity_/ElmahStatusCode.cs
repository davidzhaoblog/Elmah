using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Elmah.DataSourceEntities
{
    /// <summary>
    /// Entity class, used across the solution. <see cref="ElmahStatusCode"/>
    /// </summary>
    //[DataContract]
    public partial class ElmahStatusCode : Framework.Models.PropertyChangedNotifier, Elmah.EntityContracts.IElmahStatusCode, Framework.Models.IClone<Elmah.EntityContracts.IElmahStatusCode>
    {

        #region Storage Fields

        System.Int32 m_StatusCode;

        System.String m_Name;

        #endregion Storage Fields

        #region properties

                //[DataMember]
        [Display(Name = "StatusCode", ResourceType = typeof(Elmah.Resx.UIStringResourcePerEntity))]
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
        [Display(Name = "Name", ResourceType = typeof(Elmah.Resx.UIStringResourcePerEntity))]
        [StringLengthAttribute(50, ErrorMessageResourceType = typeof(Elmah.Resx.UIStringResourcePerEntity), ErrorMessageResourceName="The_length_of_Name_should_be_0_to_50")]
        public System.String Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                if (Framework.Models.PropertyChangedNotifierHelper.IsToRaisePropertyChanged)
                {
                    ValidateProperty(value);
                    Set(nameof(Name), ref m_Name, value);
                }
                else
                {
                    m_Name = value;
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
            bool _retval = obj is ElmahStatusCode;
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
            where T: Elmah.EntityContracts.IElmahStatusCode, new()
        {
            var cloned = new T();
            CopyTo<T>(cloned);
            return cloned;
        }

        public void CopyTo<T>(T destination, bool toDatabase = false, bool isInsert = true)
            where T: Elmah.EntityContracts.IElmahStatusCode
        {
            destination.StatusCode = StatusCode;

            destination.Name = Name;

        }

        public void CopyFrom<T>(T source)
            where T: Elmah.EntityContracts.IElmahStatusCode
        {
            StatusCode = source.StatusCode;

            Name = source.Name;

        }

        #region Nested Views classes and their collection classes 0

        #endregion Nested Views classes and their collection classes 0
    }

/*
    /// <summary>
    /// message definition, pass single entry, pulled from database, to business logic layer. <see cref="ElmahStatusCode"/> and <see cref="Framework.DataAccessLayerMessageBase&lt;T&gt;"/>
    /// </summary>
    public class DataAccessLayerMessageOfEntityElmahStatusCode
        : Framework.Models.DataAccessLayerMessageBase<ElmahStatusCode>
    {
    }
*/
    public partial class DataAccessLayerMessageOfEntityCollectionElmahStatusCode
        : Framework.Models.DataAccessLayerMessageBase<List<ElmahStatusCode>>
    {
    }
}

