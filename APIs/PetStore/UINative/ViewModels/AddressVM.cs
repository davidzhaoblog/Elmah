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
    public partial class AddressVM
        : Framework.Xaml.ViewModelBase2
    {
        public const string MessageTitle_LoadData = "Load_PetStore_Address_VM";
        public string SearchBarPlaceHolder => Elmah.PetStore.Resx.UIStringResource.Address;

        /// <summary>
        /// Initializes a new instance of the IndexVM class.
        /// </summary>
        public AddressVM()
            : base()
        {

        }

    }
}

