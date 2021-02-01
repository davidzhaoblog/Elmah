using System.Collections.Generic;
namespace Elmah.ViewModelData
{
    public class OrderByLists
    {

        #region 1.1. ElmahModel.ELMAH_Error.Index

        public static List<Framework.Models.NameValuePair> ELMAH_Error_IndexVM_GetDefaultListOfQueryOrderBySettingCollecionInString()
        {
            List<Framework.Models.NameValuePair> list = new List<Framework.Models.NameValuePair>();
            list.Add(new Framework.Models.NameValuePair("ElmahApplication_Name~ASC", "ElmahApplication_Name A-Z"));
            list.Add(new Framework.Models.NameValuePair("ElmahApplication_Name~DESC", "ElmahApplication_Name Z-A"));
            return list;
        }

        #endregion 1.1. ElmahModel.ELMAH_Error.Index

        #region 2.1. ElmahModel.ElmahApplication.Index

        public static List<Framework.Models.NameValuePair> ElmahApplication_IndexVM_GetDefaultListOfQueryOrderBySettingCollecionInString()
        {
            List<Framework.Models.NameValuePair> list = new List<Framework.Models.NameValuePair>();
            list.Add(new Framework.Models.NameValuePair("Application~ASC", "Application A-Z"));
            list.Add(new Framework.Models.NameValuePair("Application~DESC", "Application Z-A"));
            return list;
        }

        #endregion 2.1. ElmahModel.ElmahApplication.Index

        #region 3.1. ElmahModel.ElmahHost.Index

        public static List<Framework.Models.NameValuePair> ElmahHost_IndexVM_GetDefaultListOfQueryOrderBySettingCollecionInString()
        {
            List<Framework.Models.NameValuePair> list = new List<Framework.Models.NameValuePair>();
            list.Add(new Framework.Models.NameValuePair("Host~ASC", "Host A-Z"));
            list.Add(new Framework.Models.NameValuePair("Host~DESC", "Host Z-A"));
            return list;
        }

        #endregion 3.1. ElmahModel.ElmahHost.Index

        #region 4.1. ElmahModel.ElmahSource.Index

        public static List<Framework.Models.NameValuePair> ElmahSource_IndexVM_GetDefaultListOfQueryOrderBySettingCollecionInString()
        {
            List<Framework.Models.NameValuePair> list = new List<Framework.Models.NameValuePair>();
            list.Add(new Framework.Models.NameValuePair("Source~ASC", "Source A-Z"));
            list.Add(new Framework.Models.NameValuePair("Source~DESC", "Source Z-A"));
            return list;
        }

        #endregion 4.1. ElmahModel.ElmahSource.Index

        #region 5.1. ElmahModel.ElmahStatusCode.Index

        public static List<Framework.Models.NameValuePair> ElmahStatusCode_IndexVM_GetDefaultListOfQueryOrderBySettingCollecionInString()
        {
            List<Framework.Models.NameValuePair> list = new List<Framework.Models.NameValuePair>();
            list.Add(new Framework.Models.NameValuePair("Name~ASC", "Name A-Z"));
            list.Add(new Framework.Models.NameValuePair("Name~DESC", "Name Z-A"));
            return list;
        }

        #endregion 5.1. ElmahModel.ElmahStatusCode.Index

        #region 6.1. ElmahModel.ElmahType.Index

        public static List<Framework.Models.NameValuePair> ElmahType_IndexVM_GetDefaultListOfQueryOrderBySettingCollecionInString()
        {
            List<Framework.Models.NameValuePair> list = new List<Framework.Models.NameValuePair>();
            list.Add(new Framework.Models.NameValuePair("Type~ASC", "Type A-Z"));
            list.Add(new Framework.Models.NameValuePair("Type~DESC", "Type Z-A"));
            return list;
        }

        #endregion 6.1. ElmahModel.ElmahType.Index

        #region 7.1. ElmahModel.ElmahUser.Index

        public static List<Framework.Models.NameValuePair> ElmahUser_IndexVM_GetDefaultListOfQueryOrderBySettingCollecionInString()
        {
            List<Framework.Models.NameValuePair> list = new List<Framework.Models.NameValuePair>();
            list.Add(new Framework.Models.NameValuePair("User~ASC", "User A-Z"));
            list.Add(new Framework.Models.NameValuePair("User~DESC", "User Z-A"));
            return list;
        }

        #endregion 7.1. ElmahModel.ElmahUser.Index

    }
}

