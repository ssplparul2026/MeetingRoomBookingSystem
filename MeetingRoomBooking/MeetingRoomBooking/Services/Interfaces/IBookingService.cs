using MeetingRoomBooking.Models;

namespace MeetingRoomBooking.Services.Interfaces
{
    public interface IBookingService
    {
        Task<(bool Success, string message)> CreateBooking(Booking booking);
        Task<List<Booking>> GetBookingByDate(DateOnly date);
        Task<List<Booking>> GetMyBookings(string bookedBy);
        Task<Booking?> HasConflict(Booking booking);
        Task<bool> CancelBooking(int bookingId);
    }
}
