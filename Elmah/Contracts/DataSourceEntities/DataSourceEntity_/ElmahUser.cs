using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Elmah.DataSourceEntities
{
    /// <summary>
    /// Entity class, used across the solution. <see cref="ElmahUser"/>
    /// </summary>
    //[DataContract]
    public partial class ElmahUser : Framework.Models.PropertyChangedNotifier, Elmah.EntityContracts.IElmahUser, Framework.Models.IClone<Elmah.EntityContracts.IElmahUser>
    {

        #region Storage Fields

        System.String m_User;

        #endregion Storage Fields

        #region properties

                //[DataMember]
        [Display(Name = "User", ResourceType = typeof(Elmah.Resx.UIStringResourcePerEntity))]
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
                    //ValidateProperty(value);
                    Set(nameof(User), ref m_User, value);
                }
                else
                {
                    m_User = value;
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
            bool _retval = obj is ElmahUser;
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
            where T: Elmah.EntityContracts.IElmahUser, new()
        {
            var cloned = new T();
            CopyTo<T>(cloned);
            return cloned;
        }

        public void CopyTo<T>(T destination, bool toDatabase = false, bool isInsert = true)
            where T: Elmah.EntityContracts.IElmahUser
        {
            destination.User = User;

        }

        public void CopyFrom<T>(T source)
            where T: Elmah.EntityContracts.IElmahUser
        {
            User = source.User;

        }

        #region Nested Views classes and their collection classes 0

        #endregion Nested Views classes and their collection classes 0
    }

/*
    /// <summary>
    /// message definition, pass single entry, pulled from database, to business logic layer. <see cref="ElmahUser"/> and <see cref="Framework.DataAccessLayerMessageBase&lt;T&gt;"/>
    /// </summary>
    public class DataAccessLayerMessageOfEntityElmahUser
        : Framework.Models.DataAccessLayerMessageBase<ElmahUser>
    {
    }
*/
    public partial class DataAccessLayerMessageOfEntityCollectionElmahUser
        : Framework.Models.DataAccessLayerMessageBase<List<ElmahUser>>
    {
    }
}

