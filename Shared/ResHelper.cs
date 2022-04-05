using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Simple_Face_Recognition_App.Shared
{
    public static class ResHelper
    {
        private static readonly string baseURL = ConfigurationManager.AppSettings.GetValues("baseURL").FirstOrDefault();
        public static async Task<string> GetALL(string url)
        {
            using(HttpClient client = new HttpClient())
            {
                using(HttpResponseMessage res = await client.GetAsync(baseURL+ url))
                {
                    using(HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        if (data != null)
                        {
                            return data;
                        }
                    }

                }
            }
            return string.Empty;
        }
        public static async Task<string> Get(string url, string id)
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.GetAsync(baseURL + url + id))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        if (data != null)
                        {
                            return data;
                        }
                    }

                }
            }
            return string.Empty;
        }

        #region[Init FormUrlContent]
        /**var inputData = new Dictionary<string, string>
        {
            {"name", name},
            {"job", job }
        };
        var input = new FormUrlEncodedContent(inputData);
        **/
        #endregion

        #region[Init DataJson]
        // var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
         
        #endregion
        public static async Task<string> Post(string url, string inputData)
        {
            using (HttpClient client = new HttpClient())
            {
                var input = new StringContent(inputData.ToString(), Encoding.UTF8, "application/json");
                using (HttpResponseMessage res = await client.PostAsync(baseURL + url, input))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        if (data != null)
                        {
                            return data;
                        }
                    }

                }
            }
            return string.Empty;
        }
        public static async Task<string> Put(string url, string id, string inputData)
        {
            using (HttpClient client = new HttpClient())
            {
                var input = new StringContent(inputData, Encoding.UTF8, "application/json");
                using (HttpResponseMessage res = await client.PutAsync(baseURL + url +id, input))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        if (data != null)
                        {
                            return data;
                        }
                    }

                }
            }
            return string.Empty;
        }
        public static async Task<string> Delete(string url, string id)
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage res = await client.DeleteAsync(baseURL + url + id))
                {
                    using (HttpContent content = res.Content)
                    {
                        string data = await content.ReadAsStringAsync();
                        if (data != null)
                        {
                            return data;
                        }
                    }

                }
            }
            return string.Empty;
        }
        public static string beautifyJson(string jsonStr)
        {
            JToken parseJson = JToken.Parse(jsonStr);
            return parseJson.ToString(Formatting.Indented);
        }
    }
}
