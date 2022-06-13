using Elmah.ModelContracts;
using Elmah.Resx.Resources;
using System.ComponentModel.DataAnnotations;

namespace Elmah.Models
{
    public partial class ElmahUserModel : IElmahUser
    {
        [Display(Name = "User", ResourceType = typeof(UIStrings))]
        [StringLength(50, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_User_should_be_1_to_50", MinimumLength = 1)]
        public string User { get; set; } = null!;

    }
}

