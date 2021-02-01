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
    public class ElmahTypeChainedQueryCriteriaCommon
    {
        public ElmahTypeChainedQueryCriteriaCommon()
        {
        }

        public ElmahTypeQueryCriteriaCommon Common { get; set; } = new ElmahTypeQueryCriteriaCommon();

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
    public class ElmahTypeChainedQueryCriteriaIdentifier
    {
        public ElmahTypeChainedQueryCriteriaIdentifier()
        {
        }

        public ElmahTypeQueryCriteriaIdentifier Identifier { get; set; } = new ElmahTypeQueryCriteriaIdentifier();

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

