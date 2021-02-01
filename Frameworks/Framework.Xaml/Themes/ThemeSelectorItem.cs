using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Framework.Xaml.Themes
{
    public class ThemeSelectorItem : Framework.Models.PropertyChangedNotifier
    {
        private Framework.Themes.Theme m_Theme;
        public Framework.Themes.Theme Theme
        {
            get { return m_Theme; }
            set
            {
                Set(nameof(Theme), ref m_Theme, value);
            }
        }

        private bool m_IsCurrent;
        public bool IsCurrent
        {
            get { return m_IsCurrent; }
            set
            {
                Set(nameof(IsCurrent), ref m_IsCurrent, value);
            }
        }

        private string m_Text;
        public string Text
        {
            get { return m_Text; }
            set
            {
                Set(nameof(Text), ref m_Text, value);
            }
        }

        private ResourceDictionary m_ResourceDictionary;
        public ResourceDictionary ResourceDictionary
        {
            get { return m_ResourceDictionary; }
            set
            {
                Set(nameof(ResourceDictionary), ref m_ResourceDictionary, value);
            }
        }
    }
}

