using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Spatial;
using Microsoft.Extensions.Logging;

namespace Elmah.AspNetMvcCoreViewModel
{
    public partial class MapVM : Elmah.ViewModelData.MapVM
    {
        //private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        Elmah.WcfContracts.IMapService ThisService { get; set; }
        private readonly ILogger _logger;

        public MapVM()
            : base()
        {
            this.QueryPagingSetting = new Framework.Queries.QueryPagingSetting(1, 10);
        }

        public MapVM(Elmah.WcfContracts.IMapService thisService, ILogger<MapVM> logger)
            : this()
        {
            this.ThisService = thisService;
            this._logger = logger;
        }

        public async Task LoadData(double lat, double longitude, string anyText = null, long? distance1 = null, long? distance2 = 5000)
        {
            this.Criteria.GeographyIntersects = new Framework.Queries.QueryGeographyIntersectsCriteria();
            this.Criteria.GeographyRange = new Framework.Queries.QueryGeographyRangeCriteria
            {
                ReferenceLocation = Microsoft.Spatial.GeographyPoint.Create(lat, longitude)
                , NullableLowerBound = distance1, NullableUpperBound = distance2
            };
            this.Criteria.AnyText = new Framework.Queries.QuerySystemStringContainsCriteria
            {
                NullableValueToBeContained = anyText
            };
            this.Criteria.CanQueryWhenNoQuery = true;

            var searchResult = await this.ThisService.GetMessageOfMapItems(
                this.Criteria
                , this.QueryPagingSetting
                , this.QueryOrderBySettingCollection);

            this.StatusOfResult = searchResult.BusinessLogicLayerResponseStatus;

            if (this.StatusOfResult == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
            {
                this.Result = searchResult.Message;

                if (searchResult.QueryPagingResult != null)
                {
                    this.QueryPagingSetting.CountOfRecords = searchResult.QueryPagingResult.CountOfRecords;
                    this.QueryPagingSetting.RecordCountOfCurrentPage = searchResult.QueryPagingResult.RecordCountOfCurrentPage;
                }
            }
            else
            {
                this.StatusMessageOfResult = searchResult.GetStatusMessage();
#if DEBUG
                this.StatusMessageOfResult = string.Format("{0} {1}", this.StatusMessageOfResult, searchResult.ServerErrorMessage);
#endif
            }
        }

        public async Task LoadData(double lat1, double longitude1, double lat2, double longitude2, string anyText = null)
        {
            double minLatitude = Math.Min(lat1, lat2);
            double maxLatitude = Math.Max(lat1, lat2);
            double minlongitude = Math.Min(longitude1, longitude2);
            double maxlongitude = Math.Max(longitude1, longitude2); ;

            this.Criteria.GeographyIntersects = new Framework.Queries.QueryGeographyIntersectsCriteria
            {
                IsToCompare = true,
                Polygon = Framework.Helpers.GeographyFactory.Polygon()
                    .Ring(maxLatitude, maxlongitude) // North East
                    .LineTo(maxLatitude, minlongitude) // South East
                    .LineTo(minLatitude, minlongitude) // South West
                    .LineTo(minLatitude, maxlongitude) // North West
            };
            this.Criteria.GeographyRange = new Framework.Queries.QueryGeographyRangeCriteria();
            this.Criteria.AnyText = new Framework.Queries.QuerySystemStringContainsCriteria
            {
                NullableValueToBeContained = anyText
            };
            this.Criteria.CanQueryWhenNoQuery = true;

            var searchResult = await this.ThisService.GetMessageOfMapItems(
                this.Criteria
                , this.QueryPagingSetting
                , this.QueryOrderBySettingCollection);

            this.StatusOfResult = searchResult.BusinessLogicLayerResponseStatus;

            if (this.StatusOfResult == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
            {
                this.Result = searchResult.Message;

                if (searchResult.QueryPagingResult != null)
                {
                    this.QueryPagingSetting.CountOfRecords = searchResult.QueryPagingResult.CountOfRecords;
                    this.QueryPagingSetting.RecordCountOfCurrentPage = searchResult.QueryPagingResult.RecordCountOfCurrentPage;
                }
            }
            else
            {
                this.StatusMessageOfResult = searchResult.GetStatusMessage();
#if DEBUG
                this.StatusMessageOfResult = string.Format("{0} {1}", this.StatusMessageOfResult, searchResult.ServerErrorMessage);
#endif
            }
        }
    }
}

