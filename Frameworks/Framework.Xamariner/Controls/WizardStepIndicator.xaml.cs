using Framework.Xaml;
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
    public partial class WizardStepIndicator : ContentView
    {
        //bool gridInitialized = false;

        public WizardStepIndicator()
        {
            InitializeComponent();
            BindingContextChanged += WizardStepIndicator_BindingContextChanged;
        }

        private void WizardStepIndicator_BindingContextChanged(object sender, EventArgs e)
        {
            var control = (WizardStepIndicator)sender;
            InitGrid(control);
            //if (!control.gridInitialized)
            //    InitGrid(control);
        }

        private static void InitGrid(WizardStepIndicator sender)
        {
            if (sender.BindingContext is ViewModelWizardBase)
            {
                if(sender.Container.Children.Any())
                    sender.Container.Children.Clear();
                if (sender.Container.ColumnDefinitions.Any())
                    sender.Container.ColumnDefinitions.Clear();

                var viewModel = (ViewModelWizardBase)sender.BindingContext;

                if (viewModel.WizardStepItems != null && viewModel.WizardStepItems.Count > 0)
                {
                    bool isFirst = true;
                    foreach (var wizardStepItem in viewModel.WizardStepItems)
                    {
                        if (!isFirst)
                        {
                            sender.Container.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                        }

                        sender.Container.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(30) });

                        Grid grid = new Grid
                        {
                            HorizontalOptions = LayoutOptions.FillAndExpand,
                            VerticalOptions = LayoutOptions.FillAndExpand,
                            BindingContext = wizardStepItem
                        };

                        // 1. inactive
                        {
                            int imageSize = 20;
                            var image = new Image
                            {
                                HorizontalOptions = LayoutOptions.Center
                                ,
                                VerticalOptions = LayoutOptions.Center
                                ,
                                HeightRequest = imageSize
                                ,
                                WidthRequest = imageSize
                                ,
                                BackgroundColor = sender.BackgroundColor
                                ,
                                Source = "ic_lens.png"
                            };
                            grid.Children.Add(image);
                        }

                        // 2. active image
                        {
                            int imageSize = 30;
                            var image = new Image
                            {
                                HorizontalOptions = LayoutOptions.Center
                                ,
                                VerticalOptions = LayoutOptions.Center
                                ,
                                HeightRequest = imageSize
                                ,
                                WidthRequest = imageSize
                                ,
                                BackgroundColor = sender.BackgroundColor
                                ,
                                BindingContext = wizardStepItem
                                ,
                                //Source = item.Mandatory ? "ic_lens_mandatory.png" : "ic_lens.png"
                                Source = "ic_lens.png"
                            };
                            image.SetBinding(Image.IsVisibleProperty, "Active", BindingMode.OneWay);
                            grid.Children.Add(image);
                        }

                        // 3. number label
                        {
                            Label label = new Label
                            {
                                Style = (Style)Application.Current.Resources["WizardIndicatorNumber"],
                                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label))
                            };
                            label.SetBinding(Label.TextProperty, "WizardStepIndex");
                            grid.Children.Add(label);
                        }

                        sender.Container.Children.Add(grid, viewModel.WizardStepItems.IndexOf(wizardStepItem) * 2, 0);
                        isFirst = false;
                    }
                }
            }
        }
    }
}