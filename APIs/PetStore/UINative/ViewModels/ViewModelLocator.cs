/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:Elmah.PetStore.ViewModels"
                                   x:Key="LocatorPetStore" />
  </Application.Resources>

  In the View:
  DataContext="{Binding Source={StaticResource LocatorPetStore}, Path=ViewModelName}"

  OR (WPF only):

  xmlns:vm="clr-namespace:Elmah.PetStore.ViewModels"
  DataContext="{Binding Source={x:Static vm:ViewModelLocatorTemplate.ViewModelNameStatic}}"
*/
//using Elmah.PetStore.ViewModels;
using Xamarin.Forms;

namespace Elmah.PetStore.ViewModels
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// <para>
    /// Use the <strong>mvvmlocatorproperty</strong> snippet to add ViewModels
    /// to this locator.
    /// </para>
    /// <para>
    /// In Silverlight and WPF, place the ViewModelLocatorTemplate in the App.xaml resources:
    /// </para>
    /// <code>
    /// &lt;Application.Resources&gt;
    ///     &lt;vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:Elmah.PetStore.ViewModels"
    ///                                  x:Key="LocatorPetStore" /&gt;
    /// &lt;/Application.Resources&gt;
    /// </code>
    /// <para>
    /// Then use:
    /// </para>
    /// <code>
    /// DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
    /// </code>
    /// <para>
    /// You can also use Blend to do all this with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm/getstarted
    /// </para>
    /// <para>
    /// In <strong>*WPF only*</strong> (and if databinding in Blend is not relevant), you can delete
    /// the Main property and bind to the ViewModelNameStatic property instead:
    /// </para>
    /// <code>
    /// xmlns:vm="clr-namespace:Elmah.PetStore.ViewModels"
    /// DataContext="{Binding Source={x:Static vm:ViewModelLocatorTemplate.ViewModelNameStatic}}"
    /// </code>
    /// </summary>
    public partial class ViewModelLocator
    {
        private const bool ForceDesignData = false;

        public ViewModelLocator()
        {
            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view models
            ////}
            ////else
            ////{
            ////    // Create run time view models
            ////}
        }

        public static void _RegisterViewModels()
        {

            _RegisterViewModelsOfOrder();

            _RegisterViewModelsOfUser();

            _RegisterViewModelsOfPet();

        }

        static partial void _RegisterViewModelsOfOrder();

        static partial void _RegisterViewModelsOfUser();

        static partial void _RegisterViewModelsOfPet();

    }
}

