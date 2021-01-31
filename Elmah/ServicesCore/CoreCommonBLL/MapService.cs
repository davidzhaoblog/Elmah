using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Elmah.CoreCommonBLL
{
    public partial class MapService : Elmah.WcfContracts.IMapService
    {
        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        Elmah.DALContracts.IMapRepository DALClassInstance { get; set; }
        private readonly ILogger _logger;

        public MapService()
        {
        }

        public MapService(Elmah.DALContracts.IMapRepository dalClassInstance, ILogger<MapService> logger)
        {
            this.DALClassInstance = dalClassInstance;
            this._logger = logger;
        }

        public async Task<Elmah.CommonBLLEntities.BusinessLogicLayerResponseMessageMapItemCollection> GetMapItems(
            Elmah.CommonBLLEntities.GeoSearchRequestMessage request)
        {
            //log.Info(string.Format("{0}: GetCollectionOfDefaultByCommon", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Started.ToString()));

            if(!request.Criteria.CanQueryWhenNoQuery && !request.Criteria.HasQuery)
            {
                var failedResponse = new Elmah.CommonBLLEntities.BusinessLogicLayerResponseMessageMapItemCollection();
                failedResponse.BusinessLogicLayerResponseStatus = Framework.Services.BusinessLogicLayerResponseStatus.NeedAtLeastOneSearchCondition;
                failedResponse.ServerErrorMessage = "Please enter at least one search condition";
                return failedResponse;
            }

            var _resultFromDAL = await this.DALClassInstance.GetMapItems(
                request.Criteria.MapItemCategories
                , request.Criteria.AnyText
                , request.Criteria.GeographyRange
                , request.Criteria.GeographyIntersects
                , request.QueryPagingSetting.CurrentIndex
                , request.QueryPagingSetting.PageSize
                , request.QueryOrderBySettingCollection
                );

            var _retval = new Elmah.CommonBLLEntities.BusinessLogicLayerResponseMessageMapItemCollection();
            _retval.BusinessLogicLayerRequestID = request.BusinessLogicLayerRequestID;

            if (request.DataServiceType == Framework.Models.DataServiceTypes.DataSourceResult)
            {
                Framework.Services.BusinessLogicLayerResponseMessageBaseHelper.MapDataAccessLayerMessageToBusinessLogicLayerResponseMessage<Elmah.DataSourceEntities.MapItemCollection>(_resultFromDAL, _retval);
            }
            else
            {
                Framework.Services.BusinessLogicLayerResponseMessageBaseHelper.MapDataAccessLayerMessageToBusinessLogicLayerResponseMessage<Elmah.DataSourceEntities.MapItem, Elmah.DataSourceEntities.MapItemCollection>(_resultFromDAL, _retval, request.DataServiceType, new MapDataStreamService());
            }

            //log.Info(string.Format("{0}: GetCollectionOfDefaultByCommon", Framework.Models.LoggingOptions.Business_Logic_Layer_Process_Ended.ToString()));
            return _retval;
        }

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="queryPagingSetting"></param>
        /// <param name="queryOrderBySettingCollection"></param>
        /// <returns>business layer built-in message <see cref="Elmah.DataSourceEntities.MapItemCollection"/></returns>
        public async Task<Elmah.CommonBLLEntities.BusinessLogicLayerResponseMessageMapItemCollection> GetMessageOfMapItems(
            Elmah.CommonBLLEntities.GeoSearchCriteria criteria
            , Framework.Queries.QueryPagingSetting queryPagingSetting
            , Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection)
        {
            return await GetMessageOfMapItems(
                criteria
                , queryPagingSetting
                , queryOrderBySettingCollection
                , Framework.Models.DataServiceTypes.DataSourceResult);
        }

        /// <summary>
        /// Gets message of the collection of entity of common.
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="queryPagingSetting"></param>
        /// <param name="queryOrderBySettingCollection"></param>
        /// <returns>business layer built-in message <see cref="Elmah.DataSourceEntities.MapItemCollection"/></returns>
        public async Task<Elmah.CommonBLLEntities.BusinessLogicLayerResponseMessageMapItemCollection> GetMessageOfMapItems(
            Elmah.CommonBLLEntities.GeoSearchCriteria criteria
            , Framework.Queries.QueryPagingSetting queryPagingSetting
            , Framework.Queries.QueryOrderBySettingCollection queryOrderBySettingCollection
            , Framework.Models.DataServiceTypes dataServiceType)
        {
            //log.Info(string.Format("{0}: GetMessageOfMapItems", Framework.Models.LoggingOptions.UI_Process_Started.ToString()));
            var _Request = new Elmah.CommonBLLEntities.GeoSearchRequestMessage();
            _Request.Criteria = criteria;
            _Request.QueryPagingSetting = queryPagingSetting;
            if (queryOrderBySettingCollection == null || queryOrderBySettingCollection.Count == 0)
            {
                _Request.QueryOrderBySettingCollection = new Framework.Queries.QueryOrderBySettingCollection();
                _Request.QueryOrderBySettingCollection.Add("Name", Framework.Queries.QueryOrderDirections.Ascending);
            }
            else
            {
                _Request.QueryOrderBySettingCollection = queryOrderBySettingCollection;
            }
            _Request.DataServiceType = dataServiceType;

            var _Response = await GetMapItems(_Request);
            return _Response;
        }
    }
}

