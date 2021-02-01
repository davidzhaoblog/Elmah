using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elmah.CommonBLLEntities
{
    public partial class ELMAH_ErrorRequestMessageBuiltIn
        : Framework.Services.BusinessLogicLayerRequestMessageBase<List<Elmah.DataSourceEntities.ELMAH_Error>>
    {
        /*
        public ELMAH_ErrorRequestMessageBuiltIn()
            : base()
        {
            this.Criteria = new List<Elmah.DataSourceEntities.ELMAH_Error>();
        }

        public ELMAH_ErrorRequestMessageBuiltIn(
            Framework.Services.BusinessLogicLayerRequestTypes businessLogicLayerRequestTypes
            , string businessLogicLayerRequestTypeKey
            , string businessLogicLayerRequestID
            )
            : base(businessLogicLayerRequestTypes, businessLogicLayerRequestTypeKey, businessLogicLayerRequestID)
        {
            this.Criteria = new List<Elmah.DataSourceEntities.ELMAH_Error>();
        }
        */
    }

    public partial class ELMAH_ErrorRequestMessageBuiltInOfIdentifier
        : Framework.Services.BusinessLogicLayerRequestMessageBase<Elmah.DataSourceEntities.ELMAH_ErrorIdentifier>
    {
        /*
        public ELMAH_ErrorRequestMessageBuiltInOfIdentifier()
            : base()
        {
        }

        public ELMAH_ErrorRequestMessageBuiltInOfIdentifier(
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

