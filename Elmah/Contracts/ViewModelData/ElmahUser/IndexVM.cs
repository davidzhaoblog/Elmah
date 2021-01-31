using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elmah.ViewModelData.ElmahUser
{
    public partial class IndexVM : Framework.ViewModels.ViewModelBase<Elmah.CommonBLLEntities.ElmahUserChainedQueryCriteriaCommon, List<Elmah.DataSourceEntities.ElmahUser>>
    {
        public IndexVM()
            : base()
        {
        }

        public override List<Framework.Models.NameValuePair> GetDefaultListOfQueryOrderBySettingCollecionInString()
        {
            return Elmah.ViewModelData.OrderByLists.ElmahUser_IndexVM_GetDefaultListOfQueryOrderBySettingCollecionInString();
        }
    }
}

