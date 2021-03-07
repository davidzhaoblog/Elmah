using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Xamarin.Auth;
using Xamarin.Essentials;

namespace Framework.Xamariner.Helpers
{
    /// <summary>
    /// https://github.com/xamarin/Xamarin.Auth/wiki/Migrating-from-AccountStore-to-Xamarin.Essentials-SecureStorage
    /// </summary>
    public class SecureStorageThisSiteAccountStoreHelper
    {
        public static async Task SaveAsync(Framework.Xaml.SignInData1 account, string serviceId)
        {
            // Find existing accounts for the service
            var accounts = await FindAccountsForServiceAsync(serviceId);

            // Remove existing account with Id if exists
            accounts.RemoveAll(a => a.UserName == account.UserName);

            // Add account we are saving
            accounts.Add(account);

            // Serialize all the accounts to javascript
            var json = JsonConvert.SerializeObject(accounts);

            // Securely save the accounts for the given service
            await SecureStorage.SetAsync(serviceId, json);
        }
        public static async Task DeleteAsync(Framework.Xaml.SignInData1 account, string serviceId)
        {
            // Find existing accounts for the service
            var accounts = await FindAccountsForServiceAsync(serviceId);
            // Remove existing account with Id if exists
            accounts.RemoveAll(a => a.UserName == account.UserName);
        }

        public static async Task<List<Framework.Xaml.SignInData1>> FindAccountsForServiceAsync(string serviceId)
        {
            // Get the json for accounts for the service
            var json = await SecureStorage.GetAsync(serviceId);

            try
            {
                // Try to return deserialized list of accounts
                return JsonConvert.DeserializeObject<List<Framework.Xaml.SignInData1>>(json);
            }
            catch { }

            // If this fails, return an empty list
            return new List<Framework.Xaml.SignInData1>();
        }

        //public static async Task MigrateAllAccountsAsync(string serviceId, IEnumerable<Account> accountStoreAccounts)
        //{
        //    var wasMigrated = await SecureStorage.GetAsync("XamarinAuthAccountStoreMigrated");

        //    if (wasMigrated == "1")
        //        return;

        //    await SecureStorage.SetAsync("XamarinAuthAccountStoreMigrated", "1");

        //    // Just in case, look at existing 'new' accounts
        //    var accounts = await FindAccountsForServiceAsync(serviceId);

        //    foreach (var account in accountStoreAccounts)
        //    {

        //        // Check if the new storage already has this account
        //        // We don't want to overwrite it if it does
        //        if (accounts.Any(a => a.Username == account.Username))
        //            continue;

        //        // Add the account we are migrating
        //        accounts.Add(account);
        //    }

        //    // Serialize all the accounts to javascript
        //    var json = JsonConvert.SerializeObject(accounts);

        //    // Securely save the accounts for the given service
        //    await SecureStorage.SetAsync(serviceId, json);
        //}
    }
}

