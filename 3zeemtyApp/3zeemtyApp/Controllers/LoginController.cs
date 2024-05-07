using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

            return Ok(new {Message = "User Created"});
            
        }
    }
}
