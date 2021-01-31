using System;
using System.Collections.Generic;
using System.Text;

namespace Elmah.SQLite
{
    public class CodeListCachingRepository : Framework.Xaml.SQLite.SQLiteTableRepositoryBase<Elmah.SQLite.TableModels.CodeListCaching>
    {
        public CodeListCachingRepository(Framework.Xaml.SQLite.SQLiteService sqLiteService) : base(sqLiteService)
        {
            _database.CreateTable<Elmah.SQLite.TableModels.CodeListCaching>();
        }
    }
}

