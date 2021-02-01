using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Elmah.SQLite.TableModels
{
    public class ElmahSource: Elmah.DataSourceEntities.ElmahSource
    {

        [PrimaryKey]
        public new System.String Source
        {
            get { return base.Source; }
            set { base.Source = value;  }
        }

    }
}

