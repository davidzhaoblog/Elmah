using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Elmah.MVVMLightViewModels
{
    public partial class SQLiteFactory
    {
        // Step #SQLite.1: Uncomment following code
        // Step #SQLite.2: Add SQLite\TableModels\ ELMAH_Error.cs
        // Step #SQLite.3: Add SQLite\ ELMAH_ErrorRepository.cs

        private Elmah.SQLite.ELMAH_ErrorRepository _ELMAH_ErrorRepository;
        public Elmah.SQLite.ELMAH_ErrorRepository ELMAH_ErrorRepository
        {
            get
            {
                if (_ELMAH_ErrorRepository == null)
                    _ELMAH_ErrorRepository = new Elmah.SQLite.ELMAH_ErrorRepository(this.SQLiteService);
                return _ELMAH_ErrorRepository;
            }
        }
    }
}

