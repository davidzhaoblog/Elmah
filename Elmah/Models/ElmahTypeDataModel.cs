using Elmah.Resx.Resources;
using Framework.Models;
using System.ComponentModel.DataAnnotations;

namespace Elmah.Models
{
    public partial class ElmahTypeDataModel
    {
        public ItemUIStatus ItemUIStatus______ { get; set; } = ItemUIStatus.NoChange;
        public bool IsDeleted______ { get; set; } = false;

        [Display(Name = "Type", ResourceType = typeof(UIStrings))]
        [StringLength(100, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_Type_should_be_1_to_100", MinimumLength = 1)]
        public string Type { get; set; } = null!;

    }
}

