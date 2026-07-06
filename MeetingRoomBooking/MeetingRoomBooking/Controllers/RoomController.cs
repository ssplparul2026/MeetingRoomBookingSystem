using MeetingRoomBooking.Models;
using MeetingRoomBooking.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MeetingRoomBooking.Controllers
{
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;
        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var rooms =  await _roomService.GetAllRooms();
                
                return View(rooms);
            }catch(Exception)
            {
                ModelState.AddModelError("", "Unable to load meeting rooms");
                return View(new List<Room>());
            }
           
        }

        [HttpGet]
        public async Task<IActionResult> Details(int roomId, DateOnly? date)
        {
            try
            {
                var room = await _roomService.GetRoomById(roomId);
                if(room == null)
                {
                    return NotFound();
                }
                DateOnly selectedDate = date ?? DateOnly.FromDateTime(DateTime.Today);
                var bookings = await _roomService.GetRoomBooking(roomId, selectedDate);
                ViewBag.SelectedDate = selectedDate;
                ViewBag.Bookings = bookings;
                return View(room);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Unable to load meeting rooms");
                return View();
            }
        }
    }
}
