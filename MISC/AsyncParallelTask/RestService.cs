﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AsyncParallelTask
{
    public class RestService
    {
        private HttpClient _client;
        private Uri _UrlBase;
        public RestService()
        {
            _UrlBase = new Uri("http://localhost:5000/");
            _client = new HttpClient();
            _client.BaseAddress = _UrlBase;
        }
        public async Task<(int, bool)> Pay(int car)
        {
            var data = new StringContent(car.ToString(), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("ProcessPayment", data);
            if (!response.IsSuccessStatusCode)
                throw new Exception("Error al tratar de enviar la informacion del servicio web");

            var responseData = await response.Content.ReadAsStringAsync();
            return (car, JsonSerializer.Deserialize<bool>(responseData));
        }
    }
}
