using System;
using System.Collections.Generic;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Wonders.Api.Models;

namespace Wonders.IntegrationTests
{
    public class WondersControllerDriver
    {
        private readonly HttpClient _client;

        public WondersControllerDriver(HttpClient client)
        {
            _client = client;
        }
        public IActionResult Create(Wonder wonder)
        {
            return _client.PostAsJsonAsync("api/wonders", wonder).Result;
        }

        public IActionResult Get()
        {
            return _client.GetAsync<IEnumerable<Wonder>>("api/wonders").Result;
        }

        public IActionResult Get(string title)
        {
            return _client.GetAsync<Wonder>($"api/wonders/{title}").Result;
        }
    }
}