using Desafio_2._1.Models;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio_2._1.Services
{
    internal class Request
    {
        private readonly HttpClient _client;
        private readonly string _apiKey = "31c0954dac4b34d73cbebfe8";

        public Request()
        {
            _client = new HttpClient();
        }

        // Método para integração com a API de conversão de moeda
        public async Task<Conversor> IntegracaoAsync(string fonte, string destino)
        {
            try
            {
                var response = await _client.GetAsync($"https://v6.exchangerate-api.com/v6/{_apiKey}/pair/{fonte}/{destino}");

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Erro na requisição: {response.StatusCode}");
                    return new Conversor { Concrete = true };
                }

                var jsonString = await response.Content.ReadAsStringAsync();

                if (jsonString.Contains("\"result\":\"error\""))
                {
                    Console.WriteLine($"Erro na resposta da API: {jsonString}");
                    return new Conversor { Concrete = true };
                }

                var conversion = JsonConvert.DeserializeObject<Conversion>(jsonString);

                return new Conversor { Concrete = true };
            }
            catch (Exception ex)
            {
                // Tratamento de exceção caso ocorra algum erro durante a requisição
                Console.WriteLine($"Erro ao acessar a API: {ex.Message}");
                return new Conversor { Concrete = true };
            }
        }
    }
}
