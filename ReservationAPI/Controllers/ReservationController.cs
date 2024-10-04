using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ReservationAPI.Controllers
{
    [Route("api/reservations")]
    [ApiController]
    public class ReservationController : Controller
    {
        private readonly ReservationDbContext _context;

        // Inject the DbContext through the constructor
        public ReservationController(ReservationDbContext context)
        {
            _context = context;
        }

        // Create reservation
        [HttpPost("post/{id}")]
        public async Task<IActionResult> CreateReservation(int id)
        {
            // Reservation must not exist already
            if (await _context.Reservations.AnyAsync(r => r.RsvBookID == id))
                return Conflict(new { message = "Book is already reserved" });

            // Create reservation
            Reservation reservation = new Reservation 
            {
                RsvBookID = id,
                RsvReservedAt = DateTime.Now
            };

            // Add the reservation and save changes
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return Ok(reservation);
        }

        // Delete reservation
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteReservation(int id) 
        {
            // Reservation must exist
            var reservation = await _context.Reservations.FirstOrDefaultAsync(r => r.RsvBookID == id);
            if (reservation == null)
            {
                return NotFound(new { message = "Reservation not found." });
            }

            // Delete the reservation and save changes
            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Reservation deleted." });
        }

        // Check reservation
        [HttpGet("get/{id}")]
        public async Task<IActionResult> CheckReservation(int id)
        {
            // Check if the reservation exists
            var exists = await _context.Reservations.AnyAsync(r => r.RsvBookID == id);

            return Ok(new { reserved = exists });
        }

    }
}
