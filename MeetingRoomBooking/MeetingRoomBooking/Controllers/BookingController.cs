using MeetingRoomBooking.Models;
using MeetingRoomBooking.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MeetingRoomBooking.Controllers
{
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;
        private readonly IRoomService _roomService;
        public BookingController(IBookingService bookingService, IRoomService roomService)
        {
            _bookingService = bookingService;
            _roomService = roomService;
        }
      
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                ViewBag.Rooms = await _roomService.GetAllRooms();
                return View(new Booking
                {
                    BookingDate = DateOnly.FromDateTime(DateTime.Today)
                });
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Unable to load booking page");
                ViewBag.Rooms = new List<Room>();
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBooking(Booking booking)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.Rooms = await _roomService.GetAllRooms();
                    return View(booking);
                }
                var result = await _bookingService.CreateBooking(booking);
                if (!result.Success)
                {
                    ModelState.AddModelError("", result.message);
                    ViewBag.Rooms = await _roomService.GetAllRooms();
                    return View(booking);
                }
                return RedirectToAction("Index", "Room");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Unable to create booking");
                ViewBag.Rooms = await _roomService.GetAllRooms();
                return View(booking);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Index(DateOnly? date)
        {
            try
            {
                DateOnly selectedDate = date ?? DateOnly.FromDateTime(DateTime.Today);
                var bookings = await _bookingService.GetBookingByDate(selectedDate);
                ViewBag.SelectedDate = selectedDate;
                return View(bookings);
            }
            catch(Exception)
            {
                ModelState.AddModelError("", "Unable to load bookings.");

                return View(new List<Booking>());
            }
        }
        [HttpGet]
        public async Task<IActionResult> MyBookings(string bookedBy)
        {
            try
            {
                if (string.IsNullOrEmpty(bookedBy))
                {
                    ModelState.AddModelError("", "bookedBy field is required");

                    return View(new List<Booking>());
                }
                var myBookings = await _bookingService.GetMyBookings(bookedBy);
                return View(myBookings);
            }
            catch(Exception)
            {
                ModelState.AddModelError("", "Unable to load your bookings.");

                return View(new List<Booking>());
            }
        }
        [HttpPost]
        public async Task<IActionResult> Cancel(int bookingId)
        {
            try
            {
                bool cancelled = await _bookingService.CancelBooking(bookingId);
                if (!cancelled)
                {
                    ModelState.AddModelError("", "booking not found");
                }
                return RedirectToAction("Index", "Booking");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Unable to cancel booking.");

                return RedirectToAction("Index", "Booking");
            }
        }
    }
}
