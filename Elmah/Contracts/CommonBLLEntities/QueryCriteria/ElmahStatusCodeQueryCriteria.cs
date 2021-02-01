using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Queries;

namespace Elmah.CommonBLLEntities
{

    /// <summary>
    /// BusinessLogicLayerQueryCriteria, key=Common
    /// </summary>
    public partial class ElmahStatusCodeQueryCriteriaCommon
    {
        public ElmahStatusCodeQueryCriteriaCommon()
        {
        }

        public QuerySystemStringContainsCriteria Name { get; set; } = new QuerySystemStringContainsCriteria();

        /// <summary>
        /// This property for UI binding purpose only
        /// NullableValueToBeContained will assigned to all Criteria of StringContains on UI layer by calling UpdateStringContainsCriteria() if IsToCompare=true.
        /// Gets or sets the string contains all columns.
        /// </summary>
        /// <value>
        /// The string contains all columns.
        /// </value>
        public QuerySystemStringContainsCriteria StringContains_AllColumns { get; set; } = new QuerySystemStringContainsCriteria();
        public void UpdateStringContainsCriteria()
        {
            if(StringContains_AllColumns != null)
            {
                if(Name != null && !Name.IsToCompare)
                    Name = StringContains_AllColumns;

            }
        }

        public bool HasQuery
        {
            get
            {
                return Name.IsToCompare;
            }
        }
    }

    /// <summary>
    /// BusinessLogicLayerQueryCriteria, key=Identifier
    /// </summary>
    public partial class ElmahStatusCodeQueryCriteriaIdentifier
    {
        public ElmahStatusCodeQueryCriteriaIdentifier()
        {
        }

        public QuerySystemInt32EqualsCriteria StatusCode { get; set; } = new QuerySystemInt32EqualsCriteria();

        public bool HasQuery
        {
            get
            {
                return StatusCode.IsToCompare;
            }
        }
    }

}

