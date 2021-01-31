using Framework.Queries;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Framework.Xaml.SQLite
{
    public interface ISQLiteTableItemCRUD<TSQLiteItem, TItem, TIIdentifier>
        where TSQLiteItem : TItem, new()
        where TItem : TIIdentifier, new()
    {
        Task Delete<T>(T item) where T : TItem;
        Task<TItem> Get(TIIdentifier identifier);
        Task Save<T>(List<T> list) where T : TItem;
        Task Save<T>(T item) where T : TItem;
    }
}

