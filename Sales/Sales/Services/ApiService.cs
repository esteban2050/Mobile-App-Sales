namespace Sales.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using Common.Models;   

    public class ApiService
    {
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
    }
}
