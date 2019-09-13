using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using Services.Interfaces;

namespace Services.Services
{
    public class UserService 
    {
        private static HttpClient hhtpClientInstance;

        public static HttpClient GetHttpClientInstance()
        {
            if (hhtpClientInstance == null)
            {
                hhtpClientInstance = new HttpClient();
                hhtpClientInstance.DefaultRequestHeaders.ConnectionClose = false;
            }
            return hhtpClientInstance;
        }
        public string baseUrl
        {
            get
            {
                return "https://pokeapi.co/api/v2/";
            }
        }



        public string getPokemon()
        {
            string action = "pokemon/ditto/";

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, baseUrl + action);
            HttpResponseMessage response = GetHttpClientInstance().SendAsync(request).Result;
        }
    }
}
