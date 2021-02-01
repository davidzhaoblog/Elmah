using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framework.Queries;

namespace Elmah.CommonBLLEntities
{

    /// <summary>
    /// BusinessLogicLayerChainedQueryCriteria, key=Common
    /// </summary>
    public class ELMAH_ErrorChainedQueryCriteriaCommon
    {
        public ELMAH_ErrorChainedQueryCriteriaCommon()
        {
        }

        public ELMAH_ErrorQueryCriteriaCommon Common { get; set; } = new ELMAH_ErrorQueryCriteriaCommon();

        public bool CanQueryWhenNoQuery { get; set; } = false;
        public bool HasQuery
        {
            get
            {
                return Common.HasQuery;
            }
        }
    }

    /// <summary>
    /// BusinessLogicLayerChainedQueryCriteria, key=Identifier
    /// </summary>
    public class ELMAH_ErrorChainedQueryCriteriaIdentifier
    {
        public ELMAH_ErrorChainedQueryCriteriaIdentifier()
        {
        }

        public ELMAH_ErrorQueryCriteriaIdentifier Identifier { get; set; } = new ELMAH_ErrorQueryCriteriaIdentifier();

        public bool CanQueryWhenNoQuery { get; set; } = false;
        public bool HasQuery
        {
            get
            {
                return Identifier.HasQuery;
            }
        }
    }

}

