using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ReservationAPI.Controllers
{
    public class ReservationController : Controller
    {
        // Dummy data
        private static List<Reservation> _reservations = new List<Reservation>();

        // Create reservation
        [HttpPost("{id}")]
        public IActionResult CreateReservation(int id)
        {
            // Reservation must not exist already
            if (_reservations.Any(r => r.BookID == id))
                return Conflict(new { message = "Book is already reserved" });

            // Create reservation
            Reservation reservation = new Reservation 
            { 
                BookID = id,
                ReservedAt = DateTime.Now
            };

            _reservations.Add(reservation);
            return Ok(reservation);
        }

        // Delete reservation
        [HttpDelete("{id}")]
        public IActionResult DeleteReservation(int id) 
        {
            // Reservation must exist
            var reservation = _reservations.FirstOrDefault(r => r.BookID == id);
            if (reservation == null)
            {
                return NotFound(new { message = "Reservation not found." });
            }

            // Delete reservation
            _reservations.Remove(reservation);
            return Ok(new { message = "Reservation deleted." });
        }

        // Check reservation
        [HttpGet("{id}")]
        public IActionResult CheckReservation(int id)
        {
            var exists = _reservations.Any(r => r.BookID == id);
            return Ok(new { reserved = exists });
        }

    }
}
