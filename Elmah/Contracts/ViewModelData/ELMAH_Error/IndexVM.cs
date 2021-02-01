using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elmah.ViewModelData.ELMAH_Error
{
    public partial class IndexVM : Framework.ViewModels.ViewModelBase<Elmah.CommonBLLEntities.ELMAH_ErrorChainedQueryCriteriaCommon, List<Elmah.DataSourceEntities.ELMAH_Error.Default>>
    {
        public IndexVM()
            : base()
        {
        }

            public List<Framework.Models.NameValuePair> NameValueCollectionOfElmahModel_ElmahApplication { get; set; }

            public List<Framework.Models.NameValuePair> NameValueCollectionOfElmahModel_ElmahHost { get; set; }

            public List<Framework.Models.NameValuePair> NameValueCollectionOfElmahModel_ElmahSource { get; set; }

            public List<Framework.Models.NameValuePair> NameValueCollectionOfElmahModel_ElmahStatusCode { get; set; }

            public List<Framework.Models.NameValuePair> NameValueCollectionOfElmahModel_ElmahType { get; set; }

            public List<Framework.Models.NameValuePair> NameValueCollectionOfElmahModel_ElmahUser { get; set; }

        public override List<Framework.Models.NameValuePair> GetDefaultListOfQueryOrderBySettingCollecionInString()
        {
            return Elmah.ViewModelData.OrderByLists.ELMAH_Error_IndexVM_GetDefaultListOfQueryOrderBySettingCollecionInString();
        }
    }
}

