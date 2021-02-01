using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Elmah.EntityFrameworkDAL
{
    /// <summary>
    /// Linq data access layer of entity <see cref="Elmah.DataSourceEntities.ELMAH_Error"/>, with following methods:
    /// 1. Insert, Update and Delete
    /// 2. Batch Insert, Update and Delete
    /// 3. Queries
    /// There are 7 methods, 1 internal, 2 private and 4 public, for one set of return value and query criteria:
    /// *internal _GetQueryOfEntityOf...(...) gives the Linq Query
    /// private _ExistsOfEntityOf...(...) returns true if there is any records meets Query Criteria, otherwise false
    /// private _GetCountOfEntityOf...(...) returns count if there is any records meets Query Criteria, otherwise 0
    /// public GetCountOfEntityOf...(...) returns count if there is any records meets Query Criteria, otherwise 0
    /// public ExistsOfEntityOf...(...) returns true if there is any records meets Query Criteria, otherwise false
    /// public GetSingleOfEntityOf...(...) returns the first record if there is any records meets Query Criteria, otherwise null
    /// public GetCollectionOfEntityOf...(...) returns all records if there is any records meets Query Criteria, otherwise null
    /// </summary>
    public class ELMAH_ErrorRepository
        : Elmah.DALContracts.IELMAH_ErrorRepository//<List<Elmah.DataSourceEntities.ELMAH_Error>, Elmah.DataSourceEntities.ELMAH_Error, Elmah.DataSourceEntities.ELMAH_ErrorIdentifier>
    {
        // private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Elmah.EntityFrameworkContext.ElmahModelEntities LinqContext { get; set; }

        public ELMAH_ErrorRepository(Elmah.EntityFrameworkContext.ElmahModelEntities linqContext)
        {
            this.LinqContext = linqContext;
        }

        #region IDataAccessLayerContractBase<List<Elmah.DataSourceEntities.ELMAH_Error>,Elmah.DataSourceEntities.ELMAH_Error,Elmah.DataSourceEntities.ELMAH_ErrorIdentifier> Members

        /// <summary>
        /// single item insert/update with entity input
        /// </summary>
        /// <param name="input">to-be-insert/updated instance of entity class</param>
        public async Task<Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionELMAH_Error> Upsert(Elmah.DataSourceEntities.ELMAH_Error input)
        {
            //log.Info(string.Format("{0}: Update", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Started.ToString()));
            var  _retval = new Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionELMAH_Error();
            _retval.OriginalValue = new List<Elmah.DataSourceEntities.ELMAH_Error>();
            _retval.OriginalValue.Add(input);

            if (input != null)
            {
                var _LinqItem = await GetLinqObjectByIdentifier(input);
                if (_LinqItem == null)
                {
                    _LinqItem = input.GetAClone<Elmah.EntityFrameworkContext.ELMAH_Error>();
                    LinqContext.ELMAH_Error.Add(_LinqItem);
                }
                else
                {
                    input.CopyTo<Elmah.EntityFrameworkContext.ELMAH_Error>(_LinqItem);
                }

                await this.LinqContext.SaveChangesAsync();
                Elmah.DataSourceEntities.ELMAH_Error _Result = new Elmah.DataSourceEntities.ELMAH_Error();
                _Result.CopyFrom<Elmah.EntityFrameworkContext.ELMAH_Error>(_LinqItem);
                _retval.Result = new List<Elmah.DataSourceEntities.ELMAH_Error>();
                _retval.Result.Add(_Result);
            }
            //log.Info(string.Format("{0}: Update", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Started.ToString()));
            return _retval;
        }

        /// <summary>
        /// single item deletion with entity input
        /// </summary>
        /// <param name="input">item to be deleted</param>
        public async Task<Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionELMAH_Error> Delete(Elmah.DataSourceEntities.ELMAH_Error input)
        {
            //log.Info(string.Format("{0}: Delete", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Started.ToString()));

            Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionELMAH_Error  _retval = new Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionELMAH_Error();
            _retval.OriginalValue = new List<Elmah.DataSourceEntities.ELMAH_Error>();
            _retval.OriginalValue.Add(input);

            if (input != null)
            {
                Elmah.EntityFrameworkContext.ELMAH_Error _LinqItem = await GetLinqObjectByIdentifier(input);
                if (_LinqItem != null)
                {
                    this.LinqContext.ELMAH_Error.Remove(_LinqItem);
                    await this.LinqContext.SaveChangesAsync();
                }
            }
            //log.Info(string.Format("{0}: Delete", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Ended.ToString()));
            return _retval;
        }

        /// <summary>
        /// single item delete by identifier with entity identifier input
        /// </summary>
        /// <param name="id">delete the item if its identifier equals to this parameter</param>
        public async Task<Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionELMAH_Error> DeleteByIdentifier(Elmah.DataSourceEntities.ELMAH_ErrorIdentifier id)
        {
            //log.Info(string.Format("{0}: DeleteByIdentifier", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Started.ToString()));

            Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionELMAH_Error  _retval = new Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionELMAH_Error();

            if (id != null)
            {
                Elmah.EntityFrameworkContext.ELMAH_Error _LinqItem = await GetLinqObjectByIdentifier(id);
                if (_LinqItem != null)
                {
                    Elmah.DataSourceEntities.ELMAH_Error _Original = new Elmah.DataSourceEntities.ELMAH_Error();
                    _Original.CopyFrom<Elmah.EntityFrameworkContext.ELMAH_Error>(_LinqItem);

                    _retval.OriginalValue = new List<Elmah.DataSourceEntities.ELMAH_Error>();
                    _retval.OriginalValue.Add(_Original);

                    this.LinqContext.ELMAH_Error.Remove(_LinqItem);
                    await this.LinqContext.SaveChangesAsync();
                }
            }
            //log.Info(string.Format("{0}: DeleteByIdentifier", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Ended.ToString()));
            return _retval;
        }

/*
        /// <summary>
        /// single item insert with entity input
        /// </summary>
        /// <param name="input">to-be-inserted instance of entity class</param>
        public async Task<Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionELMAH_Error> Insert(Elmah.DataSourceEntities.ELMAH_Error input)
        {
            //log.Info(string.Format("{0}: Insert", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Started.ToString()));
            Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionELMAH_Error  _retval = new Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionELMAH_Error();
            _retval.OriginalValue = new List<Elmah.DataSourceEntities.ELMAH_Error>();
            _retval.OriginalValue.Add(input);

            if (input != null)
            {
                var _LinqItem = input.GetAClone<Elmah.EntityFrameworkContext.ELMAH_Error>();
                this.LinqContext.ELMAH_Error.Add(_LinqItem);
                await this.LinqContext.SaveChangesAsync();
                Elmah.DataSourceEntities.ELMAH_Error _Result = new Elmah.DataSourceEntities.ELMAH_Error();
                _Result.CopyFrom<Elmah.EntityFrameworkContext.ELMAH_Error>(_LinqItem);
                _retval.Result = new List<Elmah.DataSourceEntities.ELMAH_Error>();
                _retval.Result.Add(_Result);
            }

            //log.Info(string.Format("{0}: Insert", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Ended.ToString()));
            return _retval;
        }

        /// <summary>
        /// single item update with entity input
        /// </summary>
        /// <param name="input">to-be-updated instance of entity class</param>
        public async Task<Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionELMAH_Error> Update(Elmah.DataSourceEntities.ELMAH_Error input)
        {
            //log.Info(string.Format("{0}: Update", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Started.ToString()));
            Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionELMAH_Error  _retval = new Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionELMAH_Error();
            _retval.OriginalValue = new List<Elmah.DataSourceEntities.ELMAH_Error>();
            _retval.OriginalValue.Add(input);

            if (input != null)
            {
                Elmah.EntityFrameworkContext.ELMAH_Error _LinqItem = await GetLinqObjectByIdentifier(input);
                if (_LinqItem != null)
                {
                    input.CopyTo<Elmah.DataSourceEntities.ELMAH_Error>(_LinqItem);
                    await this.LinqContext.SaveChangesAsync();
                    Elmah.DataSourceEntities.ELMAH_Error _Result = new Elmah.DataSourceEntities.ELMAH_Error();
                    _Result.CopyFrom<Elmah.EntityFrameworkContext.ELMAH_Error>(_LinqItem);
                    _retval.Result = new List<Elmah.DataSourceEntities.ELMAH_Error>();
                    _retval.Result.Add(_Result);
                }
            }
            //log.Info(string.Format("{0}: Update", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Started.ToString()));
            return _retval;
        }

        /// <summary>
        /// Batches insert with entity collection input.
        /// </summary>
        /// <param name="input">The input.</param>
        public async Task<Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionELMAH_Error> BatchInsert(List<Elmah.DataSourceEntities.ELMAH_Error> input)
        {

            //log.Info(string.Format("{0}: BatchInsert", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Started.ToString()));

            Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionELMAH_Error  _retval = new Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionELMAH_Error();
            _retval.OriginalValue = input;

            if (input != null && input.Count > 0)
            {
                try
                {
                    DataTable _DataTable = new DataTable();
                _DataTable.Columns.Add("ErrorId", typeof(System.Guid));
                _DataTable.Columns.Add("Application", typeof(System.String));
                _DataTable.Columns.Add("Host", typeof(System.String));
                _DataTable.Columns.Add("Type", typeof(System.String));
                _DataTable.Columns.Add("Source", typeof(System.String));
                _DataTable.Columns.Add("Message", typeof(System.String));
                _DataTable.Columns.Add("User", typeof(System.String));
                _DataTable.Columns.Add("StatusCode", typeof(System.Int32));
                _DataTable.Columns.Add("TimeUtc", typeof(System.DateTime));
                _DataTable.Columns.Add("Sequence", typeof(System.Int32));
                _DataTable.Columns.Add("AllXml", typeof(System.String));

                    foreach (Elmah.DataSourceEntities.ELMAH_Error _Item in input)
                    {
                        _DataTable.Rows.Add(new object[] { _Item.ErrorId, _Item.Application, _Item.Host, _Item.Type, _Item.Source, _Item.Message, _Item.User, _Item.StatusCode, _Item.TimeUtc, _Item.Sequence, _Item.AllXml });
                    }

                    using (SqlBulkCopy s = new SqlBulkCopy(this.LinqContext.ConnectionStringOptions.Value.ConnectionString))
                    {
                        s.DestinationTableName = "dbo.ELMAH_Error";

                    s.ColumnMappings.Add("ErrorId", "ErrorId");
                    s.ColumnMappings.Add("Application", "Application");
                    s.ColumnMappings.Add("Host", "Host");
                    s.ColumnMappings.Add("Type", "Type");
                    s.ColumnMappings.Add("Source", "Source");
                    s.ColumnMappings.Add("Message", "Message");
                    s.ColumnMappings.Add("User", "User");
                    s.ColumnMappings.Add("StatusCode", "StatusCode");
                    s.ColumnMappings.Add("TimeUtc", "TimeUtc");
                    s.ColumnMappings.Add("Sequence", "Sequence");
                    s.ColumnMappings.Add("AllXml", "AllXml");

                        s.NotifyAfter = 10000;
                        await s.WriteToServerAsync(_DataTable);
                        s.Close();
                    }
                }
                catch (Exception ex)
                {
                    //log.Error(string.Format("{0}: Insert", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Ended.ToString()), ex);
                }
            }
            //log.Info(string.Format("{0}: BatchInsert", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Ended.ToString()));
            return _retval;

        }

        /// <summary>
        /// Batches the delete with entity collection input.
        /// </summary>
        /// <param name="input">The input.</param>
        public async Task<Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionELMAH_Error> BatchDelete(List<Elmah.DataSourceEntities.ELMAH_Error> input)
        {
            //log.Info(string.Format("{0}: BatchDelete", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Started.ToString()));

            Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionELMAH_Error  _retval = new Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionELMAH_Error();
            _retval.OriginalValue = input;

            if (input != null)
            {
                List<Elmah.EntityFrameworkContext.ELMAH_Error> _ListOfLinq = new List<Elmah.EntityFrameworkContext.ELMAH_Error>();
                foreach (Elmah.DataSourceEntities.ELMAH_Error _ItemOfInput in input)
                {
                    Elmah.EntityFrameworkContext.ELMAH_Error _LinqItem = await GetLinqObjectByIdentifier(_ItemOfInput);
                    this.LinqContext.ELMAH_Error.Remove(_LinqItem);
                }
                await this.LinqContext.SaveChangesAsync();
            }
            //log.Info(string.Format("{0}: BatchDelete", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Ended.ToString()));
            return _retval;
        }

        /// <summary>
        /// Batches the update with entity collection input.
        /// </summary>
        /// <param name="input">The input.</param>
        public async Task<Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionELMAH_Error> BatchUpdate(List<Elmah.DataSourceEntities.ELMAH_Error> input)
        {
            //log.Info(string.Format("{0}: BatchUpdate", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Started.ToString()));

            Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionELMAH_Error  _retval = new Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionELMAH_Error();
            _retval.OriginalValue = input;

            if (input != null)
            {
                List<Elmah.EntityFrameworkContext.ELMAH_Error> _ListOfLinq = new List<Elmah.EntityFrameworkContext.ELMAH_Error>();
                foreach (Elmah.DataSourceEntities.ELMAH_Error _ItemOfInput in input)
                {
                    Elmah.EntityFrameworkContext.ELMAH_Error _LinqItem = await GetLinqObjectByIdentifier(_ItemOfInput);
                    _ItemOfInput.CopyTo<Elmah.EntityFrameworkContext.ELMAH_Error>(_LinqItem);
                    _ListOfLinq.Add(_LinqItem);
                }
                await this.LinqContext.SaveChangesAsync();

                for (int i = 0; i < input.Count; i++)
                {
                    input[i].CopyFrom<Elmah.EntityFrameworkContext.ELMAH_Error>(_LinqItem);
                }
            }
            //log.Info(string.Format("{0}: BatchUpdate", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Ended.ToString()));
            return _retval;
        }

        /// <summary>
        /// Gets an entity instance by input identifier.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>an instance of <see cref="Elmah.DataSourceEntities.ELMAH_ErrorIdentifier"/> class, with same identifier.</returns>
        public async Task<Elmah.DataSourceEntities.ELMAH_Error> GetByIdentifier(Elmah.DataSourceEntities.ELMAH_ErrorIdentifier id)
        {
            Elmah.EntityFrameworkContext.ELMAH_Error _Only = await GetLinqObjectByIdentifier(id);
            if (_Only != null)
            {
                var retval = new Elmah.DataSourceEntities.ELMAH_Error();
                retval.CopyFrom<Elmah.EntityContracts.IELMAH_Error>(_Only);
            }
            else
            {
                return null;
            }
        }
*/

        /// <summary>
        /// Gets the linq object by identifier.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>an instance of <see cref="Elmah.EntityFrameworkContext.ELMAH_Error"/> class, with same identifier, which is a IQueryable.</returns>
        private async Task<Elmah.EntityFrameworkContext.ELMAH_Error> GetLinqObjectByIdentifier(Elmah.EntityContracts.IELMAH_ErrorIdentifier id)
        {
            return await this.LinqContext.ELMAH_Error.SingleOrDefaultAsync<Elmah.EntityFrameworkContext.ELMAH_Error>(
                t=>t.ErrorId == id.ErrorId);
        }

        #endregion

        #region Query Methods Of NameValuePairByCommon

        /// <param name="queryOrderBySettingCollection">query OrderBy setting</param>
        /// <returns>IQueryable&lt;...&gt; of Common</returns>
        internal IQueryable<Framework.Models.NameValuePair> _GetQueryOfNameValuePairByCommon(
            Framework.Queries.QuerySystemStringEqualsCriteria application
            ,Framework.Queries.QuerySystemStringEqualsCriteria host
            ,Framework.Queries.QuerySystemStringEqualsCriteria source
            ,Framework.Queries.QuerySystemInt32EqualsCriteria statusCode
            ,Framework.Queries.QuerySystemStringEqualsCriteria type
            ,Framework.Queries.QuerySystemStringEqualsCriteria user
            ,Framework.Queries.QuerySystemStringContainsCriteria message
            ,Framework.Queries.QuerySystemStringContainsCriteria allXml
            ,Framework.Queries.QuerySystemDateTimeRangeCriteria timeUtcRange
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection
            )
        {

            var _ResultFromDataSource =
                (
                    from t in this.LinqContext.ELMAH_Error
                    join ElmahApplication in this.LinqContext.ElmahApplication on t.Application equals ElmahApplication.Application
                    join ElmahHost in this.LinqContext.ElmahHost on t.Host equals ElmahHost.Host
                    join ElmahSource in this.LinqContext.ElmahSource on t.Source equals ElmahSource.Source
                    join ElmahStatusCode in this.LinqContext.ElmahStatusCode on t.StatusCode equals ElmahStatusCode.StatusCode
                    join ElmahType in this.LinqContext.ElmahType on t.Type equals ElmahType.Type
                    join ElmahUser in this.LinqContext.ElmahUser on t.User equals ElmahUser.User
                    where
                        (
                            (
                            (application.IsToCompare == false || application.IsToCompare && ElmahApplication.Application == application.ValueToCompare)
                            &&
                            (host.IsToCompare == false || host.IsToCompare && ElmahHost.Host == host.ValueToCompare)
                            &&
                            (source.IsToCompare == false || source.IsToCompare && ElmahSource.Source == source.ValueToCompare)
                            &&
                            (statusCode.IsToCompare == false || statusCode.IsToCompare && ElmahStatusCode.StatusCode == statusCode.ValueToCompare)
                            &&
                            (type.IsToCompare == false || type.IsToCompare && ElmahType.Type == type.ValueToCompare)
                            &&
                            (user.IsToCompare == false || user.IsToCompare && ElmahUser.User == user.ValueToCompare)
                            )
                        &&
                            (
                            !message.IsToCompare
                            &&
                            !allXml.IsToCompare
                            ||
                            message.IsToCompare && t.Message.Contains(message.ValueToBeContained)
                            ||
                            allXml.IsToCompare && t.AllXml.Contains(allXml.ValueToBeContained)
                            )
                        &&
                            (
                            !timeUtcRange.IsToCompare
                            ||
                            timeUtcRange.IsToCompare && (timeUtcRange.IsToCompareLowerBound == false || timeUtcRange.IsToCompareLowerBound && t.TimeUtc > timeUtcRange.LowerBound) && (timeUtcRange.IsToCompareUpperBound == false || timeUtcRange.IsToCompareUpperBound && t.TimeUtc <= timeUtcRange.UpperBound)
                            )
                        )
                    select new Framework.Models.NameValuePair { Value = t.ErrorId.ToString(), Name = t.Application } into vD0
                    select vD0
                );
            var _retval = _ResultFromDataSource;
            if (queryOrderBySettingCollection == null || queryOrderBySettingCollection.Count == 0)
            {
                return _retval;
            }
            return _retval.OrderBy(queryOrderBySettingCollection.GetOrderByExpression());

        }

        public async Task<Framework.Models.DataAccessLayerMessageOfInteger> GetCountOfNameValuePairByCommon(
            Framework.Queries.QuerySystemStringEqualsCriteria application
            ,Framework.Queries.QuerySystemStringEqualsCriteria host
            ,Framework.Queries.QuerySystemStringEqualsCriteria source
            ,Framework.Queries.QuerySystemInt32EqualsCriteria statusCode
            ,Framework.Queries.QuerySystemStringEqualsCriteria type
            ,Framework.Queries.QuerySystemStringEqualsCriteria user
            ,Framework.Queries.QuerySystemStringContainsCriteria message
            ,Framework.Queries.QuerySystemStringContainsCriteria allXml
            ,Framework.Queries.QuerySystemDateTimeRangeCriteria timeUtcRange
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            )
        {
            const string logMessageKey = "GetCountOfNameValuePairByCommon";
            IQueryable<Framework.Models.NameValuePair> _Query = _GetQueryOfNameValuePairByCommon(
                application
                ,host
                ,source
                ,statusCode
                ,type
                ,user
                ,message
                ,allXml
                ,timeUtcRange
                ,queryOrderBySettingCollection
            );

            return await Elmah.EntityFrameworkDAL.IQueryableHelper.GetCountMessageAsync(_Query, currentIndex, pageSize, logMessageKey);
        }

        public async Task<Framework.Models.DataAccessLayerMessageOfBoolean> ExistsOfNameValuePairByCommon(
            Framework.Queries.QuerySystemStringEqualsCriteria application
            ,Framework.Queries.QuerySystemStringEqualsCriteria host
            ,Framework.Queries.QuerySystemStringEqualsCriteria source
            ,Framework.Queries.QuerySystemInt32EqualsCriteria statusCode
            ,Framework.Queries.QuerySystemStringEqualsCriteria type
            ,Framework.Queries.QuerySystemStringEqualsCriteria user
            ,Framework.Queries.QuerySystemStringContainsCriteria message
            ,Framework.Queries.QuerySystemStringContainsCriteria allXml
            ,Framework.Queries.QuerySystemDateTimeRangeCriteria timeUtcRange
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            )
        {
            const string logMessageKey = "ExistsOfNameValuePairByCommon";
            IQueryable<Framework.Models.NameValuePair> _Query = _GetQueryOfNameValuePairByCommon(
                application
                ,host
                ,source
                ,statusCode
                ,type
                ,user
                ,message
                ,allXml
                ,timeUtcRange
                ,queryOrderBySettingCollection
            );

            return await Elmah.EntityFrameworkDAL.IQueryableHelper.GetExistsMessageAsync(_Query, currentIndex, pageSize, logMessageKey);
        }

        public async Task<Framework.Models.DataAccessLayerMessageOfNameValuePairEntityCollection> GetCollectionOfNameValuePairByCommon(
            Framework.Queries.QuerySystemStringEqualsCriteria application
            ,Framework.Queries.QuerySystemStringEqualsCriteria host
            ,Framework.Queries.QuerySystemStringEqualsCriteria source
            ,Framework.Queries.QuerySystemInt32EqualsCriteria statusCode
            ,Framework.Queries.QuerySystemStringEqualsCriteria type
            ,Framework.Queries.QuerySystemStringEqualsCriteria user
            ,Framework.Queries.QuerySystemStringContainsCriteria message
            ,Framework.Queries.QuerySystemStringContainsCriteria allXml
            ,Framework.Queries.QuerySystemDateTimeRangeCriteria timeUtcRange
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            )
        {
            const string logMessageKey = "GetCollectionOfNameValuePairByCommon";
            IQueryable<Framework.Models.NameValuePair> _Query = _GetQueryOfNameValuePairByCommon(
                application
                ,host
                ,source
                ,statusCode
                ,type
                ,user
                ,message
                ,allXml
                ,timeUtcRange
                ,queryOrderBySettingCollection
            );
            return await Elmah.EntityFrameworkDAL.IQueryableHelper.GetCollectionMessageAsync<
                Framework.Models.DataAccessLayerMessageOfNameValuePairEntityCollection,
                List<Framework.Models.NameValuePair>,
                Framework.Models.NameValuePair>(_Query, currentIndex, pageSize, logMessageKey);

        }

        #endregion Query Methods Of NameValuePairByCommon

        #region Query Methods Of DefaultByCommon

        /// <param name="queryOrderBySettingCollection">query OrderBy setting</param>
        /// <returns>IQueryable&lt;...&gt; of Common</returns>
        internal IQueryable<Elmah.DataSourceEntities.ELMAH_Error.Default> _GetQueryOfDefaultByCommon(
            Framework.Queries.QuerySystemStringEqualsCriteria application
            ,Framework.Queries.QuerySystemStringEqualsCriteria host
            ,Framework.Queries.QuerySystemStringEqualsCriteria source
            ,Framework.Queries.QuerySystemInt32EqualsCriteria statusCode
            ,Framework.Queries.QuerySystemStringEqualsCriteria type
            ,Framework.Queries.QuerySystemStringEqualsCriteria user
            ,Framework.Queries.QuerySystemStringContainsCriteria message
            ,Framework.Queries.QuerySystemStringContainsCriteria allXml
            ,Framework.Queries.QuerySystemDateTimeRangeCriteria timeUtcRange
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection
            )
        {

            var _ResultFromDataSource =
                (
                    from t in this.LinqContext.ELMAH_Error
                    join ElmahApplication in this.LinqContext.ElmahApplication on t.Application equals ElmahApplication.Application
                    join ElmahHost in this.LinqContext.ElmahHost on t.Host equals ElmahHost.Host
                    join ElmahSource in this.LinqContext.ElmahSource on t.Source equals ElmahSource.Source
                    join ElmahStatusCode in this.LinqContext.ElmahStatusCode on t.StatusCode equals ElmahStatusCode.StatusCode
                    join ElmahType in this.LinqContext.ElmahType on t.Type equals ElmahType.Type
                    join ElmahUser in this.LinqContext.ElmahUser on t.User equals ElmahUser.User
                    where
                        (
                            (
                            (application.IsToCompare == false || application.IsToCompare && ElmahApplication.Application == application.ValueToCompare)
                            &&
                            (host.IsToCompare == false || host.IsToCompare && ElmahHost.Host == host.ValueToCompare)
                            &&
                            (source.IsToCompare == false || source.IsToCompare && ElmahSource.Source == source.ValueToCompare)
                            &&
                            (statusCode.IsToCompare == false || statusCode.IsToCompare && ElmahStatusCode.StatusCode == statusCode.ValueToCompare)
                            &&
                            (type.IsToCompare == false || type.IsToCompare && ElmahType.Type == type.ValueToCompare)
                            &&
                            (user.IsToCompare == false || user.IsToCompare && ElmahUser.User == user.ValueToCompare)
                            )
                        &&
                            (
                            !message.IsToCompare
                            &&
                            !allXml.IsToCompare
                            ||
                            message.IsToCompare && t.Message.Contains(message.ValueToBeContained)
                            ||
                            allXml.IsToCompare && t.AllXml.Contains(allXml.ValueToBeContained)
                            )
                        &&
                            (
                            !timeUtcRange.IsToCompare
                            ||
                            timeUtcRange.IsToCompare && (timeUtcRange.IsToCompareLowerBound == false || timeUtcRange.IsToCompareLowerBound && t.TimeUtc > timeUtcRange.LowerBound) && (timeUtcRange.IsToCompareUpperBound == false || timeUtcRange.IsToCompareUpperBound && t.TimeUtc <= timeUtcRange.UpperBound)
                            )
                        )
                    select new Elmah.DataSourceEntities.ELMAH_Error.Default { ElmahApplication_Name = ElmahApplication.Application, ErrorId = t.ErrorId, ElmahHost_Name = ElmahHost.Host, ElmahSource_Name = ElmahSource.Source, ElmahStatusCode_Name = ElmahStatusCode.Name, ElmahType_Name = ElmahType.Type, ElmahUser_Name = ElmahUser.User, Application = t.Application, Host = t.Host, Type = t.Type, Source = t.Source, Message = t.Message, User = t.User, StatusCode = t.StatusCode, TimeUtc = t.TimeUtc, Sequence = t.Sequence, AllXml = t.AllXml } into vD2
                    select vD2
                );
            var _retval = _ResultFromDataSource;
            if (queryOrderBySettingCollection == null || queryOrderBySettingCollection.Count == 0)
            {
                return _retval;
            }
            return _retval.OrderBy(queryOrderBySettingCollection.GetOrderByExpression());

        }

        public async Task<Framework.Models.DataAccessLayerMessageOfInteger> GetCountOfDefaultByCommon(
            Framework.Queries.QuerySystemStringEqualsCriteria application
            ,Framework.Queries.QuerySystemStringEqualsCriteria host
            ,Framework.Queries.QuerySystemStringEqualsCriteria source
            ,Framework.Queries.QuerySystemInt32EqualsCriteria statusCode
            ,Framework.Queries.QuerySystemStringEqualsCriteria type
            ,Framework.Queries.QuerySystemStringEqualsCriteria user
            ,Framework.Queries.QuerySystemStringContainsCriteria message
            ,Framework.Queries.QuerySystemStringContainsCriteria allXml
            ,Framework.Queries.QuerySystemDateTimeRangeCriteria timeUtcRange
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            )
        {
            const string logMessageKey = "GetCountOfDefaultByCommon";
            IQueryable<Elmah.DataSourceEntities.ELMAH_Error.Default> _Query = _GetQueryOfDefaultByCommon(
                application
                ,host
                ,source
                ,statusCode
                ,type
                ,user
                ,message
                ,allXml
                ,timeUtcRange
                ,queryOrderBySettingCollection
            );

            return await Elmah.EntityFrameworkDAL.IQueryableHelper.GetCountMessageAsync(_Query, currentIndex, pageSize, logMessageKey);
        }

        public async Task<Framework.Models.DataAccessLayerMessageOfBoolean> ExistsOfDefaultByCommon(
            Framework.Queries.QuerySystemStringEqualsCriteria application
            ,Framework.Queries.QuerySystemStringEqualsCriteria host
            ,Framework.Queries.QuerySystemStringEqualsCriteria source
            ,Framework.Queries.QuerySystemInt32EqualsCriteria statusCode
            ,Framework.Queries.QuerySystemStringEqualsCriteria type
            ,Framework.Queries.QuerySystemStringEqualsCriteria user
            ,Framework.Queries.QuerySystemStringContainsCriteria message
            ,Framework.Queries.QuerySystemStringContainsCriteria allXml
            ,Framework.Queries.QuerySystemDateTimeRangeCriteria timeUtcRange
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            )
        {
            const string logMessageKey = "ExistsOfDefaultByCommon";
            IQueryable<Elmah.DataSourceEntities.ELMAH_Error.Default> _Query = _GetQueryOfDefaultByCommon(
                application
                ,host
                ,source
                ,statusCode
                ,type
                ,user
                ,message
                ,allXml
                ,timeUtcRange
                ,queryOrderBySettingCollection
            );

            return await Elmah.EntityFrameworkDAL.IQueryableHelper.GetExistsMessageAsync(_Query, currentIndex, pageSize, logMessageKey);
        }

        public async Task<Elmah.DataSourceEntities.ELMAH_Error.DataAccessLayerMessageOfDefaultCollection> GetCollectionOfDefaultByCommon(
            Framework.Queries.QuerySystemStringEqualsCriteria application
            ,Framework.Queries.QuerySystemStringEqualsCriteria host
            ,Framework.Queries.QuerySystemStringEqualsCriteria source
            ,Framework.Queries.QuerySystemInt32EqualsCriteria statusCode
            ,Framework.Queries.QuerySystemStringEqualsCriteria type
            ,Framework.Queries.QuerySystemStringEqualsCriteria user
            ,Framework.Queries.QuerySystemStringContainsCriteria message
            ,Framework.Queries.QuerySystemStringContainsCriteria allXml
            ,Framework.Queries.QuerySystemDateTimeRangeCriteria timeUtcRange
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            )
        {
            const string logMessageKey = "GetCollectionOfDefaultByCommon";
            IQueryable<Elmah.DataSourceEntities.ELMAH_Error.Default> _Query = _GetQueryOfDefaultByCommon(
                application
                ,host
                ,source
                ,statusCode
                ,type
                ,user
                ,message
                ,allXml
                ,timeUtcRange
                ,queryOrderBySettingCollection
            );
            return await Elmah.EntityFrameworkDAL.IQueryableHelper.GetCollectionMessageAsync<
                Elmah.DataSourceEntities.ELMAH_Error.DataAccessLayerMessageOfDefaultCollection,
                List<Elmah.DataSourceEntities.ELMAH_Error.Default>,
                Elmah.DataSourceEntities.ELMAH_Error.Default>(_Query, currentIndex, pageSize, logMessageKey);

        }

        #endregion Query Methods Of DefaultByCommon

        #region Query Methods Of EntityByIdentifier

        /// <param name="queryOrderBySettingCollection">query OrderBy setting</param>
        /// <returns>IQueryable&lt;...&gt; of Common</returns>
        internal IQueryable<Elmah.DataSourceEntities.ELMAH_Error> _GetQueryOfEntityByIdentifier(
            Framework.Queries.QuerySystemGuidEqualsCriteria errorId
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection
            )
        {

            var _ResultFromDataSource =
                (
                    from t in this.LinqContext.ELMAH_Error
                    where
                        (
                            (
                            (errorId.IsToCompare == false || errorId.IsToCompare && t.ErrorId == errorId.ValueToCompare)
                            )
                        )
                    select new Elmah.DataSourceEntities.ELMAH_Error { ErrorId = t.ErrorId, Application = t.Application, Host = t.Host, Type = t.Type, Source = t.Source, Message = t.Message, User = t.User, StatusCode = t.StatusCode, TimeUtc = t.TimeUtc, Sequence = t.Sequence, AllXml = t.AllXml } into x
                    select x
                );
            var _retval = _ResultFromDataSource;
            if (queryOrderBySettingCollection == null || queryOrderBySettingCollection.Count == 0)
            {
                return _retval;
            }
            return _retval.OrderBy(queryOrderBySettingCollection.GetOrderByExpression());

        }

        public async Task<Framework.Models.DataAccessLayerMessageOfInteger> GetCountOfEntityByIdentifier(
            Framework.Queries.QuerySystemGuidEqualsCriteria errorId
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            )
        {
            const string logMessageKey = "GetCountOfEntityByIdentifier";
            IQueryable<Elmah.DataSourceEntities.ELMAH_Error> _Query = _GetQueryOfEntityByIdentifier(
                errorId
                ,queryOrderBySettingCollection
            );

            return await Elmah.EntityFrameworkDAL.IQueryableHelper.GetCountMessageAsync(_Query, currentIndex, pageSize, logMessageKey);
        }

        public async Task<Framework.Models.DataAccessLayerMessageOfBoolean> ExistsOfEntityByIdentifier(
            Framework.Queries.QuerySystemGuidEqualsCriteria errorId
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            )
        {
            const string logMessageKey = "ExistsOfEntityByIdentifier";
            IQueryable<Elmah.DataSourceEntities.ELMAH_Error> _Query = _GetQueryOfEntityByIdentifier(
                errorId
                ,queryOrderBySettingCollection
            );

            return await Elmah.EntityFrameworkDAL.IQueryableHelper.GetExistsMessageAsync(_Query, currentIndex, pageSize, logMessageKey);
        }

        public async Task<Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionELMAH_Error> GetCollectionOfEntityByIdentifier(
            Framework.Queries.QuerySystemGuidEqualsCriteria errorId
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            )
        {
            const string logMessageKey = "GetCollectionOfEntityByIdentifier";
            IQueryable<Elmah.DataSourceEntities.ELMAH_Error> _Query = _GetQueryOfEntityByIdentifier(
                errorId
                ,queryOrderBySettingCollection
            );
            return await Elmah.EntityFrameworkDAL.IQueryableHelper.GetCollectionMessageAsync<
                Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionELMAH_Error,
                List<Elmah.DataSourceEntities.ELMAH_Error>,
                Elmah.DataSourceEntities.ELMAH_Error>(_Query, currentIndex, pageSize, logMessageKey);

        }

        #endregion Query Methods Of EntityByIdentifier

        #region Query Methods Of DefaultByIdentifier

        /// <param name="queryOrderBySettingCollection">query OrderBy setting</param>
        /// <returns>IQueryable&lt;...&gt; of Common</returns>
        internal IQueryable<Elmah.DataSourceEntities.ELMAH_Error.Default> _GetQueryOfDefaultByIdentifier(
            Framework.Queries.QuerySystemGuidEqualsCriteria errorId
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection
            )
        {

            var _ResultFromDataSource =
                (
                    from t in this.LinqContext.ELMAH_Error
                    join ElmahApplication in this.LinqContext.ElmahApplication on t.Application equals ElmahApplication.Application
                    join ElmahHost in this.LinqContext.ElmahHost on t.Host equals ElmahHost.Host
                    join ElmahSource in this.LinqContext.ElmahSource on t.Source equals ElmahSource.Source
                    join ElmahStatusCode in this.LinqContext.ElmahStatusCode on t.StatusCode equals ElmahStatusCode.StatusCode
                    join ElmahType in this.LinqContext.ElmahType on t.Type equals ElmahType.Type
                    join ElmahUser in this.LinqContext.ElmahUser on t.User equals ElmahUser.User
                    where
                        (
                            (
                            (errorId.IsToCompare == false || errorId.IsToCompare && t.ErrorId == errorId.ValueToCompare)
                            )
                        )
                    select new Elmah.DataSourceEntities.ELMAH_Error.Default { ElmahApplication_Name = ElmahApplication.Application, ErrorId = t.ErrorId, ElmahHost_Name = ElmahHost.Host, ElmahSource_Name = ElmahSource.Source, ElmahStatusCode_Name = ElmahStatusCode.Name, ElmahType_Name = ElmahType.Type, ElmahUser_Name = ElmahUser.User, Application = t.Application, Host = t.Host, Type = t.Type, Source = t.Source, Message = t.Message, User = t.User, StatusCode = t.StatusCode, TimeUtc = t.TimeUtc, Sequence = t.Sequence, AllXml = t.AllXml } into vD2
                    select vD2
                );
            var _retval = _ResultFromDataSource;
            if (queryOrderBySettingCollection == null || queryOrderBySettingCollection.Count == 0)
            {
                return _retval;
            }
            return _retval.OrderBy(queryOrderBySettingCollection.GetOrderByExpression());

        }

        public async Task<Framework.Models.DataAccessLayerMessageOfInteger> GetCountOfDefaultByIdentifier(
            Framework.Queries.QuerySystemGuidEqualsCriteria errorId
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            )
        {
            const string logMessageKey = "GetCountOfDefaultByIdentifier";
            IQueryable<Elmah.DataSourceEntities.ELMAH_Error.Default> _Query = _GetQueryOfDefaultByIdentifier(
                errorId
                ,queryOrderBySettingCollection
            );

            return await Elmah.EntityFrameworkDAL.IQueryableHelper.GetCountMessageAsync(_Query, currentIndex, pageSize, logMessageKey);
        }

        public async Task<Framework.Models.DataAccessLayerMessageOfBoolean> ExistsOfDefaultByIdentifier(
            Framework.Queries.QuerySystemGuidEqualsCriteria errorId
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            )
        {
            const string logMessageKey = "ExistsOfDefaultByIdentifier";
            IQueryable<Elmah.DataSourceEntities.ELMAH_Error.Default> _Query = _GetQueryOfDefaultByIdentifier(
                errorId
                ,queryOrderBySettingCollection
            );

            return await Elmah.EntityFrameworkDAL.IQueryableHelper.GetExistsMessageAsync(_Query, currentIndex, pageSize, logMessageKey);
        }

        public async Task<Elmah.DataSourceEntities.ELMAH_Error.DataAccessLayerMessageOfDefaultCollection> GetCollectionOfDefaultByIdentifier(
            Framework.Queries.QuerySystemGuidEqualsCriteria errorId
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            )
        {
            const string logMessageKey = "GetCollectionOfDefaultByIdentifier";
            IQueryable<Elmah.DataSourceEntities.ELMAH_Error.Default> _Query = _GetQueryOfDefaultByIdentifier(
                errorId
                ,queryOrderBySettingCollection
            );
            return await Elmah.EntityFrameworkDAL.IQueryableHelper.GetCollectionMessageAsync<
                Elmah.DataSourceEntities.ELMAH_Error.DataAccessLayerMessageOfDefaultCollection,
                List<Elmah.DataSourceEntities.ELMAH_Error.Default>,
                Elmah.DataSourceEntities.ELMAH_Error.Default>(_Query, currentIndex, pageSize, logMessageKey);

        }

        #endregion Query Methods Of DefaultByIdentifier

    }
}

