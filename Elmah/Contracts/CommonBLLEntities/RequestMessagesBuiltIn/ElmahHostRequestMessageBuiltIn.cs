using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elmah.CommonBLLEntities
{
    public partial class ElmahHostRequestMessageBuiltIn
        : Framework.Services.BusinessLogicLayerRequestMessageBase<List<Elmah.DataSourceEntities.ElmahHost>>
    {
        /*
        public ElmahHostRequestMessageBuiltIn()
            : base()
        {
            this.Criteria = new List<Elmah.DataSourceEntities.ElmahHost>();
        }

        public ElmahHostRequestMessageBuiltIn(
            Framework.Services.BusinessLogicLayerRequestTypes businessLogicLayerRequestTypes
            , string businessLogicLayerRequestTypeKey
            , string businessLogicLayerRequestID
            )
            : base(businessLogicLayerRequestTypes, businessLogicLayerRequestTypeKey, businessLogicLayerRequestID)
        {
            this.Criteria = new List<Elmah.DataSourceEntities.ElmahHost>();
        }
        */
    }

    public partial class ElmahHostRequestMessageBuiltInOfIdentifier
        : Framework.Services.BusinessLogicLayerRequestMessageBase<Elmah.DataSourceEntities.ElmahHostIdentifier>
    {
        /*
        public ElmahHostRequestMessageBuiltInOfIdentifier()
            : base()
        {
        }

        public ElmahHostRequestMessageBuiltInOfIdentifier(
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

