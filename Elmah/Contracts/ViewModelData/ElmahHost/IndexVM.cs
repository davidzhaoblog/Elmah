using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elmah.ViewModelData.ElmahHost
{
    public partial class IndexVM : Framework.ViewModels.ViewModelBase<Elmah.CommonBLLEntities.ElmahHostChainedQueryCriteriaCommon, List<Elmah.DataSourceEntities.ElmahHost>>
    {
        public IndexVM()
            : base()
        {
        }

        public override List<Framework.Models.NameValuePair> GetDefaultListOfQueryOrderBySettingCollecionInString()
        {
            return Elmah.ViewModelData.OrderByLists.ElmahHost_IndexVM_GetDefaultListOfQueryOrderBySettingCollecionInString();
        }
    }
}

