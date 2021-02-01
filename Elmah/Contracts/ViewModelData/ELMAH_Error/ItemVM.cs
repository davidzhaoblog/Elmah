using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elmah.ViewModelData.ELMAH_Error
{
    public partial class ItemVM : Framework.ViewModels.ViewModelItemBase<Elmah.DataSourceEntities.ELMAH_ErrorIdentifier, Elmah.DataSourceEntities.ELMAH_Error.Default>
    {
            public List<Framework.Models.NameValuePair> NameValueCollectionOfElmahModel_ElmahApplication { get; set; }

            public List<Framework.Models.NameValuePair> NameValueCollectionOfElmahModel_ElmahHost { get; set; }

            public List<Framework.Models.NameValuePair> NameValueCollectionOfElmahModel_ElmahSource { get; set; }

            public List<Framework.Models.NameValuePair> NameValueCollectionOfElmahModel_ElmahStatusCode { get; set; }

            public List<Framework.Models.NameValuePair> NameValueCollectionOfElmahModel_ElmahType { get; set; }

            public List<Framework.Models.NameValuePair> NameValueCollectionOfElmahModel_ElmahUser { get; set; }

    }
}

