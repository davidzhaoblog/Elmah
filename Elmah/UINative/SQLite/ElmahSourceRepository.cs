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
    public class ElmahSourceRepository: Framework.Xaml.SQLite.SQLiteTableRepositoryBase<Elmah.SQLite.TableModels.ElmahSource, Elmah.CommonBLLEntities.ElmahSourceChainedQueryCriteriaCommon, Elmah.DataSourceEntities.ElmahSource, Elmah.EntityContracts.IElmahSourceIdentifier>
    {
        public ElmahSourceRepository(Framework.Xaml.SQLite.SQLiteService sqLiteService) : base(sqLiteService)
        {
            _database.CreateTable<Elmah.SQLite.TableModels.ElmahSource>();
        }

        protected override Expression<Func<Elmah.SQLite.TableModels.ElmahSource, bool>> GetItemExpression(Elmah.EntityContracts.IElmahSourceIdentifier identifier)
        {
            return t => t.Source == identifier.Source;
        }

        public override async Task<Elmah.DataSourceEntities.ElmahSource> Get(Elmah.EntityContracts.IElmahSourceIdentifier identifier)
        {
            var result = await GetItemFromTableAsync(GetItemExpression(identifier));
            return result?.GetAClone<Elmah.DataSourceEntities.ElmahSource>();
        }

        public override async Task Save<T>(T item)
        {
            var cacheItem = item.GetAClone<Elmah.SQLite.TableModels.ElmahSource>();
            await InsertUpdateItemInTableAsync(GetItemExpression(cacheItem), cacheItem);
        }

        protected override Expression<Func<Elmah.SQLite.TableModels.ElmahSource, bool>> GetSQLiteTableQueryPredicate_Common(Elmah.CommonBLLEntities.ElmahSourceChainedQueryCriteriaCommon criteria)
        {
            return
                    t => true;
            /*
            return t =>
                        (
                            (
                            !criteria.Common.Source.IsToCompare
                            ||
                            criteria.Common.Source.IsToCompare && t.Source.Contains(criteria.Common.Source.ValueToBeContained)
                            )
                        );
            */
        }
    }
}

