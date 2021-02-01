using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Services
{
    /// <summary>
    /// enumeration, Business Logic Layer Request Types
    /// </summary>
    public enum BusinessLogicLayerRequestTypes
    {
        /// <summary>
        /// Create an entity
        /// </summary>
        Create,
        /// <summary>
        /// Update an entity
        /// </summary>
        Update,
        /// <summary>
        /// Delete an entity
        /// </summary>
        Delete,

        /// <summary>
        /// Replace an entity
        /// </summary>
        Replace,
        /// <summary>
        /// Copy an entity
        /// </summary>
        Copy,

        /// <summary>
        /// Search an entity
        /// </summary>
        Search,

        /// <summary>
        /// Create an entity and all parent entities
        /// </summary>
        CreateWithParent,
        /// <summary>
        /// Update an entity and all parent entities
        /// </summary>
        UpdateWithParent,
        /// <summary>
        /// Delete an entity and all parent entities
        /// </summary>
        DeleteWithParent,

    }
}

