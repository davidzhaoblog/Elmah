using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Elmah.MVVMLightViewModels
{
    public partial class SQLiteFactory
    {
        // Step #SQLite.1: Uncomment following code
        // Step #SQLite.2: Add SQLite\TableModels\ ElmahHost.cs
        // Step #SQLite.3: Add SQLite\ ElmahHostRepository.cs

        private Elmah.SQLite.ElmahHostRepository _ElmahHostRepository;
        public Elmah.SQLite.ElmahHostRepository ElmahHostRepository
        {
            get
            {
                if (_ElmahHostRepository == null)
                    _ElmahHostRepository = new Elmah.SQLite.ElmahHostRepository(this.SQLiteService);
                return _ElmahHostRepository;
            }
        }
    }
}

