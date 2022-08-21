using Elmah.Resx.Resources;
using Framework.Models;
using System.ComponentModel.DataAnnotations;

namespace Elmah.Models
{
    public partial class ElmahUserDataModel
    {
        public ItemUIStatus ItemUIStatus______ { get; set; } = ItemUIStatus.NoChange;
        public bool IsDeleted______ { get; set; } = false;

        [Display(Name = "User", ResourceType = typeof(UIStrings))]
        [StringLength(50, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_User_should_be_1_to_50", MinimumLength = 1)]
        public string User { get; set; } = null!;

    }
}

