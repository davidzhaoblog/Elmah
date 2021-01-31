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
    public partial class GenericCollectionViewNested2 : ContentView
    {
        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(GenericCollectionViewNested2), null, BindingMode.OneTime);

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }

        public static readonly BindableProperty InnerItemsLayoutProperty =
            BindableProperty.Create(nameof(InnerItemsLayout), typeof(IItemsLayout), typeof(GenericCollectionViewNested2), null, BindingMode.OneTime);

        public IItemsLayout InnerItemsLayout
        {
            get { return (IItemsLayout)GetValue(InnerItemsLayoutProperty); }
            set { SetValue(InnerItemsLayoutProperty, value); }
        }

        public static readonly BindableProperty InnerHeightRequestProperty =
            BindableProperty.Create(nameof(InnerHeightRequest), typeof(double), typeof(GenericCollectionViewNested2), null, BindingMode.OneTime);

        public double InnerHeightRequest
        {
            get { return (double)GetValue(InnerHeightRequestProperty); }
            set { SetValue(InnerHeightRequestProperty, value); }
        }

        public static readonly BindableProperty InnerMinimumHeightRequestProperty =
            BindableProperty.Create(nameof(InnerMinimumHeightRequest), typeof(double), typeof(GenericCollectionViewNested2), null, BindingMode.OneTime);

        public double InnerMinimumHeightRequest
        {
            get { return (double)GetValue(InnerMinimumHeightRequestProperty); }
            set { SetValue(InnerMinimumHeightRequestProperty, value); }
        }

        public GenericCollectionViewNested2()
        {
            InitializeComponent();
        }
    }
}