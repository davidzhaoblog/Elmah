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
    public class ElmahApplicationRepository: Framework.Xaml.SQLite.SQLiteTableRepositoryBase<Elmah.SQLite.TableModels.ElmahApplication, Elmah.CommonBLLEntities.ElmahApplicationChainedQueryCriteriaCommon, Elmah.DataSourceEntities.ElmahApplication, Elmah.EntityContracts.IElmahApplicationIdentifier>
    {
        public ElmahApplicationRepository(Framework.Xaml.SQLite.SQLiteService sqLiteService) : base(sqLiteService)
        {
            _database.CreateTable<Elmah.SQLite.TableModels.ElmahApplication>();
        }

        protected override Expression<Func<Elmah.SQLite.TableModels.ElmahApplication, bool>> GetItemExpression(Elmah.EntityContracts.IElmahApplicationIdentifier identifier)
        {
            return t => t.Application == identifier.Application;
        }

        public override async Task<Elmah.DataSourceEntities.ElmahApplication> Get(Elmah.EntityContracts.IElmahApplicationIdentifier identifier)
        {
            var result = await GetItemFromTableAsync(GetItemExpression(identifier));
            return result?.GetAClone<Elmah.DataSourceEntities.ElmahApplication>();
        }

        public override async Task Save<T>(T item)
        {
            var cacheItem = item.GetAClone<Elmah.SQLite.TableModels.ElmahApplication>();
            await InsertUpdateItemInTableAsync(GetItemExpression(cacheItem), cacheItem);
        }

        protected override Expression<Func<Elmah.SQLite.TableModels.ElmahApplication, bool>> GetSQLiteTableQueryPredicate_Common(Elmah.CommonBLLEntities.ElmahApplicationChainedQueryCriteriaCommon criteria)
        {
            return
                    ?;
            /*
            return t =>
                        (
                            (
                            !criteria.Common.Application.IsToCompare
                            ||
                            criteria.Common.Application.IsToCompare && t.Application.Contains(criteria.Common.Application.ValueToBeContained)
                            )
                        );
            */
        }
    }
}

