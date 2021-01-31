using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Elmah.MVVMLightViewModels
{
    public partial class SQLiteFactory
    {
        // Step #SQLite.1: Uncomment following code
        // Step #SQLite.2: Add SQLite\TableModels\ ElmahStatusCode.cs
        // Step #SQLite.3: Add SQLite\ ElmahStatusCodeRepository.cs

        private Elmah.SQLite.ElmahStatusCodeRepository _ElmahStatusCodeRepository;
        public Elmah.SQLite.ElmahStatusCodeRepository ElmahStatusCodeRepository
        {
            get
            {
                if (_ElmahStatusCodeRepository == null)
                    _ElmahStatusCodeRepository = new Elmah.SQLite.ElmahStatusCodeRepository(this.SQLiteService);
                return _ElmahStatusCodeRepository;
            }
        }
    }
}

