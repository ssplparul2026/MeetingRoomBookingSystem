using MeetingRoomBooking.Data;
using MeetingRoomBooking.Enum;
using MeetingRoomBooking.Helpers;
using MeetingRoomBooking.Models;
using MeetingRoomBooking.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MeetingRoomBooking.Services.Implementations
{
    public class BookingService : IBookingService
    {
        private readonly AppDbContext _appDbContext;
        public BookingService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        
        public async Task<Booking?> HasConflict(Booking booking)
        {
            var bookings = await _appDbContext.Bookings.Where(x => x.RoomId == booking.RoomId && x.BookingDate == booking.BookingDate
                && x.Status == BookingStatus.Active) .ToListAsync();
            return bookings.FirstOrDefault(x => BookingOverlapHelper.IsOverlapping(x, booking));
        }
                
       
        public async Task<(bool Success, string message)> CreateBooking(Booking booking)
        {
            if(booking.EndTime <= booking.StartTime)
            {
                return (false , "End time should be greater than the start time");
            }
            var conflict = await HasConflict(booking);
            if (conflict != null)
            {
                string message = $"Room is already booked from {conflict.StartTime} to {conflict.EndTime} by {conflict.BookedBy}";
                return (false,message);
            }
            _appDbContext.Bookings.Add(booking);
            await _appDbContext.SaveChangesAsync();
            return (true, "booking created successfully");
        }
        public async Task<List<Booking>> GetBookingByDate(DateOnly date)
        {
            return await _appDbContext.Bookings.Include(x => x.Room)
                .Where(x => x.BookingDate == date).OrderBy(x => x.StartTime).ToListAsync();
        }
        public async Task<List<Booking>> GetMyBookings(string bookedBy)
        {
            return await _appDbContext.Bookings.Include(x => x.Room)
                .Where(x => x.BookedBy == bookedBy).OrderBy(x => x.BookingDate).ToListAsync();
        }
        public async Task<bool> CancelBooking(int bookingId)
        {
            var booking = await _appDbContext.Bookings.FindAsync(bookingId);
            if(booking == null)
            {
                return false;
            }
            booking.Status = BookingStatus.Cancelled;
             _appDbContext.Bookings.Update(booking);
            await _appDbContext.SaveChangesAsync();
            return true;
        }

    }

    }

