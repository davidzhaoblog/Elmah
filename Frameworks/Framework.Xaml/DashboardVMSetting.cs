using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Xaml
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TOptions">from xyz.ViewModels.{table name}.DashboardDataOptions</typeparam>
    public class DashboardVMSetting<TOptions>: _ViewModelBase
    {
        private TOptions m_Option;
        public TOptions Option
        {
            get { return m_Option; }
            set
            {
                Set(nameof(Option), ref m_Option, value);
            }
        }

        private CachingOptions m_CachingOption;
        /// <summary>
        /// will decide which <typeparamref name="TOptions"/> will be loaded when the Dashboard is loading
        /// 1. NoCaching,
        /// 1.1. will refresh when the Dashboard is loading
        /// 1.2. the binding path must be in the Dashboard.
        /// 2. AlwayCaching,
        /// 2.1. will load at very the application is lauching, will refresh when the Dashboard is loading
        /// 2.1.1. very few
        /// 2.2. the binding path must be in the Dashboard.
        /// 3. UpdateCacheWhenMasterLoaded,
        /// 3.1. will refresh when the Dashboard is loading
        /// 3.1.1. most
        /// 3.2. binding is an individual VM in Locator.
        /// 4. UpdateCacheWhenViewLoaded,
        /// 4.1. will not load/refresh when the Dashboard is loading
        /// 4.1.1. will refresh when the view model is loading
        /// </summary>
        public CachingOptions CachingOption
        {
            get { return m_CachingOption; }
            set
            {
                Set(nameof(CachingOption), ref m_CachingOption, value);
            }
        }

        private bool m_IsRequired = false;
        /// <summary>
        /// must in wizard
        /// </summary>
        public bool IsRequired
        {
            get { return m_IsRequired; }
            set
            {
                Set(nameof(IsRequired), ref m_IsRequired, value);
            }
        }

        private int m_OrderInWizard;
        public int OrderInWizard
        {
            get { return m_OrderInWizard; }
            set
            {
                Set(nameof(OrderInWizard), ref m_OrderInWizard, value);
            }
        }

        private int m_OrderInNavigation;
        public int OrderInNavigation
        {
            get { return m_OrderInNavigation; }
            set
            {
                Set(nameof(OrderInNavigation), ref m_OrderInNavigation, value);
            }
        }

        private bool m_IsATab = false;
        /// <summary>
        /// this option will be a separate tab in view
        /// will reference to CommonResultView control in view
        /// </summary>
        public bool IsATab
        {
            get { return m_IsATab; }
            set
            {
                Set(nameof(IsATab), ref m_IsATab, value);
            }
        }

        private bool m_IsOutstanding = false;
        /// <summary>
        /// there will be a separate view for this and list.
        /// outstanding items/aggragates result will be in Today tab.
        /// e.g. employee status changed, modifieddate
        /// e.g. not finished tasks, wizard?
        /// </summary>
        public bool IsOutstanding
        {
            get { return m_IsOutstanding; }
            set
            {
                Set(nameof(IsOutstanding), ref m_IsOutstanding, value);
            }
        }
    }
}

