using System;

namespace Framework.Xaml
{
    public enum CachingOptions
    {
        /// <summary>
        /// always from server
        /// </summary>
        NoCaching,
        /// <summary>
        /// will update cache(SQLite for now) when application loaded
        /// </summary>
        AlwayCaching,
        /// <summary>
        /// will update cache(SQLite for now) when Master entity loaded, will clear caching when exits master view
        /// </summary>
        UpdateCacheWhenMasterLoaded,
        /// <summary>
        /// will update cache(SQLite for now) when view loaded, will clear caching when exits current view.
        /// </summary>
        UpdateCacheWhenViewLoaded,
    }
}

