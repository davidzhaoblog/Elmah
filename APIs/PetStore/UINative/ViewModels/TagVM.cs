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
    public partial class TagVM
        : Framework.Xaml.ViewModelBase2
    {
        public const string MessageTitle_LoadData = "Load_PetStore_Tag_VM";
        public string SearchBarPlaceHolder => Elmah.PetStore.Resx.UIStringResource.Tag;

        /// <summary>
        /// Initializes a new instance of the IndexVM class.
        /// </summary>
        public TagVM()
            : base()
        {

        }

    }
}

