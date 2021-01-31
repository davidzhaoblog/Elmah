using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elmah.CoreCommonBLL
{
    public class MapDataStreamService
        : FrameworkCore.Services.DataStreamServiceProviderBase<Elmah.DataSourceEntities.MapItemCollection, Elmah.DataSourceEntities.MapItem>
    {
        public override void WriteHeaderLineToClosedXmlWorkSheet(ClosedXML.Excel.IXLWorksheet worksheet, int row)
        {
            char cell = 'A';

            string cellKey;
            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = "MapItemCategory";

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = "Identifiers";

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = "Name";

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = "AddressType";

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = "Address";

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = "SpatialLocation";

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = "Url";
        }

        public override void WriteDataItemToClosedXmlWorkSheet(ClosedXML.Excel.IXLWorksheet worksheet, Elmah.DataSourceEntities.MapItem dataItem, int row)
        {
            char cell = 'A';

            string cellKey;
            cellKey = GetCellKey(row, cell++);
            worksheet.Cell(cellKey).Value = dataItem.MapItemCategory;

            cellKey = GetCellKey(row, cell++);
            worksheet.Cell(cellKey).Value = dataItem.Identifiers;

            cellKey = GetCellKey(row, cell++);
            worksheet.Cell(cellKey).Value = dataItem.Name;

            cellKey = GetCellKey(row, cell++);
            worksheet.Cell(cellKey).Value = dataItem.AddressType;

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = dataItem.Address;

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = Framework.Helpers.GeoHelperSinglton.Instance.GeographyToGmlV3(dataItem.SpatialLocation);

            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = dataItem.Url;
        }
    }
}

