using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Framework.Weather
{
    public class LiteWebClient
    {
        public static async Task<T> Request<T>(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                try
                {
                    if (response != null)
                        {
                            string json = response.Content.ReadAsStringAsync().Result;
                            return JsonConvert.DeserializeObject<T>(json);
                        }
                } catch (Exception ex) {
                    Console.WriteLine("*debug: " + ex.ToString());
                }

                return (T)Convert.ChangeType(null, typeof(T));
            }
        }
    }
}

