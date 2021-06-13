using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Elmah.PetStore.ViewModels
{
    public partial class NavigationVM
    {
        public StoreContainer Store
        {
            get
            {
                return DependencyService.Resolve<StoreContainer>();
            }
        }

        public enum StoreActions
        {
            DeleteOrder,
            GetInventory,
            GetOrderById,
            PlaceOrder
        }

        public partial class StoreContainer: Framework.Xaml.NavigationVMEntityContainer<Elmah.PetStore.Models.Store>
        {
            public const string DomainKey = "PetStore_Store";

            public StoreContainer(): base()
            {
            }

            public void DefaultItem(long entityID)
            {
            }
        }
    }
}

