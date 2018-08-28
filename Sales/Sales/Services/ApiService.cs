namespace Sales.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Common.Models;
    using Helpers;
    using Newtonsoft.Json;
    using Plugin.Connectivity;    

    public class ApiService
    {
        public async Task<Response> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Languages.TurnOnInternet,
                };
            }

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable("google.com");
            if (!isReachable)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = Languages.NoInternet,
                };
            }

            return new Response
            {
                IsSuccess = true,
            };
        }

        public async Task<Response> GetList<T>(string urlBase, string prefix, string controller)
        {
            try 
	        {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}", prefix, controller);
                var response = await client.GetAsync(url);
                var answer = await response.Content.ReadAsStringAsync(); // se usa para leer todo el json, por que es string
                if (!response.IsSuccessStatusCode)//si no canchilo la vueltaaca se le devuelve la respuesta de false con la respuesta "answer"
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = answer,
                    };
                }
                var list = JsonConvert.DeserializeObject<List<T>>(answer); //Va a convertir todo el string que viene "answer" en una lista de objetos
                return new Response
                {
                    IsSuccess = true,
                    Result = list,
                };
	        }
	        catch (Exception ex)
	        {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };		        
	        }
        }

        public async Task<Response> Post<T>(string urlBase, string prefix, string controller, T model)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model); // Coge un objeto y lo vuelev string, osea el JSON
                var content = new StringContent(request, Encoding.UTF8, "application/json"); // El body que se le envia por medio del metodo POST
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}", prefix, controller);
                var response = await client.PostAsync(url, content);
                var answer = await response.Content.ReadAsStringAsync(); // se usa para leer todo el json, por que es string
                if (!response.IsSuccessStatusCode)//si no canchilo la vueltaaca se le devuelve la respuesta de false con la respuesta "answer"
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = answer,
                    };
                }
                var obj = JsonConvert.DeserializeObject<List<T>>(answer); //Va a convertir todo el string que viene "answer" en una lista de objetos
                return new Response
                {
                    IsSuccess = true,
                    Result = obj,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}
