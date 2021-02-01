using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Framework.Xaml
{
    public class FontIconSettings: Framework.Models.PropertyChangedNotifier
    {
        #region Fields and Properties

        private string m_MasterFontIcon;
        public string MasterFontIcon
        {
            get { return m_MasterFontIcon; }
            set
            {
                Set(nameof(MasterFontIcon), ref m_MasterFontIcon, value);
            }
        }

        private string m_MasterFontIconFamily;
        public string MasterFontIconFamily
        {
            get { return m_MasterFontIconFamily; }
            set
            {
                Set(nameof(MasterFontIconFamily), ref m_MasterFontIconFamily, value);
            }
        }

        private double m_MasterFontIconSize = 30;
        public double MasterFontIconSize
        {
            get { return m_MasterFontIconSize; }
            set
            {
                Set(nameof(MasterFontIconSize), ref m_MasterFontIconSize, value);
            }
        }

        private string m_SubFontIcon;
        public string SubFontIcon
        {
            get { return m_SubFontIcon; }
            set
            {
                Set(nameof(SubFontIcon), ref m_SubFontIcon, value);
            }
        }

        private string m_SubFontIconFamily;
        public string SubFontIconFamily
        {
            get { return m_SubFontIconFamily; }
            set
            {
                Set(nameof(SubFontIconFamily), ref m_SubFontIconFamily, value);
            }
        }

        private double m_SubFontIconSize = 12;
        public double SubFontIconSize
        {
            get { return m_SubFontIconSize; }
            set
            {
                Set(nameof(SubFontIconSize), ref m_SubFontIconSize, value);
            }
        }

        private string m_InfoFontIcon;
        public string InfoFontIcon
        {
            get { return m_InfoFontIcon; }
            set
            {
                Set(nameof(InfoFontIcon), ref m_InfoFontIcon, value);
            }
        }

        private string m_InfoFontIconFamily;
        public string InfoFontIconFamily
        {
            get { return m_InfoFontIconFamily; }
            set
            {
                Set(nameof(InfoFontIconFamily), ref m_InfoFontIconFamily, value);
            }
        }

        private double m_InfoFontIconSize = 12;
        public double InfoFontIconSize
        {
            get { return m_InfoFontIconSize; }
            set
            {
                Set(nameof(InfoFontIconSize), ref m_InfoFontIconSize, value);
            }
        }

        private Framework.Models.Priorities m_InfoFontIconPriority;
        public Framework.Models.Priorities InfoFontIconPriority
        {
            get { return m_InfoFontIconPriority; }
            set
            {
                Set(nameof(InfoFontIconPriority), ref m_InfoFontIconPriority, value);
            }
        }

        #endregion Fields and Properties

        public FontIconSettings()
        {

        }

        public void SetInfoFontIcon(string infoFontIcon, string infoFontIconFamily, double infoFontIconSize, Framework.Models.Priorities infoFontIconPriority)
        {
            InfoFontIcon = infoFontIcon;
            InfoFontIconFamily = infoFontIconFamily;
            if(infoFontIconSize > 0)
                InfoFontIconSize = infoFontIconSize;
            InfoFontIconPriority = infoFontIconPriority;
        }
        public void SetInfoFontIconCanUpdate(string infoFontIcon, string infoFontIconFamily, double infoFontIconSize, Framework.Models.Priorities infoFontIconPriority, string messageTitle)
        {
            SetInfoFontIcon(infoFontIcon, infoFontIconFamily, infoFontIconSize, infoFontIconPriority);

            // TODO: Some where should
            MessagingCenter.Subscribe<FontIconSettings, InfoFontIconSettings>(this, messageTitle, (sender, param) =>
            {
                if (param != null)
                {
                    InfoFontIcon = param.InfoFontIcon;
                    InfoFontIconFamily = param.InfoFontIconFamily.ToString();
                    InfoFontIconSize = param.InfoFontIconSize;
                    InfoFontIconPriority = param.InfoFontIconPriority;
                }
            });
        }

        public static FontIconSettings ParseFontIconSettings(string label)
        {
            var split = label.Split(",".ToCharArray());
            string masterFontIcon = string.Empty;
            string masterFontIconFamily = string.Empty;
            string subFontIcon = string.Empty;
            string subFontIconFamily = string.Empty;

            if (split.Length >= 1)
            {
                var split2 = split[0].Split(":".ToCharArray());
                masterFontIcon = HexToUnicode(split2[1]).ToString();
                masterFontIconFamily = split2[0];
            }

            if (split.Length >= 2)
            {
                var split2 = split[1].Split(":".ToCharArray());
                subFontIcon = HexToUnicode(split2[1]).ToString();
                subFontIconFamily = split2[0];
            }

            var fontIconSettings = new Framework.Xaml.FontIconSettings
            {
                MasterFontIcon = masterFontIcon,
                MasterFontIconFamily = masterFontIconFamily,
                SubFontIcon = subFontIcon,
                SubFontIconFamily = subFontIconFamily,
            };
            return fontIconSettings;
        }

        public static char HexToUnicode(string hex)
        {
            byte[] bytes = HexToBytes(hex);
            char c;
            if (bytes.Length == 1)
            {
                c = (char)bytes[0];
            }
            else if (bytes.Length == 2)
            {
                c = (char)((bytes[0] << 8) + bytes[1]);
            }
            else
            {
                throw new Exception(hex);
            }

            return c;
        }

        public static byte[] HexToBytes(string hex)
        {
            hex = hex.Trim();

            byte[] bytes = new byte[hex.Length / 2];

            for (int index = 0; index < bytes.Length; index++)
            {
                bytes[index] = byte.Parse(hex.Substring(index * 2, 2), NumberStyles.HexNumber);
            }

            return bytes;
        }
    }

    public class InfoFontIconSettings
    {
        public string InfoFontIcon { get; set; }
        public IconFontFamily InfoFontIconFamily { get; set; }
        public double InfoFontIconSize { get; set; }
        public Framework.Models.Priorities InfoFontIconPriority { get; set; }
    }
}

