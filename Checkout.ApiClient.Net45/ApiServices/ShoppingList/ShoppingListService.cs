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

                        var token = JsonConvert.DeserializeObject<Models.TokenModel>(resultString);
                        return new HttpResponse<Models.TokenModel>(token);
                    }
                }

                
            }
        }

        public async Task<HttpResponse<bool>> AddItem(string token, ShoppingItem shoppingItem)
        {
            return new ApiHttpClient().PostRequest<bool>(ApiUrls.ShoppingListInsert, token, shoppingItem, true);
        }

        public async Task<HttpResponse<bool>> UpdateItem(string token, ShoppingItem shoppingItem)
        {
            return new ApiHttpClient().PutRequest<bool>(ApiUrls.ShoppingListUpdate, token, shoppingItem, true);

            #region [Alternatively create the request and client and run it with await]
            //var request = new HttpRequestMessage(HttpMethod.Put, "api/shoppinglist");
            //var jsonString = JsonConvert.SerializeObject(shoppingItem);
            //request.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");

            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri(ApiUrls.ShoppingList);
            //    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            //    using (var response = client.SendAsync(request).Result)
            //    {
            //        using (var responseContent = response.Content)
            //        {
            //            var resultString = await responseContent.ReadAsStringAsync();
            //            var item = JsonConvert.DeserializeObject<bool>(resultString);
            //            return new HttpResponse<bool>(item);
            //        }
            //    }

            //} 
            #endregion
        }

        public async Task<HttpResponse<bool>> DeleteItem(string token, string name)
        {
            return new ApiHttpClient().DeleteRequest<bool>(string.Format(ApiUrls.ShoppingListDelete, name), token, true);
        }

        public async Task<HttpResponse<IEnumerable<Models.ShoppingItem>>> GetItems(string token)
        {
            return new ApiHttpClient().GetRequest<IEnumerable<Models.ShoppingItem>>(ApiUrls.ShoppingListGetAll, token, true);
        }

        public async Task<HttpResponse<Models.ShoppingItem>> GetItem(string token, string itemname)
        {
            return new ApiHttpClient().GetRequest<Models.ShoppingItem>(string.Format(ApiUrls.ShoppingListGet, itemname), token, true);
            
        }
    }
}
