using Elmah.Resx.Resources;
using System.ComponentModel.DataAnnotations;

namespace Elmah.Models
{
    public partial class ElmahStatusCodeModel
    {
        [Display(Name = "StatusCode", ResourceType = typeof(UIStrings))]
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="StatusCode_is_required")]
        public int StatusCode { get; set; }

        [Display(Name = "Name", ResourceType = typeof(UIStrings))]
        [StringLength(50, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_Name_should_be_0_to_50")]
        public string? Name { get; set; }

    }
}

