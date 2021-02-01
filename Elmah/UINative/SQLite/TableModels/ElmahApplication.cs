using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Elmah.SQLite.TableModels
{
    public class ElmahApplication: Elmah.DataSourceEntities.ElmahApplication
    {

        [PrimaryKey]
        public new System.String Application
        {
            get { return base.Application; }
            set { base.Application = value;  }
        }

    }
}

