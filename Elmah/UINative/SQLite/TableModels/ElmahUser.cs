using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Elmah.SQLite.TableModels
{
    public class ElmahUser: Elmah.DataSourceEntities.ElmahUser
    {

        [PrimaryKey]
        public new System.String User
        {
            get { return base.User; }
            set { base.User = value;  }
        }

    }
}

