using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Elmah.DataSourceEntities
{
    /// <summary>
    /// Entity class, used across the solution. <see cref="ElmahSource"/>
    /// </summary>
    //[DataContract]
    public partial class ElmahSource : Framework.Models.PropertyChangedNotifier, Elmah.EntityContracts.IElmahSource, Framework.Models.IClone<Elmah.EntityContracts.IElmahSource>
    {

        #region Storage Fields

        System.String m_Source;

        #endregion Storage Fields

        #region properties

                //[DataMember]
        [Display(Name = "Source", ResourceType = typeof(Elmah.Resx.UIStringResourcePerEntity))]
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
                    //ValidateProperty(value);
                    Set(nameof(Source), ref m_Source, value);
                }
                else
                {
                    m_Source = value;
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
            bool _retval = obj is ElmahSource;
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
            where T: Elmah.EntityContracts.IElmahSource, new()
        {
            var cloned = new T();
            CopyTo<T>(cloned);
            return cloned;
        }

        public void CopyTo<T>(T destination, bool toDatabase = false, bool isInsert = true)
            where T: Elmah.EntityContracts.IElmahSource
        {
            destination.Source = Source;

        }

        public void CopyFrom<T>(T source)
            where T: Elmah.EntityContracts.IElmahSource
        {
            Source = source.Source;

        }

        #region Nested Views classes and their collection classes 0

        #endregion Nested Views classes and their collection classes 0
    }

/*
    /// <summary>
    /// message definition, pass single entry, pulled from database, to business logic layer. <see cref="ElmahSource"/> and <see cref="Framework.DataAccessLayerMessageBase&lt;T&gt;"/>
    /// </summary>
    public class DataAccessLayerMessageOfEntityElmahSource
        : Framework.Models.DataAccessLayerMessageBase<ElmahSource>
    {
    }
*/
    public partial class DataAccessLayerMessageOfEntityCollectionElmahSource
        : Framework.Models.DataAccessLayerMessageBase<List<ElmahSource>>
    {
    }
}

