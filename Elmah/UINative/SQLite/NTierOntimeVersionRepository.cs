using System;
using System.Collections.Generic;
using System.Text;

namespace Elmah.SQLite
{
    public class NTierOntimeVersionRepository : Framework.Xaml.SQLite.SQLiteTableRepositoryBase<Elmah.SQLite.TableModels.NTierOntimeVersion>
    {
        public NTierOntimeVersionRepository(Framework.Xaml.SQLite.SQLiteService sqLiteService) : base(sqLiteService)
        {
            _database.CreateTable<Elmah.SQLite.TableModels.NTierOntimeVersion>();
        }
    }
}

