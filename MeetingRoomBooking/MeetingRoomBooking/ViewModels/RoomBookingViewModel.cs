using MeetingRoomBooking.Models;

namespace MeetingRoomBooking.ViewModels
{
    public class RoomBookingViewModel
    {
        public int RoomId { get; set; }

        public string RoomName { get; set; } = string.Empty;

        public int Capacity { get; set; }

        public string Location { get; set; } = string.Empty;

        public DateOnly SelectedDate { get; set; }

        public List<Booking> Bookings { get; set; } = new();
    }
}
