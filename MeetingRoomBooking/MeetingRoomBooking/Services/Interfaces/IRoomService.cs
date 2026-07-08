using MeetingRoomBooking.Models;
using MeetingRoomBooking.ViewModels;

namespace MeetingRoomBooking.Services.Interfaces
{
    public interface IRoomService
    {
        Task <List<RoomViewModel>> GetAllRooms();
        Task<Room?> GetRoomById(int roomId);
        Task<List<Booking>> GetRoomBooking(int roomId, DateOnly date);

        Task<Room> CreateRoom(RoomViewModel model);
        Task<bool> DeleteRoom(int roomId);
    }
}
