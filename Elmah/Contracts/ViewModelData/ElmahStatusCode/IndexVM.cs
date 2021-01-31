using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elmah.ViewModelData.ElmahStatusCode
{
    public partial class IndexVM : Framework.ViewModels.ViewModelBase<Elmah.CommonBLLEntities.ElmahStatusCodeChainedQueryCriteriaCommon, List<Elmah.DataSourceEntities.ElmahStatusCode>>
    {
        public IndexVM()
            : base()
        {
        }

        public override List<Framework.Models.NameValuePair> GetDefaultListOfQueryOrderBySettingCollecionInString()
        {
            return Elmah.ViewModelData.OrderByLists.ElmahStatusCode_IndexVM_GetDefaultListOfQueryOrderBySettingCollecionInString();
        }
    }
}

