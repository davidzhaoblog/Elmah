using Elmah.Resx.Resources;
using Framework.Models;
using System.ComponentModel.DataAnnotations;

namespace Elmah.Models
{
    public partial class ElmahSourceDataModel
    {
        public ItemUIStatus ItemUIStatus______ { get; set; } = ItemUIStatus.NoChange;
        public bool IsDeleted______ { get; set; } = false;

        [Display(Name = "Source", ResourceType = typeof(UIStrings))]
        [StringLength(60, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_Source_should_be_1_to_60", MinimumLength = 1)]
        public string Source { get; set; } = null!;

    }
}

