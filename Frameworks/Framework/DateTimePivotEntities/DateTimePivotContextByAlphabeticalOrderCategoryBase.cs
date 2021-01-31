using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.DateTimePivotEntities
{
    /// <summary>
    /// DateTimePivotContext By AlphabeticalOrderCategory
    /// </summary>
    /// <typeparam name="TCollection">The type of the collection.</typeparam>
    /// <typeparam name="T">entity type</typeparam>
    /// <typeparam name="TSubSummary">The type of the sub summary.</typeparam>
    /// <typeparam name="TSummary">The type of the summary.</typeparam>
    public class DateTimePivotContextByAlphabeticalOrderCategoryBase<TCollection, T, TSubSummary, TSummary>
        : Framework.DateTimePivotEntities.DateTimePivotContextBase<TCollection, T, Framework.Queries.AlphabeticalOrderCategoryCollection, Framework.Queries.AlphabeticalOrderCategory, TSubSummary, TSummary>
        where TCollection : List<T>, new()
        where T : new()
        where TSubSummary : class, new()
        where TSummary : class, new()
    {
        #region constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimePivotContextByAlphabeticalOrderCategoryBase&lt;TCollection, T, TSubSummary, TSummary&gt;"/> class.
        /// </summary>
        public DateTimePivotContextByAlphabeticalOrderCategoryBase()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DateTimePivotContextByAlphabeticalOrderCategoryBase&lt;TCollection, T, TSubSummary, TSummary&gt;"/> class.
        /// </summary>
        /// <param name="categoryCollection">The category collection.</param>
        /// <param name="entityCollection">The entity collection.</param>
        /// <param name="criteria">The criteria.</param>
        /// <param name="func_GetEntityCollectionOfOneCategory">The func_ get entity collection of one category.</param>
        /// <param name="func_CalculateSummary">The func_ calculate summary.</param>
        /// <param name="func_GetEntityCollectionOfOneCriteria">The func_ get entity collection of one criteria.</param>
        /// <param name="func_CalculateSubSummary">The func_ calculate sub summary.</param>
        public DateTimePivotContextByAlphabeticalOrderCategoryBase(
            Framework.Queries.AlphabeticalOrderCategoryCollection categoryCollection
            , TCollection entityCollection
            , Framework.Queries.QuerySystemDateTimeRangeCriteria criteria
            , Func<Framework.Queries.AlphabeticalOrderCategory, TCollection, TCollection> func_GetEntityCollectionOfOneCategory
            , Func<TCollection, TSummary> func_CalculateSummary
            , Func<Framework.Queries.QuerySystemDateTimeRangeCriteria, TCollection, TCollection> func_GetEntityCollectionOfOneCriteria
            , Func<TCollection, TSubSummary> func_CalculateSubSummary
            )
            : base(
                categoryCollection
                , entityCollection
                , criteria
                , func_GetEntityCollectionOfOneCategory
                , func_CalculateSummary
                , func_GetEntityCollectionOfOneCriteria
                , func_CalculateSubSummary)
        {
        }

        #endregion constructors
    }
}

