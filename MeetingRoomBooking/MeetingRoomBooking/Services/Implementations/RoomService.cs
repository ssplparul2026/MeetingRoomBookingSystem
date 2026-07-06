using MeetingRoomBooking.Data;
using MeetingRoomBooking.Models;
using MeetingRoomBooking.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MeetingRoomBooking.Services.Implementations
{
    public class RoomService : IRoomService
    {
        private readonly AppDbContext _appDbContext;
        public RoomService(AppDbContext appDbContext)
        {
                _appDbContext = appDbContext;
        }
        public async Task<List<Room>> GetAllRooms()
        {
            return await _appDbContext.Rooms.OrderBy(x => x.Name).ToListAsync();
        }
        public async Task<Room?> GetRoomById(int id)
        {
            return await _appDbContext.Rooms.FirstOrDefaultAsync(x => x.Id == id);
            
        }
        public async Task<List<Booking>> GetRoomBooking(int roomId, DateOnly date)
        {
            return await _appDbContext.Bookings
                .Where(x => x.RoomId == roomId && x.BookingDate == date && x.Status == Enum.BookingStatus.Active)
                .OrderBy(x => x.StartTime).ToListAsync();
            
        }
    }
}
