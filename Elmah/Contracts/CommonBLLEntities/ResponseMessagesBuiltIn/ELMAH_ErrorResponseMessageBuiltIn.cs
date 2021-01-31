using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Elmah.CommonBLLEntities
{
    /// <summary>
    /// BusinessLogicLayerResponseMessage of entity ElmahModel.ELMAH_Error
    /// </summary>
    public partial class ELMAH_ErrorResponseMessageBuiltIn
        : Framework.Services.BusinessLogicLayerResponseMessageBase<List<Elmah.DataSourceEntities.ELMAH_Error>>
    {

        /// <summary>
        /// BusinessLogicLayerResponseMessage of of view Default of entity ElmahModel.ELMAH_Error
        /// </summary>
        public partial class Default
            : Framework.Services.BusinessLogicLayerResponseMessageBase<List<Elmah.DataSourceEntities.ELMAH_Error.Default>>
        {
        }

    }
}

