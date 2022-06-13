using Microsoft.Extensions.Localization;

namespace Elmah.Resx
{
    public class UIStrings : IUIStrings
    {
        private readonly IStringLocalizer<UIStrings> _localizer;
        public UIStrings(IStringLocalizer<UIStrings> localizer)
        {
            _localizer = localizer;
        }

        public string Get(string key)
        {
            return _localizer[key];
        }
    }
}

