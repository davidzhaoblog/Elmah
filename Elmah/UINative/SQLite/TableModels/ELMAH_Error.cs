using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Elmah.SQLite.TableModels
{
    public class ELMAH_Error: Elmah.DataSourceEntities.ELMAH_Error.Default
    {

        [PrimaryKey, AutoIncrement]
        public Int32 __DBId__ { get; set; }

    }
}

