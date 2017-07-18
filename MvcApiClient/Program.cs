using MvcApi.Models;
using MvcApiSelfHost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;

namespace MvcApiClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //Client.client.BaseAddress =new Uri("http://localhost:8080");
            //Client.ShowProducts();
            //Client.ShowProductById(1);
            //Console.ReadKey();
            //RestClient client = new RestClient("http://localhost:8068");

            //client.Post("dssddd", "/api/values");
            AuthMvcApibyToken();
           
        }

        private static void AuthMvcApibyToken() 
        {
            string baseAddress = "http://localhost:2843/";
            
                var client = new HttpClient();
                var response = client.GetAsync(baseAddress + "api/TestApi").Result;
                Console.WriteLine(response);

                Console.WriteLine();

                var authorizationHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes("rajeev:secretKey"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authorizationHeader);

               var form = new Dictionary<string, string>  
               {  
                   {"grant_type", "password"},  
                   {"username", "rranjan"},  
                   {"password", "password@123"},  
               };

                var tokenResponse = client.PostAsync(baseAddress + "accesstoken", new FormUrlEncodedContent(form)).Result;
                var token = tokenResponse.Content.ReadAsAsync<Token>().Result;
                Console.WriteLine("Token issued is: {0}", token.AccessToken);
                Console.WriteLine();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
                var authorizedResponse = client.GetAsync(baseAddress + "/api/TestApi").Result;
                Console.WriteLine(authorizedResponse);
                Console.WriteLine(authorizedResponse.Content.ReadAsStringAsync().Result);

                var authorizedResponse2 = client.GetAsync(baseAddress + "/api/TestApi").Result;
                Console.WriteLine(authorizedResponse2);
                Console.WriteLine(authorizedResponse2.Content.ReadAsStringAsync().Result);


               //通过refreshtoken 获取token
                var parameters = new Dictionary<string, string>();
                parameters.Add("grant_type", "refresh_token");
                parameters.Add("refresh_token",token.RefreshToken);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authorizationHeader);
                tokenResponse = client.PostAsync(baseAddress + "accesstoken", new FormUrlEncodedContent(parameters)).Result;
                token = tokenResponse.Content.ReadAsAsync<Token>(new[] { new JsonMediaTypeFormatter() }).Result;
                Console.WriteLine("Token issued is: {0}", token.AccessToken);
                Console.WriteLine();

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
                var authorizedResponse3 = client.GetAsync(baseAddress + "/api/TestApi").Result;
                Console.WriteLine(authorizedResponse3);
                Console.WriteLine(authorizedResponse3.Content.ReadAsStringAsync().Result);

                Console.ReadLine();

        }
    }

    class Client 
    {
       internal static HttpClient client = new HttpClient();
        public static void ShowProducts()
        {
            try
            {
                var products = GetHttpRessonseMessage<IEnumerable<Product>>("api/Products");
                foreach (var item in products)
                {
                    Console.WriteLine(string.Format("id:{0},name:{1},price:{2}", item.Id, item.Name, item.Price));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("exception"+ ex.InnerException);
            }

        }

        public static void ShowProductById(int id) 
        {
            if (id<=0)
            {
                Console.WriteLine("id Error");
            }
            else
            {
                var product= GetHttpRessonseMessage<Product>(string.Format("api/products/{0}",id));
                Console.WriteLine("====================================");
                Console.WriteLine(string.Format("id:{0},name:{1},price:{2}", product.Id, product.Name, product.Price));
            }
        }

        public static T GetHttpRessonseMessage<T>(string url, string baseAddress = "http://localhost:8080") where T:class
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.BaseAddress = new Uri(baseAddress);
                HttpResponseMessage response = httpClient.GetAsync(url).Result;
                response.EnsureSuccessStatusCode();
           
                if (response.IsSuccessStatusCode)
                {
                    var result = response.Content.ReadAsAsync<T>().Result;

                    return result;
                }
                else
                {
                    return default(T);
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
    
    }
}
