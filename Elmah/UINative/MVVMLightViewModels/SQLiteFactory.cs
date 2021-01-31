using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Elmah.MVVMLightViewModels
{
    public partial class SQLiteFactory
    {
        private Framework.Xaml.SQLite.SQLiteService SQLiteService
        {
            get
            {
                return DependencyService.Resolve<Framework.Xaml.SQLite.SQLiteService>();
            }
        }

        public Elmah.MVVMLightViewModels.ListsHelper ListsHelper
        {
            get
            {
                return DependencyService.Resolve<Elmah.MVVMLightViewModels.ListsHelper>();
            }
        }

        private Elmah.SQLite.NTierOntimeVersionRepository _NTierOntimeVersionRepository;
        public Elmah.SQLite.NTierOntimeVersionRepository NTierOntimeVersionRepository
        {
            get
            {
                if (_NTierOntimeVersionRepository == null)
                    _NTierOntimeVersionRepository = new Elmah.SQLite.NTierOntimeVersionRepository(this.SQLiteService);
                return _NTierOntimeVersionRepository;
            }
        }

        private Elmah.SQLite.CodeListCachingRepository _CodeListCachingRepository;
        public Elmah.SQLite.CodeListCachingRepository CodeListCachingRepository
        {
            get
            {
                if (_CodeListCachingRepository == null)
                    _CodeListCachingRepository = new Elmah.SQLite.CodeListCachingRepository(this.SQLiteService);
                return _CodeListCachingRepository;
            }
        }

        public SQLiteFactory()
        {
            SQLiteService.Init();
        }

        public static void Initialize()
        {
            DependencyService.Register<SQLiteFactory>();
            DependencyService.Register<Framework.Xaml.SQLite.SQLiteService>();
        }

        public void DeleteDatabase()
        {
            this.SQLiteService.DeleteDatabase();
        }
    }
}

