using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Elmah.DataSourceEntities
{
    /// <summary>
    /// Entity class, used across the solution. <see cref="ElmahType"/>
    /// </summary>
    //[DataContract]
    public partial class ElmahType : Framework.Models.PropertyChangedNotifier, Elmah.EntityContracts.IElmahType, Framework.Models.IClone<Elmah.EntityContracts.IElmahType>
    {

        #region Storage Fields

        System.String m_Type;

        #endregion Storage Fields

        #region properties

                //[DataMember]
        [Display(Name = "Type", ResourceType = typeof(Elmah.Resx.UIStringResourcePerEntity))]
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
                    //ValidateProperty(value);
                    Set(nameof(Type), ref m_Type, value);
                }
                else
                {
                    m_Type = value;
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
            bool _retval = obj is ElmahType;
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
            where T: Elmah.EntityContracts.IElmahType, new()
        {
            var cloned = new T();
            CopyTo<T>(cloned);
            return cloned;
        }

        public void CopyTo<T>(T destination, bool toDatabase = false, bool isInsert = true)
            where T: Elmah.EntityContracts.IElmahType
        {
            destination.Type = Type;

        }

        public void CopyFrom<T>(T source)
            where T: Elmah.EntityContracts.IElmahType
        {
            Type = source.Type;

        }

        #region Nested Views classes and their collection classes 0

        #endregion Nested Views classes and their collection classes 0
    }

/*
    /// <summary>
    /// message definition, pass single entry, pulled from database, to business logic layer. <see cref="ElmahType"/> and <see cref="Framework.DataAccessLayerMessageBase&lt;T&gt;"/>
    /// </summary>
    public class DataAccessLayerMessageOfEntityElmahType
        : Framework.Models.DataAccessLayerMessageBase<ElmahType>
    {
    }
*/
    public partial class DataAccessLayerMessageOfEntityCollectionElmahType
        : Framework.Models.DataAccessLayerMessageBase<List<ElmahType>>
    {
    }
}

