using MeetingRoomBooking.Models;
using MeetingRoomBooking.ViewModels;

namespace MeetingRoomBooking.Services.Interfaces
{
    public interface IBookingService
    {
        Task<(bool Success, string message)> CreateBooking(Booking booking);
        Task<List<Booking>> GetBookingByDate(DateOnly date);
        Task<List<MyBookingsViewModel>> GetMyBookings(string bookedBy);
        Task<List<Booking>> HasConflict(Booking booking);
        Task<bool> CancelBooking(int bookingId);

        Task<MyBookingsViewModel?> GetBookingById(int bookingId);
    }
}
