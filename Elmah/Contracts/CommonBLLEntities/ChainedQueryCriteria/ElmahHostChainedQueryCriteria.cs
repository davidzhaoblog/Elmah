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
    public class ElmahHostChainedQueryCriteriaCommon
    {
        public ElmahHostChainedQueryCriteriaCommon()
        {
        }

        public ElmahHostQueryCriteriaCommon Common { get; set; } = new ElmahHostQueryCriteriaCommon();

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
    public class ElmahHostChainedQueryCriteriaIdentifier
    {
        public ElmahHostChainedQueryCriteriaIdentifier()
        {
        }

        public ElmahHostQueryCriteriaIdentifier Identifier { get; set; } = new ElmahHostQueryCriteriaIdentifier();

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

