using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Framework.Xamariner.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GenericCollectionView : ContentView
    {
        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(GenericCollectionView), null, BindingMode.TwoWay);

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public static readonly BindableProperty ItemContentProperty =
            BindableProperty.Create(nameof(ItemContent), typeof(View), typeof(GenericCollectionView), null, BindingMode.TwoWay
                , propertyChanged: (sender, oldValue, newValue) => {
                    try
                    {
                        (sender as GenericCollectionView).ItemContentView.Content = (View)newValue;
                    }
                    catch (Exception)
                    {
                    }
                });

        public View ItemContent
        {
            get { return (View)GetValue(ItemContentProperty); }
            set { SetValue(ItemContentProperty, value); }
        }

        public static readonly BindableProperty RightSidePopupProperty =
            BindableProperty.Create(nameof(RightSidePopup), typeof(View), typeof(GenericCollectionView), null, BindingMode.TwoWay
                , propertyChanged: (sender, oldValue, newValue) => {
                    try
                    {
                        (sender as GenericCollectionView).RightSidePopupView.Content = (View)newValue;
                    }
                    catch (Exception)
                    {
                    }
                });


        public View RightSidePopup
        {
            get { return (View)GetValue(RightSidePopupProperty); }
            set { SetValue(RightSidePopupProperty, value); }
        }

        public GenericCollectionView()
        {
            InitializeComponent();
        }
    }
}