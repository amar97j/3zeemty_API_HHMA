using BankFrontEnd.API;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using ProductApi.Services;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;

namespace BankFrontEnd.Controllers
{
    [Authorize]
    public class CatererController : Controller
    {
        private readonly CatererAPIClient _client;
        public CatererController(CatererAPIClient client)
        {
            _client = client;
        }
        public async Task<IActionResult> Index(string Search="")
        {

            if (!string.IsNullOrEmpty(Search))
            {
                var caterer1 = await _client.GetCaterer(Search);
                return View(caterer1.Data);
            }
            var caterer = await _client.GetCaterer();
            return View(caterer.Data);
        }

        public async Task<IActionResult> Details(int id)
        {
            var caterer = await _client.GetDetails(id);
            return View(caterer);
        }


        public async Task<IActionResult> Bookings(int id)
        {
            var booking = await _client.GetBookings(id);
            return View(booking);
        }

        public async Task<IActionResult> AddToCart(int id)
        {
            var booking = await _client.SaveBookings(id);
            return RedirectToAction("Bookings");
        }
       
        public IActionResult PaymentPage(int id)
        {

            return View(PaymentPage);
        }


      
        public async Task<IActionResult> DeleteBooking(int id)
        {
            var userId = int.Parse(User.FindFirst(TokenClaimsConstant.UserId).Value);

            var booking = await _client.GetBookings(id);

            if (booking == null )
            {
                return NotFound();
            }

            await _client.DeleteBooking(id);

            return RedirectToAction("Bookings");
        }

        public async Task<IActionResult> Profile(int id)
        {
            var profile = await _client.GetProfile(id);
            return View(profile);
        }

    }
}
