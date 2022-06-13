using Elmah.ModelContracts;
using Elmah.Resx.Resources;
using System.ComponentModel.DataAnnotations;

namespace Elmah.Models
{
    public partial class ElmahSourceModel : IElmahSource
    {
        [Display(Name = "Source", ResourceType = typeof(UIStrings))]
        [StringLength(60, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_Source_should_be_1_to_60", MinimumLength = 1)]
        public string Source { get; set; } = null!;

    }
}

