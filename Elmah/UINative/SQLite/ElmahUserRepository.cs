using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SQLite;

using Framework.Xaml.SQLite;

namespace Elmah.SQLite
{
    public class ElmahUserRepository: Framework.Xaml.SQLite.SQLiteTableRepositoryBase<Elmah.SQLite.TableModels.ElmahUser, Elmah.CommonBLLEntities.ElmahUserChainedQueryCriteriaCommon, Elmah.DataSourceEntities.ElmahUser, Elmah.EntityContracts.IElmahUserIdentifier>
    {
        public ElmahUserRepository(Framework.Xaml.SQLite.SQLiteService sqLiteService) : base(sqLiteService)
        {
            _database.CreateTable<Elmah.SQLite.TableModels.ElmahUser>();
        }

        protected override Expression<Func<Elmah.SQLite.TableModels.ElmahUser, bool>> GetItemExpression(Elmah.EntityContracts.IElmahUserIdentifier identifier)
        {
            return t => t.User == identifier.User;
        }

        public override async Task<Elmah.DataSourceEntities.ElmahUser> Get(Elmah.EntityContracts.IElmahUserIdentifier identifier)
        {
            var result = await GetItemFromTableAsync(GetItemExpression(identifier));
            return result?.GetAClone<Elmah.DataSourceEntities.ElmahUser>();
        }

        public override async Task Save<T>(T item)
        {
            var cacheItem = item.GetAClone<Elmah.SQLite.TableModels.ElmahUser>();
            await InsertUpdateItemInTableAsync(GetItemExpression(cacheItem), cacheItem);
        }

        protected override Expression<Func<Elmah.SQLite.TableModels.ElmahUser, bool>> GetSQLiteTableQueryPredicate_Common(Elmah.CommonBLLEntities.ElmahUserChainedQueryCriteriaCommon criteria)
        {
            return
                    t => true;
            /*
            return t =>
                        (
                            (
                            !criteria.Common.User.IsToCompare
                            ||
                            criteria.Common.User.IsToCompare && t.User.Contains(criteria.Common.User.ValueToBeContained)
                            )
                        );
            */
        }
    }
}

