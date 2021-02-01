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
    public class ElmahSourceChainedQueryCriteriaCommon
    {
        public ElmahSourceChainedQueryCriteriaCommon()
        {
        }

        public ElmahSourceQueryCriteriaCommon Common { get; set; } = new ElmahSourceQueryCriteriaCommon();

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
    public class ElmahSourceChainedQueryCriteriaIdentifier
    {
        public ElmahSourceChainedQueryCriteriaIdentifier()
        {
        }

        public ElmahSourceQueryCriteriaIdentifier Identifier { get; set; } = new ElmahSourceQueryCriteriaIdentifier();

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

