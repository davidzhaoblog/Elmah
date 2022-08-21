using Elmah.Resx.Resources;
using Framework.Models;
using System.ComponentModel.DataAnnotations;

namespace Elmah.Models
{
    public partial class ElmahErrorDataModel
    {
        public ItemUIStatus ItemUIStatus______ { get; set; } = ItemUIStatus.NoChange;
        public bool IsDeleted______ { get; set; } = false;

        [Display(Name = "ErrorId", ResourceType = typeof(UIStrings))]
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="ErrorId_is_required")]
        public System.Guid ErrorId { get; set; }

        [Display(Name = "ElmahApplication", ResourceType = typeof(UIStrings))]
        [StringLength(60, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_Application_should_be_1_to_60", MinimumLength = 1)]
        public string Application { get; set; } = null!;

        [Display(Name = "ElmahHost", ResourceType = typeof(UIStrings))]
        [StringLength(50, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_Host_should_be_1_to_50", MinimumLength = 1)]
        public string Host { get; set; } = null!;

        [Display(Name = "ElmahType", ResourceType = typeof(UIStrings))]
        [StringLength(100, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_Type_should_be_1_to_100", MinimumLength = 1)]
        public string Type { get; set; } = null!;

        [Display(Name = "ElmahSource", ResourceType = typeof(UIStrings))]
        [StringLength(60, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_Source_should_be_1_to_60", MinimumLength = 1)]
        public string Source { get; set; } = null!;

        [Display(Name = "Message", ResourceType = typeof(UIStrings))]
        [StringLength(500, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_Message_should_be_1_to_500", MinimumLength = 1)]
        public string Message { get; set; } = null!;

        [Display(Name = "ElmahUser", ResourceType = typeof(UIStrings))]
        [StringLength(50, ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="The_length_of_User_should_be_1_to_50", MinimumLength = 1)]
        public string User { get; set; } = null!;

        [Display(Name = "ElmahStatusCode", ResourceType = typeof(UIStrings))]
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="StatusCode_is_required")]
        public int StatusCode { get; set; }

        [Display(Name = "TimeUtc", ResourceType = typeof(UIStrings))]
        [DataType(DataType.DateTime)]
        [Required(ErrorMessageResourceType = typeof(UIStrings), ErrorMessageResourceName="TimeUtc_is_required")]
        public System.DateTime TimeUtc { get; set; }

        [Display(Name = "Sequence", ResourceType = typeof(UIStrings))]
        public int Sequence { get; set; }

        [Display(Name = "AllXml", ResourceType = typeof(UIStrings))]
        public string AllXml { get; set; } = null!;

        public partial class DefaultView: ElmahErrorDataModel
        {
            [Display(Name = "Application", ResourceType = typeof(UIStrings))]
            public string? Application_Name { get; set; }

            [Display(Name = "Host", ResourceType = typeof(UIStrings))]
            public string? Host_Name { get; set; }

            [Display(Name = "Source", ResourceType = typeof(UIStrings))]
            public string? Source_Name { get; set; }

            [Display(Name = "ElmahStatusCode", ResourceType = typeof(UIStrings))]
            public string? StatusCode_Name { get; set; }

            [Display(Name = "Type", ResourceType = typeof(UIStrings))]
            public string? Type_Name { get; set; }

            [Display(Name = "User", ResourceType = typeof(UIStrings))]
            public string? User_Name { get; set; }
        }

    }
}

