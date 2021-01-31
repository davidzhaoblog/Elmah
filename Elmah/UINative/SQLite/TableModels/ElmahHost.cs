using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Elmah.SQLite.TableModels
{
    public class ElmahHost: Elmah.DataSourceEntities.ElmahHost
    {

        [PrimaryKey]
        public new System.String Host
        {
            get { return base.Host; }
            set { base.Host = value;  }
        }

    }
}

