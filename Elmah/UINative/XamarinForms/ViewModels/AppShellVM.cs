using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Elmah.XamarinForms.ViewModels
{
    public class AppShellVM: Framework.Xaml.AppShellVM
    {
        // 1.1. NavigationVM
        public Elmah.MVVMLightViewModels.NavigationVM NavigationVM
        {
            get
            {
                return DependencyService.Resolve<Elmah.MVVMLightViewModels.NavigationVM>(DependencyFetchTarget.GlobalInstance);
            }
        }

        // 1.2.1 PetStore NavigationVM
        //public Elmah.PetStore.ViewModels.NavigationVM NavigationVM_PetStore
        //{
        //    get
        //    {
        //        return DependencyService.Resolve<Elmah.PetStore.ViewModels.NavigationVM>();
        //    }
        //}

        // 2.1.1.1 ELMAH_Error.CommonResultView
        //public Framework.Xaml.ActionForm.ActionParameter ELMAH_Error_NavigateToCommandParam_CommonResultView
        //{
        //    get; private set;
        //}

        // 2.1.2.1 ElmahApplication.CommonResultView
        //public Framework.Xaml.ActionForm.ActionParameter ElmahApplication_NavigateToCommandParam_CommonResultView
        //{
        //    get; private set;
        //}

        // 2.1.3.1 ElmahHost.CommonResultView
        //public Framework.Xaml.ActionForm.ActionParameter ElmahHost_NavigateToCommandParam_CommonResultView
        //{
        //    get; private set;
        //}

        // 2.1.4.1 ElmahSource.CommonResultView
        //public Framework.Xaml.ActionForm.ActionParameter ElmahSource_NavigateToCommandParam_CommonResultView
        //{
        //    get; private set;
        //}

        // 2.1.5.1 ElmahStatusCode.CommonResultView
        //public Framework.Xaml.ActionForm.ActionParameter ElmahStatusCode_NavigateToCommandParam_CommonResultView
        //{
        //    get; private set;
        //}

        // 2.1.6.1 ElmahType.CommonResultView
        //public Framework.Xaml.ActionForm.ActionParameter ElmahType_NavigateToCommandParam_CommonResultView
        //{
        //    get; private set;
        //}

        // 2.1.7.1 ElmahUser.CommonResultView
        //public Framework.Xaml.ActionForm.ActionParameter ElmahUser_NavigateToCommandParam_CommonResultView
        //{
        //    get; private set;
        //}

        // 2.2.1.1 PetStore.Order.ListPage
        //public Framework.Xaml.ActionForm.ActionParameter PetStore_Order_NavigateToCommandParam_ListPage
        //{
        //    get; private set;
        //}

        // 2.2.1.2 PetStore.User.ListPage
        //public Framework.Xaml.ActionForm.ActionParameter PetStore_User_NavigateToCommandParam_ListPage
        //{
        //    get; private set;
        //}

        // 2.2.1.3 PetStore.Pet.ListPage
        //public Framework.Xaml.ActionForm.ActionParameter PetStore_Pet_NavigateToCommandParam_ListPage
        //{
        //    get; private set;
        //}

        public AppShellVM()
        {
            //TODO: change listItemViewMode will display different menus
            //TODO: #1: clear selection button and done button, IsSelectionList=true and IsRegularList=false when SingleSelection and MultipleSelection
            //TODO: #2: orderby list, IsSelectionList=false and IsRegularList=true when NavigationWhenClickItem and NavigationWhenRightArrow
            var listItemViewMode = Framework.Xaml.ListItemViewModes.NavigationWhenRightArrow;

            // 3.1.1.1 ELMAH_Error.CommonResultView
            //ELMAH_Error_NavigateToCommandParam_CommonResultView = NavigationVM.ELMAH_Error.GetNavigateToCommandParam_CommonResultView(0, listItemViewMode, false, nameof(Elmah.DataSourceEntities.ELMAH_Error.??), Framework.Queries.QueryOrderDirections.Descending);

            // 3.1.2.1 ElmahApplication.CommonResultView
            //ElmahApplication_NavigateToCommandParam_CommonResultView = NavigationVM.ElmahApplication.GetNavigateToCommandParam_CommonResultView(0, listItemViewMode, false, nameof(Elmah.DataSourceEntities.ElmahApplication.??), Framework.Queries.QueryOrderDirections.Descending);

            // 3.1.3.1 ElmahHost.CommonResultView
            //ElmahHost_NavigateToCommandParam_CommonResultView = NavigationVM.ElmahHost.GetNavigateToCommandParam_CommonResultView(0, listItemViewMode, false, nameof(Elmah.DataSourceEntities.ElmahHost.??), Framework.Queries.QueryOrderDirections.Descending);

            // 3.1.4.1 ElmahSource.CommonResultView
            //ElmahSource_NavigateToCommandParam_CommonResultView = NavigationVM.ElmahSource.GetNavigateToCommandParam_CommonResultView(0, listItemViewMode, false, nameof(Elmah.DataSourceEntities.ElmahSource.??), Framework.Queries.QueryOrderDirections.Descending);

            // 3.1.5.1 ElmahStatusCode.CommonResultView
            //ElmahStatusCode_NavigateToCommandParam_CommonResultView = NavigationVM.ElmahStatusCode.GetNavigateToCommandParam_CommonResultView(0, listItemViewMode, false, nameof(Elmah.DataSourceEntities.ElmahStatusCode.??), Framework.Queries.QueryOrderDirections.Descending);

            // 3.1.6.1 ElmahType.CommonResultView
            //ElmahType_NavigateToCommandParam_CommonResultView = NavigationVM.ElmahType.GetNavigateToCommandParam_CommonResultView(0, listItemViewMode, false, nameof(Elmah.DataSourceEntities.ElmahType.??), Framework.Queries.QueryOrderDirections.Descending);

            // 3.1.7.1 ElmahUser.CommonResultView
            //ElmahUser_NavigateToCommandParam_CommonResultView = NavigationVM.ElmahUser.GetNavigateToCommandParam_CommonResultView(0, listItemViewMode, false, nameof(Elmah.DataSourceEntities.ElmahUser.??), Framework.Queries.QueryOrderDirections.Descending);

            // 3.2.1.1 PetStore.Order.ListPage
            //PetStore_Order_NavigateToCommandParam_ListPage = NavigationVM_PetStore.Order.GetNavigateToCommandParam_ListPage(0, Elmah.PetStore.ViewModels.OrderVM.MessageTitle_LoadData_?, listItemViewMode, false, nameof(Elmah.PetStore.Models.Order.?), Framework.Queries.QueryOrderDirections.Descending);

            // 3.2.1.2 PetStore.User.ListPage
            //PetStore_User_NavigateToCommandParam_ListPage = NavigationVM_PetStore.User.GetNavigateToCommandParam_ListPage(0, Elmah.PetStore.ViewModels.UserVM.MessageTitle_LoadData_?, listItemViewMode, false, nameof(Elmah.PetStore.Models.User.?), Framework.Queries.QueryOrderDirections.Descending);

            // 3.2.1.3 PetStore.Pet.ListPage
            //PetStore_Pet_NavigateToCommandParam_ListPage = NavigationVM_PetStore.Pet.GetNavigateToCommandParam_ListPage(0, Elmah.PetStore.ViewModels.PetVM.MessageTitle_LoadData_?, listItemViewMode, false, nameof(Elmah.PetStore.Models.Pet.?), Framework.Queries.QueryOrderDirections.Descending);

        }
    }
}

/*
    <!-- 4.1.1.1 ELMAH_Error.CommonResultView -->
    <MenuItem Text="{i18n:Translate Text=ELMAH_Error, ResourceId=Elmah.Resx.UIStringResourcePerApp}" AutomationId="ELMAH_Error_CommonResultView"
                BindingContext="{x:Reference self}"
                Command="{Binding NavigationVM.NavigationCommand, Source={StaticResource LocatorClient}}" CommandParameter="{Binding BindingContext.ELMAH_Error_NavigateToCommandParam_CommonResultView}" >
        <MenuItem.IconImageSource>
            <FontImageSource FontFamily="{DynamicResource FontAwesomeSolid}" Glyph="&#xf200;"/>
        </MenuItem.IconImageSource>
    </MenuItem>
*/

/*
    <!-- 4.1.2.1 ElmahApplication.CommonResultView -->
    <MenuItem Text="{i18n:Translate Text=ElmahApplication, ResourceId=Elmah.Resx.UIStringResourcePerApp}" AutomationId="ElmahApplication_CommonResultView"
                BindingContext="{x:Reference self}"
                Command="{Binding NavigationVM.NavigationCommand, Source={StaticResource LocatorClient}}" CommandParameter="{Binding BindingContext.ElmahApplication_NavigateToCommandParam_CommonResultView}" >
        <MenuItem.IconImageSource>
            <FontImageSource FontFamily="{DynamicResource FontAwesomeSolid}" Glyph="&#xf200;"/>
        </MenuItem.IconImageSource>
    </MenuItem>
*/

/*
    <!-- 4.1.3.1 ElmahHost.CommonResultView -->
    <MenuItem Text="{i18n:Translate Text=ElmahHost, ResourceId=Elmah.Resx.UIStringResourcePerApp}" AutomationId="ElmahHost_CommonResultView"
                BindingContext="{x:Reference self}"
                Command="{Binding NavigationVM.NavigationCommand, Source={StaticResource LocatorClient}}" CommandParameter="{Binding BindingContext.ElmahHost_NavigateToCommandParam_CommonResultView}" >
        <MenuItem.IconImageSource>
            <FontImageSource FontFamily="{DynamicResource FontAwesomeSolid}" Glyph="&#xf200;"/>
        </MenuItem.IconImageSource>
    </MenuItem>
*/

/*
    <!-- 4.1.4.1 ElmahSource.CommonResultView -->
    <MenuItem Text="{i18n:Translate Text=ElmahSource, ResourceId=Elmah.Resx.UIStringResourcePerApp}" AutomationId="ElmahSource_CommonResultView"
                BindingContext="{x:Reference self}"
                Command="{Binding NavigationVM.NavigationCommand, Source={StaticResource LocatorClient}}" CommandParameter="{Binding BindingContext.ElmahSource_NavigateToCommandParam_CommonResultView}" >
        <MenuItem.IconImageSource>
            <FontImageSource FontFamily="{DynamicResource FontAwesomeSolid}" Glyph="&#xf200;"/>
        </MenuItem.IconImageSource>
    </MenuItem>
*/

/*
    <!-- 4.1.5.1 ElmahStatusCode.CommonResultView -->
    <MenuItem Text="{i18n:Translate Text=ElmahStatusCode, ResourceId=Elmah.Resx.UIStringResourcePerApp}" AutomationId="ElmahStatusCode_CommonResultView"
                BindingContext="{x:Reference self}"
                Command="{Binding NavigationVM.NavigationCommand, Source={StaticResource LocatorClient}}" CommandParameter="{Binding BindingContext.ElmahStatusCode_NavigateToCommandParam_CommonResultView}" >
        <MenuItem.IconImageSource>
            <FontImageSource FontFamily="{DynamicResource FontAwesomeSolid}" Glyph="&#xf200;"/>
        </MenuItem.IconImageSource>
    </MenuItem>
*/

/*
    <!-- 4.1.6.1 ElmahType.CommonResultView -->
    <MenuItem Text="{i18n:Translate Text=ElmahType, ResourceId=Elmah.Resx.UIStringResourcePerApp}" AutomationId="ElmahType_CommonResultView"
                BindingContext="{x:Reference self}"
                Command="{Binding NavigationVM.NavigationCommand, Source={StaticResource LocatorClient}}" CommandParameter="{Binding BindingContext.ElmahType_NavigateToCommandParam_CommonResultView}" >
        <MenuItem.IconImageSource>
            <FontImageSource FontFamily="{DynamicResource FontAwesomeSolid}" Glyph="&#xf200;"/>
        </MenuItem.IconImageSource>
    </MenuItem>
*/

/*
    <!-- 4.1.7.1 ElmahUser.CommonResultView -->
    <MenuItem Text="{i18n:Translate Text=ElmahUser, ResourceId=Elmah.Resx.UIStringResourcePerApp}" AutomationId="ElmahUser_CommonResultView"
                BindingContext="{x:Reference self}"
                Command="{Binding NavigationVM.NavigationCommand, Source={StaticResource LocatorClient}}" CommandParameter="{Binding BindingContext.ElmahUser_NavigateToCommandParam_CommonResultView}" >
        <MenuItem.IconImageSource>
            <FontImageSource FontFamily="{DynamicResource FontAwesomeSolid}" Glyph="&#xf200;"/>
        </MenuItem.IconImageSource>
    </MenuItem>
*/

/*
    <!-- 4.2.1.1 PetStore.Order.ListPage -->
    <MenuItem Text="{i18n:Translate Text=Order, ResourceId=Elmah.PetStore.Resx.UIStringResource}" AutomationId="PetStore_Order_ListPage"
                BindingContext="{x:Reference self}"
                Command="{Binding NavigationVM.NavigationCommand, Source={StaticResource LocatorClient}}" CommandParameter="{Binding BindingContext.PetStore_Order_NavigateToCommandParam_ListPage}" >
        <MenuItem.IconImageSource>
            <FontImageSource FontFamily="{DynamicResource FontAwesomeSolid}" Glyph="&#xf200;"/>
        </MenuItem.IconImageSource>
    </MenuItem>
*/

/*
    <!-- 4.2.2.2 PetStore.User.ListPage -->
    <MenuItem Text="{i18n:Translate Text=User, ResourceId=Elmah.PetStore.Resx.UIStringResource}" AutomationId="PetStore_User_ListPage"
                BindingContext="{x:Reference self}"
                Command="{Binding NavigationVM.NavigationCommand, Source={StaticResource LocatorClient}}" CommandParameter="{Binding BindingContext.PetStore_User_NavigateToCommandParam_ListPage}" >
        <MenuItem.IconImageSource>
            <FontImageSource FontFamily="{DynamicResource FontAwesomeSolid}" Glyph="&#xf200;"/>
        </MenuItem.IconImageSource>
    </MenuItem>
*/

/*
    <!-- 4.2.3.3 PetStore.Pet.ListPage -->
    <MenuItem Text="{i18n:Translate Text=Pet, ResourceId=Elmah.PetStore.Resx.UIStringResource}" AutomationId="PetStore_Pet_ListPage"
                BindingContext="{x:Reference self}"
                Command="{Binding NavigationVM.NavigationCommand, Source={StaticResource LocatorClient}}" CommandParameter="{Binding BindingContext.PetStore_Pet_NavigateToCommandParam_ListPage}" >
        <MenuItem.IconImageSource>
            <FontImageSource FontFamily="{DynamicResource FontAwesomeSolid}" Glyph="&#xf200;"/>
        </MenuItem.IconImageSource>
    </MenuItem>
*/

