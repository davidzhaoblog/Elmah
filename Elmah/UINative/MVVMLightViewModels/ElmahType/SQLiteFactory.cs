using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Elmah.MVVMLightViewModels
{
    public partial class SQLiteFactory
    {
        // Step #SQLite.1: Uncomment following code
        // Step #SQLite.2: Add SQLite\TableModels\ ElmahType.cs
        // Step #SQLite.3: Add SQLite\ ElmahTypeRepository.cs

        private Elmah.SQLite.ElmahTypeRepository _ElmahTypeRepository;
        public Elmah.SQLite.ElmahTypeRepository ElmahTypeRepository
        {
            get
            {
                if (_ElmahTypeRepository == null)
                    _ElmahTypeRepository = new Elmah.SQLite.ElmahTypeRepository(this.SQLiteService);
                return _ElmahTypeRepository;
            }
        }
    }
}

