using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

//using MathNet.Numerics.LinqExtensions;
//using System.Data.SqlClient;
using System.Data;

namespace Elmah.EntityFrameworkDAL
{
    public class MapRepository : Elmah.DALContracts.IMapRepository
    {
        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Elmah.EntityFrameworkContext.ElmahModelEntities LinqContext { get; set; }

        /*
        public MapRepository()
        {
        }
        */

        public MapRepository(Elmah.EntityFrameworkContext.ElmahModelEntities linqContext)
        {
            this.LinqContext = linqContext;
        }

        public async Task<Elmah.DataSourceEntities.DataAccessLayerMessageOfMapItemCollection> GetMapItems(
            Elmah.EntityContracts.MapItemCategory[] mapItemCategories
            , Framework.Queries.QuerySystemStringContainsCriteria anyText
            , Framework.Queries.QueryGeographyRangeCriteria geographyRange
            , Framework.Queries.QueryGeographyIntersectsCriteria geographyIntersects
            , int currentIndex
            , int pageSize
            , Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection
            )
        {
            var retval = new Elmah.DataSourceEntities.DataAccessLayerMessageOfMapItemCollection();
            retval.DataAccessLayerMessageStatus = Framework.Models.DataAccessLayerMessageStatus.Success;
            retval.QueryPagingResult = new Framework.Queries.QueryPagingResult();
            retval.QueryPagingResult.PageSize = pageSize;

            // TODO: Geography throwing: System.ArgumentException: 'When writing a SQL Server geography value, the shell of a polygon must be oriented counter-clockwise. To write polygons without a shell, set SkipGeographyChecks.'
            retval.QueryPagingResult.CurrentIndexOfStartRecord = currentIndex;
            retval.Result = new Elmah.DataSourceEntities.MapItemCollection();

            return retval;
        }
    }
}

