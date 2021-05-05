using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Elmah.XamarinForms.ViewModels
{
    public class AppShellVM: Framework.Xaml.AppShellVM
    {
        public Elmah.MVVMLightViewModels.NavigationVM NavigationVM
        {
            get
            {
                return DependencyService.Resolve<Elmah.MVVMLightViewModels.NavigationVM>(DependencyFetchTarget.GlobalInstance);
            }
        }
        public Elmah.PetStore.ViewModels.NavigationVM NavigationVM_PetStore
        {
            get
            {
                return DependencyService.Resolve<Elmah.PetStore.ViewModels.NavigationVM>();
            }
        }

        public Framework.Xaml.ActionForm.ActionParameter ELMAH_Error_NavigateToCommandParam_CommonResultView
        {
            get; private set;
        }

        //public Framework.Xaml.ActionForm.ActionParameter ElmahApplication_NavigateToCommandParam_CommonResultView
        //{
        //    get; private set;
        //}

        //public Framework.Xaml.ActionForm.ActionParameter ElmahHost_NavigateToCommandParam_CommonResultView
        //{
        //    get; private set;
        //}

        //public Framework.Xaml.ActionForm.ActionParameter ElmahSource_NavigateToCommandParam_CommonResultView
        //{
        //    get; private set;
        //}

        //public Framework.Xaml.ActionForm.ActionParameter ElmahStatusCode_NavigateToCommandParam_CommonResultView
        //{
        //    get; private set;
        //}
        //public Framework.Xaml.ActionForm.ActionParameter ElmahType_NavigateToCommandParam_CommonResultView
        //{
        //    get; private set;
        //}
        //public Framework.Xaml.ActionForm.ActionParameter ElmahUser_NavigateToCommandParam_CommonResultView
        //{
        //    get; private set;
        //}
        public Framework.Xaml.ActionForm.ActionParameter PetStore_Pet_NavigateToCommandParam_ListPage
        {
            get; private set;
        }

        public AppShellVM()
        {
            ELMAH_Error_NavigateToCommandParam_CommonResultView = NavigationVM.ELMAH_Error.GetNavigateToCommandParam_CommonResultView(0, Framework.Xaml.ListItemViewModes.SingleSelection, false, nameof(Elmah.DataSourceEntities.ELMAH_Error.TimeUtc), Framework.Queries.QueryOrderDirections.Descending);
            //ElmahApplication_NavigateToCommandParam_CommonResultView = NavigationVM.ElmahApplication.GetNavigateToCommandParam_CommonResultView(0, Framework.Xaml.ListItemViewModes.SingleSelection, false, nameof(Elmah.DataSourceEntities.ELMAH_Error.TimeUtc), Framework.Queries.QueryOrderDirections.Descending);
            //ElmahHost_NavigateToCommandParam_CommonResultView = NavigationVM.ElmahHost.GetNavigateToCommandParam_CommonResultView(0, Framework.Xaml.ListItemViewModes.SingleSelection, false, nameof(Elmah.DataSourceEntities.ELMAH_Error.TimeUtc), Framework.Queries.QueryOrderDirections.Descending);
            //ElmahSource_NavigateToCommandParam_CommonResultView = NavigationVM.ElmahSource.GetNavigateToCommandParam_CommonResultView(0, Framework.Xaml.ListItemViewModes.SingleSelection, false, nameof(Elmah.DataSourceEntities.ELMAH_Error.TimeUtc), Framework.Queries.QueryOrderDirections.Descending);
            //ElmahStatusCode_NavigateToCommandParam_CommonResultView = NavigationVM.ElmahStatusCode.GetNavigateToCommandParam_CommonResultView(0, Framework.Xaml.ListItemViewModes.SingleSelection, false, nameof(Elmah.DataSourceEntities.ELMAH_Error.TimeUtc), Framework.Queries.QueryOrderDirections.Descending);
            //ElmahType_NavigateToCommandParam_CommonResultView = NavigationVM.ElmahType.GetNavigateToCommandParam_CommonResultView(0, Framework.Xaml.ListItemViewModes.SingleSelection, false, nameof(Elmah.DataSourceEntities.ELMAH_Error.TimeUtc), Framework.Queries.QueryOrderDirections.Descending);
            //ElmahUser_NavigateToCommandParam_CommonResultView = NavigationVM.ElmahUser.GetNavigateToCommandParam_CommonResultView(0, Framework.Xaml.ListItemViewModes.SingleSelection, false, nameof(Elmah.DataSourceEntities.ELMAH_Error.TimeUtc), Framework.Queries.QueryOrderDirections.Descending);

            PetStore_Pet_NavigateToCommandParam_ListPage = NavigationVM_PetStore.Pet.GetNavigateToCommandParam_ListPage(0, Framework.Xaml.ListItemViewModes.SingleSelection, false, nameof(Elmah.DataSourceEntities.ELMAH_Error.TimeUtc), Framework.Queries.QueryOrderDirections.Descending);
        }
    }
}
