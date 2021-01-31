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
    public partial class ElmahHostQueryCriteriaCommon
    {
        public ElmahHostQueryCriteriaCommon()
        {
        }

        public QuerySystemStringContainsCriteria Host { get; set; } = new QuerySystemStringContainsCriteria();

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
                if(Host != null && !Host.IsToCompare)
                    Host = StringContains_AllColumns;

            }
        }

        public bool HasQuery
        {
            get
            {
                return Host.IsToCompare;
            }
        }
    }

    /// <summary>
    /// BusinessLogicLayerQueryCriteria, key=Identifier
    /// </summary>
    public partial class ElmahHostQueryCriteriaIdentifier
    {
        public ElmahHostQueryCriteriaIdentifier()
        {
        }

        public QuerySystemStringEqualsCriteria Host { get; set; } = new QuerySystemStringEqualsCriteria();

        public bool HasQuery
        {
            get
            {
                return Host.IsToCompare;
            }
        }
    }

}

