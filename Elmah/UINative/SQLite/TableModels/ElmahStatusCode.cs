using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Elmah.SQLite.TableModels
{
    public class ElmahStatusCode: Elmah.DataSourceEntities.ElmahStatusCode
    {

        [PrimaryKey]
        public new System.Int32 StatusCode
        {
            get { return base.StatusCode; }
            set { base.StatusCode = value;  }
        }

    }
}

