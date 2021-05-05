using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Elmah.MVVMLightViewModels
{
    public partial class NavigationVM : Framework.Xaml.NavigationVM
    {
        public NavigationVM()
            : base()
        {
            RegisterRoutes();
        }

        public virtual void RegisterRoutes()
        {
            throw new NotImplementedException();
        }
    }
}

