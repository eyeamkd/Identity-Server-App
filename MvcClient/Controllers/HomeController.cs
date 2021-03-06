﻿using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace IdentityServer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Secret()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var idToken = await HttpContext.GetTokenAsync("id_token");
            var refreshToken = await HttpContext.GetTokenAsync("refresh_token");

            var accessTokenInJwtFormat = new JwtSecurityTokenHandler().ReadJwtToken(accessToken);

            var idTokenInJwtFormat = new JwtSecurityTokenHandler().ReadJwtToken(idToken);
            var claims = User.Claims;

           // var result = await GetSecret(accessToken);

            return View();

        } 

        /*public async Task<string> GetSecret(string accessToken)
        {
            var apiClient = _httpClientFactory.CreateClient(); 
            apiClient.SetBearerToken(accessToken);
            var response = await apiClient.GetAsync("https://localhost:44332/secret");
            var content = await response.Content.ReadAsStringAsync();

            return content;
        }*/

    }
}
