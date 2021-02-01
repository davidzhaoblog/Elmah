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
    public partial class ImageCapture : ContentView
    {
        protected Framework.Xaml.ImageCaptureVM ViewModel
        {
            get
            {
                return DependencyService.Resolve<Framework.Xaml.ImageCaptureVM>();
            }
        }

        public ImageCapture()
        {
            InitializeComponent();
            this.BindingContext = this.ViewModel;
        }
    }
}