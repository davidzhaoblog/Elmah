using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;
using System.Threading.Tasks;

namespace Framework.Repositories
{
    /// <summary>
    ///  base interface of all DataAccessLayerContract, which defines common methods.
    /// </summary>
    /// <typeparam name="TCollection">The type of the collection of Type T.</typeparam>
    /// <typeparam name="T">entity class</typeparam>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    /// <typeparam name="TCollectionMessage">The type of the collection message.</typeparam>
    public interface DataAccessLayerContractBase<TCollection, T, TIdentifier, TCollectionMessage>
//        where TCollection : IList<T>, new()
//        where T : TIdentifier, new()
//        where TIdentifier: new()
    {
        /// <summary>
        /// Insert/Update the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        Task<TCollectionMessage> Upsert(T input);

        ///// <summary>
        ///// Inserts the specified input.
        ///// </summary>
        ///// <param name="input">The input.</param>
        //Task<TCollectionMessage> Insert(T input);
        ///// <summary>
        ///// Updates the specified input.
        ///// </summary>
        ///// <param name="input">The input.</param>
        //Task<TCollectionMessage> Update(T input);

        /// <summary>
        /// Deletes the specified input.
        /// </summary>
        /// <param name="input">The input.</param>
        Task<TCollectionMessage> Delete(T input);
        /// <summary>
        /// Deletes the by identifier.
        /// </summary>
        /// <param name="id">The id.</param>
        Task<TCollectionMessage> DeleteByIdentifier(TIdentifier id);

/*
        /// <summary>
        /// Batches the insert.
        /// </summary>
        /// <param name="input">The input.</param>
        Task<TCollectionMessage> BatchInsert(TCollection input);
        /// <summary>
        /// Batches the delete.
        /// </summary>
        /// <param name="input">The input.</param>
        Task<TCollectionMessage> BatchDelete(TCollection input);
        /// <summary>
        /// Batches the update.
        /// </summary>
        /// <param name="input">The input.</param>
        Task<TCollectionMessage> BatchUpdate(TCollection input);
*/

//        TCollection GetAll();
//        int GetCountOfAll();
//        T GetByIdentifier(TIdentifier id);
    }
}

