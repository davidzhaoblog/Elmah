using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Elmah.MVVMLightViewModels
{
    public partial class SQLiteFactory
    {
        // Step #SQLite.1: Uncomment following code
        // Step #SQLite.2: Add SQLite\TableModels\ ElmahUser.cs
        // Step #SQLite.3: Add SQLite\ ElmahUserRepository.cs

        private Elmah.SQLite.ElmahUserRepository _ElmahUserRepository;
        public Elmah.SQLite.ElmahUserRepository ElmahUserRepository
        {
            get
            {
                if (_ElmahUserRepository == null)
                    _ElmahUserRepository = new Elmah.SQLite.ElmahUserRepository(this.SQLiteService);
                return _ElmahUserRepository;
            }
        }
    }
}

