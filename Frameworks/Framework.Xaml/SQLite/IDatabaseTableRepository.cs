using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Xaml.SQLite
{
    public interface IDatabaseTableRepository<T>
    {
        Task<List<T>> GetAllItemsFromTableAsync();

        Task<T> GetItemFromTableAsync(Expression<Func<T, bool>> predicate);

        Task<List<T>> GetItemsFromTableAsync(Expression<Func<T, bool>> predicate);

        Task<List<T>> GetItemRangeFromTableAsync<U>(Expression<Func<T, bool>> predicate, int limit, Expression<Func<T, U>> orderExp, string orderType);

        Task<int> InsertItemIntoTableAsync(T item);

        Task<int> DeleteItemFromTableAsync(Expression<Func<T, bool>> predicate);

        Task<int> DeleteAllItemsFromTableAsync();

        Task<int> UpdateItemInTableAsync(T item);

        Task<List<T>> Execute(string sql, params object[] paramVals);

        Task<int> ExecuteNonQuery(string sql, params object[] paramVals);

        Task<int> GetTotalByPredicate(Expression<Func<T, bool>> predicate);
    }
}

