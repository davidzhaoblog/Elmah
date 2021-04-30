using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.Generic;
using System.Windows.Input;

//using Framework.Xaml.SQLite;

namespace Elmah.PetStore.ViewModels
{
    public partial class CustomerVM
        : Framework.Xaml.ViewModelBase2
    {
        public const string MessageTitle_LoadData = "Load_PetStore_Customer_VM";
        public string SearchBarPlaceHolder => Elmah.PetStore.Resx.UIStringResource.Customer;

        /// <summary>
        /// Initializes a new instance of the IndexVM class.
        /// </summary>
        public CustomerVM()
            : base()
        {

        }

    }
}

