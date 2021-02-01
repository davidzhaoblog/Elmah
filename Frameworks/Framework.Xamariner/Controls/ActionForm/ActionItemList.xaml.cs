using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Framework.Xamariner.Controls.ActionForm
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ActionItemList : ContentView
    {
        public ActionItemList()
        {
            InitializeComponent();
            this.BindingContextChanged += ActionItemList_BindingContextChanged;
        }

        private void ActionItemList_BindingContextChanged(object sender, EventArgs e)
        {
            Initialize();
        }

        public void Initialize()
        {
            if (this.BindingContext != null && this.BindingContext is IList<Framework.Xaml.ActionForm.ActionItemModel>)
            {
                var list = (IList<Framework.Xaml.ActionForm.ActionItemModel>)BindingContext;
                this.Container.Children.Clear();
                foreach (var item in list)
                {
                    bool addSeparator = list.IndexOf(item) < list.Count - 1; // last item
                    if (item.ActionFormItemType == Framework.Xaml.ActionForm.ActionFormItemTypes.NavigationItem)
                    {
                        addSeparator = true;
                        var itemControl = new Framework.Xamariner.Controls.ActionForm.NavigationItem
                        {
                            BindingContext = item
                        };
                        itemControl.SetBinding(NavigationItem.TitleProperty, "Title");
                        itemControl.SetBinding(NavigationItem.DescriptionProperty, "Description");
                        if (!item.ShowRightArrow)
                        {
                            itemControl.SetBinding(NavigationItem.ShowNavigationIconProperty, "ShowRightArrow");
                        }
                        itemControl.SetBinding(NavigationItem.FontIconSettingsProperty, "FontIconSettings");
                        itemControl.SetBinding(NavigationItem.NavigationCommandProperty, "NavigationCommand");
                        if(item.NavigationCommandParam != null)
                            itemControl.SetBinding(NavigationItem.NavigationCommandParamProperty, "NavigationCommandParam");
                        this.Container.Children.Add(itemControl);
                    }
                    else if (item.ActionFormItemType == Framework.Xaml.ActionForm.ActionFormItemTypes.CustomControl)
                    {
                        addSeparator = true;
                        var itemControl = new Framework.Xamariner.Controls.ActionForm.CustomControl
                        {
                            BindingContext = item
                        };

                        itemControl.SetBinding(CustomControl.TitleProperty, "Title");
                        itemControl.SetBinding(CustomControl.DescriptionProperty, "Description");
                        itemControl.SetBinding(CustomControl.FontIconSettingsProperty, "FontIconSettings");
                        itemControl.SetChild(item.CustomControlInstance, true);
                        this.Container.Children.Add(itemControl);
                    }
                    else
                        addSeparator = false;

                    if (addSeparator)
                    {
                        var gridSeparator = new Grid
                        {
                            Style = (Style)Application.Current.Resources["HSeparator"]
                        };
                        this.Container.Children.Add(gridSeparator);
                    }
                }
            }
        }
    }
}