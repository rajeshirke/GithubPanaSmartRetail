using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Retail.Infrastructure.Hepler;
using Newtonsoft.Json;
using Retail.Infrastructure.RequestModels;
using System.Globalization;

namespace Retail.Infrastructure.DataServices
{
    public static class WebServiceUtils<T>
    {
        private static int CONFIG_TIMEOUT = 1;


        public static HttpClient ReturnHttpClient(string fields = "", string contentType = "application/json", string sourceIp = "", string platform = "", string model = "")
        {
            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromMinutes(CONFIG_TIMEOUT);

            //SessionData.accessToken = "Un5uRmMx/ktYs9zNa/tcmNK498ZN2sh0B7FvTMO2AVYI2C5vze8T4w6vRrtDRJYoVEF2JoNsnLKEL6z3ghQm6Q==";

            //if (!string.IsNullOrEmpty(SessionData.accessToken))
            //    client.DefaultRequestHeaders.Add("EMSToken", SessionData.accessToken);

            client.DefaultRequestHeaders.Add("ApiKey", "P$n2021");

            if (!string.IsNullOrEmpty(sourceIp))
                client.DefaultRequestHeaders.Add("IP", sourceIp);

            if (!string.IsNullOrEmpty(platform))
                client.DefaultRequestHeaders.Add("PlatForm", platform);

            if (!string.IsNullOrEmpty(model))
                client.DefaultRequestHeaders.Add("Model", model);

            if (!string.IsNullOrEmpty(contentType))
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
            else
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(fields))
                client.DefaultRequestHeaders.Add("fields", fields);

            return client;
        }

       

        public static async Task<T> GetData(string address, string fields = "", string contentType = "application/json", string sourceIp = "", string platform = "", string model = "")
        {
            try
            {
                address = Uri.EscapeUriString(ServiceEndPoints.ServerBaseUri.OriginalString + address);
                using (HttpClient client = ReturnHttpClient(fields, contentType, sourceIp, platform, model))
                {
                    var result = await client.GetAsync(address);
                    string jsonString = string.Empty;
                    if (result.IsSuccessStatusCode)
                    {

                        jsonString = await result.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<T>(jsonString);

                    }
                    else
                    {
                        return default(T);
                    }

                    //	var RootObject = JsonConvert.DeserializeObject<RootObject<T>>(jsonString);
                    //return RootObject;
                }

            }
            catch (System.Exception ex)
            {
                //Debug.WriteLine("Exception in Register User" + exception.Message);
                return default(T);
            }
        }

        public static async Task<T> GetDataNoAsync(string address, string fields = "", string contentType = "application/json")
        {
            try
            {
                address = Uri.EscapeUriString(ServiceEndPoints.ServerBaseUri.OriginalString + address);
                using (HttpClient client = ReturnHttpClient(fields, contentType))
                {
                    var result = client.GetAsync(address).Result;
                    string jsonString = string.Empty;
                    if (result.IsSuccessStatusCode)
                    {
                        jsonString = await result.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<T>(jsonString);

                        //jsonString = await result.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        return default(T);
                    }

                    //	var RootObject = JsonConvert.DeserializeObject<RootObject<T>>(jsonString);
                    //return RootObject;
                }

            }
            catch (System.Exception ex)
            {
                //Debug.WriteLine("Exception in Register User" + exception.Message);
                return default(T);
            }
        }

        public static async Task<T> PostData(string address, object postobject, string fields = "",
                                                         string contentType = "application/json")
        {
            try
            {
                string jsonString = string.Empty;
                address = Uri.EscapeUriString(ServiceEndPoints.ServerBaseUri.OriginalString + address);
                using (HttpClient client = ReturnHttpClient(fields, contentType))
                {
                    var jsonPOSTobj = JsonConvert.SerializeObject(postobject, new JsonSerializerSettings()
                    { Culture = CultureInfo.InvariantCulture, ReferenceLoopHandling = ReferenceLoopHandling.Ignore });


                    var response = client.PostAsync(address,
                        new StringContent(JsonConvert.SerializeObject(postobject, new JsonSerializerSettings()
                        { Culture = CultureInfo.InvariantCulture,
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore }).ToString(),
                            Encoding.UTF8, contentType))
                            .Result;

                    //var jsonPOSTobj = JsonConvert.SerializeObject(postobject, new JsonSerializerSettings()
                    //{ Culture = CultureInfo.InvariantCulture, ReferenceLoopHandling = ReferenceLoopHandling.Ignore });

                    //var response = client.PostAsync(address,
                    //    new StringContent(JsonConvert.SerializeObject(postobject, Formatting.None,
                    //    new JsonSerializerSettings()
                    //    {
                    //        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    //    }).ToString(),
                    //        Encoding.UTF8, contentType))
                    //        .Result;

                    if (response.IsSuccessStatusCode)
                    {

                        jsonString = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<T>(jsonString);
                    }
                    else
                    {
                        return default(T);
                    }
                }
            }
            catch (System.Exception ex)
            {
                //Debug.WriteLine("Exception in Register User" + exception.Message);
                return default(T);
            }
        }

        public static async Task<T> authuser(string address, object postobject, string fields = "", string Username = "", string password = "",
                                                         string contentType = "application/json")
        {
            try
            {
                var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("UserName", password),
                new KeyValuePair<string, string>("Password", Username),
                 new KeyValuePair<string, string>("grant_type", "password"),
                // ...
            });
                string jsonString = string.Empty;
                address = Uri.EscapeUriString(ServiceEndPoints.ServerBaseUri.OriginalString + address);
                using (HttpClient client = ReturnHttpClient(fields, contentType))
                {
                    var response = client.PostAsync(address, content)
                            .Result;

                    if (response.IsSuccessStatusCode)
                    {

                        jsonString = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<T>(jsonString);
                    }
                    else
                    {
                        return default(T);
                    }
                }
            }
            catch (System.Exception ex)
            {
                //Debug.WriteLine("Exception in Register User" + exception.Message);
                return default(T);
            }
        }



        public static async Task<T> PutData(string address, object postobject, string fields = "", string contentType = "application/json")
        {
            try
            {
                address = Uri.EscapeUriString(ServiceEndPoints.ServerBaseUri.OriginalString + address);
                using (HttpClient client = ReturnHttpClient(fields, contentType))
                {
                    var response = client.PutAsync(address,
                        new StringContent(JsonConvert.SerializeObject(postobject).ToString(),
                            Encoding.UTF8, contentType))
                            .Result;

                    if (response.IsSuccessStatusCode)
                    {
                        string jsonString = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<T>(jsonString);
                    }
                    else
                    {
                        return default(T);
                    }
                }
            }
            catch (System.Exception ex)
            {
                return default(T);
            }
        }

    }
}
