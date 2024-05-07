using _3zeemtyApp.Models.Requests;
using _3zeemtyApp.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Models;
using ProductApi.Models.Entites;
using ProductApi.Models.Requests;
using ProductApi.Models.Responses;
using ProductApi.Services;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatererController : ControllerBase
    {
        CaterContext _context;
        public CatererController(CaterContext context)
        {
            _context = context;
        }

        [HttpGet]
        public PageListResult<CatererResponse> GetAll(int page = 1, string search = "")
        {
            if (search == "")
            {
                return _context.Cateres
                .Select(b => new CatererResponse
                {
                    Description = b.Description,
                    Type = b.Type,
                    Name = b.Name,
                    Id = b.Id,
                    ImagePath = b.Image
                }).ToPageList(page, 50);
            }

            return _context.Cateres
                .Where(r => r.Name.StartsWith(search))
                .Select(b => new CatererResponse
                {
                    Description = b.Description,
                    Type = b.Type,
                    Name = b.Name,
                    Id = b.Id,
                    ImagePath = b.Image

                }).ToPageList(page, 50);

        }

        [HttpPost("Cart")]
        [Authorize]
        public ActionResult CreateBook(BookingRequest bookingRequest) {
            var cater = _context.Cateres.Find(bookingRequest.CatererId);

            var userId = int.Parse(User.FindFirst(TokenClaimsConstant.UserId).Value);

            var userBook = _context.UserAccounts.Find(userId);

            var newBooking = new Booking();
            newBooking.IsBooked = false;
            newBooking.CatererService = cater;
            newBooking.DateOnly = DateOnly.FromDateTime(DateTime.Now);
            newBooking.Name = bookingRequest.Name;
            newBooking.User = userBook;


            _context.Bookings.Add(newBooking);
            _context.SaveChanges();

            return Ok();

        }

        [HttpGet("Booking")]
        [Authorize]
        public ActionResult<List <BookingResponce>> GetBookings(int id)
        {
            var userId = int.Parse(User.FindFirst(TokenClaimsConstant.UserId).Value);

            var boooking = _context.Bookings.Where(t=> t.User.Id == userId);

            return boooking
                 .Select(b => new BookingResponce
                 {
                     CatererService = b.CatererService.Name,
                     Name = b.Name,
                     Date = b.DateOnly,
                 }).ToList();

                 }

      


        [HttpGet("Details/{id}")]
        [ProducesResponseType(typeof(CatererResponse), 200)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<CatererResponse> Details([FromRoute] int id)
        {
          
            var cater = _context.Cateres.Find(id);
            if (cater == null)
            {
                return NotFound();
            }
            return Ok(new CatererResponse
            {

                Description = cater.Description,
                Type = cater.Type,  
                Name = cater.Name,
                Id = cater.Id,
                ImagePath = cater.Image,

              
            });
        }

        [HttpPatch("{id}")]
        public IActionResult Edit(int id, AddCaterRequest req)
        {
            var cater = _context.Cateres.Find(id);

            cater.Name = req.Name;
            cater.Description = req.Description;
            cater.Type = req.Type;


           

            _context.SaveChanges();

            return CreatedAtAction(nameof(Details), new { Id = cater.Id }, cater);
        }
        [HttpPost]
        public IActionResult Add(AddCaterRequest req)
        {
            var newCater = new CatererstEntity()
            {
                Name = req.Name,
                Description = req.Description,
                Type = req.Type,
                

            };
            _context.Cateres.Add(newCater);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Details), new { Id = newCater.Id }, newCater);
        }
        [HttpDelete("{id}")]
        [Authorize(Roles ="Admin")]
        public IActionResult Delete(int id)
        {
            var user = HttpContext.User;
            var cater = _context.Cateres.Find(id);
            if (cater == null)
            {
                return BadRequest();
            }
              
            _context.Cateres.Remove(cater);
            _context.SaveChanges();
     
            return Ok();
        }
        
    }
}
