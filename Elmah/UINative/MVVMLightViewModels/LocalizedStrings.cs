namespace Elmah.MVVMLightViewModels
{
    public class LocalizedStrings
    {
        public LocalizedStrings()
        {
        }

        private static Framework.Resx.UIStringResource m_Framework_Resx_UIStringResource = Framework.Resx.ResourceFileFactory.GetUIStringResourceInstance();

        public Framework.Resx.UIStringResource Framework_Resx_UIStringResource { get { return m_Framework_Resx_UIStringResource; } }

        private static Elmah.Resx.UIStringResourcePerApp m_Elmah_Resx_UIStringResourcePerApp = Elmah.Resx.ResourceFileFactory.GetUIStringResourcePerAppInstance();

        public Elmah.Resx.UIStringResourcePerApp Elmah_Resx_UIStringResourcePerApp { get { return m_Elmah_Resx_UIStringResourcePerApp; } }

        private static Elmah.Resx.UIStringResourcePerEntity m_Elmah_Resx_UIStringResourcePerEntity = Elmah.Resx.ResourceFileFactory.GetUIStringResourcePerEntityInstance();

        public Elmah.Resx.UIStringResourcePerEntity Elmah_Resx_UIStringResourcePerEntity { get { return m_Elmah_Resx_UIStringResourcePerEntity; } }
    }
}

