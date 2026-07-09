using MeetingRoomBooking.Models;
using MeetingRoomBooking.ViewModels;

namespace MeetingRoomBooking.Helpers
{
    public static class BookingOverlapHelper
    {
        public static bool IsOverlapping(Booking existingBooking, Booking newBooking)
        {
            
            return newBooking.StartTime < existingBooking.EndTime && newBooking.EndTime > existingBooking.StartTime;
        }
    }
}
