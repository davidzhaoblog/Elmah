using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elmah.CommonBLLEntities
{
    public partial class ElmahApplicationRequestMessageBuiltIn
        : Framework.Services.BusinessLogicLayerRequestMessageBase<List<Elmah.DataSourceEntities.ElmahApplication>>
    {
        /*
        public ElmahApplicationRequestMessageBuiltIn()
            : base()
        {
            this.Criteria = new List<Elmah.DataSourceEntities.ElmahApplication>();
        }

        public ElmahApplicationRequestMessageBuiltIn(
            Framework.Services.BusinessLogicLayerRequestTypes businessLogicLayerRequestTypes
            , string businessLogicLayerRequestTypeKey
            , string businessLogicLayerRequestID
            )
            : base(businessLogicLayerRequestTypes, businessLogicLayerRequestTypeKey, businessLogicLayerRequestID)
        {
            this.Criteria = new List<Elmah.DataSourceEntities.ElmahApplication>();
        }
        */
    }

    public partial class ElmahApplicationRequestMessageBuiltInOfIdentifier
        : Framework.Services.BusinessLogicLayerRequestMessageBase<Elmah.DataSourceEntities.ElmahApplicationIdentifier>
    {
        /*
        public ElmahApplicationRequestMessageBuiltInOfIdentifier()
            : base()
        {
        }

        public ElmahApplicationRequestMessageBuiltInOfIdentifier(
            Framework.Services.BusinessLogicLayerRequestTypes businessLogicLayerRequestTypes
            , string businessLogicLayerRequestTypeKey
            , string businessLogicLayerRequestID
            )
            : base(businessLogicLayerRequestTypes, businessLogicLayerRequestTypeKey, businessLogicLayerRequestID)
        {
        }
        */
    }
}

