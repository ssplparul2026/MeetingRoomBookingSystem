using MeetingRoomBooking.Enum;

namespace MeetingRoomBooking.ViewModels
{
    public class MyBookingsViewModel
    {
        public int BookingId { get; set; }

        public string RoomName { get; set; } = string.Empty;

        public string BookedBy { get; set; } = string.Empty;

        public DateOnly BookingDate { get; set; }

        public TimeOnly StartTime { get; set; }

        public TimeOnly EndTime { get; set; }

        public string Purpose { get; set; } = string.Empty;

        public BookingStatus Status { get; set; }
    }
}
