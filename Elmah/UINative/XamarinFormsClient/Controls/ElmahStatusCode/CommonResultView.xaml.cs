using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Elmah.XamarinForms.Controls.ElmahStatusCode
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CommonResultView : ContentView
    {
        public CommonResultView()
        {
            InitializeComponent();
        }

        /* ItemAction 4. you can click ... to inline action sheet, will display on left side of ...  */
        /*
        private void  OnShowInlineActionSheet_Tapped(object sender, EventArgs e)
        {
            var navigationVM = DependencyService.Resolve<Elmah.XamarinForms.ViewModels.NavigationVM>();
            Elmah.XamarinForms.ViewModels.NavigationVM.InlineActionSheetHandler.Show_InlineActionSheet<Elmah.DataSourceEntities.ElmahStatusCode>(
                sender, e, this.InlineActionSheet, navigationVM.ElmahStatusCode.GetItemInlineActionSheet);
        }
        */

    }
}

