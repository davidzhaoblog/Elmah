using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elmah.CoreCommonBLL
{
    public class ElmahHostDataStreamService
        : FrameworkCore.Services.DataStreamServiceProviderBase<List<Elmah.DataSourceEntities.ElmahHost>, Elmah.DataSourceEntities.ElmahHost>
    {
        public override void WriteHeaderLineToClosedXmlWorkSheet(ClosedXML.Excel.IXLWorksheet worksheet, int row)
        {
            char cell = 'A';

            string cellKey;
            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = "Host";

        }

        public override void WriteDataItemToClosedXmlWorkSheet(ClosedXML.Excel.IXLWorksheet worksheet, Elmah.DataSourceEntities.ElmahHost dataItem, int row)
        {
            char cell = 'A';

            string cellKey;
            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = dataItem.Host;

        }

    }
}

