using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elmah.ViewModelData.ElmahApplication
{
    public partial class IndexVM : Framework.ViewModels.ViewModelBase<Elmah.CommonBLLEntities.ElmahApplicationChainedQueryCriteriaCommon, List<Elmah.DataSourceEntities.ElmahApplication>>
    {
        public IndexVM()
            : base()
        {
        }

        public override List<Framework.Models.NameValuePair> GetDefaultListOfQueryOrderBySettingCollecionInString()
        {
            return Elmah.ViewModelData.OrderByLists.ElmahApplication_IndexVM_GetDefaultListOfQueryOrderBySettingCollecionInString();
        }
    }
}

