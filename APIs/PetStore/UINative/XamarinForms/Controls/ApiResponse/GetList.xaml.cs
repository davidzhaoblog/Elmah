using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Elmah.PetStore.XamarinForms.Controls.ApiResponse
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GetList : ContentView
    {
        public GetList()
        {
            InitializeComponent();
        }

        /* ItemAction 4. you can click ... to inline action sheet, will display on left side of ...  */
        /*
        private void  OnShowInlineActionSheet_Tapped(object sender, EventArgs e)
        {
            var navigationVM = DependencyService.Resolve<Elmah.XamarinForms.ViewModels.NavigationVM>();
            Elmah.XamarinForms.ViewModels.NavigationVM.InlineActionSheetHandler.Show_InlineActionSheet<Elmah.DataSourceEntities.ELMAH_Error.Default>(
                sender, e, this.InlineActionSheet, navigationVM.ELMAH_Error.GetItemInlineActionSheet);
        }
        */

    }
}

