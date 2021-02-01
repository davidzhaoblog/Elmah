using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elmah.CommonBLLEntities
{

    /// <summary>
    /// BusinessLogicLayerRequestMessageUserDefined, with Key=Common
    /// </summary>
    public partial class ElmahUserRequestMessageUserDefinedOfCommon
        : Framework.Services.BusinessLogicLayerRequestMessageBase<ElmahUserChainedQueryCriteriaCommon>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ElmahUserRequestMessageUserDefinedOfCommon"/> class.
        /// </summary>
        public ElmahUserRequestMessageUserDefinedOfCommon()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElmahUserRequestMessageUserDefinedOfCommon"/> class.
        /// </summary>
        /// <param name="businessLogicLayerRequestTypes">The business logic layer request types.</param>
        /// <param name="businessLogicLayerRequestTypeKey">The business logic layer request type key.</param>
        /// <param name="businessLogicLayerRequestID">The business logic layer request ID.</param>
        public ElmahUserRequestMessageUserDefinedOfCommon(
            Framework.Services.BusinessLogicLayerRequestTypes businessLogicLayerRequestTypes
            , string businessLogicLayerRequestTypeKey
            , string businessLogicLayerRequestID)
            : base(businessLogicLayerRequestTypes, businessLogicLayerRequestTypeKey, businessLogicLayerRequestID)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElmahUserRequestMessageUserDefinedOfCommon"/> class.
        /// </summary>
        /// <param name="businessLogicLayerRequestTypes">The business logic layer request types.</param>
        /// <param name="businessLogicLayerRequestTypeKey">The business logic layer request type key.</param>
        /// <param name="businessLogicLayerRequestID">The business logic layer request ID.</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="queryOrderByExpression">The query order by expression.</param>
        public ElmahUserRequestMessageUserDefinedOfCommon(
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
    public partial class ElmahUserRequestMessageUserDefinedOfIdentifier
        : Framework.Services.BusinessLogicLayerRequestMessageBase<ElmahUserChainedQueryCriteriaIdentifier>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ElmahUserRequestMessageUserDefinedOfIdentifier"/> class.
        /// </summary>
        public ElmahUserRequestMessageUserDefinedOfIdentifier()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElmahUserRequestMessageUserDefinedOfIdentifier"/> class.
        /// </summary>
        /// <param name="businessLogicLayerRequestTypes">The business logic layer request types.</param>
        /// <param name="businessLogicLayerRequestTypeKey">The business logic layer request type key.</param>
        /// <param name="businessLogicLayerRequestID">The business logic layer request ID.</param>
        public ElmahUserRequestMessageUserDefinedOfIdentifier(
            Framework.Services.BusinessLogicLayerRequestTypes businessLogicLayerRequestTypes
            , string businessLogicLayerRequestTypeKey
            , string businessLogicLayerRequestID)
            : base(businessLogicLayerRequestTypes, businessLogicLayerRequestTypeKey, businessLogicLayerRequestID)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElmahUserRequestMessageUserDefinedOfIdentifier"/> class.
        /// </summary>
        /// <param name="businessLogicLayerRequestTypes">The business logic layer request types.</param>
        /// <param name="businessLogicLayerRequestTypeKey">The business logic layer request type key.</param>
        /// <param name="businessLogicLayerRequestID">The business logic layer request ID.</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="queryOrderByExpression">The query order by expression.</param>
        public ElmahUserRequestMessageUserDefinedOfIdentifier(
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

