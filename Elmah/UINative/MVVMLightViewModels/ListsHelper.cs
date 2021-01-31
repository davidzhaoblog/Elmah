using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Elmah.MVVMLightViewModels
{
    /// <summary>
    /// 1. ListsApiClient.GetAllCachedLists
    /// 2. CodeListCachingRepository Save to Cache per
    /// </summary>
    public class ListsHelper
    {
        public Elmah.MVVMLightViewModels.SQLiteFactory Caching
        {
            get
            {
                return DependencyService.Resolve<Elmah.MVVMLightViewModels.SQLiteFactory>();
            }
        }

        public async Task<List<Framework.Models.NameValuePair>> GetList(Elmah.EntityContracts.Enums.CacheLists tableName)
        {
            try
            {
                var cacheDataKey = BuildCacheDataKey(tableName.ToString());
                if(Framework.Xaml.ApplicationPropertiesHelper.HasTableCachingData(cacheDataKey))
                {
                    var cachingData = Framework.Xaml.ApplicationPropertiesHelper.GetTableCachingData(cacheDataKey);
                    if (!cachingData.ShouldRefresh)
                    {
                        var cachedList = await Caching.CodeListCachingRepository.GetItemsFromTableAsync(t => t.TableName == cachingData.TableName);
                        var retval = from t in cachedList
                                     select new Framework.Models.NameValuePair { Name = t.Name, Value = t.Value };
                        return retval.ToList();
                    }
                }

                var client = Elmah.MVVMLightViewModels.WebApiClientFactory.CreateListsApiClient();

                if(false)
                {
                }

            }
            catch //(Exception ex)
            {
            }

            return new List<Framework.Models.NameValuePair>();
        }

        public async Task GetAndCacheLists()
        {
            var client = Elmah.MVVMLightViewModels.WebApiClientFactory.CreateListsApiClient();
            var result = await client.GetAllCachedLists();
            if(result != null && result.Count > 0)
            {
                foreach(var listMessage in result)
                {
                    if(listMessage.Value.BusinessLogicLayerResponseStatus == Framework.Services.BusinessLogicLayerResponseStatus.MessageOK)
                    {
                        var cacheDataKey = BuildCacheDataKey(listMessage.Key.ToString());
                        var tableName = cacheDataKey;
                        await Caching.CodeListCachingRepository.DeleteItemFromTableAsync(t => t.TableName == tableName);
                        foreach (var item in listMessage.Value.Message)
                        {
                            var cacheItem = new Elmah.SQLite.TableModels.CodeListCaching { TableName = tableName, Name = item.Name, Value = item.Value };
                            await Caching.CodeListCachingRepository.InsertUpdateItemInTableAsync(t => (t.TableName == tableName && t.Value == item.Value), cacheItem);
                        }
                    }
                }

                var allCacheDataKey = BuildCacheDataKey(Elmah.EntityContracts.Enums.CacheLists.__ALL__.ToString());
                await Framework.Xaml.ApplicationPropertiesHelper.SetTableCachingData(allCacheDataKey, DateTime.Now, false);
            }
        }

        public string BuildCacheDataKey(string tableName)
        {
            return string.Format("NameValue_{0}", tableName);
        }
    }
}

