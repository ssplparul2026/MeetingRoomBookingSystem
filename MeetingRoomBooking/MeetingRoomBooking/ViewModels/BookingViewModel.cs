using MeetingRoomBooking.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MeetingRoomBooking.ViewModels
{
    public class BookingViewModel
    {
        public int Id { get; set; }
        [Required]
        public int RoomId { get; set; }

        [ForeignKey(nameof(RoomId))]
        public Room? Room { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Booked By cannot exceed 100 characters.")]
        public string BookedBy { get; set; } = string.Empty;
        [Required]
        public DateOnly BookingDate { get; set; }
        [Required]

        public TimeOnly StartTime { get; set; }
        [Required]

        public TimeOnly EndTime { get; set; }
        [Required]
        [StringLength(200, ErrorMessage = "Purpose cannot exceed 200 characters.")]
        public string Purpose { get; set; } = string.Empty;

        public List<RoomViewModel> Rooms { get; set; } = new();
    }
}
