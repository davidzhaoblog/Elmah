using Elmah.Resx.Resources;
using Framework.Models;
using System.ComponentModel.DataAnnotations;

namespace Elmah.Models
{
    public partial class ElmahHostModel
    {
        public ItemUIStatus ItemUIStatus______ { get; set; } = ItemUIStatus.NoChange;
        public bool IsDeleted______ { get; set; } = false;

        [Display(Name = "Host", ResourceType = typeof(UIStrings))]
        [StringLength(50, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_Host_should_be_1_to_50", MinimumLength = 1)]
        public string Host { get; set; } = null!;

        [Display(Name = "SpatialLocation", ResourceType = typeof(UIStrings))]
        public Microsoft.Spatial.Geography? SpatialLocation { get; set; }

    }
}

