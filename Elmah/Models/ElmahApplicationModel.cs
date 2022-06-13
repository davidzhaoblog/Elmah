using Elmah.ModelContracts;
using Elmah.Resx.Resources;
using System.ComponentModel.DataAnnotations;

namespace Elmah.Models
{
    public partial class ElmahApplicationModel : IElmahApplication
    {
        [Display(Name = "Application", ResourceType = typeof(UIStrings))]
        [StringLength(60, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_Application_should_be_1_to_60", MinimumLength = 1)]
        public string Application { get; set; } = null!;

    }
}

