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
    public class ElmahStatusCodeChainedQueryCriteriaCommon
    {
        public ElmahStatusCodeChainedQueryCriteriaCommon()
        {
        }

        public ElmahStatusCodeQueryCriteriaCommon Common { get; set; } = new ElmahStatusCodeQueryCriteriaCommon();

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
    public class ElmahStatusCodeChainedQueryCriteriaIdentifier
    {
        public ElmahStatusCodeChainedQueryCriteriaIdentifier()
        {
        }

        public ElmahStatusCodeQueryCriteriaIdentifier Identifier { get; set; } = new ElmahStatusCodeQueryCriteriaIdentifier();

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

