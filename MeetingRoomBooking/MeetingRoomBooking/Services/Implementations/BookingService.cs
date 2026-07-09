using MeetingRoomBooking.Data;
using MeetingRoomBooking.Enum;
using MeetingRoomBooking.Helpers;
using MeetingRoomBooking.Models;
using MeetingRoomBooking.Services.Interfaces;
using MeetingRoomBooking.ViewModels;
using Microsoft.AspNetCore.Http.HttpResults;
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
        
        public async Task<List<Booking>> HasConflict(Booking booking)
        {
            var bookings = await _appDbContext.Bookings.Where(x => x.RoomId == booking.RoomId && x.BookingDate == booking.BookingDate
                && x.Status == BookingStatus.Active) .ToListAsync();
            return bookings.Where(x => BookingOverlapHelper.IsOverlapping(x, booking)).ToList();
        }
                
       
        public async Task<(bool Success, string message)> CreateBooking(Booking booking)
        {
            if(booking.EndTime <= booking.StartTime)
            {
                return (false , "End time should be greater than the start time");
            }
            var conflict = await HasConflict(booking);
            if (conflict.Any())
            {
                string message = "";
                foreach (var item in conflict)
                {
                    message += $"Room is already booked from {item.StartTime} to {item.EndTime} by {item.BookedBy}"+Environment.NewLine;
                }
                return (false, message);
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
        public async Task<List<MyBookingsViewModel>> GetMyBookings(string bookedBy)
        {
            return await _appDbContext.Bookings.Include(x => x.Room)
                .Where(x => x.BookedBy == bookedBy).OrderBy(x => x.BookingDate).Select(x => new MyBookingsViewModel
                {
                    BookingId = x.Id,
                    RoomName = x.Room != null ? x.Room.Name : "",
                    BookedBy = x.BookedBy,
                    BookingDate = x.BookingDate,
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    Purpose = x.Purpose,
                    Status = x.Status,
                }).ToListAsync();
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
        public async Task<MyBookingsViewModel?> GetBookingById(int bookingId)
        {
            return await _appDbContext.Bookings.Include(x => x.Room).Where(x => x.Id == bookingId).Select(x => new MyBookingsViewModel
            {
                BookingId = x.Id,
                RoomName = x.Room != null ? x.Room.Name : "",
                BookedBy = x.BookedBy,
                BookingDate = x.BookingDate,
                StartTime = x.StartTime,
                EndTime = x.EndTime,
                Purpose = x.Purpose,
                Status = x.Status,
            }).FirstOrDefaultAsync();
        }
    }

    }

