using Elmah.ModelContracts;
using Elmah.Resx.Resources;
using System.ComponentModel.DataAnnotations;

namespace Elmah.Models
{
    public partial class ElmahTypeModel : IElmahType
    {
        [Display(Name = "Type", ResourceType = typeof(UIStrings))]
        [StringLength(100, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_Type_should_be_1_to_100", MinimumLength = 1)]
        public string Type { get; set; } = null!;

    }
}

