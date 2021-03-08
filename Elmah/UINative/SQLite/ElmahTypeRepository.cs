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
    public class ElmahTypeRepository: Framework.Xaml.SQLite.SQLiteTableRepositoryBase<Elmah.SQLite.TableModels.ElmahType, Elmah.CommonBLLEntities.ElmahTypeChainedQueryCriteriaCommon, Elmah.DataSourceEntities.ElmahType, Elmah.EntityContracts.IElmahTypeIdentifier>
    {
        public ElmahTypeRepository(Framework.Xaml.SQLite.SQLiteService sqLiteService) : base(sqLiteService)
        {
            _database.CreateTable<Elmah.SQLite.TableModels.ElmahType>();
        }

        protected override Expression<Func<Elmah.SQLite.TableModels.ElmahType, bool>> GetItemExpression(Elmah.EntityContracts.IElmahTypeIdentifier identifier)
        {
            return t => t.Type == identifier.Type;
        }

        public override async Task<Elmah.DataSourceEntities.ElmahType> Get(Elmah.EntityContracts.IElmahTypeIdentifier identifier)
        {
            var result = await GetItemFromTableAsync(GetItemExpression(identifier));
            return result?.GetAClone<Elmah.DataSourceEntities.ElmahType>();
        }

        public override async Task Save<T>(T item)
        {
            var cacheItem = item.GetAClone<Elmah.SQLite.TableModels.ElmahType>();
            await InsertUpdateItemInTableAsync(GetItemExpression(cacheItem), cacheItem);
        }

        protected override Expression<Func<Elmah.SQLite.TableModels.ElmahType, bool>> GetSQLiteTableQueryPredicate_Common(Elmah.CommonBLLEntities.ElmahTypeChainedQueryCriteriaCommon criteria)
        {
            return
                    t => true;
            /*
            return t =>
                        (
                            (
                            !criteria.Common.Type.IsToCompare
                            ||
                            criteria.Common.Type.IsToCompare && t.Type.Contains(criteria.Common.Type.ValueToBeContained)
                            )
                        );
            */
        }
    }
}

