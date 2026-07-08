using MeetingRoomBooking.Data;
using MeetingRoomBooking.Enum;
using MeetingRoomBooking.Models;
using MeetingRoomBooking.Services.Interfaces;
using MeetingRoomBooking.ViewModels;
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
        public async Task<List<RoomViewModel>> GetAllRooms()
        {
            return await _appDbContext.Rooms.OrderBy(x => x.Name).Select(x => new RoomViewModel
            {
                Id = x.Id,
                Name = x.Name,
                Capacity = x.Capacity,
                Location = x.Location
            }).ToListAsync();
        }
        public async Task<Room?> GetRoomById(int id)
        {
            return await _appDbContext.Rooms.FirstOrDefaultAsync(x => x.Id == id);
            
        }
        public async Task<List<Booking>> GetRoomBooking(int roomId, DateOnly date)
        {
            return await _appDbContext.Bookings
                .Where(x => x.RoomId == roomId && x.BookingDate == date && x.Status == BookingStatus.Active)
                .OrderBy(x => x.StartTime).ToListAsync();
            
        }
        public async Task<Room> CreateRoom(RoomViewModel model)
        {
            var room = new Room
            {
                Name = model.Name,
                Capacity = model.Capacity,
                Location = model.Location,
            };
            await _appDbContext.Rooms.AddAsync(room);
            _appDbContext.SaveChanges();
            return room;
        }
        public async Task<bool> DeleteRoom(int roomId)
        {
            var room = await _appDbContext.Rooms.FindAsync(roomId);
            if(room == null)
            {
                return false;
            }
            _appDbContext.Rooms.Remove(room);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
    }
}
