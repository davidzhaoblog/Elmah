using System.ComponentModel.DataAnnotations;

namespace Framework.Themes
{
    public enum Theme
    {
        [Display(Name = "Light", ResourceType = typeof(Framework.Resx.UIStringResource))]
        Light,
        [Display(Name = "Dark", ResourceType = typeof(Framework.Resx.UIStringResource))]
        Dark
    }
}

