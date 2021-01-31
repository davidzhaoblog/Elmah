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
    public class ElmahHostRepository: Framework.Xaml.SQLite.SQLiteTableRepositoryBase<Elmah.SQLite.TableModels.ElmahHost, Elmah.CommonBLLEntities.ElmahHostChainedQueryCriteriaCommon, Elmah.DataSourceEntities.ElmahHost, Elmah.EntityContracts.IElmahHostIdentifier>
    {
        public ElmahHostRepository(Framework.Xaml.SQLite.SQLiteService sqLiteService) : base(sqLiteService)
        {
            _database.CreateTable<Elmah.SQLite.TableModels.ElmahHost>();
        }

        protected override Expression<Func<Elmah.SQLite.TableModels.ElmahHost, bool>> GetItemExpression(Elmah.EntityContracts.IElmahHostIdentifier identifier)
        {
            return t => t.Host == identifier.Host;
        }

        public override async Task<Elmah.DataSourceEntities.ElmahHost> Get(Elmah.EntityContracts.IElmahHostIdentifier identifier)
        {
            var result = await GetItemFromTableAsync(GetItemExpression(identifier));
            return result?.GetAClone<Elmah.DataSourceEntities.ElmahHost>();
        }

        public override async Task Save<T>(T item)
        {
            var cacheItem = item.GetAClone<Elmah.SQLite.TableModels.ElmahHost>();
            await InsertUpdateItemInTableAsync(GetItemExpression(cacheItem), cacheItem);
        }

        protected override Expression<Func<Elmah.SQLite.TableModels.ElmahHost, bool>> GetSQLiteTableQueryPredicate_Common(Elmah.CommonBLLEntities.ElmahHostChainedQueryCriteriaCommon criteria)
        {
            return
                    ?;
            /*
            return t =>
                        (
                            (
                            !criteria.Common.Host.IsToCompare
                            ||
                            criteria.Common.Host.IsToCompare && t.Host.Contains(criteria.Common.Host.ValueToBeContained)
                            )
                        );
            */
        }
    }
}

