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
    public class ElmahStatusCodeRepository: Framework.Xaml.SQLite.SQLiteTableRepositoryBase<Elmah.SQLite.TableModels.ElmahStatusCode, Elmah.CommonBLLEntities.ElmahStatusCodeChainedQueryCriteriaCommon, Elmah.DataSourceEntities.ElmahStatusCode, Elmah.EntityContracts.IElmahStatusCodeIdentifier>
    {
        public ElmahStatusCodeRepository(Framework.Xaml.SQLite.SQLiteService sqLiteService) : base(sqLiteService)
        {
            _database.CreateTable<Elmah.SQLite.TableModels.ElmahStatusCode>();
        }

        protected override Expression<Func<Elmah.SQLite.TableModels.ElmahStatusCode, bool>> GetItemExpression(Elmah.EntityContracts.IElmahStatusCodeIdentifier identifier)
        {
            return t => t.StatusCode == identifier.StatusCode;
        }

        public override async Task<Elmah.DataSourceEntities.ElmahStatusCode> Get(Elmah.EntityContracts.IElmahStatusCodeIdentifier identifier)
        {
            var result = await GetItemFromTableAsync(GetItemExpression(identifier));
            return result?.GetAClone<Elmah.DataSourceEntities.ElmahStatusCode>();
        }

        public override async Task Save<T>(T item)
        {
            var cacheItem = item.GetAClone<Elmah.SQLite.TableModels.ElmahStatusCode>();
            await InsertUpdateItemInTableAsync(GetItemExpression(cacheItem), cacheItem);
        }

        protected override Expression<Func<Elmah.SQLite.TableModels.ElmahStatusCode, bool>> GetSQLiteTableQueryPredicate_Common(Elmah.CommonBLLEntities.ElmahStatusCodeChainedQueryCriteriaCommon criteria)
        {
            return
                    t => true;
            /*
            return t =>
                        (
                            (
                            !criteria.Common.Name.IsToCompare
                            ||
                            criteria.Common.Name.IsToCompare && t.Name.Contains(criteria.Common.Name.ValueToBeContained)
                            )
                        );
            */
        }
    }
}

