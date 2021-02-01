using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elmah.ViewModelData.ElmahSource
{
    public partial class IndexVM : Framework.ViewModels.ViewModelBase<Elmah.CommonBLLEntities.ElmahSourceChainedQueryCriteriaCommon, List<Elmah.DataSourceEntities.ElmahSource>>
    {
        public IndexVM()
            : base()
        {
        }

        public override List<Framework.Models.NameValuePair> GetDefaultListOfQueryOrderBySettingCollecionInString()
        {
            return Elmah.ViewModelData.OrderByLists.ElmahSource_IndexVM_GetDefaultListOfQueryOrderBySettingCollecionInString();
        }
    }
}

