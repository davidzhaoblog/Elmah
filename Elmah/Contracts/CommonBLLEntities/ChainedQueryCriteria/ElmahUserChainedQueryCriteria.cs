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
    public class ElmahUserChainedQueryCriteriaCommon
    {
        public ElmahUserChainedQueryCriteriaCommon()
        {
        }

        public ElmahUserQueryCriteriaCommon Common { get; set; } = new ElmahUserQueryCriteriaCommon();

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
    public class ElmahUserChainedQueryCriteriaIdentifier
    {
        public ElmahUserChainedQueryCriteriaIdentifier()
        {
        }

        public ElmahUserQueryCriteriaIdentifier Identifier { get; set; } = new ElmahUserQueryCriteriaIdentifier();

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

