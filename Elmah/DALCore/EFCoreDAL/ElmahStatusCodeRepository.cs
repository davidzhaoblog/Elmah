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
    /// Linq data access layer of entity <see cref="Elmah.DataSourceEntities.ElmahStatusCode"/>, with following methods:
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
    public class ElmahStatusCodeRepository
        : Elmah.DALContracts.IElmahStatusCodeRepository//<List<Elmah.DataSourceEntities.ElmahStatusCode>, Elmah.DataSourceEntities.ElmahStatusCode, Elmah.DataSourceEntities.ElmahStatusCodeIdentifier>
    {
        // private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Elmah.EntityFrameworkContext.ElmahModelEntities LinqContext { get; set; }

        public ElmahStatusCodeRepository(Elmah.EntityFrameworkContext.ElmahModelEntities linqContext)
        {
            this.LinqContext = linqContext;
        }

        #region IDataAccessLayerContractBase<List<Elmah.DataSourceEntities.ElmahStatusCode>,Elmah.DataSourceEntities.ElmahStatusCode,Elmah.DataSourceEntities.ElmahStatusCodeIdentifier> Members

        /// <summary>
        /// single item insert/update with entity input
        /// </summary>
        /// <param name="input">to-be-insert/updated instance of entity class</param>
        public async Task<Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahStatusCode> Upsert(Elmah.DataSourceEntities.ElmahStatusCode input)
        {
            //log.Info(string.Format("{0}: Update", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Started.ToString()));
            var  _retval = new Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahStatusCode();
            _retval.OriginalValue = new List<Elmah.DataSourceEntities.ElmahStatusCode>();
            _retval.OriginalValue.Add(input);

            if (input != null)
            {
                var _LinqItem = await GetLinqObjectByIdentifier(input);
                if (_LinqItem == null)
                {
                    _LinqItem = input.GetAClone<Elmah.EntityFrameworkContext.ElmahStatusCode>();
                    LinqContext.ElmahStatusCode.Add(_LinqItem);
                }
                else
                {
                    input.CopyTo<Elmah.EntityFrameworkContext.ElmahStatusCode>(_LinqItem);
                }

                await this.LinqContext.SaveChangesAsync();
                Elmah.DataSourceEntities.ElmahStatusCode _Result = new Elmah.DataSourceEntities.ElmahStatusCode();
                _Result.CopyFrom<Elmah.EntityFrameworkContext.ElmahStatusCode>(_LinqItem);
                _retval.Result = new List<Elmah.DataSourceEntities.ElmahStatusCode>();
                _retval.Result.Add(_Result);
            }
            //log.Info(string.Format("{0}: Update", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Started.ToString()));
            return _retval;
        }

        /// <summary>
        /// single item deletion with entity input
        /// </summary>
        /// <param name="input">item to be deleted</param>
        public async Task<Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahStatusCode> Delete(Elmah.DataSourceEntities.ElmahStatusCode input)
        {
            //log.Info(string.Format("{0}: Delete", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Started.ToString()));

            Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahStatusCode  _retval = new Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahStatusCode();
            _retval.OriginalValue = new List<Elmah.DataSourceEntities.ElmahStatusCode>();
            _retval.OriginalValue.Add(input);

            if (input != null)
            {
                Elmah.EntityFrameworkContext.ElmahStatusCode _LinqItem = await GetLinqObjectByIdentifier(input);
                if (_LinqItem != null)
                {
                    this.LinqContext.ElmahStatusCode.Remove(_LinqItem);
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
        public async Task<Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahStatusCode> DeleteByIdentifier(Elmah.DataSourceEntities.ElmahStatusCodeIdentifier id)
        {
            //log.Info(string.Format("{0}: DeleteByIdentifier", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Started.ToString()));

            Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahStatusCode  _retval = new Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahStatusCode();

            if (id != null)
            {
                Elmah.EntityFrameworkContext.ElmahStatusCode _LinqItem = await GetLinqObjectByIdentifier(id);
                if (_LinqItem != null)
                {
                    Elmah.DataSourceEntities.ElmahStatusCode _Original = new Elmah.DataSourceEntities.ElmahStatusCode();
                    _Original.CopyFrom<Elmah.EntityFrameworkContext.ElmahStatusCode>(_LinqItem);

                    _retval.OriginalValue = new List<Elmah.DataSourceEntities.ElmahStatusCode>();
                    _retval.OriginalValue.Add(_Original);

                    this.LinqContext.ElmahStatusCode.Remove(_LinqItem);
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
        public async Task<Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahStatusCode> Insert(Elmah.DataSourceEntities.ElmahStatusCode input)
        {
            //log.Info(string.Format("{0}: Insert", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Started.ToString()));
            Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahStatusCode  _retval = new Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahStatusCode();
            _retval.OriginalValue = new List<Elmah.DataSourceEntities.ElmahStatusCode>();
            _retval.OriginalValue.Add(input);

            if (input != null)
            {
                var _LinqItem = input.GetAClone<Elmah.EntityFrameworkContext.ElmahStatusCode>();
                this.LinqContext.ElmahStatusCode.Add(_LinqItem);
                await this.LinqContext.SaveChangesAsync();
                Elmah.DataSourceEntities.ElmahStatusCode _Result = new Elmah.DataSourceEntities.ElmahStatusCode();
                _Result.CopyFrom<Elmah.EntityFrameworkContext.ElmahStatusCode>(_LinqItem);
                _retval.Result = new List<Elmah.DataSourceEntities.ElmahStatusCode>();
                _retval.Result.Add(_Result);
            }

            //log.Info(string.Format("{0}: Insert", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Ended.ToString()));
            return _retval;
        }

        /// <summary>
        /// single item update with entity input
        /// </summary>
        /// <param name="input">to-be-updated instance of entity class</param>
        public async Task<Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahStatusCode> Update(Elmah.DataSourceEntities.ElmahStatusCode input)
        {
            //log.Info(string.Format("{0}: Update", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Started.ToString()));
            Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahStatusCode  _retval = new Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahStatusCode();
            _retval.OriginalValue = new List<Elmah.DataSourceEntities.ElmahStatusCode>();
            _retval.OriginalValue.Add(input);

            if (input != null)
            {
                Elmah.EntityFrameworkContext.ElmahStatusCode _LinqItem = await GetLinqObjectByIdentifier(input);
                if (_LinqItem != null)
                {
                    input.CopyTo<Elmah.DataSourceEntities.ElmahStatusCode>(_LinqItem);
                    await this.LinqContext.SaveChangesAsync();
                    Elmah.DataSourceEntities.ElmahStatusCode _Result = new Elmah.DataSourceEntities.ElmahStatusCode();
                    _Result.CopyFrom<Elmah.EntityFrameworkContext.ElmahStatusCode>(_LinqItem);
                    _retval.Result = new List<Elmah.DataSourceEntities.ElmahStatusCode>();
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
        public async Task<Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahStatusCode> BatchInsert(List<Elmah.DataSourceEntities.ElmahStatusCode> input)
        {

            //log.Info(string.Format("{0}: BatchInsert", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Started.ToString()));

            Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahStatusCode  _retval = new Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahStatusCode();
            _retval.OriginalValue = input;

            if (input != null && input.Count > 0)
            {
                try
                {
                    DataTable _DataTable = new DataTable();
                _DataTable.Columns.Add("StatusCode", typeof(System.Int32));
                _DataTable.Columns.Add("Name", typeof(System.String));

                    foreach (Elmah.DataSourceEntities.ElmahStatusCode _Item in input)
                    {
                        _DataTable.Rows.Add(new object[] { _Item.StatusCode, _Item.Name });
                    }

                    using (SqlBulkCopy s = new SqlBulkCopy(this.LinqContext.ConnectionStringOptions.Value.ConnectionString))
                    {
                        s.DestinationTableName = "dbo.ElmahStatusCode";

                    s.ColumnMappings.Add("StatusCode", "StatusCode");
                    s.ColumnMappings.Add("Name", "Name");

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
        public async Task<Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahStatusCode> BatchDelete(List<Elmah.DataSourceEntities.ElmahStatusCode> input)
        {
            //log.Info(string.Format("{0}: BatchDelete", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Started.ToString()));

            Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahStatusCode  _retval = new Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahStatusCode();
            _retval.OriginalValue = input;

            if (input != null)
            {
                List<Elmah.EntityFrameworkContext.ElmahStatusCode> _ListOfLinq = new List<Elmah.EntityFrameworkContext.ElmahStatusCode>();
                foreach (Elmah.DataSourceEntities.ElmahStatusCode _ItemOfInput in input)
                {
                    Elmah.EntityFrameworkContext.ElmahStatusCode _LinqItem = await GetLinqObjectByIdentifier(_ItemOfInput);
                    this.LinqContext.ElmahStatusCode.Remove(_LinqItem);
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
        public async Task<Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahStatusCode> BatchUpdate(List<Elmah.DataSourceEntities.ElmahStatusCode> input)
        {
            //log.Info(string.Format("{0}: BatchUpdate", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Started.ToString()));

            Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahStatusCode  _retval = new Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahStatusCode();
            _retval.OriginalValue = input;

            if (input != null)
            {
                List<Elmah.EntityFrameworkContext.ElmahStatusCode> _ListOfLinq = new List<Elmah.EntityFrameworkContext.ElmahStatusCode>();
                foreach (Elmah.DataSourceEntities.ElmahStatusCode _ItemOfInput in input)
                {
                    Elmah.EntityFrameworkContext.ElmahStatusCode _LinqItem = await GetLinqObjectByIdentifier(_ItemOfInput);
                    _ItemOfInput.CopyTo<Elmah.EntityFrameworkContext.ElmahStatusCode>(_LinqItem);
                    _ListOfLinq.Add(_LinqItem);
                }
                await this.LinqContext.SaveChangesAsync();

                for (int i = 0; i < input.Count; i++)
                {
                    input[i].CopyFrom<Elmah.EntityFrameworkContext.ElmahStatusCode>(_LinqItem);
                }
            }
            //log.Info(string.Format("{0}: BatchUpdate", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Ended.ToString()));
            return _retval;
        }

        /// <summary>
        /// Gets an entity instance by input identifier.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>an instance of <see cref="Elmah.DataSourceEntities.ElmahStatusCodeIdentifier"/> class, with same identifier.</returns>
        public async Task<Elmah.DataSourceEntities.ElmahStatusCode> GetByIdentifier(Elmah.DataSourceEntities.ElmahStatusCodeIdentifier id)
        {
            Elmah.EntityFrameworkContext.ElmahStatusCode _Only = await GetLinqObjectByIdentifier(id);
            if (_Only != null)
            {
                var retval = new Elmah.DataSourceEntities.ElmahStatusCode();
                retval.CopyFrom<Elmah.EntityContracts.IElmahStatusCode>(_Only);
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
        /// <returns>an instance of <see cref="Elmah.EntityFrameworkContext.ElmahStatusCode"/> class, with same identifier, which is a IQueryable.</returns>
        private async Task<Elmah.EntityFrameworkContext.ElmahStatusCode> GetLinqObjectByIdentifier(Elmah.EntityContracts.IElmahStatusCodeIdentifier id)
        {
            return await this.LinqContext.ElmahStatusCode.SingleOrDefaultAsync<Elmah.EntityFrameworkContext.ElmahStatusCode>(
                t=>t.StatusCode == id.StatusCode);
        }

        #endregion

        #region Query Methods Of EntityByCommon

        /// <param name="queryOrderBySettingCollection">query OrderBy setting</param>
        /// <returns>IQueryable&lt;...&gt; of Common</returns>
        internal IQueryable<Elmah.DataSourceEntities.ElmahStatusCode> _GetQueryOfEntityByCommon(
            Framework.Queries.QuerySystemStringContainsCriteria name
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection
            )
        {

            var _ResultFromDataSource =
                (
                    from t in this.LinqContext.ElmahStatusCode
                    where
                        (
                            (
                            !name.IsToCompare
                            ||
                            name.IsToCompare && t.Name.Contains(name.ValueToBeContained)
                            )
                        )
                    select new Elmah.DataSourceEntities.ElmahStatusCode { StatusCode = t.StatusCode, Name = t.Name } into x
                    select x
                );
            var _retval = _ResultFromDataSource;
            if (queryOrderBySettingCollection == null || queryOrderBySettingCollection.Count == 0)
            {
                return _retval;
            }
            return _retval.OrderBy(queryOrderBySettingCollection.GetOrderByExpression());

        }

        public async Task<Framework.Models.DataAccessLayerMessageOfInteger> GetCountOfEntityByCommon(
            Framework.Queries.QuerySystemStringContainsCriteria name
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            )
        {
            const string logMessageKey = "GetCountOfEntityByCommon";
            IQueryable<Elmah.DataSourceEntities.ElmahStatusCode> _Query = _GetQueryOfEntityByCommon(
                name
                ,queryOrderBySettingCollection
            );

            return await Elmah.EntityFrameworkDAL.IQueryableHelper.GetCountMessageAsync(_Query, currentIndex, pageSize, logMessageKey);
        }

        public async Task<Framework.Models.DataAccessLayerMessageOfBoolean> ExistsOfEntityByCommon(
            Framework.Queries.QuerySystemStringContainsCriteria name
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            )
        {
            const string logMessageKey = "ExistsOfEntityByCommon";
            IQueryable<Elmah.DataSourceEntities.ElmahStatusCode> _Query = _GetQueryOfEntityByCommon(
                name
                ,queryOrderBySettingCollection
            );

            return await Elmah.EntityFrameworkDAL.IQueryableHelper.GetExistsMessageAsync(_Query, currentIndex, pageSize, logMessageKey);
        }

        public async Task<Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahStatusCode> GetCollectionOfEntityByCommon(
            Framework.Queries.QuerySystemStringContainsCriteria name
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            )
        {
            const string logMessageKey = "GetCollectionOfEntityByCommon";
            IQueryable<Elmah.DataSourceEntities.ElmahStatusCode> _Query = _GetQueryOfEntityByCommon(
                name
                ,queryOrderBySettingCollection
            );
            return await Elmah.EntityFrameworkDAL.IQueryableHelper.GetCollectionMessageAsync<
                Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahStatusCode,
                List<Elmah.DataSourceEntities.ElmahStatusCode>,
                Elmah.DataSourceEntities.ElmahStatusCode>(_Query, currentIndex, pageSize, logMessageKey);

        }

        #endregion Query Methods Of EntityByCommon

        #region Query Methods Of NameValuePairByCommon

        /// <param name="queryOrderBySettingCollection">query OrderBy setting</param>
        /// <returns>IQueryable&lt;...&gt; of Common</returns>
        internal IQueryable<Framework.Models.NameValuePair> _GetQueryOfNameValuePairByCommon(
            Framework.Queries.QuerySystemStringContainsCriteria name
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection
            )
        {

            var _ResultFromDataSource =
                (
                    from t in this.LinqContext.ElmahStatusCode
                    where
                        (
                            (
                            !name.IsToCompare
                            ||
                            name.IsToCompare && t.Name.Contains(name.ValueToBeContained)
                            )
                        )
                    select new Framework.Models.NameValuePair { Value = t.StatusCode.ToString(), Name = t.Name } into vD0
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
            Framework.Queries.QuerySystemStringContainsCriteria name
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            )
        {
            const string logMessageKey = "GetCountOfNameValuePairByCommon";
            IQueryable<Framework.Models.NameValuePair> _Query = _GetQueryOfNameValuePairByCommon(
                name
                ,queryOrderBySettingCollection
            );

            return await Elmah.EntityFrameworkDAL.IQueryableHelper.GetCountMessageAsync(_Query, currentIndex, pageSize, logMessageKey);
        }

        public async Task<Framework.Models.DataAccessLayerMessageOfBoolean> ExistsOfNameValuePairByCommon(
            Framework.Queries.QuerySystemStringContainsCriteria name
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            )
        {
            const string logMessageKey = "ExistsOfNameValuePairByCommon";
            IQueryable<Framework.Models.NameValuePair> _Query = _GetQueryOfNameValuePairByCommon(
                name
                ,queryOrderBySettingCollection
            );

            return await Elmah.EntityFrameworkDAL.IQueryableHelper.GetExistsMessageAsync(_Query, currentIndex, pageSize, logMessageKey);
        }

        public async Task<Framework.Models.DataAccessLayerMessageOfNameValuePairEntityCollection> GetCollectionOfNameValuePairByCommon(
            Framework.Queries.QuerySystemStringContainsCriteria name
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            )
        {
            const string logMessageKey = "GetCollectionOfNameValuePairByCommon";
            IQueryable<Framework.Models.NameValuePair> _Query = _GetQueryOfNameValuePairByCommon(
                name
                ,queryOrderBySettingCollection
            );
            return await Elmah.EntityFrameworkDAL.IQueryableHelper.GetCollectionMessageAsync<
                Framework.Models.DataAccessLayerMessageOfNameValuePairEntityCollection,
                List<Framework.Models.NameValuePair>,
                Framework.Models.NameValuePair>(_Query, currentIndex, pageSize, logMessageKey);

        }

        #endregion Query Methods Of NameValuePairByCommon

        #region Query Methods Of EntityByIdentifier

        /// <param name="queryOrderBySettingCollection">query OrderBy setting</param>
        /// <returns>IQueryable&lt;...&gt; of Common</returns>
        internal IQueryable<Elmah.DataSourceEntities.ElmahStatusCode> _GetQueryOfEntityByIdentifier(
            Framework.Queries.QuerySystemInt32EqualsCriteria statusCode
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection
            )
        {

            var _ResultFromDataSource =
                (
                    from t in this.LinqContext.ElmahStatusCode
                    where
                        (
                            (
                            (statusCode.IsToCompare == false || statusCode.IsToCompare && t.StatusCode == statusCode.ValueToCompare)
                            )
                        )
                    select new Elmah.DataSourceEntities.ElmahStatusCode { StatusCode = t.StatusCode, Name = t.Name } into x
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
            Framework.Queries.QuerySystemInt32EqualsCriteria statusCode
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            )
        {
            const string logMessageKey = "GetCountOfEntityByIdentifier";
            IQueryable<Elmah.DataSourceEntities.ElmahStatusCode> _Query = _GetQueryOfEntityByIdentifier(
                statusCode
                ,queryOrderBySettingCollection
            );

            return await Elmah.EntityFrameworkDAL.IQueryableHelper.GetCountMessageAsync(_Query, currentIndex, pageSize, logMessageKey);
        }

        public async Task<Framework.Models.DataAccessLayerMessageOfBoolean> ExistsOfEntityByIdentifier(
            Framework.Queries.QuerySystemInt32EqualsCriteria statusCode
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            )
        {
            const string logMessageKey = "ExistsOfEntityByIdentifier";
            IQueryable<Elmah.DataSourceEntities.ElmahStatusCode> _Query = _GetQueryOfEntityByIdentifier(
                statusCode
                ,queryOrderBySettingCollection
            );

            return await Elmah.EntityFrameworkDAL.IQueryableHelper.GetExistsMessageAsync(_Query, currentIndex, pageSize, logMessageKey);
        }

        public async Task<Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahStatusCode> GetCollectionOfEntityByIdentifier(
            Framework.Queries.QuerySystemInt32EqualsCriteria statusCode
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            )
        {
            const string logMessageKey = "GetCollectionOfEntityByIdentifier";
            IQueryable<Elmah.DataSourceEntities.ElmahStatusCode> _Query = _GetQueryOfEntityByIdentifier(
                statusCode
                ,queryOrderBySettingCollection
            );
            return await Elmah.EntityFrameworkDAL.IQueryableHelper.GetCollectionMessageAsync<
                Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahStatusCode,
                List<Elmah.DataSourceEntities.ElmahStatusCode>,
                Elmah.DataSourceEntities.ElmahStatusCode>(_Query, currentIndex, pageSize, logMessageKey);

        }

        #endregion Query Methods Of EntityByIdentifier

    }
}

