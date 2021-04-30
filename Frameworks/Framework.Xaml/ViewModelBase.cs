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

    /// <summary>
    /// used by Swagger
    /// </summary>
    public abstract class ViewModelBase2 : ViewModelBase
    {
        #region 1. Properties

        private bool m_ShowSavingPopup;
        public bool ShowSavingPopup
        {
            get { return m_ShowSavingPopup; }
            set
            {
                Set(nameof(ShowSavingPopup), ref m_ShowSavingPopup, value);
            }
        }


        private bool m_ShowSuccessfullySavedPopup;
        public bool ShowSuccessfullySavedPopup
        {
            get { return m_ShowSuccessfullySavedPopup; }
            set
            {
                Set(nameof(ShowSuccessfullySavedPopup), ref m_ShowSuccessfullySavedPopup, value);
            }
        }

        private bool m_ShowSaveFailedPopup;
        public bool ShowSaveFailedPopup
        {
            get { return m_ShowSaveFailedPopup; }
            set
            {
                Set(nameof(ShowSaveFailedPopup), ref m_ShowSaveFailedPopup, value);
            }
        }

        private Framework.Xaml.ControlParentOptions m_ControlParentOption = ControlParentOptions.InPage;
        public Framework.Xaml.ControlParentOptions ControlParentOption
        {
            get { return m_ControlParentOption; }
            set
            {
                Set(nameof(ControlParentOption), ref m_ControlParentOption, value);
            }
        }

        private bool m_IsContentEnable = false;
        public bool IsContentEnable
        {
            get { return m_IsContentEnable; }
            set
            {
                Set(nameof(IsContentEnable), ref m_IsContentEnable, value);
            }
        }

        #endregion 1. Properties

        protected void PostAction(bool showBuiltInPopup = true, BuiltInPopupTypes builtInPopupType = BuiltInPopupTypes.Custom, string message = null, string highlightedMessage = null, string endMessage = null)
        {
            if (ShowSavingPopup)
                PopupVM.HidePopup();

            if (showBuiltInPopup)
                PopupVM.ShowBuiltInPopup(builtInPopupType, message, highlightedMessage, endMessage, null, true, false);
            else
                PopupVM.HideItemControlPopup();
        }

        public virtual string GetThisItemDisplayString()
        {
            return Framework.Resx.UIStringResource.ThisItem;
        }
    }
}

