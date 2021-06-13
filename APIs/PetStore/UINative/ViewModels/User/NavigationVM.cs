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
        public UserContainer User
        {
            get
            {
                return DependencyService.Resolve<UserContainer>();
            }
        }

        public enum UserActions
        {
            DeleteUser,
            LoginUser,
            LogoutUser,
            GetUserByName,
            CreateUser,
            CreateUsersWithListInput,
            UpdateUser
        }

        public partial class UserContainer: Framework.Xaml.NavigationVMEntityContainer<Elmah.PetStore.Models.User>
        {
            public const string DomainKey = "PetStore_User";

            public UserContainer(): base()
            {
            }

            public void DefaultItem(long entityID)
            {
            }
        }
    }
}

