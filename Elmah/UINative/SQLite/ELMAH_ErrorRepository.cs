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
    public class ELMAH_ErrorRepository: Framework.Xaml.SQLite.SQLiteTableRepositoryBase<Elmah.SQLite.TableModels.ELMAH_Error, Elmah.CommonBLLEntities.ELMAH_ErrorChainedQueryCriteriaCommon, Elmah.DataSourceEntities.ELMAH_Error.Default, Elmah.EntityContracts.IELMAH_ErrorIdentifier>
    {
        public ELMAH_ErrorRepository(Framework.Xaml.SQLite.SQLiteService sqLiteService) : base(sqLiteService)
        {
            _database.CreateTable<Elmah.SQLite.TableModels.ELMAH_Error>();
        }

        protected override Expression<Func<Elmah.SQLite.TableModels.ELMAH_Error, bool>> GetItemExpression(Elmah.EntityContracts.IELMAH_ErrorIdentifier identifier)
        {
            return t => t.ErrorId == identifier.ErrorId;
        }

        public override async Task<Elmah.DataSourceEntities.ELMAH_Error.Default> Get(Elmah.EntityContracts.IELMAH_ErrorIdentifier identifier)
        {
            var result = await GetItemFromTableAsync(GetItemExpression(identifier));
            return result?.GetAClone<Elmah.DataSourceEntities.ELMAH_Error.Default>();
        }

        public override async Task Save<T>(T item)
        {
            var cacheItem = item.GetAClone<Elmah.SQLite.TableModels.ELMAH_Error>();
            await InsertUpdateItemInTableAsync(GetItemExpression(cacheItem), cacheItem);
        }

        protected override Expression<Func<Elmah.SQLite.TableModels.ELMAH_Error, bool>> GetSQLiteTableQueryPredicate_Common(Elmah.CommonBLLEntities.ELMAH_ErrorChainedQueryCriteriaCommon criteria)
        {
            return
                    ?;
            /*
            return t =>
                        (
                            (
                            (criteria.Common.Application.IsToCompare == false || t.Application == criteria.Common.Application.ValueToCompare)
                            &&
                            (criteria.Common.Host.IsToCompare == false || t.Host == criteria.Common.Host.ValueToCompare)
                            &&
                            (criteria.Common.Source.IsToCompare == false || t.Source == criteria.Common.Source.ValueToCompare)
                            &&
                            (criteria.Common.StatusCode.IsToCompare == false || t.StatusCode == criteria.Common.StatusCode.ValueToCompare)
                            &&
                            (criteria.Common.Type.IsToCompare == false || t.Type == criteria.Common.Type.ValueToCompare)
                            &&
                            (criteria.Common.User.IsToCompare == false || t.User == criteria.Common.User.ValueToCompare)
                            )
                        &&
                            (
                            !criteria.Common.Message.IsToCompare
                            &&
                            !criteria.Common.AllXml.IsToCompare
                            ||
                            criteria.Common.Message.IsToCompare && t.Message.Contains(criteria.Common.Message.ValueToBeContained)
                            ||
                            criteria.Common.AllXml.IsToCompare && t.AllXml.Contains(criteria.Common.AllXml.ValueToBeContained)
                            )
                        &&
                            (
                            !criteria.Common.TimeUtcRange.IsToCompare
                            ||
                            criteria.Common.TimeUtcRange.IsToCompare && (criteria.Common.TimeUtcRange.IsToCompareLowerBound == false || criteria.Common.TimeUtcRange.IsToCompareLowerBound && t.TimeUtc > criteria.Common.TimeUtcRange.LowerBound) && (criteria.Common.TimeUtcRange.IsToCompareUpperBound == false || criteria.Common.TimeUtcRange.IsToCompareUpperBound && t.TimeUtc <= criteria.Common.TimeUtcRange.UpperBound)
                            )
                        );
            */
        }
    }
}

