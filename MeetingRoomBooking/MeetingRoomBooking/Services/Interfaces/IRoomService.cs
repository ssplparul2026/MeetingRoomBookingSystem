using MeetingRoomBooking.Models;

namespace MeetingRoomBooking.Services.Interfaces
{
    public interface IRoomService
    {
        Task <List<Room>> GetAllRooms();
        Task<Room?> GetRoomById(int roomId);
        Task<List<Booking>> GetRoomBooking(int roomId, DateOnly date);
    }
}
