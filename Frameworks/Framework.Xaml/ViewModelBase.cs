using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

using System.Reflection;
using Xamarin.Forms;

namespace Framework.Xaml
{
    /// <summary>
    /// TODO: how to initialize validation
    /// </summary>
    public class _ViewModelBase : Framework.Models.PropertyChangedNotifier
    {
        public virtual void Cleanup()
        {
        }
    }

    /// <summary>
    /// TODO: how to initialize validation
    /// </summary>
    public class ViewModelBase : _ViewModelBase
    {
        private bool m_IsContentVisible = false;
        public bool IsContentVisible
        {
            get { return m_IsContentVisible; }
            set
            {
                Set(nameof(IsContentVisible), ref m_IsContentVisible, value);
            }
        }

        public Framework.Xaml.PopupVM PopupVM
        {
            get
            {
                return DependencyService.Resolve<Framework.Xaml.PopupVM>();
            }
        }
    }
}

