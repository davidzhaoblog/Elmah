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
    public class ElmahApplicationChainedQueryCriteriaCommon
    {
        public ElmahApplicationChainedQueryCriteriaCommon()
        {
        }

        public ElmahApplicationQueryCriteriaCommon Common { get; set; } = new ElmahApplicationQueryCriteriaCommon();

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
    public class ElmahApplicationChainedQueryCriteriaIdentifier
    {
        public ElmahApplicationChainedQueryCriteriaIdentifier()
        {
        }

        public ElmahApplicationQueryCriteriaIdentifier Identifier { get; set; } = new ElmahApplicationQueryCriteriaIdentifier();

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

