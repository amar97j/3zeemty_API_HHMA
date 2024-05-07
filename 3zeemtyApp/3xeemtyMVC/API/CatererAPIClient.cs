using System.Net.Http.Headers;
using System.Net.Http;
using ProductApi.Models;
using ProductApi.Models.Responses;
using Microsoft.AspNetCore.Identity.Data;
using ProductApi.Models.Requests;
using _3zeemtyApp.Models.Responses;
using _3zeemtyApp.Models.Requests;

namespace BankFrontEnd.API
{
    public class CatererAPIClient
    {
        private readonly HttpClient _api;
        public CatererAPIClient(IHttpContextAccessor accessor, IHttpClientFactory factory)
        {
            _api = factory.CreateClient("catererApi");

            accessor.HttpContext.Request.Cookies.TryGetValue("token", out string token);
            _api.DefaultRequestHeaders.Authorization =
                          new AuthenticationHeaderValue("Bearer", token);
        }

        public async Task<PageListResult<CatererResponse>> GetCaterer(string Search="")
        {
            var url = "api/caterer";
            if (Search != "")
            {
                url += $"?search={Search}";
            }
            var response = await _api
                .GetFromJsonAsync<PageListResult<CatererResponse>>(url);
            return response;
        }

        public async Task<CatererResponse> GetDetails(int id)
        {
            var response = await _api
                .GetFromJsonAsync<CatererResponse>($"api/caterer/Details/{id}");
            return response;
        }

        public async Task<List<BookingResponce>> GetBookings(int id)
        {
            var bookresponse = await _api
                .GetFromJsonAsync<List<BookingResponce>>($"api/caterer/booking");
            return bookresponse;
        }

        public async Task<bool> SaveBookings(int id)
        {
       await _api.PostAsJsonAsync("api/caterer/cart", new BookingRequest { CatererId = id, Name = "" });
            

            return true;
        }

        public async Task<bool> Register(SignupRequest request)
        {
            var response = await _api.PostAsJsonAsync("/api/login/Registor", request);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
        public async Task<string> Login(string username, string password)
        {
            var response = await _api.PostAsJsonAsync("/api/login",
                new UserLoginRequest { Username = username, Password = password });

            if (response.IsSuccessStatusCode)
            {
                var tokenResponse = await response.Content.ReadFromJsonAsync<UserLoginResponse>();

                var token = tokenResponse.Token;
                return token;
            }
            return "";
        }
    }
}
