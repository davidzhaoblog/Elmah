using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Models
{
    /// <summary>
    /// for deletion
    /// </summary>
    /// <typeparam name="TIdentifier"></typeparam>
    public class BatchActionViewModel<TIdentifier>
    {
        public List<TIdentifier> Ids { get; set; } = null!;
        // public HttpMethod? ActionType { get; set; }// = HttpMethod.Put; // Update
    }

    /// <summary>
    /// for update
    /// </summary>
    /// <typeparam name="TIdentifier"></typeparam>
    /// <typeparam name="TActionData"></typeparam>
    public class BatchActionViewModel<TIdentifier, TActionData>: BatchActionViewModel<TIdentifier>
    {
        public TActionData? ActionData { get; set; }
    }
}

