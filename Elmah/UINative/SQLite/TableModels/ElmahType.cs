using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Elmah.SQLite.TableModels
{
    public class ElmahType: Elmah.DataSourceEntities.ElmahType
    {

        [PrimaryKey]
        public new System.String Type
        {
            get { return base.Type; }
            set { base.Type = value;  }
        }

    }
}

