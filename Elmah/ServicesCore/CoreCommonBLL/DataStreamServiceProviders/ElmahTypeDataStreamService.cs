using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elmah.CoreCommonBLL
{
    public class ElmahTypeDataStreamService
        : FrameworkCore.Services.DataStreamServiceProviderBase<List<Elmah.DataSourceEntities.ElmahType>, Elmah.DataSourceEntities.ElmahType>
    {
        public override void WriteHeaderLineToClosedXmlWorkSheet(ClosedXML.Excel.IXLWorksheet worksheet, int row)
        {
            char cell = 'A';

            string cellKey;
            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = "Type";

        }

        public override void WriteDataItemToClosedXmlWorkSheet(ClosedXML.Excel.IXLWorksheet worksheet, Elmah.DataSourceEntities.ElmahType dataItem, int row)
        {
            char cell = 'A';

            string cellKey;
            cellKey = GetCellKey(row, cell ++);
            worksheet.Cell(cellKey).Value = dataItem.Type;

        }

    }
}

