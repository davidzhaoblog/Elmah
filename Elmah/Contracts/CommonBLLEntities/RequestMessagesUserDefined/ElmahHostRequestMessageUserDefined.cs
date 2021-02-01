using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elmah.CommonBLLEntities
{

    /// <summary>
    /// BusinessLogicLayerRequestMessageUserDefined, with Key=Common
    /// </summary>
    public partial class ElmahHostRequestMessageUserDefinedOfCommon
        : Framework.Services.BusinessLogicLayerRequestMessageBase<ElmahHostChainedQueryCriteriaCommon>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ElmahHostRequestMessageUserDefinedOfCommon"/> class.
        /// </summary>
        public ElmahHostRequestMessageUserDefinedOfCommon()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElmahHostRequestMessageUserDefinedOfCommon"/> class.
        /// </summary>
        /// <param name="businessLogicLayerRequestTypes">The business logic layer request types.</param>
        /// <param name="businessLogicLayerRequestTypeKey">The business logic layer request type key.</param>
        /// <param name="businessLogicLayerRequestID">The business logic layer request ID.</param>
        public ElmahHostRequestMessageUserDefinedOfCommon(
            Framework.Services.BusinessLogicLayerRequestTypes businessLogicLayerRequestTypes
            , string businessLogicLayerRequestTypeKey
            , string businessLogicLayerRequestID)
            : base(businessLogicLayerRequestTypes, businessLogicLayerRequestTypeKey, businessLogicLayerRequestID)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElmahHostRequestMessageUserDefinedOfCommon"/> class.
        /// </summary>
        /// <param name="businessLogicLayerRequestTypes">The business logic layer request types.</param>
        /// <param name="businessLogicLayerRequestTypeKey">The business logic layer request type key.</param>
        /// <param name="businessLogicLayerRequestID">The business logic layer request ID.</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="queryOrderByExpression">The query order by expression.</param>
        public ElmahHostRequestMessageUserDefinedOfCommon(
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
    public partial class ElmahHostRequestMessageUserDefinedOfIdentifier
        : Framework.Services.BusinessLogicLayerRequestMessageBase<ElmahHostChainedQueryCriteriaIdentifier>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ElmahHostRequestMessageUserDefinedOfIdentifier"/> class.
        /// </summary>
        public ElmahHostRequestMessageUserDefinedOfIdentifier()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElmahHostRequestMessageUserDefinedOfIdentifier"/> class.
        /// </summary>
        /// <param name="businessLogicLayerRequestTypes">The business logic layer request types.</param>
        /// <param name="businessLogicLayerRequestTypeKey">The business logic layer request type key.</param>
        /// <param name="businessLogicLayerRequestID">The business logic layer request ID.</param>
        public ElmahHostRequestMessageUserDefinedOfIdentifier(
            Framework.Services.BusinessLogicLayerRequestTypes businessLogicLayerRequestTypes
            , string businessLogicLayerRequestTypeKey
            , string businessLogicLayerRequestID)
            : base(businessLogicLayerRequestTypes, businessLogicLayerRequestTypeKey, businessLogicLayerRequestID)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElmahHostRequestMessageUserDefinedOfIdentifier"/> class.
        /// </summary>
        /// <param name="businessLogicLayerRequestTypes">The business logic layer request types.</param>
        /// <param name="businessLogicLayerRequestTypeKey">The business logic layer request type key.</param>
        /// <param name="businessLogicLayerRequestID">The business logic layer request ID.</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="queryOrderByExpression">The query order by expression.</param>
        public ElmahHostRequestMessageUserDefinedOfIdentifier(
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

