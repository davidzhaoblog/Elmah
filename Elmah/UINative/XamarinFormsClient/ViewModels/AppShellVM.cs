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
        public Elmah.PetStore.ViewModels.NavigationVM NavigationVM_PetStore
        {
            get
            {
                return DependencyService.Resolve<Elmah.PetStore.ViewModels.NavigationVM>();
            }
        }

        // 2.1.1.1 ELMAH_Error.Index
        //public Framework.Xaml.ActionForm.ActionParameter ELMAH_Error_NavigateToCommandParam_Index
        //{
        //    get; private set;
        //}

        // 2.1.2.2 ELMAH_Error.CommonSearchView
        //public Framework.Xaml.ActionForm.ActionParameter ELMAH_Error_NavigateToCommandParam_CommonSearchView
        //{
        //    get; private set;
        //}

        // 2.1.3.3 ELMAH_Error.CommonResultView
        public Framework.Xaml.ActionForm.ActionParameter ELMAH_Error_NavigateToCommandParam_CommonResultView
        {
            get; private set;
        }

        // 2.1.4.1 ElmahApplication.Index
        //public Framework.Xaml.ActionForm.ActionParameter ElmahApplication_NavigateToCommandParam_Index
        //{
        //    get; private set;
        //}

        // 2.1.5.2 ElmahApplication.CommonSearchView
        //public Framework.Xaml.ActionForm.ActionParameter ElmahApplication_NavigateToCommandParam_CommonSearchView
        //{
        //    get; private set;
        //}

        // 2.1.6.3 ElmahApplication.CommonResultView
        //public Framework.Xaml.ActionForm.ActionParameter ElmahApplication_NavigateToCommandParam_CommonResultView
        //{
        //    get; private set;
        //}

        // 2.1.7.1 ElmahHost.Index
        //public Framework.Xaml.ActionForm.ActionParameter ElmahHost_NavigateToCommandParam_Index
        //{
        //    get; private set;
        //}

        // 2.1.8.2 ElmahHost.CommonSearchView
        //public Framework.Xaml.ActionForm.ActionParameter ElmahHost_NavigateToCommandParam_CommonSearchView
        //{
        //    get; private set;
        //}

        // 2.1.9.3 ElmahHost.CommonResultView
        //public Framework.Xaml.ActionForm.ActionParameter ElmahHost_NavigateToCommandParam_CommonResultView
        //{
        //    get; private set;
        //}

        // 2.1.10.1 ElmahSource.Index
        //public Framework.Xaml.ActionForm.ActionParameter ElmahSource_NavigateToCommandParam_Index
        //{
        //    get; private set;
        //}

        // 2.1.11.2 ElmahSource.CommonSearchView
        //public Framework.Xaml.ActionForm.ActionParameter ElmahSource_NavigateToCommandParam_CommonSearchView
        //{
        //    get; private set;
        //}

        // 2.1.12.3 ElmahSource.CommonResultView
        //public Framework.Xaml.ActionForm.ActionParameter ElmahSource_NavigateToCommandParam_CommonResultView
        //{
        //    get; private set;
        //}

        // 2.1.13.1 ElmahStatusCode.Index
        //public Framework.Xaml.ActionForm.ActionParameter ElmahStatusCode_NavigateToCommandParam_Index
        //{
        //    get; private set;
        //}

        // 2.1.14.2 ElmahStatusCode.CommonSearchView
        //public Framework.Xaml.ActionForm.ActionParameter ElmahStatusCode_NavigateToCommandParam_CommonSearchView
        //{
        //    get; private set;
        //}

        // 2.1.15.3 ElmahStatusCode.CommonResultView
        //public Framework.Xaml.ActionForm.ActionParameter ElmahStatusCode_NavigateToCommandParam_CommonResultView
        //{
        //    get; private set;
        //}

        // 2.1.16.1 ElmahType.Index
        //public Framework.Xaml.ActionForm.ActionParameter ElmahType_NavigateToCommandParam_Index
        //{
        //    get; private set;
        //}

        // 2.1.17.2 ElmahType.CommonSearchView
        //public Framework.Xaml.ActionForm.ActionParameter ElmahType_NavigateToCommandParam_CommonSearchView
        //{
        //    get; private set;
        //}

        // 2.1.18.3 ElmahType.CommonResultView
        //public Framework.Xaml.ActionForm.ActionParameter ElmahType_NavigateToCommandParam_CommonResultView
        //{
        //    get; private set;
        //}

        // 2.1.19.1 ElmahUser.Index
        //public Framework.Xaml.ActionForm.ActionParameter ElmahUser_NavigateToCommandParam_Index
        //{
        //    get; private set;
        //}

        // 2.1.20.2 ElmahUser.CommonSearchView
        //public Framework.Xaml.ActionForm.ActionParameter ElmahUser_NavigateToCommandParam_CommonSearchView
        //{
        //    get; private set;
        //}

        // 2.1.21.3 ElmahUser.CommonResultView
        //public Framework.Xaml.ActionForm.ActionParameter ElmahUser_NavigateToCommandParam_CommonResultView
        //{
        //    get; private set;
        //}

        // 2.2.1.1 PetStore.Order.ListPage
        //public Framework.Xaml.ActionForm.ActionParameter PetStore_Order_NavigateToCommandParam_ListPage
        //{
        //    get; private set;
        //}

        // 2.2.2.2 PetStore.User.ListPage
        //public Framework.Xaml.ActionForm.ActionParameter PetStore_User_NavigateToCommandParam_ListPage
        //{
        //    get; private set;
        //}

        // 2.2.3.3 PetStore.Pet.ListPage
        public Framework.Xaml.ActionForm.ActionParameter PetStore_Pet_NavigateToCommandParam_ListPage
        {
            get; private set;
        }

        public AppShellVM()
        {

            // 3.1.1.1 ELMAH_Error.Index
            //ELMAH_Error_NavigateToCommandParam_Index = NavigationVM.ELMAH_Error.GetNavigateToCommandParam_CommonResultView(0, Framework.Xaml.ListItemViewModes.SingleSelection, false, nameof(Elmah.DataSourceEntities.DataSourceEntity.??), Framework.Queries.QueryOrderDirections.Descending);

            // 3.1.2.2 ELMAH_Error.CommonSearchView
            //ELMAH_Error_NavigateToCommandParam_CommonSearchView = NavigationVM.ELMAH_Error.GetNavigateToCommandParam_CommonResultView(0, Framework.Xaml.ListItemViewModes.SingleSelection, false, nameof(Elmah.DataSourceEntities.DataSourceEntity.??), Framework.Queries.QueryOrderDirections.Descending);

           // 3.1.3.3 ELMAH_Error.CommonResultView
           ELMAH_Error_NavigateToCommandParam_CommonResultView = NavigationVM.ELMAH_Error.GetNavigateToCommandParam_CommonResultView(0, Framework.Xaml.ListItemViewModes.SingleSelection, false, nameof(Elmah.DataSourceEntities.ELMAH_Error.Host), Framework.Queries.QueryOrderDirections.Descending);

            // 3.1.4.1 ElmahApplication.Index
            //ElmahApplication_NavigateToCommandParam_Index = NavigationVM.ElmahApplication.GetNavigateToCommandParam_CommonResultView(0, Framework.Xaml.ListItemViewModes.SingleSelection, false, nameof(Elmah.DataSourceEntities.DataSourceEntity.??), Framework.Queries.QueryOrderDirections.Descending);

            // 3.1.5.2 ElmahApplication.CommonSearchView
            //ElmahApplication_NavigateToCommandParam_CommonSearchView = NavigationVM.ElmahApplication.GetNavigateToCommandParam_CommonResultView(0, Framework.Xaml.ListItemViewModes.SingleSelection, false, nameof(Elmah.DataSourceEntities.DataSourceEntity.??), Framework.Queries.QueryOrderDirections.Descending);

            // 3.1.6.3 ElmahApplication.CommonResultView
            //ElmahApplication_NavigateToCommandParam_CommonResultView = NavigationVM.ElmahApplication.GetNavigateToCommandParam_CommonResultView(0, Framework.Xaml.ListItemViewModes.SingleSelection, false, nameof(Elmah.DataSourceEntities.DataSourceEntity.??), Framework.Queries.QueryOrderDirections.Descending);

            // 3.1.7.1 ElmahHost.Index
            //ElmahHost_NavigateToCommandParam_Index = NavigationVM.ElmahHost.GetNavigateToCommandParam_CommonResultView(0, Framework.Xaml.ListItemViewModes.SingleSelection, false, nameof(Elmah.DataSourceEntities.DataSourceEntity.??), Framework.Queries.QueryOrderDirections.Descending);

            // 3.1.8.2 ElmahHost.CommonSearchView
            //ElmahHost_NavigateToCommandParam_CommonSearchView = NavigationVM.ElmahHost.GetNavigateToCommandParam_CommonResultView(0, Framework.Xaml.ListItemViewModes.SingleSelection, false, nameof(Elmah.DataSourceEntities.DataSourceEntity.??), Framework.Queries.QueryOrderDirections.Descending);

            // 3.1.9.3 ElmahHost.CommonResultView
            //ElmahHost_NavigateToCommandParam_CommonResultView = NavigationVM.ElmahHost.GetNavigateToCommandParam_CommonResultView(0, Framework.Xaml.ListItemViewModes.SingleSelection, false, nameof(Elmah.DataSourceEntities.DataSourceEntity.??), Framework.Queries.QueryOrderDirections.Descending);

            // 3.1.10.1 ElmahSource.Index
            //ElmahSource_NavigateToCommandParam_Index = NavigationVM.ElmahSource.GetNavigateToCommandParam_CommonResultView(0, Framework.Xaml.ListItemViewModes.SingleSelection, false, nameof(Elmah.DataSourceEntities.DataSourceEntity.??), Framework.Queries.QueryOrderDirections.Descending);

            // 3.1.11.2 ElmahSource.CommonSearchView
            //ElmahSource_NavigateToCommandParam_CommonSearchView = NavigationVM.ElmahSource.GetNavigateToCommandParam_CommonResultView(0, Framework.Xaml.ListItemViewModes.SingleSelection, false, nameof(Elmah.DataSourceEntities.DataSourceEntity.??), Framework.Queries.QueryOrderDirections.Descending);

            // 3.1.12.3 ElmahSource.CommonResultView
            //ElmahSource_NavigateToCommandParam_CommonResultView = NavigationVM.ElmahSource.GetNavigateToCommandParam_CommonResultView(0, Framework.Xaml.ListItemViewModes.SingleSelection, false, nameof(Elmah.DataSourceEntities.DataSourceEntity.??), Framework.Queries.QueryOrderDirections.Descending);

            // 3.1.13.1 ElmahStatusCode.Index
            //ElmahStatusCode_NavigateToCommandParam_Index = NavigationVM.ElmahStatusCode.GetNavigateToCommandParam_CommonResultView(0, Framework.Xaml.ListItemViewModes.SingleSelection, false, nameof(Elmah.DataSourceEntities.DataSourceEntity.??), Framework.Queries.QueryOrderDirections.Descending);

            // 3.1.14.2 ElmahStatusCode.CommonSearchView
            //ElmahStatusCode_NavigateToCommandParam_CommonSearchView = NavigationVM.ElmahStatusCode.GetNavigateToCommandParam_CommonResultView(0, Framework.Xaml.ListItemViewModes.SingleSelection, false, nameof(Elmah.DataSourceEntities.DataSourceEntity.??), Framework.Queries.QueryOrderDirections.Descending);

            // 3.1.15.3 ElmahStatusCode.CommonResultView
            //ElmahStatusCode_NavigateToCommandParam_CommonResultView = NavigationVM.ElmahStatusCode.GetNavigateToCommandParam_CommonResultView(0, Framework.Xaml.ListItemViewModes.SingleSelection, false, nameof(Elmah.DataSourceEntities.DataSourceEntity.??), Framework.Queries.QueryOrderDirections.Descending);

            // 3.1.16.1 ElmahType.Index
            //ElmahType_NavigateToCommandParam_Index = NavigationVM.ElmahType.GetNavigateToCommandParam_CommonResultView(0, Framework.Xaml.ListItemViewModes.SingleSelection, false, nameof(Elmah.DataSourceEntities.DataSourceEntity.??), Framework.Queries.QueryOrderDirections.Descending);

            // 3.1.17.2 ElmahType.CommonSearchView
            //ElmahType_NavigateToCommandParam_CommonSearchView = NavigationVM.ElmahType.GetNavigateToCommandParam_CommonResultView(0, Framework.Xaml.ListItemViewModes.SingleSelection, false, nameof(Elmah.DataSourceEntities.DataSourceEntity.??), Framework.Queries.QueryOrderDirections.Descending);

            // 3.1.18.3 ElmahType.CommonResultView
            //ElmahType_NavigateToCommandParam_CommonResultView = NavigationVM.ElmahType.GetNavigateToCommandParam_CommonResultView(0, Framework.Xaml.ListItemViewModes.SingleSelection, false, nameof(Elmah.DataSourceEntities.DataSourceEntity.??), Framework.Queries.QueryOrderDirections.Descending);

            // 3.1.19.1 ElmahUser.Index
            //ElmahUser_NavigateToCommandParam_Index = NavigationVM.ElmahUser.GetNavigateToCommandParam_CommonResultView(0, Framework.Xaml.ListItemViewModes.SingleSelection, false, nameof(Elmah.DataSourceEntities.DataSourceEntity.??), Framework.Queries.QueryOrderDirections.Descending);

            // 3.1.20.2 ElmahUser.CommonSearchView
            //ElmahUser_NavigateToCommandParam_CommonSearchView = NavigationVM.ElmahUser.GetNavigateToCommandParam_CommonResultView(0, Framework.Xaml.ListItemViewModes.SingleSelection, false, nameof(Elmah.DataSourceEntities.DataSourceEntity.??), Framework.Queries.QueryOrderDirections.Descending);

            // 3.1.21.3 ElmahUser.CommonResultView
            //ElmahUser_NavigateToCommandParam_CommonResultView = NavigationVM.ElmahUser.GetNavigateToCommandParam_CommonResultView(0, Framework.Xaml.ListItemViewModes.SingleSelection, false, nameof(Elmah.DataSourceEntities.DataSourceEntity.??), Framework.Queries.QueryOrderDirections.Descending);

            // 3.2.1.1 PetStore.Order.ListPage
            //PetStore_Order_NavigateToCommandParam_ListPage = NavigationVM_PetStore.Order.GetNavigateToCommandParam_ListPage(0, Framework.Xaml.ListItemViewModes.SingleSelection, false, nameof(Elmah.PetStore.Models.{0}.?), Framework.Queries.QueryOrderDirections.Descending);

            // 3.2.2.2 PetStore.User.ListPage
            //PetStore_User_NavigateToCommandParam_ListPage = NavigationVM_PetStore.User.GetNavigateToCommandParam_ListPage(0, Framework.Xaml.ListItemViewModes.SingleSelection, false, nameof(Elmah.PetStore.Models.{0}.?), Framework.Queries.QueryOrderDirections.Descending);

            // 3.2.3.3 PetStore.Pet.ListPage
            PetStore_Pet_NavigateToCommandParam_ListPage = NavigationVM_PetStore.Pet.GetNavigateToCommandParam_ListPage(0, Framework.Xaml.ListItemViewModes.SingleSelection, false, nameof(Elmah.PetStore.Models.Pet.Category), Framework.Queries.QueryOrderDirections.Descending);

        }
    }
}

/*
    <!-- 4.1.1.1 ELMAH_Error.Index -->
    <MenuItem Text="{i18n:Translate Text=ELMAH_Error, ResourceId=Elmah.Resx.UIStringResourcePerApp}" AutomationId="ELMAH_Error_Index"
                BindingContext="{x:Reference self}"
                Command="{Binding NavigationVM.NavigationCommand, Source={StaticResource LocatorClient}}" CommandParameter="{Binding BindingContext.ELMAH_Error_NavigateToCommandParam_Index}" >
        <MenuItem.IconImageSource>
            <FontImageSource FontFamily="{DynamicResource FontAwesomeSolid}" Glyph="&#xf200;"/>
        </MenuItem.IconImageSource>
    </MenuItem>
*/

/*
    <!-- 4.1.2.2 ELMAH_Error.CommonSearchView -->
    <MenuItem Text="{i18n:Translate Text=ELMAH_Error, ResourceId=Elmah.Resx.UIStringResourcePerApp}" AutomationId="ELMAH_Error_CommonSearchView"
                BindingContext="{x:Reference self}"
                Command="{Binding NavigationVM.NavigationCommand, Source={StaticResource LocatorClient}}" CommandParameter="{Binding BindingContext.ELMAH_Error_NavigateToCommandParam_CommonSearchView}" >
        <MenuItem.IconImageSource>
            <FontImageSource FontFamily="{DynamicResource FontAwesomeSolid}" Glyph="&#xf200;"/>
        </MenuItem.IconImageSource>
    </MenuItem>
*/

/*
    <!-- 4.1.3.3 ELMAH_Error.CommonResultView -->
    <MenuItem Text="{i18n:Translate Text=ELMAH_Error, ResourceId=Elmah.Resx.UIStringResourcePerApp}" AutomationId="ELMAH_Error_CommonResultView"
                BindingContext="{x:Reference self}"
                Command="{Binding NavigationVM.NavigationCommand, Source={StaticResource LocatorClient}}" CommandParameter="{Binding BindingContext.ELMAH_Error_NavigateToCommandParam_CommonResultView}" >
        <MenuItem.IconImageSource>
            <FontImageSource FontFamily="{DynamicResource FontAwesomeSolid}" Glyph="&#xf200;"/>
        </MenuItem.IconImageSource>
    </MenuItem>
*/

/*
    <!-- 4.1.4.1 ElmahApplication.Index -->
    <MenuItem Text="{i18n:Translate Text=ElmahApplication, ResourceId=Elmah.Resx.UIStringResourcePerApp}" AutomationId="ElmahApplication_Index"
                BindingContext="{x:Reference self}"
                Command="{Binding NavigationVM.NavigationCommand, Source={StaticResource LocatorClient}}" CommandParameter="{Binding BindingContext.ElmahApplication_NavigateToCommandParam_Index}" >
        <MenuItem.IconImageSource>
            <FontImageSource FontFamily="{DynamicResource FontAwesomeSolid}" Glyph="&#xf200;"/>
        </MenuItem.IconImageSource>
    </MenuItem>
*/

/*
    <!-- 4.1.5.2 ElmahApplication.CommonSearchView -->
    <MenuItem Text="{i18n:Translate Text=ElmahApplication, ResourceId=Elmah.Resx.UIStringResourcePerApp}" AutomationId="ElmahApplication_CommonSearchView"
                BindingContext="{x:Reference self}"
                Command="{Binding NavigationVM.NavigationCommand, Source={StaticResource LocatorClient}}" CommandParameter="{Binding BindingContext.ElmahApplication_NavigateToCommandParam_CommonSearchView}" >
        <MenuItem.IconImageSource>
            <FontImageSource FontFamily="{DynamicResource FontAwesomeSolid}" Glyph="&#xf200;"/>
        </MenuItem.IconImageSource>
    </MenuItem>
*/

/*
    <!-- 4.1.6.3 ElmahApplication.CommonResultView -->
    <MenuItem Text="{i18n:Translate Text=ElmahApplication, ResourceId=Elmah.Resx.UIStringResourcePerApp}" AutomationId="ElmahApplication_CommonResultView"
                BindingContext="{x:Reference self}"
                Command="{Binding NavigationVM.NavigationCommand, Source={StaticResource LocatorClient}}" CommandParameter="{Binding BindingContext.ElmahApplication_NavigateToCommandParam_CommonResultView}" >
        <MenuItem.IconImageSource>
            <FontImageSource FontFamily="{DynamicResource FontAwesomeSolid}" Glyph="&#xf200;"/>
        </MenuItem.IconImageSource>
    </MenuItem>
*/

/*
    <!-- 4.1.7.1 ElmahHost.Index -->
    <MenuItem Text="{i18n:Translate Text=ElmahHost, ResourceId=Elmah.Resx.UIStringResourcePerApp}" AutomationId="ElmahHost_Index"
                BindingContext="{x:Reference self}"
                Command="{Binding NavigationVM.NavigationCommand, Source={StaticResource LocatorClient}}" CommandParameter="{Binding BindingContext.ElmahHost_NavigateToCommandParam_Index}" >
        <MenuItem.IconImageSource>
            <FontImageSource FontFamily="{DynamicResource FontAwesomeSolid}" Glyph="&#xf200;"/>
        </MenuItem.IconImageSource>
    </MenuItem>
*/

/*
    <!-- 4.1.8.2 ElmahHost.CommonSearchView -->
    <MenuItem Text="{i18n:Translate Text=ElmahHost, ResourceId=Elmah.Resx.UIStringResourcePerApp}" AutomationId="ElmahHost_CommonSearchView"
                BindingContext="{x:Reference self}"
                Command="{Binding NavigationVM.NavigationCommand, Source={StaticResource LocatorClient}}" CommandParameter="{Binding BindingContext.ElmahHost_NavigateToCommandParam_CommonSearchView}" >
        <MenuItem.IconImageSource>
            <FontImageSource FontFamily="{DynamicResource FontAwesomeSolid}" Glyph="&#xf200;"/>
        </MenuItem.IconImageSource>
    </MenuItem>
*/

/*
    <!-- 4.1.9.3 ElmahHost.CommonResultView -->
    <MenuItem Text="{i18n:Translate Text=ElmahHost, ResourceId=Elmah.Resx.UIStringResourcePerApp}" AutomationId="ElmahHost_CommonResultView"
                BindingContext="{x:Reference self}"
                Command="{Binding NavigationVM.NavigationCommand, Source={StaticResource LocatorClient}}" CommandParameter="{Binding BindingContext.ElmahHost_NavigateToCommandParam_CommonResultView}" >
        <MenuItem.IconImageSource>
            <FontImageSource FontFamily="{DynamicResource FontAwesomeSolid}" Glyph="&#xf200;"/>
        </MenuItem.IconImageSource>
    </MenuItem>
*/

/*
    <!-- 4.1.10.1 ElmahSource.Index -->
    <MenuItem Text="{i18n:Translate Text=ElmahSource, ResourceId=Elmah.Resx.UIStringResourcePerApp}" AutomationId="ElmahSource_Index"
                BindingContext="{x:Reference self}"
                Command="{Binding NavigationVM.NavigationCommand, Source={StaticResource LocatorClient}}" CommandParameter="{Binding BindingContext.ElmahSource_NavigateToCommandParam_Index}" >
        <MenuItem.IconImageSource>
            <FontImageSource FontFamily="{DynamicResource FontAwesomeSolid}" Glyph="&#xf200;"/>
        </MenuItem.IconImageSource>
    </MenuItem>
*/

/*
    <!-- 4.1.11.2 ElmahSource.CommonSearchView -->
    <MenuItem Text="{i18n:Translate Text=ElmahSource, ResourceId=Elmah.Resx.UIStringResourcePerApp}" AutomationId="ElmahSource_CommonSearchView"
                BindingContext="{x:Reference self}"
                Command="{Binding NavigationVM.NavigationCommand, Source={StaticResource LocatorClient}}" CommandParameter="{Binding BindingContext.ElmahSource_NavigateToCommandParam_CommonSearchView}" >
        <MenuItem.IconImageSource>
            <FontImageSource FontFamily="{DynamicResource FontAwesomeSolid}" Glyph="&#xf200;"/>
        </MenuItem.IconImageSource>
    </MenuItem>
*/

/*
    <!-- 4.1.12.3 ElmahSource.CommonResultView -->
    <MenuItem Text="{i18n:Translate Text=ElmahSource, ResourceId=Elmah.Resx.UIStringResourcePerApp}" AutomationId="ElmahSource_CommonResultView"
                BindingContext="{x:Reference self}"
                Command="{Binding NavigationVM.NavigationCommand, Source={StaticResource LocatorClient}}" CommandParameter="{Binding BindingContext.ElmahSource_NavigateToCommandParam_CommonResultView}" >
        <MenuItem.IconImageSource>
            <FontImageSource FontFamily="{DynamicResource FontAwesomeSolid}" Glyph="&#xf200;"/>
        </MenuItem.IconImageSource>
    </MenuItem>
*/

/*
    <!-- 4.1.13.1 ElmahStatusCode.Index -->
    <MenuItem Text="{i18n:Translate Text=ElmahStatusCode, ResourceId=Elmah.Resx.UIStringResourcePerApp}" AutomationId="ElmahStatusCode_Index"
                BindingContext="{x:Reference self}"
                Command="{Binding NavigationVM.NavigationCommand, Source={StaticResource LocatorClient}}" CommandParameter="{Binding BindingContext.ElmahStatusCode_NavigateToCommandParam_Index}" >
        <MenuItem.IconImageSource>
            <FontImageSource FontFamily="{DynamicResource FontAwesomeSolid}" Glyph="&#xf200;"/>
        </MenuItem.IconImageSource>
    </MenuItem>
*/

/*
    <!-- 4.1.14.2 ElmahStatusCode.CommonSearchView -->
    <MenuItem Text="{i18n:Translate Text=ElmahStatusCode, ResourceId=Elmah.Resx.UIStringResourcePerApp}" AutomationId="ElmahStatusCode_CommonSearchView"
                BindingContext="{x:Reference self}"
                Command="{Binding NavigationVM.NavigationCommand, Source={StaticResource LocatorClient}}" CommandParameter="{Binding BindingContext.ElmahStatusCode_NavigateToCommandParam_CommonSearchView}" >
        <MenuItem.IconImageSource>
            <FontImageSource FontFamily="{DynamicResource FontAwesomeSolid}" Glyph="&#xf200;"/>
        </MenuItem.IconImageSource>
    </MenuItem>
*/

/*
    <!-- 4.1.15.3 ElmahStatusCode.CommonResultView -->
    <MenuItem Text="{i18n:Translate Text=ElmahStatusCode, ResourceId=Elmah.Resx.UIStringResourcePerApp}" AutomationId="ElmahStatusCode_CommonResultView"
                BindingContext="{x:Reference self}"
                Command="{Binding NavigationVM.NavigationCommand, Source={StaticResource LocatorClient}}" CommandParameter="{Binding BindingContext.ElmahStatusCode_NavigateToCommandParam_CommonResultView}" >
        <MenuItem.IconImageSource>
            <FontImageSource FontFamily="{DynamicResource FontAwesomeSolid}" Glyph="&#xf200;"/>
        </MenuItem.IconImageSource>
    </MenuItem>
*/

/*
    <!-- 4.1.16.1 ElmahType.Index -->
    <MenuItem Text="{i18n:Translate Text=ElmahType, ResourceId=Elmah.Resx.UIStringResourcePerApp}" AutomationId="ElmahType_Index"
                BindingContext="{x:Reference self}"
                Command="{Binding NavigationVM.NavigationCommand, Source={StaticResource LocatorClient}}" CommandParameter="{Binding BindingContext.ElmahType_NavigateToCommandParam_Index}" >
        <MenuItem.IconImageSource>
            <FontImageSource FontFamily="{DynamicResource FontAwesomeSolid}" Glyph="&#xf200;"/>
        </MenuItem.IconImageSource>
    </MenuItem>
*/

/*
    <!-- 4.1.17.2 ElmahType.CommonSearchView -->
    <MenuItem Text="{i18n:Translate Text=ElmahType, ResourceId=Elmah.Resx.UIStringResourcePerApp}" AutomationId="ElmahType_CommonSearchView"
                BindingContext="{x:Reference self}"
                Command="{Binding NavigationVM.NavigationCommand, Source={StaticResource LocatorClient}}" CommandParameter="{Binding BindingContext.ElmahType_NavigateToCommandParam_CommonSearchView}" >
        <MenuItem.IconImageSource>
            <FontImageSource FontFamily="{DynamicResource FontAwesomeSolid}" Glyph="&#xf200;"/>
        </MenuItem.IconImageSource>
    </MenuItem>
*/

/*
    <!-- 4.1.18.3 ElmahType.CommonResultView -->
    <MenuItem Text="{i18n:Translate Text=ElmahType, ResourceId=Elmah.Resx.UIStringResourcePerApp}" AutomationId="ElmahType_CommonResultView"
                BindingContext="{x:Reference self}"
                Command="{Binding NavigationVM.NavigationCommand, Source={StaticResource LocatorClient}}" CommandParameter="{Binding BindingContext.ElmahType_NavigateToCommandParam_CommonResultView}" >
        <MenuItem.IconImageSource>
            <FontImageSource FontFamily="{DynamicResource FontAwesomeSolid}" Glyph="&#xf200;"/>
        </MenuItem.IconImageSource>
    </MenuItem>
*/

/*
    <!-- 4.1.19.1 ElmahUser.Index -->
    <MenuItem Text="{i18n:Translate Text=ElmahUser, ResourceId=Elmah.Resx.UIStringResourcePerApp}" AutomationId="ElmahUser_Index"
                BindingContext="{x:Reference self}"
                Command="{Binding NavigationVM.NavigationCommand, Source={StaticResource LocatorClient}}" CommandParameter="{Binding BindingContext.ElmahUser_NavigateToCommandParam_Index}" >
        <MenuItem.IconImageSource>
            <FontImageSource FontFamily="{DynamicResource FontAwesomeSolid}" Glyph="&#xf200;"/>
        </MenuItem.IconImageSource>
    </MenuItem>
*/

/*
    <!-- 4.1.20.2 ElmahUser.CommonSearchView -->
    <MenuItem Text="{i18n:Translate Text=ElmahUser, ResourceId=Elmah.Resx.UIStringResourcePerApp}" AutomationId="ElmahUser_CommonSearchView"
                BindingContext="{x:Reference self}"
                Command="{Binding NavigationVM.NavigationCommand, Source={StaticResource LocatorClient}}" CommandParameter="{Binding BindingContext.ElmahUser_NavigateToCommandParam_CommonSearchView}" >
        <MenuItem.IconImageSource>
            <FontImageSource FontFamily="{DynamicResource FontAwesomeSolid}" Glyph="&#xf200;"/>
        </MenuItem.IconImageSource>
    </MenuItem>
*/

/*
    <!-- 4.1.21.3 ElmahUser.CommonResultView -->
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

