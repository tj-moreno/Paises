
namespace Paises.Services
{
    using Newtonsoft.Json;
    using Paises.Models;
    using Plugin.Connectivity;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;

    class ApiService
    {
        public async Task<Response> ChekConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {
                    IsSurccess = false,
                    Message = "Por favor active la coneccion a Internet.",
                };               
            }

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");

            if (!isReachable)
            {
                return new Response
                {
                    IsSurccess = false,
                    Message = "Tenemos problemas para comprobar su coneccion. Por favor verificque su coneccion a internet"
                };                
            }

            return new Response
            {
                IsSurccess = true,
                Message = "Ok"
            };
        }

        public async Task<TokenResponse> GetToken(string urlBase, string userName, string password)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);

                var response = await client.PostAsync("Token", 
                    new StringContent(string.Format("grant_type=password&username={0}&password={1}",
                    userName, password), Encoding.UTF8,"application/x-www-from-urlencoded"));
                var resultJSON = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<TokenResponse>(resultJSON);

                return result;

            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<Response> Get<T>(string urlBase,string servicePrefix,string contrller,string tokenType,string accessToken,int id)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}/{2}", servicePrefix, contrller, id);

                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSurccess=false,
                        Message=response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<T>(result);
                return new Response
                {
                    IsSurccess = true,
                    Message = "Ok",
                    Result = model
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSurccess = false,
                    Message = ex.Message                    
                };
            }
        }

        public async Task<Response> GetList<T>(string urlBase,string servicePrefix,string controller,string tokenType,string accessToken)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                client.BaseAddress = new Uri(urlBase);

                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSurccess = false,
                        Message = response.StatusCode.ToString()
                    };
                }

                var list = JsonConvert.DeserializeObject<List<T>>(result);

                return new Response
                {
                    IsSurccess = true,
                    Message = "Ok",
                    Result = list
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSurccess=false,
                    Message=ex.Message
                };                    
            }
        }

        public async Task<Response> GetList<T>(string urlBase,string servicePrefix,string controller)
        {
            try
            {
                var client = new HttpClient();
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(tokenType, accessToken);
                client.BaseAddress = new Uri(urlBase);

                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSurccess = false,
                        Message = response.StatusCode.ToString()
                    };
                }

                var list = JsonConvert.DeserializeObject<List<T>>(result);

                return new Response
                {
                    IsSurccess = true,
                    Message = "Ok",
                    Result = list
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSurccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}
