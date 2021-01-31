using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Xaml.SQLite
{
    public static class TableQueryExtensions
    {
        public static TableQuery<TSQLiteItem> Sort<TSQLiteItem, U>(this TableQuery<TSQLiteItem> tableQuery, Expression<Func<TSQLiteItem, U>> orderByExpression, Framework.Queries.QueryOrderDirections direction)
        {
            if (direction == Queries.QueryOrderDirections.Ascending)
                return tableQuery.OrderBy(orderByExpression);
            return tableQuery.OrderByDescending(orderByExpression);
        }
        public static TableQuery<TSQLiteItem> ThenSort<TSQLiteItem, U>(this TableQuery<TSQLiteItem> tableQuery, Expression<Func<TSQLiteItem, U>> orderByExpression, Framework.Queries.QueryOrderDirections direction)
        {
            if (direction == Queries.QueryOrderDirections.Ascending)
                return tableQuery.ThenBy(orderByExpression);
            return tableQuery.ThenByDescending(orderByExpression);
        }
    }

    public abstract class SQLiteTableRepositoryBase<TSQLiteItem, TCommonCriteria, TItem, TIIdentifier>
        : SQLiteTableRepositoryBase<TSQLiteItem>
            , ISQLiteTableItemCRUD<TSQLiteItem, TItem, TIIdentifier>
            , Framework.Xaml.SQLite.ISQLiteTableQueryWithCommonCriteria<TSQLiteItem, TCommonCriteria, TItem>
        where TSQLiteItem : TItem, new()
        where TItem : TIIdentifier, new()
    {
        public SQLiteTableRepositoryBase(Framework.Xaml.SQLite.SQLiteService sqLiteService)
            : base(sqLiteService)
        {
        }

        protected virtual Expression<Func<TSQLiteItem, bool>> GetItemExpression(TIIdentifier identifier)
        {
            throw new NotImplementedException("Please implement GetItemExpression in SqliteRepository");
        }

        public virtual async Task<TItem> Get(TIIdentifier identifier)
        {
            return await Task.FromResult(new TItem());
        }

        public virtual async Task Save<T>(List<T> list)
            where T : TItem
        {
            foreach (var item in list)
            {
                await Save(item);
            }
        }

        public virtual async Task Save<T>(T item)
            where T : TItem
        {
            await Task.FromException(new NotImplementedException());
        }

        public virtual async Task Delete<T>(T item)
            where T : TItem
        {
            await DeleteItemFromTableAsync(GetItemExpression(item));
        }

        public async Task<int> Count(TCommonCriteria criteria)
        {
            var predicate = GetSQLiteTableQueryPredicate_Common(criteria);
            return await Task.FromResult(_database.Table<TSQLiteItem>().Count(predicate));
        }

        public async Task<List<TItem>> Load(
            TCommonCriteria criteria
            , Framework.Queries.QueryPagingSetting queryPagingSetting
            , Framework.Queries.QueryOrderBySetting queryOrderBySetting
            , Func<TableQuery<TSQLiteItem>, Framework.Queries.QueryOrderDirections, TableQuery<TSQLiteItem>> sortFunction)
        {
            var predicate = GetSQLiteTableQueryPredicate_Common(criteria);

            var tableQuery = _database.Table<TSQLiteItem>().Where(predicate);

            if (sortFunction != null)
                tableQuery = sortFunction(tableQuery, queryOrderBySetting.Direction);

            tableQuery = tableQuery.Skip((queryPagingSetting.CurrentPage - 1) * queryPagingSetting.PageSize).Take(queryPagingSetting.PageSize);

            return
                await Task.FromResult((from t in tableQuery
                                       select (TItem)t).ToList());
        }

        protected abstract Expression<Func<TSQLiteItem, bool>> GetSQLiteTableQueryPredicate_Common(TCommonCriteria criteria);

    }

    public class SQLiteTableRepositoryBase<TSQLiteItem> : Framework.Xaml.SQLite.IDatabaseTableRepository<TSQLiteItem>
        where TSQLiteItem : new()
    {
        private readonly Framework.Xaml.SQLite.SQLiteService _sqLiteService;
        protected SQLiteConnection _database;

        public SQLiteTableRepositoryBase(Framework.Xaml.SQLite.SQLiteService sqLiteService)
        {
            try
            {
                _sqLiteService = sqLiteService;
                string path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                _database = _sqLiteService.GetDatabase(Path.Combine(path, "LocalSQLite.db3"));
            }
            //catch (MobileServiceInvalidOperationException msioe)
            //{
            //    Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
            //    throw;
            //}
            catch (Exception e)
            {
                Debug.WriteLine(@"Sync error: {0}", e.Message);
                throw;
            }
        }

        public Task<List<TSQLiteItem>> GetAllItemsFromTableAsync()
        {
            List<TSQLiteItem> retItem = new List<TSQLiteItem>();
            try
            {
                retItem = _database.Table<TSQLiteItem>().ToList();
            }
            //catch (MobileServiceInvalidOperationException msioe)
            //{
            //    Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
            //    throw;
            //}
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Task.FromResult(retItem);
        }

        public Task<TSQLiteItem> GetItemFromTableAsync(Expression<Func<TSQLiteItem, bool>> predicate)
        {
            TSQLiteItem retItem = new TSQLiteItem();
            try
            {
                retItem = _database.Table<TSQLiteItem>().Where(predicate).FirstOrDefault();
            }
            //catch (MobileServiceInvalidOperationException msioe)
            //{
            //    Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
            //    throw;
            //}
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Task.FromResult(retItem);
        }

        public Task<List<TSQLiteItem>> GetItemsFromTableAsync(Expression<Func<TSQLiteItem, bool>> predicate)
        {
            List<TSQLiteItem> retItem = new List<TSQLiteItem>();

            try
            {
                retItem = _database.Table<TSQLiteItem>().Where(predicate).ToList();
            }
            //catch (MobileServiceInvalidOperationException msioe)
            //{
            //    Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
            //    throw;
            //}
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Task.FromResult(retItem);
        }

        public Task<int> GetTotalByPredicate(Expression<Func<TSQLiteItem, bool>> predicate)
        {
            int retItem = 0;

            try
            {
                retItem = _database.Table<TSQLiteItem>().Where(predicate).ToList().Count;
            }
            //catch (MobileServiceInvalidOperationException msioe)
            //{
            //    Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
            //    throw;
            //}
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Task.FromResult(retItem);
        }

        public Task<List<TSQLiteItem>> GetItemRangeFromTableAsync<U>(Expression<Func<TSQLiteItem, bool>> predicate, int limit, Expression<Func<TSQLiteItem, U>> orderExp, string orderType)
        {
            List<TSQLiteItem> retItem = new List<TSQLiteItem>();

            try
            {
                if (string.IsNullOrEmpty(orderType) || orderType.ToLower() == "asc")
                {
                    retItem = _database.Table<TSQLiteItem>().Where(predicate).OrderBy(orderExp).Take(limit).ToList();
                }
                else
                {
                    retItem = _database.Table<TSQLiteItem>().Where(predicate).OrderByDescending(orderExp).Take(limit).ToList();
                }
            }
            //catch (MobileServiceInvalidOperationException msioe)
            //{
            //    Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
            //    throw;
            //}
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Task.FromResult(retItem);
        }

        public Task<List<TSQLiteItem>> GetItemRangeFromTableAsync<U>(Expression<Func<TSQLiteItem, bool>> predicate, int skip, int take, Expression<Func<TSQLiteItem, U>> orderExp, Framework.Queries.QueryOrderDirections orderDirections = Framework.Queries.QueryOrderDirections.Ascending)
        {
            TableQuery<TSQLiteItem> tableQuery = _database.Table<TSQLiteItem>().Where(predicate);
            List<TSQLiteItem> retItem = new List<TSQLiteItem>();

            try
            {
                if (orderDirections == Framework.Queries.QueryOrderDirections.Ascending)
                {
                    tableQuery = tableQuery.OrderBy(orderExp).Skip(skip).Take(take);
                }
                else
                {
                    tableQuery = tableQuery.OrderByDescending(orderExp).Skip(skip).Take(take);
                }
                retItem = tableQuery.ToList();
            }
            //catch (MobileServiceInvalidOperationException msioe)
            //{
            //    Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
            //    throw;
            //}
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Task.FromResult(retItem);
        }

        public Task<int> InsertItemIntoTableAsync(TSQLiteItem item)
        {
            try
            {
                return Task.FromResult(_database.Insert(item));
            }
            //catch (MobileServiceInvalidOperationException msioe)
            //{
            //    Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
            //    throw;
            //}
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

#pragma warning disable CS0162 // Unreachable code detected
            return Task.FromResult(-1);
#pragma warning restore CS0162 // Unreachable code detected
        }

        public Task<int> DeleteItemFromTableAsync(Expression<Func<TSQLiteItem, bool>> predicate)
        {
            var retItem = -1;
            try
            {
                TSQLiteItem item = GetItemFromTableAsync(predicate).Result;
                if (item != null) retItem = _database.Delete(item);
            }
            //catch (MobileServiceInvalidOperationException msioe)
            //{
            //    Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
            //    throw;
            //}
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return Task.FromResult(retItem);
        }

        public Task<int> DeleteAllItemsFromTableAsync()
        {
            try
            {
                return Task.FromResult(_database.DeleteAll<TSQLiteItem>());
            }
            //catch (MobileServiceInvalidOperationException msioe)
            //{
            //    Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
            //    throw;
            //}
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

#pragma warning disable CS0162 // Unreachable code detected
            return Task.FromResult(-1);
#pragma warning restore CS0162 // Unreachable code detected
        }

        public Task<int> UpdateItemInTableAsync(TSQLiteItem item)
        {
            try
            {
                return Task.FromResult(_database.InsertOrReplace(item));
            }
            //catch (MobileServiceInvalidOperationException msioe)
            //{
            //    Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
            //    throw;
            //}
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

#pragma warning disable CS0162 // Unreachable code detected
            return Task.FromResult(-1);
#pragma warning restore CS0162 // Unreachable code detected
        }

        public Task<int> InsertUpdateItemInTableAsync(Expression<Func<TSQLiteItem, bool>> predicate, TSQLiteItem item)
        {
            var retItem = GetItemFromTableAsync(predicate).Result;

            try
            {
                if (retItem == null)
                {
                    return Task.FromResult(_database.Insert(item));
                }
                else
                {
                    return Task.FromResult(_database.InsertOrReplace(item));
                }
            }
            //catch (MobileServiceInvalidOperationException msioe)
            //{
            //    Debug.WriteLine(@"Invalid sync operation: {0}", msioe.Message);
            //    throw;
            //}
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public Task<List<TSQLiteItem>> Execute(string sql, params object[] paramVals)
        {
            List<TSQLiteItem> ret = new List<TSQLiteItem>();

            try
            {
                SQLiteCommand cmd = _database.CreateCommand(sql, paramVals);
                ret = cmd.ExecuteQuery<TSQLiteItem>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

            return Task.FromResult(ret);
        }

        public Task<int> ExecuteNonQuery(string sql, params object[] paramVals)
        {
            int ret = 0;

            try
            {
                SQLiteCommand cmd = _database.CreateCommand(sql, paramVals);
                ret = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }

            return Task.FromResult(ret);
        }
    }
}

