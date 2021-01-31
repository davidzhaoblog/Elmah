using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Framework.Xaml.Themes
{
    public class ThemeSelectorVM : _ViewModelBase
    {
        private Framework.Xaml.Themes.ThemeSelectorItem m_LightTheme;
        public Framework.Xaml.Themes.ThemeSelectorItem LightTheme
        {
            get { return m_LightTheme; }
            set
            {
                Set(nameof(LightTheme), ref m_LightTheme, value);
            }
        }

        private Framework.Xaml.Themes.ThemeSelectorItem m_DarkTheme;
        public Framework.Xaml.Themes.ThemeSelectorItem DarkTheme
        {
            get { return m_DarkTheme; }
            set
            {
                Set(nameof(DarkTheme), ref m_DarkTheme, value);
            }
        }
        public ICommand Command_ThemeSelected { get; set; }

        public ThemeSelectorVM()
        {
        }

        public void Initialize(ResourceDictionary lightTheme, ResourceDictionary darkTheme)
        {
            var currentTheme = Framework.Xaml.ApplicationPropertiesHelper.GetTheme();
            LightTheme = new ThemeSelectorItem
            {
                Theme = Framework.Themes.Theme.Light
                 ,
                IsCurrent = currentTheme == Framework.Themes.Theme.Light
                 ,
                Text = Framework.Resx.UIStringResource.Light
                ,
                ResourceDictionary = lightTheme
            };
            DarkTheme = new ThemeSelectorItem
            {
                Theme = Framework.Themes.Theme.Dark
                 ,
                IsCurrent = currentTheme == Framework.Themes.Theme.Dark
                 ,
                Text = Framework.Resx.UIStringResource.Dark
                ,
                ResourceDictionary = darkTheme
            };

            Command_ThemeSelected = new Command<Framework.Themes.Theme>(async t =>
            {
                LightTheme.IsCurrent = t == LightTheme.Theme;
                DarkTheme.IsCurrent = t == DarkTheme.Theme;

                await Framework.Xaml.ApplicationPropertiesHelper.SetTheme(t);
                SwitchTheme(t);
            });
        }

        public void SwitchTheme(Framework.Themes.Theme theme)
        {
            ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            if (mergedDictionaries != null)
            {
                mergedDictionaries.Clear();

                switch (theme)
                {
                    case Framework.Themes.Theme.Dark:
                        mergedDictionaries.Add(DarkTheme.ResourceDictionary);
                        break;
                    case Framework.Themes.Theme.Light:
                    default:
                        mergedDictionaries.Add(LightTheme.ResourceDictionary);
                        break;
                }
            }
        }
    }
}

