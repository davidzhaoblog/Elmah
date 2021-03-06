using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Framework.Services
{
    public class BusinessLogicLayerResponseMessageBase : Framework.IBusinessLogicLayerResponseMessageBase
    {

        /// <summary>
        /// Gets or sets the business logic layer response status.
        /// </summary>
        /// <value>
        /// The business logic layer response status.
        /// </value>
        public BusinessLogicLayerResponseStatus BusinessLogicLayerResponseStatus { get; set; }
        /// <summary>
        /// Gets or sets the query paging result.
        /// </summary>
        /// <value>
        /// The query paging result.
        /// </value>
        public Framework.Queries.QueryPagingResult QueryPagingResult { get; set; }
        /// <summary>
        /// Gets or sets the server error message.
        /// </summary>
        /// <value>
        /// The server error message.
        /// </value>
        public string ServerErrorMessage { get; set; }
        /// <summary>
        /// Gets or sets the business logic layer request ID.
        /// </summary>
        /// <value>
        /// The business logic layer request ID.
        /// </value>
        public string BusinessLogicLayerRequestID { get; set; }
        /// <summary>
        /// Gets or sets the business logic layer response ID.
        /// </summary>
        /// <value>
        /// The business logic layer response ID.
        /// </value>
        public string BusinessLogicLayerResponseID { get; set; }

        /// <summary>
        /// Gets or sets the message in stream. Message is converted to Stream, e.g. LinqToCsv, ClosedXml
        /// </summary>
        /// <value>
        /// The message in stream.
        /// </value>
        public Framework.Models.DataStreamServiceResult DataStreamServiceResult { get; set; }

        public string GetStatusMessage()
        {
            return Framework.Resx.UIStringResource.ResourceManager.GetString(this.BusinessLogicLayerResponseStatus.ToString());
        }

        public Framework.ViewModels.ResponseStatus<TOptions> GetStatusMessage<TOptions>(TOptions option, BusinessLogicLayerRequestTypes requestType)
        {
            return new Framework.ViewModels.ResponseStatus<TOptions> {
                Option = option
                , RequestType = requestType
                , Status  = this.BusinessLogicLayerResponseStatus
                , StatusMessage = ServerErrorMessage
            };
        }
    }

    /// <summary>
    /// base class of all BusinessLogicLayerResponseMessages
    /// </summary>
    /// <typeparam name="TResponse">The type of the response.</typeparam>
    public class BusinessLogicLayerResponseMessageBase<TResponse>: BusinessLogicLayerResponseMessageBase
    {
        public BusinessLogicLayerResponseMessageBase()
        {
            //this.Message = new TResponse();
        }

        /// <summary>
        /// Gets or sets the response message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public TResponse Message { get; set; }

        public override string ToString()
        {
            return string.Format("ResponseStatus:{0};PagingResult:{1}"
                , this.BusinessLogicLayerResponseStatus
                , this.QueryPagingResult != null ? this.QueryPagingResult.ToString() : ""
                , this.BusinessLogicLayerRequestID
                , this.BusinessLogicLayerResponseID
                , typeof(TResponse));
        }
    }

    /// <summary>
    /// Helper class for BusinessLogicLayerResponseMessageBase, to map the data access layer message to business logic layer response message
    /// </summary>
    public class BusinessLogicLayerResponseMessageBaseHelper
    {
        #region MapDataAccessLayerMessageToBusinessLogicLayerResponseMessage(...)

        public static void MapDataAccessLayerMessageToBusinessLogicLayerResponseMessage<T, TList>(
            Framework.Models.DataAccessLayerMessageBase<TList> from, BusinessLogicLayerResponseMessageBase<TList> to
            , Framework.Models.DataServiceTypes dataServiceType
            , Framework.Models.IDataStreamServiceProviderBase<TList, T> dataStreamServiceProvider
            )
            where TList: List<T>, new()
            where T : class, new()
        {
            if (from.DataAccessLayerMessageStatus == Framework.Models.DataAccessLayerMessageStatus.Success)
            {
                to.BusinessLogicLayerResponseStatus = BusinessLogicLayerResponseStatus.MessageOK;
                if (from.QueryPagingResult != null)
                {
                    to.QueryPagingResult = from.QueryPagingResult.Clone();
                }
                to.DataStreamServiceResult = dataStreamServiceProvider.BuildResult(from.Result, dataServiceType);
            }
            else if (from.DataAccessLayerMessageStatus == Framework.Models.DataAccessLayerMessageStatus.SuccessButNoResult)
            {
                to.BusinessLogicLayerResponseStatus = BusinessLogicLayerResponseStatus.NoValueFromDataSource;
                to.DataStreamServiceResult = dataStreamServiceProvider.BuildResult(from.Result, dataServiceType);
            }
            else
            {
                to.BusinessLogicLayerResponseStatus = BusinessLogicLayerResponseStatus.MessageErrorDetected;
                to.ServerErrorMessage = from.Message;
            }
        }

        /// <summary>
        /// Maps the data access layer message to business logic layer response message.
        /// </summary>
        /// <typeparam name="T">any type</typeparam>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        public static void MapDataAccessLayerMessageToBusinessLogicLayerResponseMessage<T>(
            Framework.Models.DataAccessLayerMessageBase<T> from, BusinessLogicLayerResponseMessageBase<T> to)
        {
            if (from.DataAccessLayerMessageStatus == Framework.Models.DataAccessLayerMessageStatus.Success)
            {
                to.BusinessLogicLayerResponseStatus = BusinessLogicLayerResponseStatus.MessageOK;
                to.Message = from.Result;
                if (from.QueryPagingResult != null)
                {
                    to.QueryPagingResult = from.QueryPagingResult.Clone();
                }
            }
            else if (from.DataAccessLayerMessageStatus == Framework.Models.DataAccessLayerMessageStatus.SuccessButNoResult)
            {
                to.BusinessLogicLayerResponseStatus = BusinessLogicLayerResponseStatus.NoValueFromDataSource;
            }
            else
            {
                to.BusinessLogicLayerResponseStatus = BusinessLogicLayerResponseStatus.MessageErrorDetected;
                to.ServerErrorMessage = from.Message;
            }
        }

        /// <summary>
        /// Maps the data access layer message to business logic layer response message.
        /// </summary>
        /// <typeparam name="T">any type</typeparam>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        public static void MapDataAccessLayerMessageToBusinessLogicLayerResponseMessage<T, TList>(
            Framework.Models.DataAccessLayerMessageBase<T> from, BusinessLogicLayerResponseMessageBase<TList> to
            , Framework.Models.DataServiceTypes dataServiceType
            , Framework.Models.IDataStreamServiceProviderBase<TList, T> dataStreamServiceProvider)
            where TList: List<T>, new()
            where T : class, new()
        {
            if (from.DataAccessLayerMessageStatus == Framework.Models.DataAccessLayerMessageStatus.Success)
            {
                to.BusinessLogicLayerResponseStatus = BusinessLogicLayerResponseStatus.MessageOK;
                if (from.QueryPagingResult != null)
                {
                    to.QueryPagingResult = from.QueryPagingResult.Clone();
                }
                TList fromList = new TList();
                fromList.Add(from.Result);
                to.DataStreamServiceResult = dataStreamServiceProvider.BuildResult(fromList, dataServiceType);
            }
            else if (from.DataAccessLayerMessageStatus == Framework.Models.DataAccessLayerMessageStatus.SuccessButNoResult)
            {
                to.BusinessLogicLayerResponseStatus = BusinessLogicLayerResponseStatus.NoValueFromDataSource;
                TList fromList = new TList();
                fromList.Add(from.Result);
                to.DataStreamServiceResult = dataStreamServiceProvider.BuildResult(fromList, dataServiceType);
            }
            else
            {
                to.BusinessLogicLayerResponseStatus = BusinessLogicLayerResponseStatus.MessageErrorDetected;
                to.ServerErrorMessage = from.Message;
            }
        }

        /// <summary>
        /// Maps the data access layer message to business logic layer response message.
        /// </summary>
        /// <typeparam name="T">any type</typeparam>
        /// <typeparam name="TList">The type of the list.</typeparam>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        public static void MapDataAccessLayerMessageToBusinessLogicLayerResponseMessage<T, TList>(
            Framework.Models.DataAccessLayerMessageBase<T> from, BusinessLogicLayerResponseMessageBase<TList> to)
            where TList: IList<T>, new()
            where T : class, new()
        {
            if (from.DataAccessLayerMessageStatus == Framework.Models.DataAccessLayerMessageStatus.Success)
            {
                to.BusinessLogicLayerResponseStatus = BusinessLogicLayerResponseStatus.MessageOK;
                to.Message = new TList();
                to.Message.Add(from.Result);
            }
            else if (from.DataAccessLayerMessageStatus == Framework.Models.DataAccessLayerMessageStatus.SuccessButNoResult)
            {
                to.BusinessLogicLayerResponseStatus = BusinessLogicLayerResponseStatus.NoValueFromDataSource;
            }
            else
            {
                to.BusinessLogicLayerResponseStatus = BusinessLogicLayerResponseStatus.MessageErrorDetected;
                to.ServerErrorMessage = from.Message;
            }
        }

        #endregion MapDataAccessLayerMessageToBusinessLogicLayerResponseMessage(...)

    }

    /// <summary>
    /// Built-In BusinessLogicLayerResponseMessage of NameValuePairCollection
    /// </summary>
    public class BusinessLogicLayerResponseMessageNameValuePairCollection
        : BusinessLogicLayerResponseMessageBase<List<Framework.Models.NameValuePair>>
    {
    }

    /*
    /// <summary>
    /// Built-In BusinessLogicLayerResponseMessage of RssItemCollection
    /// </summary>
    public class BusinessLogicLayerResponseMessageRssItemCollection
        : BusinessLogicLayerResponseMessageBase<Framework.Models.RssItemCollection>
    {
    }
    */

    /// <summary>
    /// Built-In BusinessLogicLayerResponseMessage of Integer
    /// </summary>
    public class BusinessLogicLayerResponseMessageInteger
        : BusinessLogicLayerResponseMessageBase<int>
    {
    }

    /// <summary>
    /// Built-In BusinessLogicLayerResponseMessage of Boolean
    /// </summary>
    public class BusinessLogicLayerResponseMessageBoolean
        : BusinessLogicLayerResponseMessageBase<bool>
    {
    }

    /// <summary>
    ///  Business Logic Layer Response Status
    /// </summary>
    public enum BusinessLogicLayerResponseStatus
    {
        ///// <remarks/>
        //Received,

        ///// <remarks/>
        //Pending,

        ///// <remarks/>
        //BeingProcessed,

        ///// <remarks/>
        //Rejected,

        ///// <remarks/>
        //Accepted,

        ///// <remarks/>
        //Succeeded,

        ///// <remarks/>
        //OnHold,

        ///// <remarks/>
        //Failed,

        /// <remarks/>
        NoAction,

        /// <remarks/>
        UIProcessReady,

        /// <remarks/>
        RequestError,

        /// <remarks/>
        MessageOK,

        /// <remarks/>
        NoValueFromDataSource,

        /// <remarks/>
        MessageErrorDetected,

        // <remarks/>
        YouHaveNoPermissionToView,

        // <remarks/>
        YouHaveNoPermissionToInsert,

        // <remarks/>
        YouHaveNoPermissionToUpdate,

        // <remarks/>
        YouHaveNoPermissionToDelete,

        // <remarks/>
        NeedAtLeastOneSearchCondition
    }
}

