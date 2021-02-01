using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elmah.CommonBLLEntities
{
    public partial class ElmahUserRequestMessageBuiltIn
        : Framework.Services.BusinessLogicLayerRequestMessageBase<List<Elmah.DataSourceEntities.ElmahUser>>
    {
        /*
        public ElmahUserRequestMessageBuiltIn()
            : base()
        {
            this.Criteria = new List<Elmah.DataSourceEntities.ElmahUser>();
        }

        public ElmahUserRequestMessageBuiltIn(
            Framework.Services.BusinessLogicLayerRequestTypes businessLogicLayerRequestTypes
            , string businessLogicLayerRequestTypeKey
            , string businessLogicLayerRequestID
            )
            : base(businessLogicLayerRequestTypes, businessLogicLayerRequestTypeKey, businessLogicLayerRequestID)
        {
            this.Criteria = new List<Elmah.DataSourceEntities.ElmahUser>();
        }
        */
    }

    public partial class ElmahUserRequestMessageBuiltInOfIdentifier
        : Framework.Services.BusinessLogicLayerRequestMessageBase<Elmah.DataSourceEntities.ElmahUserIdentifier>
    {
        /*
        public ElmahUserRequestMessageBuiltInOfIdentifier()
            : base()
        {
        }

        public ElmahUserRequestMessageBuiltInOfIdentifier(
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

