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
    public partial class ELMAH_ErrorQueryCriteriaCommon
    {
        public ELMAH_ErrorQueryCriteriaCommon()
        {
        }

        public QuerySystemStringEqualsCriteria Application { get; set; } = new QuerySystemStringEqualsCriteria();
        public QuerySystemStringEqualsCriteria Host { get; set; } = new QuerySystemStringEqualsCriteria();
        public QuerySystemStringEqualsCriteria Source { get; set; } = new QuerySystemStringEqualsCriteria();
        public QuerySystemInt32EqualsCriteria StatusCode { get; set; } = new QuerySystemInt32EqualsCriteria();
        public QuerySystemStringEqualsCriteria Type { get; set; } = new QuerySystemStringEqualsCriteria();
        public QuerySystemStringEqualsCriteria User { get; set; } = new QuerySystemStringEqualsCriteria();
        public QuerySystemStringContainsCriteria Message { get; set; } = new QuerySystemStringContainsCriteria();
        public QuerySystemStringContainsCriteria AllXml { get; set; } = new QuerySystemStringContainsCriteria();
        public QuerySystemDateTimeRangeCriteria TimeUtcRange { get; set; } = new QuerySystemDateTimeRangeCriteria();

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
                if(Message != null && !Message.IsToCompare)
                    Message = StringContains_AllColumns;
                if(AllXml != null && !AllXml.IsToCompare)
                    AllXml = StringContains_AllColumns;

            }
        }

        public bool HasQuery
        {
            get
            {
                return Application.IsToCompare || Host.IsToCompare || Source.IsToCompare || StatusCode.IsToCompare || Type.IsToCompare || User.IsToCompare || Message.IsToCompare || AllXml.IsToCompare || TimeUtcRange.IsToCompare;
            }
        }
    }

    /// <summary>
    /// BusinessLogicLayerQueryCriteria, key=Identifier
    /// </summary>
    public partial class ELMAH_ErrorQueryCriteriaIdentifier
    {
        public ELMAH_ErrorQueryCriteriaIdentifier()
        {
        }

        public QuerySystemGuidEqualsCriteria ErrorId { get; set; } = new QuerySystemGuidEqualsCriteria();

        public bool HasQuery
        {
            get
            {
                return ErrorId.IsToCompare;
            }
        }
    }

}

