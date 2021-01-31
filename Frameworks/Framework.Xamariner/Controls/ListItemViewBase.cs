using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Framework.Xamariner.Controls
{
    public class ListItemViewBase : ContentView, Framework.Xamariner.Controls.IIsSelected
    {
        public static readonly BindableProperty IsSelectedProperty = BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(ListItemViewBase), false, BindingMode.TwoWay);
        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public static readonly BindableProperty HasSelectedIconProperty =
            BindableProperty.Create(nameof(HasSelectedIcon), typeof(bool), typeof(ListItemViewBase), false, BindingMode.OneWay);
        public bool HasSelectedIcon
        {
            get { return (bool)GetValue(HasSelectedIconProperty); }
            set { SetValue(HasSelectedIconProperty, value); }
        }
    }
}

