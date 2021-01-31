using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Elmah.SQLite.TableModels
{
    public class NTierOntimeVersion
    {
        [PrimaryKey]
        public int Key { get; set; } = 1;
        public string Version { get; set; } = "1.0.0.0";
    }
}

