using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elmah.CommonBLLEntities
{

    /// <summary>
    /// BusinessLogicLayerRequestMessageUserDefined, with Key=Common
    /// </summary>
    public partial class ElmahApplicationRequestMessageUserDefinedOfCommon
        : Framework.Services.BusinessLogicLayerRequestMessageBase<ElmahApplicationChainedQueryCriteriaCommon>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ElmahApplicationRequestMessageUserDefinedOfCommon"/> class.
        /// </summary>
        public ElmahApplicationRequestMessageUserDefinedOfCommon()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElmahApplicationRequestMessageUserDefinedOfCommon"/> class.
        /// </summary>
        /// <param name="businessLogicLayerRequestTypes">The business logic layer request types.</param>
        /// <param name="businessLogicLayerRequestTypeKey">The business logic layer request type key.</param>
        /// <param name="businessLogicLayerRequestID">The business logic layer request ID.</param>
        public ElmahApplicationRequestMessageUserDefinedOfCommon(
            Framework.Services.BusinessLogicLayerRequestTypes businessLogicLayerRequestTypes
            , string businessLogicLayerRequestTypeKey
            , string businessLogicLayerRequestID)
            : base(businessLogicLayerRequestTypes, businessLogicLayerRequestTypeKey, businessLogicLayerRequestID)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElmahApplicationRequestMessageUserDefinedOfCommon"/> class.
        /// </summary>
        /// <param name="businessLogicLayerRequestTypes">The business logic layer request types.</param>
        /// <param name="businessLogicLayerRequestTypeKey">The business logic layer request type key.</param>
        /// <param name="businessLogicLayerRequestID">The business logic layer request ID.</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="queryOrderByExpression">The query order by expression.</param>
        public ElmahApplicationRequestMessageUserDefinedOfCommon(
            Framework.Services.BusinessLogicLayerRequestTypes businessLogicLayerRequestTypes
            , string businessLogicLayerRequestTypeKey
            , string businessLogicLayerRequestID
            , int currentIndex
            , int pageSize
            , string queryOrderByExpression
            )
            : base(businessLogicLayerRequestTypes, businessLogicLayerRequestTypeKey, businessLogicLayerRequestID, currentIndex, pageSize, queryOrderByExpression)
        {
        }
    }

    /// <summary>
    /// BusinessLogicLayerRequestMessageUserDefined, with Key=Identifier
    /// </summary>
    public partial class ElmahApplicationRequestMessageUserDefinedOfIdentifier
        : Framework.Services.BusinessLogicLayerRequestMessageBase<ElmahApplicationChainedQueryCriteriaIdentifier>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ElmahApplicationRequestMessageUserDefinedOfIdentifier"/> class.
        /// </summary>
        public ElmahApplicationRequestMessageUserDefinedOfIdentifier()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElmahApplicationRequestMessageUserDefinedOfIdentifier"/> class.
        /// </summary>
        /// <param name="businessLogicLayerRequestTypes">The business logic layer request types.</param>
        /// <param name="businessLogicLayerRequestTypeKey">The business logic layer request type key.</param>
        /// <param name="businessLogicLayerRequestID">The business logic layer request ID.</param>
        public ElmahApplicationRequestMessageUserDefinedOfIdentifier(
            Framework.Services.BusinessLogicLayerRequestTypes businessLogicLayerRequestTypes
            , string businessLogicLayerRequestTypeKey
            , string businessLogicLayerRequestID)
            : base(businessLogicLayerRequestTypes, businessLogicLayerRequestTypeKey, businessLogicLayerRequestID)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElmahApplicationRequestMessageUserDefinedOfIdentifier"/> class.
        /// </summary>
        /// <param name="businessLogicLayerRequestTypes">The business logic layer request types.</param>
        /// <param name="businessLogicLayerRequestTypeKey">The business logic layer request type key.</param>
        /// <param name="businessLogicLayerRequestID">The business logic layer request ID.</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="queryOrderByExpression">The query order by expression.</param>
        public ElmahApplicationRequestMessageUserDefinedOfIdentifier(
            Framework.Services.BusinessLogicLayerRequestTypes businessLogicLayerRequestTypes
            , string businessLogicLayerRequestTypeKey
            , string businessLogicLayerRequestID
            , int currentIndex
            , int pageSize
            , string queryOrderByExpression
            )
            : base(businessLogicLayerRequestTypes, businessLogicLayerRequestTypeKey, businessLogicLayerRequestID, currentIndex, pageSize, queryOrderByExpression)
        {
        }
    }

}

