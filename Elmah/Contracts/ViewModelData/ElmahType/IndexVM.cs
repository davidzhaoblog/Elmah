using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elmah.ViewModelData.ElmahType
{
    public partial class IndexVM : Framework.ViewModels.ViewModelBase<Elmah.CommonBLLEntities.ElmahTypeChainedQueryCriteriaCommon, List<Elmah.DataSourceEntities.ElmahType>>
    {
        public IndexVM()
            : base()
        {
        }

        public override List<Framework.Models.NameValuePair> GetDefaultListOfQueryOrderBySettingCollecionInString()
        {
            return Elmah.ViewModelData.OrderByLists.ElmahType_IndexVM_GetDefaultListOfQueryOrderBySettingCollecionInString();
        }
    }
}

