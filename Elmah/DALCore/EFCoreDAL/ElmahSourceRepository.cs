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
    /// Linq data access layer of entity <see cref="Elmah.DataSourceEntities.ElmahSource"/>, with following methods:
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
    public class ElmahSourceRepository
        : Elmah.DALContracts.IElmahSourceRepository//<List<Elmah.DataSourceEntities.ElmahSource>, Elmah.DataSourceEntities.ElmahSource, Elmah.DataSourceEntities.ElmahSourceIdentifier>
    {
        // private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Elmah.EntityFrameworkContext.ElmahModelEntities LinqContext { get; set; }

        public ElmahSourceRepository(Elmah.EntityFrameworkContext.ElmahModelEntities linqContext)
        {
            this.LinqContext = linqContext;
        }

        #region IDataAccessLayerContractBase<List<Elmah.DataSourceEntities.ElmahSource>,Elmah.DataSourceEntities.ElmahSource,Elmah.DataSourceEntities.ElmahSourceIdentifier> Members

        /// <summary>
        /// single item insert/update with entity input
        /// </summary>
        /// <param name="input">to-be-insert/updated instance of entity class</param>
        public async Task<Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahSource> Upsert(Elmah.DataSourceEntities.ElmahSource input)
        {
            //log.Info(string.Format("{0}: Update", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Started.ToString()));
            var  _retval = new Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahSource();
            _retval.OriginalValue = new List<Elmah.DataSourceEntities.ElmahSource>();
            _retval.OriginalValue.Add(input);

            if (input != null)
            {
                var _LinqItem = await GetLinqObjectByIdentifier(input);
                if (_LinqItem == null)
                {
                    _LinqItem = input.GetAClone<Elmah.EntityFrameworkContext.ElmahSource>();
                    LinqContext.ElmahSource.Add(_LinqItem);
                }
                else
                {
                    input.CopyTo<Elmah.EntityFrameworkContext.ElmahSource>(_LinqItem);
                }

                await this.LinqContext.SaveChangesAsync();
                Elmah.DataSourceEntities.ElmahSource _Result = new Elmah.DataSourceEntities.ElmahSource();
                _Result.CopyFrom<Elmah.EntityFrameworkContext.ElmahSource>(_LinqItem);
                _retval.Result = new List<Elmah.DataSourceEntities.ElmahSource>();
                _retval.Result.Add(_Result);
            }
            //log.Info(string.Format("{0}: Update", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Started.ToString()));
            return _retval;
        }

        /// <summary>
        /// single item deletion with entity input
        /// </summary>
        /// <param name="input">item to be deleted</param>
        public async Task<Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahSource> Delete(Elmah.DataSourceEntities.ElmahSource input)
        {
            //log.Info(string.Format("{0}: Delete", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Started.ToString()));

            Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahSource  _retval = new Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahSource();
            _retval.OriginalValue = new List<Elmah.DataSourceEntities.ElmahSource>();
            _retval.OriginalValue.Add(input);

            if (input != null)
            {
                Elmah.EntityFrameworkContext.ElmahSource _LinqItem = await GetLinqObjectByIdentifier(input);
                if (_LinqItem != null)
                {
                    this.LinqContext.ElmahSource.Remove(_LinqItem);
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
        public async Task<Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahSource> DeleteByIdentifier(Elmah.DataSourceEntities.ElmahSourceIdentifier id)
        {
            //log.Info(string.Format("{0}: DeleteByIdentifier", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Started.ToString()));

            Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahSource  _retval = new Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahSource();

            if (id != null)
            {
                Elmah.EntityFrameworkContext.ElmahSource _LinqItem = await GetLinqObjectByIdentifier(id);
                if (_LinqItem != null)
                {
                    Elmah.DataSourceEntities.ElmahSource _Original = new Elmah.DataSourceEntities.ElmahSource();
                    _Original.CopyFrom<Elmah.EntityFrameworkContext.ElmahSource>(_LinqItem);

                    _retval.OriginalValue = new List<Elmah.DataSourceEntities.ElmahSource>();
                    _retval.OriginalValue.Add(_Original);

                    this.LinqContext.ElmahSource.Remove(_LinqItem);
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
        public async Task<Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahSource> Insert(Elmah.DataSourceEntities.ElmahSource input)
        {
            //log.Info(string.Format("{0}: Insert", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Started.ToString()));
            Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahSource  _retval = new Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahSource();
            _retval.OriginalValue = new List<Elmah.DataSourceEntities.ElmahSource>();
            _retval.OriginalValue.Add(input);

            if (input != null)
            {
                var _LinqItem = input.GetAClone<Elmah.EntityFrameworkContext.ElmahSource>();
                this.LinqContext.ElmahSource.Add(_LinqItem);
                await this.LinqContext.SaveChangesAsync();
                Elmah.DataSourceEntities.ElmahSource _Result = new Elmah.DataSourceEntities.ElmahSource();
                _Result.CopyFrom<Elmah.EntityFrameworkContext.ElmahSource>(_LinqItem);
                _retval.Result = new List<Elmah.DataSourceEntities.ElmahSource>();
                _retval.Result.Add(_Result);
            }

            //log.Info(string.Format("{0}: Insert", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Ended.ToString()));
            return _retval;
        }

        /// <summary>
        /// single item update with entity input
        /// </summary>
        /// <param name="input">to-be-updated instance of entity class</param>
        public async Task<Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahSource> Update(Elmah.DataSourceEntities.ElmahSource input)
        {
            //log.Info(string.Format("{0}: Update", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Started.ToString()));
            Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahSource  _retval = new Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahSource();
            _retval.OriginalValue = new List<Elmah.DataSourceEntities.ElmahSource>();
            _retval.OriginalValue.Add(input);

            if (input != null)
            {
                Elmah.EntityFrameworkContext.ElmahSource _LinqItem = await GetLinqObjectByIdentifier(input);
                if (_LinqItem != null)
                {
                    input.CopyTo<Elmah.DataSourceEntities.ElmahSource>(_LinqItem);
                    await this.LinqContext.SaveChangesAsync();
                    Elmah.DataSourceEntities.ElmahSource _Result = new Elmah.DataSourceEntities.ElmahSource();
                    _Result.CopyFrom<Elmah.EntityFrameworkContext.ElmahSource>(_LinqItem);
                    _retval.Result = new List<Elmah.DataSourceEntities.ElmahSource>();
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
        public async Task<Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahSource> BatchInsert(List<Elmah.DataSourceEntities.ElmahSource> input)
        {

            //log.Info(string.Format("{0}: BatchInsert", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Started.ToString()));

            Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahSource  _retval = new Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahSource();
            _retval.OriginalValue = input;

            if (input != null && input.Count > 0)
            {
                try
                {
                    DataTable _DataTable = new DataTable();
                _DataTable.Columns.Add("Source", typeof(System.String));

                    foreach (Elmah.DataSourceEntities.ElmahSource _Item in input)
                    {
                        _DataTable.Rows.Add(new object[] { _Item.Source });
                    }

                    using (SqlBulkCopy s = new SqlBulkCopy(this.LinqContext.ConnectionStringOptions.Value.ConnectionString))
                    {
                        s.DestinationTableName = "dbo.ElmahSource";

                    s.ColumnMappings.Add("Source", "Source");

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
        public async Task<Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahSource> BatchDelete(List<Elmah.DataSourceEntities.ElmahSource> input)
        {
            //log.Info(string.Format("{0}: BatchDelete", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Started.ToString()));

            Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahSource  _retval = new Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahSource();
            _retval.OriginalValue = input;

            if (input != null)
            {
                List<Elmah.EntityFrameworkContext.ElmahSource> _ListOfLinq = new List<Elmah.EntityFrameworkContext.ElmahSource>();
                foreach (Elmah.DataSourceEntities.ElmahSource _ItemOfInput in input)
                {
                    Elmah.EntityFrameworkContext.ElmahSource _LinqItem = await GetLinqObjectByIdentifier(_ItemOfInput);
                    this.LinqContext.ElmahSource.Remove(_LinqItem);
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
        public async Task<Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahSource> BatchUpdate(List<Elmah.DataSourceEntities.ElmahSource> input)
        {
            //log.Info(string.Format("{0}: BatchUpdate", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Started.ToString()));

            Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahSource  _retval = new Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahSource();
            _retval.OriginalValue = input;

            if (input != null)
            {
                List<Elmah.EntityFrameworkContext.ElmahSource> _ListOfLinq = new List<Elmah.EntityFrameworkContext.ElmahSource>();
                foreach (Elmah.DataSourceEntities.ElmahSource _ItemOfInput in input)
                {
                    Elmah.EntityFrameworkContext.ElmahSource _LinqItem = await GetLinqObjectByIdentifier(_ItemOfInput);
                    _ItemOfInput.CopyTo<Elmah.EntityFrameworkContext.ElmahSource>(_LinqItem);
                    _ListOfLinq.Add(_LinqItem);
                }
                await this.LinqContext.SaveChangesAsync();

                for (int i = 0; i < input.Count; i++)
                {
                    input[i].CopyFrom<Elmah.EntityFrameworkContext.ElmahSource>(_LinqItem);
                }
            }
            //log.Info(string.Format("{0}: BatchUpdate", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Ended.ToString()));
            return _retval;
        }

        /// <summary>
        /// Gets an entity instance by input identifier.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>an instance of <see cref="Elmah.DataSourceEntities.ElmahSourceIdentifier"/> class, with same identifier.</returns>
        public async Task<Elmah.DataSourceEntities.ElmahSource> GetByIdentifier(Elmah.DataSourceEntities.ElmahSourceIdentifier id)
        {
            Elmah.EntityFrameworkContext.ElmahSource _Only = await GetLinqObjectByIdentifier(id);
            if (_Only != null)
            {
                var retval = new Elmah.DataSourceEntities.ElmahSource();
                retval.CopyFrom<Elmah.EntityContracts.IElmahSource>(_Only);
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
        /// <returns>an instance of <see cref="Elmah.EntityFrameworkContext.ElmahSource"/> class, with same identifier, which is a IQueryable.</returns>
        private async Task<Elmah.EntityFrameworkContext.ElmahSource> GetLinqObjectByIdentifier(Elmah.EntityContracts.IElmahSourceIdentifier id)
        {
            return await this.LinqContext.ElmahSource.SingleOrDefaultAsync<Elmah.EntityFrameworkContext.ElmahSource>(
                t=>t.Source == id.Source);
        }

        #endregion

        #region Query Methods Of EntityByCommon

        /// <param name="queryOrderBySettingCollection">query OrderBy setting</param>
        /// <returns>IQueryable&lt;...&gt; of Common</returns>
        internal IQueryable<Elmah.DataSourceEntities.ElmahSource> _GetQueryOfEntityByCommon(
            Framework.Queries.QuerySystemStringContainsCriteria source
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection
            )
        {

            var _ResultFromDataSource =
                (
                    from t in this.LinqContext.ElmahSource
                    where
                        (
                            (
                            !source.IsToCompare
                            ||
                            source.IsToCompare && t.Source.Contains(source.ValueToBeContained)
                            )
                        )
                    select new Elmah.DataSourceEntities.ElmahSource { Source = t.Source } into x
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
            Framework.Queries.QuerySystemStringContainsCriteria source
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            )
        {
            const string logMessageKey = "GetCountOfEntityByCommon";
            IQueryable<Elmah.DataSourceEntities.ElmahSource> _Query = _GetQueryOfEntityByCommon(
                source
                ,queryOrderBySettingCollection
            );

            return await Elmah.EntityFrameworkDAL.IQueryableHelper.GetCountMessageAsync(_Query, currentIndex, pageSize, logMessageKey);
        }

        public async Task<Framework.Models.DataAccessLayerMessageOfBoolean> ExistsOfEntityByCommon(
            Framework.Queries.QuerySystemStringContainsCriteria source
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            )
        {
            const string logMessageKey = "ExistsOfEntityByCommon";
            IQueryable<Elmah.DataSourceEntities.ElmahSource> _Query = _GetQueryOfEntityByCommon(
                source
                ,queryOrderBySettingCollection
            );

            return await Elmah.EntityFrameworkDAL.IQueryableHelper.GetExistsMessageAsync(_Query, currentIndex, pageSize, logMessageKey);
        }

        public async Task<Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahSource> GetCollectionOfEntityByCommon(
            Framework.Queries.QuerySystemStringContainsCriteria source
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            )
        {
            const string logMessageKey = "GetCollectionOfEntityByCommon";
            IQueryable<Elmah.DataSourceEntities.ElmahSource> _Query = _GetQueryOfEntityByCommon(
                source
                ,queryOrderBySettingCollection
            );
            return await Elmah.EntityFrameworkDAL.IQueryableHelper.GetCollectionMessageAsync<
                Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahSource,
                List<Elmah.DataSourceEntities.ElmahSource>,
                Elmah.DataSourceEntities.ElmahSource>(_Query, currentIndex, pageSize, logMessageKey);

        }

        #endregion Query Methods Of EntityByCommon

        #region Query Methods Of NameValuePairByCommon

        /// <param name="queryOrderBySettingCollection">query OrderBy setting</param>
        /// <returns>IQueryable&lt;...&gt; of Common</returns>
        internal IQueryable<Framework.Models.NameValuePair> _GetQueryOfNameValuePairByCommon(
            Framework.Queries.QuerySystemStringContainsCriteria source
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection
            )
        {

            var _ResultFromDataSource =
                (
                    from t in this.LinqContext.ElmahSource
                    where
                        (
                            (
                            !source.IsToCompare
                            ||
                            source.IsToCompare && t.Source.Contains(source.ValueToBeContained)
                            )
                        )
                    select new Framework.Models.NameValuePair { Value = t.Source, Name = t.Source } into vD0
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
            Framework.Queries.QuerySystemStringContainsCriteria source
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            )
        {
            const string logMessageKey = "GetCountOfNameValuePairByCommon";
            IQueryable<Framework.Models.NameValuePair> _Query = _GetQueryOfNameValuePairByCommon(
                source
                ,queryOrderBySettingCollection
            );

            return await Elmah.EntityFrameworkDAL.IQueryableHelper.GetCountMessageAsync(_Query, currentIndex, pageSize, logMessageKey);
        }

        public async Task<Framework.Models.DataAccessLayerMessageOfBoolean> ExistsOfNameValuePairByCommon(
            Framework.Queries.QuerySystemStringContainsCriteria source
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            )
        {
            const string logMessageKey = "ExistsOfNameValuePairByCommon";
            IQueryable<Framework.Models.NameValuePair> _Query = _GetQueryOfNameValuePairByCommon(
                source
                ,queryOrderBySettingCollection
            );

            return await Elmah.EntityFrameworkDAL.IQueryableHelper.GetExistsMessageAsync(_Query, currentIndex, pageSize, logMessageKey);
        }

        public async Task<Framework.Models.DataAccessLayerMessageOfNameValuePairEntityCollection> GetCollectionOfNameValuePairByCommon(
            Framework.Queries.QuerySystemStringContainsCriteria source
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            )
        {
            const string logMessageKey = "GetCollectionOfNameValuePairByCommon";
            IQueryable<Framework.Models.NameValuePair> _Query = _GetQueryOfNameValuePairByCommon(
                source
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
        internal IQueryable<Elmah.DataSourceEntities.ElmahSource> _GetQueryOfEntityByIdentifier(
            Framework.Queries.QuerySystemStringEqualsCriteria source
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection
            )
        {

            var _ResultFromDataSource =
                (
                    from t in this.LinqContext.ElmahSource
                    where
                        (
                            (
                            (source.IsToCompare == false || source.IsToCompare && t.Source == source.ValueToCompare)
                            )
                        )
                    select new Elmah.DataSourceEntities.ElmahSource { Source = t.Source } into x
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
            Framework.Queries.QuerySystemStringEqualsCriteria source
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            )
        {
            const string logMessageKey = "GetCountOfEntityByIdentifier";
            IQueryable<Elmah.DataSourceEntities.ElmahSource> _Query = _GetQueryOfEntityByIdentifier(
                source
                ,queryOrderBySettingCollection
            );

            return await Elmah.EntityFrameworkDAL.IQueryableHelper.GetCountMessageAsync(_Query, currentIndex, pageSize, logMessageKey);
        }

        public async Task<Framework.Models.DataAccessLayerMessageOfBoolean> ExistsOfEntityByIdentifier(
            Framework.Queries.QuerySystemStringEqualsCriteria source
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            )
        {
            const string logMessageKey = "ExistsOfEntityByIdentifier";
            IQueryable<Elmah.DataSourceEntities.ElmahSource> _Query = _GetQueryOfEntityByIdentifier(
                source
                ,queryOrderBySettingCollection
            );

            return await Elmah.EntityFrameworkDAL.IQueryableHelper.GetExistsMessageAsync(_Query, currentIndex, pageSize, logMessageKey);
        }

        public async Task<Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahSource> GetCollectionOfEntityByIdentifier(
            Framework.Queries.QuerySystemStringEqualsCriteria source
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            )
        {
            const string logMessageKey = "GetCollectionOfEntityByIdentifier";
            IQueryable<Elmah.DataSourceEntities.ElmahSource> _Query = _GetQueryOfEntityByIdentifier(
                source
                ,queryOrderBySettingCollection
            );
            return await Elmah.EntityFrameworkDAL.IQueryableHelper.GetCollectionMessageAsync<
                Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahSource,
                List<Elmah.DataSourceEntities.ElmahSource>,
                Elmah.DataSourceEntities.ElmahSource>(_Query, currentIndex, pageSize, logMessageKey);

        }

        #endregion Query Methods Of EntityByIdentifier

    }
}

