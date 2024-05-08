using _3zeemtyApp.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApi.Models.Entites;
using ProductApi.Models.Requests;
using ProductApi.Models.Responses;
using ProductApi.Services;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {

       // CaterContext _context;

        private readonly TokenService service;
        private readonly CaterContext context;

        public LoginController(TokenService service, CaterContext context)
        {
            this.service = service;
            this.context = context;
        }
        [HttpPost]
        public IActionResult Login(UserLoginRequest loginDetails)
        {

            var response = service.GenerateToken(loginDetails.Username, loginDetails.Password);
            if (response.IsValid)
            {
                return Ok(new UserLoginResponse { Token = response.Token });
            }
            return BadRequest("Username and/or Password is wrong");
        }

        [HttpPost("Registor")]
        public IActionResult Registor(SignupRequest request)
        {

            var newUser = UserAEntity.Create(request.Username, request.Password, request.IsAdmin);

            context.UserAccounts.Add(newUser);
            context.SaveChanges();

            return Ok(new { Message = "User Created" });

        }

        [HttpGet("Profile")]

        public ActionResult<UserResponse> Profile()
        {
            var userId = int.Parse(User.FindFirst(TokenClaimsConstant.UserId).Value);
            var userProfile = context.UserAccounts.SingleOrDefault(t => t.Id == userId);
            var boooking = context.Bookings.Where(t => t.User.Id == userId);

            if (userProfile == null)
            {
                return NotFound();
            }
            return Ok(new UserResponse
            {

                Name = userProfile.Username,
                Bookings = boooking.Select(b => new BookingResponce
                {
                    CatererService = b.CatererService.Name,
                    Name = b.Name,
                    Date = b.DateOnly,
                    Id = b.Id
                }).ToList()





            });
        }
    }
}

