using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Framework.Xaml
{
    public class AppShellVM : Framework.Xaml.ViewModelBase
    {
        private bool m_ShellNavBarIsVisible = true;
        public bool ShellNavBarIsVisible
        {
            get { return m_ShellNavBarIsVisible; }
            set
            {
                Set(nameof(ShellNavBarIsVisible), ref m_ShellNavBarIsVisible, value);
            }
        }

        //private bool m_ShellTabBarIsVisible;
        //public bool ShellTabBarIsVisible
        //{
        //    get { return m_ShellTabBarIsVisible; }
        //    set
        //    {
        //        Set(nameof(ShellTabBarIsVisible), ref m_ShellTabBarIsVisible, value);
        //    }
        //}

        public AppShellVM()
        {
        }

        public void SetShellNavBarIsVisible(bool show)
        {
            ShellNavBarIsVisible = show;
        }

        //public void SetShellTabBarIsVisible(bool show)
        //{
        //    ShellTabBarIsVisible = show;
        //}

        public override void Cleanup()
        {
            //SetShellNavBarIsVisible(false);
        }
    }
}

