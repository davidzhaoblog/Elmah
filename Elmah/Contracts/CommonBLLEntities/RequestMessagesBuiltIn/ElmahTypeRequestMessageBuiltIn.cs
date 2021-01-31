using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elmah.CommonBLLEntities
{
    public partial class ElmahTypeRequestMessageBuiltIn
        : Framework.Services.BusinessLogicLayerRequestMessageBase<List<Elmah.DataSourceEntities.ElmahType>>
    {
        /*
        public ElmahTypeRequestMessageBuiltIn()
            : base()
        {
            this.Criteria = new List<Elmah.DataSourceEntities.ElmahType>();
        }

        public ElmahTypeRequestMessageBuiltIn(
            Framework.Services.BusinessLogicLayerRequestTypes businessLogicLayerRequestTypes
            , string businessLogicLayerRequestTypeKey
            , string businessLogicLayerRequestID
            )
            : base(businessLogicLayerRequestTypes, businessLogicLayerRequestTypeKey, businessLogicLayerRequestID)
        {
            this.Criteria = new List<Elmah.DataSourceEntities.ElmahType>();
        }
        */
    }

    public partial class ElmahTypeRequestMessageBuiltInOfIdentifier
        : Framework.Services.BusinessLogicLayerRequestMessageBase<Elmah.DataSourceEntities.ElmahTypeIdentifier>
    {
        /*
        public ElmahTypeRequestMessageBuiltInOfIdentifier()
            : base()
        {
        }

        public ElmahTypeRequestMessageBuiltInOfIdentifier(
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

