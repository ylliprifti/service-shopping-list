using Checkout.ApiServices.SharedModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Checkout.ApiServices.ShoppingList.Models;

namespace Checkout.ApiServices.ShoppingList
{
    public class ShoppingListService
    {
        public async Task<HttpResponse<Models.TokenModel>> Login(string username, string password)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "token");
            var content = new List<KeyValuePair<string, string>>();
            content.Add(new KeyValuePair<string, string>("username", username));
            content.Add(new KeyValuePair<string, string>("password", password));
            content.Add(new KeyValuePair<string, string>("grant_type", "password"));

            request.Content = new FormUrlEncodedContent(content);

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrls.ShoppingList);
               
                using (var response = client.SendAsync(request).Result)
                {
                    using (var responseContent = response.Content)
                    {
                        var resultString = await responseContent.ReadAsStringAsync();
                        //resultString.Wait();
                        var token = JsonConvert.DeserializeObject<Models.TokenModel>(resultString);
                        return new HttpResponse<Models.TokenModel>(token);
                    }
                }

                
            }
        }

        public async Task<HttpResponse<bool>> AddItem(string token, ShoppingItem shoppingItem)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, "api/shoppinglist");
            var jsonString = JsonConvert.SerializeObject(shoppingItem);
            request.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrls.ShoppingList);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = client.SendAsync(request).Result)
                {
                    using (var responseContent = response.Content)
                    {
                        var resultString = await responseContent.ReadAsStringAsync();
                        var item = JsonConvert.DeserializeObject<bool>(resultString);
                        return new HttpResponse<bool>(item);
                    }
                }

            }
        }

        public async Task<HttpResponse<bool>> UpdateItem(string token, ShoppingItem shoppingItem)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, "api/shoppinglist");
            var jsonString = JsonConvert.SerializeObject(shoppingItem);
            request.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrls.ShoppingList);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = client.SendAsync(request).Result)
                {
                    using (var responseContent = response.Content)
                    {
                        var resultString = await responseContent.ReadAsStringAsync();
                        var item = JsonConvert.DeserializeObject<bool>(resultString);
                        return new HttpResponse<bool>(item);
                    }
                }

            }
        }

        public async Task<HttpResponse<bool>> DeleteItem(string token, string name)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, string.Format("api/shoppinglist/{0}", name));
            
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrls.ShoppingList);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = client.SendAsync(request).Result)
                {
                    using (var responseContent = response.Content)
                    {
                        var resultString = await responseContent.ReadAsStringAsync();
                        var item = JsonConvert.DeserializeObject<bool>(resultString);
                        return new HttpResponse<bool>(item);
                    }
                }

            }
        }

        public async Task<HttpResponse<IEnumerable<Models.ShoppingItem>>> GetItems(string token)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "api/shoppinglist");

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrls.ShoppingList);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = client.SendAsync(request).Result)
                {
                    using (var responseContent = response.Content)
                    {
                        var resultString = await responseContent.ReadAsStringAsync();
                        var item = JsonConvert.DeserializeObject<IEnumerable<Models.ShoppingItem>>(resultString);
                        return new HttpResponse<IEnumerable<Models.ShoppingItem>>(item);
                    }
                }

            }
        }

        public async Task<HttpResponse<Models.ShoppingItem>> GetItem(string token, string itemname)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, string.Format("api/shoppinglist/{0}", itemname));
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(ApiUrls.ShoppingList);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                using (var response = client.SendAsync(request).Result)
                {
                    using (var responseContent = response.Content)
                    {
                        var resultString = await responseContent.ReadAsStringAsync();
                        var item = JsonConvert.DeserializeObject<Models.ShoppingItem>(resultString);
                        return new HttpResponse<Models.ShoppingItem>(item);
                    }
                }

            }
            }
        }
}
