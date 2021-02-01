using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Elmah.MVVMLightViewModels
{
    public partial class SQLiteFactory
    {
        // Step #SQLite.1: Uncomment following code
        // Step #SQLite.2: Add SQLite\TableModels\ ElmahApplication.cs
        // Step #SQLite.3: Add SQLite\ ElmahApplicationRepository.cs

        private Elmah.SQLite.ElmahApplicationRepository _ElmahApplicationRepository;
        public Elmah.SQLite.ElmahApplicationRepository ElmahApplicationRepository
        {
            get
            {
                if (_ElmahApplicationRepository == null)
                    _ElmahApplicationRepository = new Elmah.SQLite.ElmahApplicationRepository(this.SQLiteService);
                return _ElmahApplicationRepository;
            }
        }
    }
}

