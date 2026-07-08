using MeetingRoomBooking.Models;
using MeetingRoomBooking.Services.Interfaces;
using MeetingRoomBooking.ViewModels;
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
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RoomViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }
                var room = await _roomService.CreateRoom(model);
                return RedirectToAction("Index","Room");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Unable to create room");
                return View();

            }

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
                return View(new List<RoomViewModel>());
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
                var model = new RoomBookingViewModel
                {
                    RoomId = roomId,
                    RoomName = room.Name,
                    Capacity = room.Capacity,
                    Location = room.Location,
                    Bookings = bookings,
                    SelectedDate = selectedDate

                };
                return View(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Unable to load meeting rooms");
                return View(new RoomBookingViewModel());
            }
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int roomId)
        {
            try
            {
                var room = await _roomService.GetRoomById(roomId);
                if(room == null)
                {
                    return NotFound();
                }
                var model = new RoomViewModel
                {
                    Id = room.Id,
                    Name = room.Name,
                    Capacity = room.Capacity,
                    Location = room.Location
                };
                return View(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Unable to load room detail");
                return RedirectToAction("Index","Room");
            }
        
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteRoom(int roomId)
        {
            try
            {
                bool result = await _roomService.DeleteRoom(roomId);
                if (!result)
                {
                    ModelState.AddModelError("", "Room not found");
                }
                return RedirectToAction("Index", "Room");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Unable to delete the room.");
                return RedirectToAction("Index", "Room");
            }
        }
    }
}
