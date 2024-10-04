using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReservationAPI;

public class Reservation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int RsvBookID { get; set; }


    public DateTime RsvReservedAt { get; set; }
}
