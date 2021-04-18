using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.CommonBLLEntities
{
    /// <summary>
    ///  is a wrapper class which  includes / should include global parameters and approaches on how to create data access layer instances.
    ///  e.g. membership, and data access layer factory class instance.
    /// </summary>
    //[Framework.BusinessLogicLayerContextSetting("Elmah", typeof(BusinessLogicLayerContext), typeof(Elmah.DALContracts.DataAccessLayerFactoryContract))]
    public partial class BusinessLogicLayerContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessLogicLayerContext"/> class.
        /// </summary>
        /// <param name="businessLogicLayerMemberShip">The business logic layer member ship.</param>
        // <param name="dataAccessLayerFactory">The data access layer factory.</param>
        public BusinessLogicLayerContext(
            Framework.CommonBLLEntities.BusinessLogicLayerMemberShip businessLogicLayerMemberShip
            //, Elmah.DALContracts.DataAccessLayerFactoryContract dataAccessLayerFactory
            )
        {
            this.BusinessLogicLayerMemberShip = businessLogicLayerMemberShip;
            //this.DataAccessLayerFactory = dataAccessLayerFactory;
        }

        /// <summary>
        /// Gets or sets the business logic layer member ship.
        /// </summary>
        /// <value>
        /// The business logic layer member ship.
        /// </value>
        public Framework.CommonBLLEntities.BusinessLogicLayerMemberShip BusinessLogicLayerMemberShip { get; set; }

        public Microsoft.Spatial.GeographyPoint CurrentLocation { get; set; }

        ///// <summary>
        ///// Gets or sets the data access layer factory.
        ///// </summary>
        ///// <value>
        ///// The data access layer factory.
        ///// </value>
        //public Elmah.DALContracts.DataAccessLayerFactoryContract DataAccessLayerFactory { get; set; }
    }
}

