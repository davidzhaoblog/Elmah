using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Elmah.MVVMLightViewModels
{
    public partial class SQLiteFactory
    {
        // Step #SQLite.1: Uncomment following code
        // Step #SQLite.2: Add SQLite\TableModels\ ElmahSource.cs
        // Step #SQLite.3: Add SQLite\ ElmahSourceRepository.cs

        private Elmah.SQLite.ElmahSourceRepository _ElmahSourceRepository;
        public Elmah.SQLite.ElmahSourceRepository ElmahSourceRepository
        {
            get
            {
                if (_ElmahSourceRepository == null)
                    _ElmahSourceRepository = new Elmah.SQLite.ElmahSourceRepository(this.SQLiteService);
                return _ElmahSourceRepository;
            }
        }
    }
}

