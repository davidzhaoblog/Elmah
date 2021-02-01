using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Elmah.EntityFrameworkDAL
{
    public static class IQueryableHelper
    {
        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static bool Any<T>(IQueryable<T> _Query, int currentIndex, int pageSize)
        {
            bool result;
            if (currentIndex == -1 || pageSize == -1)
            {
                result = _Query.Any();
            }
            else
            {
                result = _Query.Skip(currentIndex).Take(pageSize).Any();
            }
            return result;
        }

        public static int Count<T>(IQueryable<T> _Query, int currentIndex, int pageSize)
        {
            int _Count;
            if (currentIndex == -1 || pageSize == -1)
            {
                _Count = _Query.Count();
            }
            else
            {
                _Count = _Query.Skip(currentIndex).Take(pageSize).Count();
            }
            return _Count;
        }

        public static Framework.Models.DataAccessLayerMessageOfInteger GetCountMessage<T>(IQueryable<T> _Query, int currentIndex, int pageSize, string logMessage)
        {
            //log.Info(string.Format("{0}: {1}", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Started.ToString(), logMessage));

            Framework.Models.DataAccessLayerMessageOfInteger _retMessage = new Framework.Models.DataAccessLayerMessageOfInteger();
            try
            {
                int _retval = IQueryableHelper.Count(
                    _Query
                    , currentIndex
                    , pageSize
                    );
                _retMessage.DataAccessLayerMessageStatus = Framework.Models.DataAccessLayerMessageStatus.Success;
                _retMessage.Result = _retval;
            }
            catch (Exception ex)
            {
                _retMessage.DataAccessLayerMessageStatus = Framework.Models.DataAccessLayerMessageStatus.Fail;
                _retMessage.Result = 0;
                _retMessage.Message = ex.Message;
                //log.Error(string.Format("{0}: {1}", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Failed.ToString(), logMessage), ex);
            }
            //log.Info(string.Format("{0}: {1}", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Ended.ToString(), logMessage));
            return _retMessage;
        }

        public static Framework.Models.DataAccessLayerMessageOfBoolean GetExistsMessage<T>(IQueryable<T> _Query, int currentIndex, int pageSize, string logMessage)
        {
            //log.Info(string.Format("{0}: {1}", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Started.ToString(), logMessage));
            Framework.Models.DataAccessLayerMessageOfBoolean _retMessage = new Framework.Models.DataAccessLayerMessageOfBoolean();
            try
            {
                bool _retval = IQueryableHelper.Any(
                    _Query
                    , currentIndex
                    , pageSize
                    );
                _retMessage.DataAccessLayerMessageStatus = Framework.Models.DataAccessLayerMessageStatus.Success;
                _retMessage.Result = _retval;
            }
            catch (Exception ex)
            {
                _retMessage.DataAccessLayerMessageStatus = Framework.Models.DataAccessLayerMessageStatus.Fail;
                _retMessage.Result = false;
                _retMessage.Message = ex.Message;
                //log.Error(string.Format("{0}: {1}", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Failed.ToString(), logMessage), ex);
            }
            //log.Info(string.Format("{0}: {1}", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Ended.ToString(), logMessage));
            return _retMessage;
        }

        public static TMessage GetCollectionMessage<TMessage, TCollection, TItem>(IQueryable<TItem> _Query, int currentIndex, int pageSize, string logMessage)
            where TMessage: Framework.Models.DataAccessLayerMessageBase<TCollection>, new()
            where TCollection: List<TItem>, new()
        {
            //log.Info(string.Format("{0}: {1}", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Started.ToString(), logMessage));
            TMessage _retMessage = new TMessage();
            try
            {
                bool _Exists = IQueryableHelper.Any(
                    _Query
                    , currentIndex
                    , pageSize
                    );

                if (_Exists)
                {
                    IEnumerable<TItem> _retval;

                    if (currentIndex == -1 || pageSize == -1)
                    {
                        _retval = _Query;
                    }
                    else
                    {
                        _retval = _Query.Skip(currentIndex).Take(pageSize);
                    }
                    _retMessage.QueryPagingResult = new Framework.Queries.QueryPagingResult();
                    _retMessage.QueryPagingResult.PageSize = pageSize;
                    _retMessage.QueryPagingResult.RecordCountOfCurrentPage = _retval.Count();
                    _retMessage.DataAccessLayerMessageStatus = _retMessage.QueryPagingResult.RecordCountOfCurrentPage == 0 ? Framework.Models.DataAccessLayerMessageStatus.SuccessButNoResult : Framework.Models.DataAccessLayerMessageStatus.Success;
                    _retMessage.QueryPagingResult.CurrentIndexOfStartRecord = currentIndex;
                    _retMessage.QueryPagingResult.CountOfRecords = _Query.Count();
                    _retMessage.Result = new TCollection();
                    _retMessage.Result.AddRange(_retval);
                }
                else
                {
                    _retMessage.DataAccessLayerMessageStatus = Framework.Models.DataAccessLayerMessageStatus.SuccessButNoResult;
                    _retMessage.Result = null;
                }
            }
            catch (Exception ex)
            {
                _retMessage.DataAccessLayerMessageStatus = Framework.Models.DataAccessLayerMessageStatus.Fail;
                _retMessage.Result = null;
                _retMessage.Message = ex.Message;
                //log.Error(string.Format("{0}: {1}", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Failed.ToString(), logMessage), ex);
            }
            //log.Info(string.Format("{0}: {1}", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Ended.ToString(), logMessage));
            return _retMessage;
        }

        public static TMessage GetSingleMessage<TMessage, TCollection, TItem>(IQueryable<TItem> _Query, int currentIndex, int pageSize, string logMessage)
            where TMessage: Framework.Models.DataAccessLayerMessageBase<TCollection>, new()
            where TCollection: List<TItem>, new()
        {
            //log.Info(string.Format("{0}: {1}", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Started.ToString(), logMessage));
            TMessage _retMessage = new TMessage();
            try
            {
                bool _Exists = IQueryableHelper.Any(
                    _Query
                    , currentIndex
                    , pageSize
                    );

                if (_Exists)
                {
                    IEnumerable<TItem> _retval;
                    _retval = _Query.Take(1);

                    _retMessage.DataAccessLayerMessageStatus = Framework.Models.DataAccessLayerMessageStatus.Success;
                    _retMessage.QueryPagingResult = new Framework.Queries.QueryPagingResult();
                    _retMessage.QueryPagingResult.PageSize = 1;
                    _retMessage.QueryPagingResult.RecordCountOfCurrentPage = 1;
                    _retMessage.QueryPagingResult.CurrentIndexOfStartRecord = 1;
                    _retMessage.QueryPagingResult.CountOfRecords = 1;
                    _retMessage.Result = new TCollection();
                    _retMessage.Result.AddRange(_retval);
                }
                else
                {
                    _retMessage.DataAccessLayerMessageStatus = Framework.Models.DataAccessLayerMessageStatus.SuccessButNoResult;
                    _retMessage.Result = null;
                }
            }
            catch (Exception ex)
            {
                _retMessage.DataAccessLayerMessageStatus = Framework.Models.DataAccessLayerMessageStatus.Fail;
                _retMessage.Result = null;
                _retMessage.Message = ex.Message;
                //log.Error(string.Format("{0}: {1}", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Failed.ToString(), logMessage), ex);
            }
            //log.Info(string.Format("{0}: {1}", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Ended.ToString(), logMessage));
            return _retMessage;
        }

        public static async Task<bool> AnyAsync<T>(IQueryable<T> _Query, int currentIndex, int pageSize)
        {
            bool result;
            if (currentIndex == -1 || pageSize == -1)
            {
                result = await _Query.AnyAsync();
            }
            else
            {
                result = await _Query.Skip(currentIndex).Take(pageSize).AnyAsync();
            }
            return result;
        }

        public static async Task<int> CountAsync<T>(IQueryable<T> _Query, int currentIndex, int pageSize)
        {
            int _Count;
            if (currentIndex == -1 || pageSize == -1)
            {
                _Count = await _Query.CountAsync();
            }
            else
            {
                _Count = await _Query.Skip(currentIndex).Take(pageSize).CountAsync();
            }
            return _Count;
        }

        public static async Task<Framework.Models.DataAccessLayerMessageOfInteger> GetCountMessageAsync<T>(IQueryable<T> _Query, int currentIndex, int pageSize, string logMessage)
        {
            //log.Info(string.Format("{0}: {1}", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Started.ToString(), logMessage));

            Framework.Models.DataAccessLayerMessageOfInteger _retMessage = new Framework.Models.DataAccessLayerMessageOfInteger();
            try
            {
                int _retval = await IQueryableHelper.CountAsync(
                    _Query
                    , currentIndex
                    , pageSize
                    );
                _retMessage.DataAccessLayerMessageStatus = Framework.Models.DataAccessLayerMessageStatus.Success;
                _retMessage.Result = _retval;
            }
            catch (Exception ex)
            {
                _retMessage.DataAccessLayerMessageStatus = Framework.Models.DataAccessLayerMessageStatus.Fail;
                _retMessage.Result = 0;
                _retMessage.Message = ex.Message;
                //log.Error(string.Format("{0}: {1}", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Failed.ToString(), logMessage), ex);
            }
            //log.Info(string.Format("{0}: {1}", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Ended.ToString(), logMessage));
            return _retMessage;
        }

        public static async Task<Framework.Models.DataAccessLayerMessageOfBoolean> GetExistsMessageAsync<T>(IQueryable<T> _Query, int currentIndex, int pageSize, string logMessage)
        {
            //log.Info(string.Format("{0}: {1}", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Started.ToString(), logMessage));
            Framework.Models.DataAccessLayerMessageOfBoolean _retMessage = new Framework.Models.DataAccessLayerMessageOfBoolean();
            try
            {
                bool _retval = await IQueryableHelper.AnyAsync(
                    _Query
                    , currentIndex
                    , pageSize
                    );
                _retMessage.DataAccessLayerMessageStatus = Framework.Models.DataAccessLayerMessageStatus.Success;
                _retMessage.Result = _retval;
            }
            catch (Exception ex)
            {
                _retMessage.DataAccessLayerMessageStatus = Framework.Models.DataAccessLayerMessageStatus.Fail;
                _retMessage.Result = false;
                _retMessage.Message = ex.Message;
                //log.Error(string.Format("{0}: {1}", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Failed.ToString(), logMessage), ex);
            }
            //log.Info(string.Format("{0}: {1}", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Ended.ToString(), logMessage));
            return _retMessage;
        }

        public static async Task<TMessage> GetCollectionMessageAsync<TMessage, TCollection, TItem>(IQueryable<TItem> _Query, int currentIndex, int pageSize, string logMessage)
            where TMessage : Framework.Models.DataAccessLayerMessageBase<TCollection>, new()
            where TCollection : List<TItem>, new()
        {
            //log.Info(string.Format("{0}: {1}", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Started.ToString(), logMessage));
            TMessage _retMessage = new TMessage();
            try
            {
                bool _Exists = await IQueryableHelper.AnyAsync(
                    _Query
                    , currentIndex
                    , pageSize
                    );

                if (_Exists)
                {
                    IEnumerable<TItem> _retval;

                    if (currentIndex == -1 || pageSize == -1)
                    {
                        _retval = await _Query.ToListAsync();
                    }
                    else
                    {
                        _retval = await _Query.Skip(currentIndex).Take(pageSize).ToListAsync();
                    }
                    _retMessage.QueryPagingResult = new Framework.Queries.QueryPagingResult();
                    _retMessage.QueryPagingResult.PageSize = pageSize;
                    _retMessage.QueryPagingResult.RecordCountOfCurrentPage = _retval.Count();
                    _retMessage.DataAccessLayerMessageStatus = _retMessage.QueryPagingResult.RecordCountOfCurrentPage == 0 ? Framework.Models.DataAccessLayerMessageStatus.SuccessButNoResult : Framework.Models.DataAccessLayerMessageStatus.Success;
                    _retMessage.QueryPagingResult.CurrentIndexOfStartRecord = currentIndex;
                    _retMessage.QueryPagingResult.CountOfRecords = _Query.Count();
                    _retMessage.Result = new TCollection();
                    _retMessage.Result.AddRange(_retval);
                }
                else
                {
                    _retMessage.DataAccessLayerMessageStatus = Framework.Models.DataAccessLayerMessageStatus.SuccessButNoResult;
                    _retMessage.Result = null;
                }
            }
            catch (Exception ex)
            {
                _retMessage.DataAccessLayerMessageStatus = Framework.Models.DataAccessLayerMessageStatus.Fail;
                _retMessage.Result = null;
                _retMessage.Message = ex.Message;
                //log.Error(string.Format("{0}: {1}", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Failed.ToString(), logMessage), ex);
            }
            //log.Info(string.Format("{0}: {1}", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Ended.ToString(), logMessage));
            return _retMessage;
        }

        public static async Task<TMessage> GetSingleMessageAsync<TMessage, TCollection, TItem>(IQueryable<TItem> _Query, int currentIndex, int pageSize, string logMessage)
            where TMessage : Framework.Models.DataAccessLayerMessageBase<TCollection>, new()
            where TCollection : List<TItem>, new()
        {
            //log.Info(string.Format("{0}: {1}", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Started.ToString(), logMessage));
            TMessage _retMessage = new TMessage();
            try
            {
                bool _Exists = await IQueryableHelper.AnyAsync(
                    _Query
                    , currentIndex
                    , pageSize
                    );

                if (_Exists)
                {
                    IEnumerable<TItem> _retval;
                    _retval = await _Query.Take(1).ToListAsync();

                    _retMessage.DataAccessLayerMessageStatus = Framework.Models.DataAccessLayerMessageStatus.Success;
                    _retMessage.QueryPagingResult = new Framework.Queries.QueryPagingResult();
                    _retMessage.QueryPagingResult.PageSize = 1;
                    _retMessage.QueryPagingResult.RecordCountOfCurrentPage = 1;
                    _retMessage.QueryPagingResult.CurrentIndexOfStartRecord = 1;
                    _retMessage.QueryPagingResult.CountOfRecords = 1;
                    _retMessage.Result = new TCollection();
                    _retMessage.Result.AddRange(_retval);
                }
                else
                {
                    _retMessage.DataAccessLayerMessageStatus = Framework.Models.DataAccessLayerMessageStatus.SuccessButNoResult;
                    _retMessage.Result = null;
                }
            }
            catch (Exception ex)
            {
                _retMessage.DataAccessLayerMessageStatus = Framework.Models.DataAccessLayerMessageStatus.Fail;
                _retMessage.Result = null;
                _retMessage.Message = ex.Message;
                //log.Error(string.Format("{0}: {1}", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Failed.ToString(), logMessage), ex);
            }
            //log.Info(string.Format("{0}: {1}", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Ended.ToString(), logMessage));
            return _retMessage;
        }
    }
}

