using System.ComponentModel.DataAnnotations;

namespace MeetingRoomBooking.Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Room name is required.")]
        [StringLength(100, ErrorMessage = "Room name cannot exceed 100 characters.")]
        [Display(Name = "Room Name")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "Capacity is required.")]
        [Range(1, 50, ErrorMessage = "Capacity must be between 1 and 50.")]
        public int Capacity { get; set; }
        [Required(ErrorMessage = "Location is required.")]
        [StringLength(100, ErrorMessage = "Location cannot exceed 100 characters.")]
        public string Location { get; set; } = string.Empty;

        public ICollection<Booking> Bookings { get; set; }= new List<Booking>();
    }
}
