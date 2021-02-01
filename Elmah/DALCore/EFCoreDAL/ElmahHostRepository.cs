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
    /// Linq data access layer of entity <see cref="Elmah.DataSourceEntities.ElmahHost"/>, with following methods:
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
    public class ElmahHostRepository
        : Elmah.DALContracts.IElmahHostRepository//<List<Elmah.DataSourceEntities.ElmahHost>, Elmah.DataSourceEntities.ElmahHost, Elmah.DataSourceEntities.ElmahHostIdentifier>
    {
        // private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Elmah.EntityFrameworkContext.ElmahModelEntities LinqContext { get; set; }

        public ElmahHostRepository(Elmah.EntityFrameworkContext.ElmahModelEntities linqContext)
        {
            this.LinqContext = linqContext;
        }

        #region IDataAccessLayerContractBase<List<Elmah.DataSourceEntities.ElmahHost>,Elmah.DataSourceEntities.ElmahHost,Elmah.DataSourceEntities.ElmahHostIdentifier> Members

        /// <summary>
        /// single item insert/update with entity input
        /// </summary>
        /// <param name="input">to-be-insert/updated instance of entity class</param>
        public async Task<Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahHost> Upsert(Elmah.DataSourceEntities.ElmahHost input)
        {
            //log.Info(string.Format("{0}: Update", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Started.ToString()));
            var  _retval = new Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahHost();
            _retval.OriginalValue = new List<Elmah.DataSourceEntities.ElmahHost>();
            _retval.OriginalValue.Add(input);

            if (input != null)
            {
                var _LinqItem = await GetLinqObjectByIdentifier(input);
                if (_LinqItem == null)
                {
                    _LinqItem = input.GetAClone<Elmah.EntityFrameworkContext.ElmahHost>();
                    LinqContext.ElmahHost.Add(_LinqItem);
                }
                else
                {
                    input.CopyTo<Elmah.EntityFrameworkContext.ElmahHost>(_LinqItem);
                }

                await this.LinqContext.SaveChangesAsync();
                Elmah.DataSourceEntities.ElmahHost _Result = new Elmah.DataSourceEntities.ElmahHost();
                _Result.CopyFrom<Elmah.EntityFrameworkContext.ElmahHost>(_LinqItem);
                _retval.Result = new List<Elmah.DataSourceEntities.ElmahHost>();
                _retval.Result.Add(_Result);
            }
            //log.Info(string.Format("{0}: Update", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Started.ToString()));
            return _retval;
        }

        /// <summary>
        /// single item deletion with entity input
        /// </summary>
        /// <param name="input">item to be deleted</param>
        public async Task<Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahHost> Delete(Elmah.DataSourceEntities.ElmahHost input)
        {
            //log.Info(string.Format("{0}: Delete", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Started.ToString()));

            Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahHost  _retval = new Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahHost();
            _retval.OriginalValue = new List<Elmah.DataSourceEntities.ElmahHost>();
            _retval.OriginalValue.Add(input);

            if (input != null)
            {
                Elmah.EntityFrameworkContext.ElmahHost _LinqItem = await GetLinqObjectByIdentifier(input);
                if (_LinqItem != null)
                {
                    this.LinqContext.ElmahHost.Remove(_LinqItem);
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
        public async Task<Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahHost> DeleteByIdentifier(Elmah.DataSourceEntities.ElmahHostIdentifier id)
        {
            //log.Info(string.Format("{0}: DeleteByIdentifier", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Started.ToString()));

            Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahHost  _retval = new Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahHost();

            if (id != null)
            {
                Elmah.EntityFrameworkContext.ElmahHost _LinqItem = await GetLinqObjectByIdentifier(id);
                if (_LinqItem != null)
                {
                    Elmah.DataSourceEntities.ElmahHost _Original = new Elmah.DataSourceEntities.ElmahHost();
                    _Original.CopyFrom<Elmah.EntityFrameworkContext.ElmahHost>(_LinqItem);

                    _retval.OriginalValue = new List<Elmah.DataSourceEntities.ElmahHost>();
                    _retval.OriginalValue.Add(_Original);

                    this.LinqContext.ElmahHost.Remove(_LinqItem);
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
        public async Task<Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahHost> Insert(Elmah.DataSourceEntities.ElmahHost input)
        {
            //log.Info(string.Format("{0}: Insert", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Started.ToString()));
            Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahHost  _retval = new Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahHost();
            _retval.OriginalValue = new List<Elmah.DataSourceEntities.ElmahHost>();
            _retval.OriginalValue.Add(input);

            if (input != null)
            {
                var _LinqItem = input.GetAClone<Elmah.EntityFrameworkContext.ElmahHost>();
                this.LinqContext.ElmahHost.Add(_LinqItem);
                await this.LinqContext.SaveChangesAsync();
                Elmah.DataSourceEntities.ElmahHost _Result = new Elmah.DataSourceEntities.ElmahHost();
                _Result.CopyFrom<Elmah.EntityFrameworkContext.ElmahHost>(_LinqItem);
                _retval.Result = new List<Elmah.DataSourceEntities.ElmahHost>();
                _retval.Result.Add(_Result);
            }

            //log.Info(string.Format("{0}: Insert", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Ended.ToString()));
            return _retval;
        }

        /// <summary>
        /// single item update with entity input
        /// </summary>
        /// <param name="input">to-be-updated instance of entity class</param>
        public async Task<Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahHost> Update(Elmah.DataSourceEntities.ElmahHost input)
        {
            //log.Info(string.Format("{0}: Update", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Started.ToString()));
            Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahHost  _retval = new Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahHost();
            _retval.OriginalValue = new List<Elmah.DataSourceEntities.ElmahHost>();
            _retval.OriginalValue.Add(input);

            if (input != null)
            {
                Elmah.EntityFrameworkContext.ElmahHost _LinqItem = await GetLinqObjectByIdentifier(input);
                if (_LinqItem != null)
                {
                    input.CopyTo<Elmah.DataSourceEntities.ElmahHost>(_LinqItem);
                    await this.LinqContext.SaveChangesAsync();
                    Elmah.DataSourceEntities.ElmahHost _Result = new Elmah.DataSourceEntities.ElmahHost();
                    _Result.CopyFrom<Elmah.EntityFrameworkContext.ElmahHost>(_LinqItem);
                    _retval.Result = new List<Elmah.DataSourceEntities.ElmahHost>();
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
        public async Task<Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahHost> BatchInsert(List<Elmah.DataSourceEntities.ElmahHost> input)
        {

            //log.Info(string.Format("{0}: BatchInsert", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Started.ToString()));

            Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahHost  _retval = new Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahHost();
            _retval.OriginalValue = input;

            if (input != null && input.Count > 0)
            {
                try
                {
                    DataTable _DataTable = new DataTable();
                _DataTable.Columns.Add("Host", typeof(System.String));

                    foreach (Elmah.DataSourceEntities.ElmahHost _Item in input)
                    {
                        _DataTable.Rows.Add(new object[] { _Item.Host });
                    }

                    using (SqlBulkCopy s = new SqlBulkCopy(this.LinqContext.ConnectionStringOptions.Value.ConnectionString))
                    {
                        s.DestinationTableName = "dbo.ElmahHost";

                    s.ColumnMappings.Add("Host", "Host");

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
        public async Task<Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahHost> BatchDelete(List<Elmah.DataSourceEntities.ElmahHost> input)
        {
            //log.Info(string.Format("{0}: BatchDelete", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Started.ToString()));

            Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahHost  _retval = new Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahHost();
            _retval.OriginalValue = input;

            if (input != null)
            {
                List<Elmah.EntityFrameworkContext.ElmahHost> _ListOfLinq = new List<Elmah.EntityFrameworkContext.ElmahHost>();
                foreach (Elmah.DataSourceEntities.ElmahHost _ItemOfInput in input)
                {
                    Elmah.EntityFrameworkContext.ElmahHost _LinqItem = await GetLinqObjectByIdentifier(_ItemOfInput);
                    this.LinqContext.ElmahHost.Remove(_LinqItem);
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
        public async Task<Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahHost> BatchUpdate(List<Elmah.DataSourceEntities.ElmahHost> input)
        {
            //log.Info(string.Format("{0}: BatchUpdate", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Started.ToString()));

            Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahHost  _retval = new Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahHost();
            _retval.OriginalValue = input;

            if (input != null)
            {
                List<Elmah.EntityFrameworkContext.ElmahHost> _ListOfLinq = new List<Elmah.EntityFrameworkContext.ElmahHost>();
                foreach (Elmah.DataSourceEntities.ElmahHost _ItemOfInput in input)
                {
                    Elmah.EntityFrameworkContext.ElmahHost _LinqItem = await GetLinqObjectByIdentifier(_ItemOfInput);
                    _ItemOfInput.CopyTo<Elmah.EntityFrameworkContext.ElmahHost>(_LinqItem);
                    _ListOfLinq.Add(_LinqItem);
                }
                await this.LinqContext.SaveChangesAsync();

                for (int i = 0; i < input.Count; i++)
                {
                    input[i].CopyFrom<Elmah.EntityFrameworkContext.ElmahHost>(_LinqItem);
                }
            }
            //log.Info(string.Format("{0}: BatchUpdate", Framework.Models.LoggingOptions.Data_Access_Layer_Process_Ended.ToString()));
            return _retval;
        }

        /// <summary>
        /// Gets an entity instance by input identifier.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>an instance of <see cref="Elmah.DataSourceEntities.ElmahHostIdentifier"/> class, with same identifier.</returns>
        public async Task<Elmah.DataSourceEntities.ElmahHost> GetByIdentifier(Elmah.DataSourceEntities.ElmahHostIdentifier id)
        {
            Elmah.EntityFrameworkContext.ElmahHost _Only = await GetLinqObjectByIdentifier(id);
            if (_Only != null)
            {
                var retval = new Elmah.DataSourceEntities.ElmahHost();
                retval.CopyFrom<Elmah.EntityContracts.IElmahHost>(_Only);
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
        /// <returns>an instance of <see cref="Elmah.EntityFrameworkContext.ElmahHost"/> class, with same identifier, which is a IQueryable.</returns>
        private async Task<Elmah.EntityFrameworkContext.ElmahHost> GetLinqObjectByIdentifier(Elmah.EntityContracts.IElmahHostIdentifier id)
        {
            return await this.LinqContext.ElmahHost.SingleOrDefaultAsync<Elmah.EntityFrameworkContext.ElmahHost>(
                t=>t.Host == id.Host);
        }

        #endregion

        #region Query Methods Of EntityByCommon

        /// <param name="queryOrderBySettingCollection">query OrderBy setting</param>
        /// <returns>IQueryable&lt;...&gt; of Common</returns>
        internal IQueryable<Elmah.DataSourceEntities.ElmahHost> _GetQueryOfEntityByCommon(
            Framework.Queries.QuerySystemStringContainsCriteria host
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection
            )
        {

            var _ResultFromDataSource =
                (
                    from t in this.LinqContext.ElmahHost
                    where
                        (
                            (
                            !host.IsToCompare
                            ||
                            host.IsToCompare && t.Host.Contains(host.ValueToBeContained)
                            )
                        )
                    select new Elmah.DataSourceEntities.ElmahHost { Host = t.Host } into x
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
            Framework.Queries.QuerySystemStringContainsCriteria host
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            )
        {
            const string logMessageKey = "GetCountOfEntityByCommon";
            IQueryable<Elmah.DataSourceEntities.ElmahHost> _Query = _GetQueryOfEntityByCommon(
                host
                ,queryOrderBySettingCollection
            );

            return await Elmah.EntityFrameworkDAL.IQueryableHelper.GetCountMessageAsync(_Query, currentIndex, pageSize, logMessageKey);
        }

        public async Task<Framework.Models.DataAccessLayerMessageOfBoolean> ExistsOfEntityByCommon(
            Framework.Queries.QuerySystemStringContainsCriteria host
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            )
        {
            const string logMessageKey = "ExistsOfEntityByCommon";
            IQueryable<Elmah.DataSourceEntities.ElmahHost> _Query = _GetQueryOfEntityByCommon(
                host
                ,queryOrderBySettingCollection
            );

            return await Elmah.EntityFrameworkDAL.IQueryableHelper.GetExistsMessageAsync(_Query, currentIndex, pageSize, logMessageKey);
        }

        public async Task<Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahHost> GetCollectionOfEntityByCommon(
            Framework.Queries.QuerySystemStringContainsCriteria host
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            )
        {
            const string logMessageKey = "GetCollectionOfEntityByCommon";
            IQueryable<Elmah.DataSourceEntities.ElmahHost> _Query = _GetQueryOfEntityByCommon(
                host
                ,queryOrderBySettingCollection
            );
            return await Elmah.EntityFrameworkDAL.IQueryableHelper.GetCollectionMessageAsync<
                Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahHost,
                List<Elmah.DataSourceEntities.ElmahHost>,
                Elmah.DataSourceEntities.ElmahHost>(_Query, currentIndex, pageSize, logMessageKey);

        }

        #endregion Query Methods Of EntityByCommon

        #region Query Methods Of NameValuePairByCommon

        /// <param name="queryOrderBySettingCollection">query OrderBy setting</param>
        /// <returns>IQueryable&lt;...&gt; of Common</returns>
        internal IQueryable<Framework.Models.NameValuePair> _GetQueryOfNameValuePairByCommon(
            Framework.Queries.QuerySystemStringContainsCriteria host
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection
            )
        {

            var _ResultFromDataSource =
                (
                    from t in this.LinqContext.ElmahHost
                    where
                        (
                            (
                            !host.IsToCompare
                            ||
                            host.IsToCompare && t.Host.Contains(host.ValueToBeContained)
                            )
                        )
                    select new Framework.Models.NameValuePair { Value = t.Host, Name = t.Host } into vD0
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
            Framework.Queries.QuerySystemStringContainsCriteria host
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            )
        {
            const string logMessageKey = "GetCountOfNameValuePairByCommon";
            IQueryable<Framework.Models.NameValuePair> _Query = _GetQueryOfNameValuePairByCommon(
                host
                ,queryOrderBySettingCollection
            );

            return await Elmah.EntityFrameworkDAL.IQueryableHelper.GetCountMessageAsync(_Query, currentIndex, pageSize, logMessageKey);
        }

        public async Task<Framework.Models.DataAccessLayerMessageOfBoolean> ExistsOfNameValuePairByCommon(
            Framework.Queries.QuerySystemStringContainsCriteria host
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            )
        {
            const string logMessageKey = "ExistsOfNameValuePairByCommon";
            IQueryable<Framework.Models.NameValuePair> _Query = _GetQueryOfNameValuePairByCommon(
                host
                ,queryOrderBySettingCollection
            );

            return await Elmah.EntityFrameworkDAL.IQueryableHelper.GetExistsMessageAsync(_Query, currentIndex, pageSize, logMessageKey);
        }

        public async Task<Framework.Models.DataAccessLayerMessageOfNameValuePairEntityCollection> GetCollectionOfNameValuePairByCommon(
            Framework.Queries.QuerySystemStringContainsCriteria host
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            )
        {
            const string logMessageKey = "GetCollectionOfNameValuePairByCommon";
            IQueryable<Framework.Models.NameValuePair> _Query = _GetQueryOfNameValuePairByCommon(
                host
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
        internal IQueryable<Elmah.DataSourceEntities.ElmahHost> _GetQueryOfEntityByIdentifier(
            Framework.Queries.QuerySystemStringEqualsCriteria host
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection
            )
        {

            var _ResultFromDataSource =
                (
                    from t in this.LinqContext.ElmahHost
                    where
                        (
                            (
                            (host.IsToCompare == false || host.IsToCompare && t.Host == host.ValueToCompare)
                            )
                        )
                    select new Elmah.DataSourceEntities.ElmahHost { Host = t.Host } into x
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
            Framework.Queries.QuerySystemStringEqualsCriteria host
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            )
        {
            const string logMessageKey = "GetCountOfEntityByIdentifier";
            IQueryable<Elmah.DataSourceEntities.ElmahHost> _Query = _GetQueryOfEntityByIdentifier(
                host
                ,queryOrderBySettingCollection
            );

            return await Elmah.EntityFrameworkDAL.IQueryableHelper.GetCountMessageAsync(_Query, currentIndex, pageSize, logMessageKey);
        }

        public async Task<Framework.Models.DataAccessLayerMessageOfBoolean> ExistsOfEntityByIdentifier(
            Framework.Queries.QuerySystemStringEqualsCriteria host
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            )
        {
            const string logMessageKey = "ExistsOfEntityByIdentifier";
            IQueryable<Elmah.DataSourceEntities.ElmahHost> _Query = _GetQueryOfEntityByIdentifier(
                host
                ,queryOrderBySettingCollection
            );

            return await Elmah.EntityFrameworkDAL.IQueryableHelper.GetExistsMessageAsync(_Query, currentIndex, pageSize, logMessageKey);
        }

        public async Task<Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahHost> GetCollectionOfEntityByIdentifier(
            Framework.Queries.QuerySystemStringEqualsCriteria host
            ,int currentIndex = -1
            ,int pageSize = -1
            ,Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection = null
            )
        {
            const string logMessageKey = "GetCollectionOfEntityByIdentifier";
            IQueryable<Elmah.DataSourceEntities.ElmahHost> _Query = _GetQueryOfEntityByIdentifier(
                host
                ,queryOrderBySettingCollection
            );
            return await Elmah.EntityFrameworkDAL.IQueryableHelper.GetCollectionMessageAsync<
                Elmah.DataSourceEntities.DataAccessLayerMessageOfEntityCollectionElmahHost,
                List<Elmah.DataSourceEntities.ElmahHost>,
                Elmah.DataSourceEntities.ElmahHost>(_Query, currentIndex, pageSize, logMessageKey);

        }

        #endregion Query Methods Of EntityByIdentifier

    }
}

