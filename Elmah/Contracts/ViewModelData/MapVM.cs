using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elmah.ViewModelData
{
    public partial class MapVM : Framework.ViewModels.ViewModelBase<Elmah.CommonBLLEntities.GeoSearchCriteria, Elmah.DataSourceEntities.MapItemCollection>
    {
        public MapVM()
            : base()
        {
        }

        public override List<Framework.Models.NameValuePair> GetDefaultListOfQueryOrderBySettingCollecionInString()
        {
            return new List<Framework.Models.NameValuePair>();
        }
    }
}

