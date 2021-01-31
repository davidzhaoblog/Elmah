using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elmah.CommonBLLEntities
{

    /// <summary>
    /// BusinessLogicLayerRequestMessageUserDefined, with Key=Common
    /// </summary>
    public partial class ElmahSourceRequestMessageUserDefinedOfCommon
        : Framework.Services.BusinessLogicLayerRequestMessageBase<ElmahSourceChainedQueryCriteriaCommon>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ElmahSourceRequestMessageUserDefinedOfCommon"/> class.
        /// </summary>
        public ElmahSourceRequestMessageUserDefinedOfCommon()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElmahSourceRequestMessageUserDefinedOfCommon"/> class.
        /// </summary>
        /// <param name="businessLogicLayerRequestTypes">The business logic layer request types.</param>
        /// <param name="businessLogicLayerRequestTypeKey">The business logic layer request type key.</param>
        /// <param name="businessLogicLayerRequestID">The business logic layer request ID.</param>
        public ElmahSourceRequestMessageUserDefinedOfCommon(
            Framework.Services.BusinessLogicLayerRequestTypes businessLogicLayerRequestTypes
            , string businessLogicLayerRequestTypeKey
            , string businessLogicLayerRequestID)
            : base(businessLogicLayerRequestTypes, businessLogicLayerRequestTypeKey, businessLogicLayerRequestID)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElmahSourceRequestMessageUserDefinedOfCommon"/> class.
        /// </summary>
        /// <param name="businessLogicLayerRequestTypes">The business logic layer request types.</param>
        /// <param name="businessLogicLayerRequestTypeKey">The business logic layer request type key.</param>
        /// <param name="businessLogicLayerRequestID">The business logic layer request ID.</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="queryOrderByExpression">The query order by expression.</param>
        public ElmahSourceRequestMessageUserDefinedOfCommon(
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
    public partial class ElmahSourceRequestMessageUserDefinedOfIdentifier
        : Framework.Services.BusinessLogicLayerRequestMessageBase<ElmahSourceChainedQueryCriteriaIdentifier>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ElmahSourceRequestMessageUserDefinedOfIdentifier"/> class.
        /// </summary>
        public ElmahSourceRequestMessageUserDefinedOfIdentifier()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElmahSourceRequestMessageUserDefinedOfIdentifier"/> class.
        /// </summary>
        /// <param name="businessLogicLayerRequestTypes">The business logic layer request types.</param>
        /// <param name="businessLogicLayerRequestTypeKey">The business logic layer request type key.</param>
        /// <param name="businessLogicLayerRequestID">The business logic layer request ID.</param>
        public ElmahSourceRequestMessageUserDefinedOfIdentifier(
            Framework.Services.BusinessLogicLayerRequestTypes businessLogicLayerRequestTypes
            , string businessLogicLayerRequestTypeKey
            , string businessLogicLayerRequestID)
            : base(businessLogicLayerRequestTypes, businessLogicLayerRequestTypeKey, businessLogicLayerRequestID)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElmahSourceRequestMessageUserDefinedOfIdentifier"/> class.
        /// </summary>
        /// <param name="businessLogicLayerRequestTypes">The business logic layer request types.</param>
        /// <param name="businessLogicLayerRequestTypeKey">The business logic layer request type key.</param>
        /// <param name="businessLogicLayerRequestID">The business logic layer request ID.</param>
        /// <param name="currentIndex">Index of the current.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="queryOrderByExpression">The query order by expression.</param>
        public ElmahSourceRequestMessageUserDefinedOfIdentifier(
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

