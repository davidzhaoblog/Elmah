using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elmah.CoreCommonBLL
{
    public class ElmahSourceDataStreamService
        : FrameworkCore.Services.DataStreamServiceProviderBase<List<Elmah.DataSourceEntities.ElmahSource>, Elmah.DataSourceEntities.ElmahSource>
    {
        public override void WriteHeaderLineToClosedXmlWorkSheet(ClosedXML.Excel.IXLWorksheet worksheet, int row)
        {
            char cell = 'A';

            string cellKey;
            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = "Source";

        }

        public override void WriteDataItemToClosedXmlWorkSheet(ClosedXML.Excel.IXLWorksheet worksheet, Elmah.DataSourceEntities.ElmahSource dataItem, int row)
        {
            char cell = 'A';

            string cellKey;
            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = dataItem.Source;

        }

    }
}

