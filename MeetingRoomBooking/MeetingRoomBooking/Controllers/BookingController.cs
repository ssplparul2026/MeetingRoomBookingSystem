using MeetingRoomBooking.Models;
using MeetingRoomBooking.Services.Interfaces;
using MeetingRoomBooking.ViewModels;
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
        public async Task<IActionResult> Create(int? roomId)
        {
            try
            {
                var rooms  = await _roomService.GetAllRooms();
                return View(new BookingViewModel
                {
                    BookingDate = DateOnly.FromDateTime(DateTime.Today),
                    Rooms = rooms,
                    RoomId = roomId ?? 0

                });
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Unable to load booking page");
                return View(new BookingViewModel
                {
                    BookingDate = DateOnly.FromDateTime(DateTime.Today),
                    Rooms = new List<RoomViewModel>()
                });
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateBooking(BookingViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    model.Rooms = await _roomService.GetAllRooms();
                    return View("Create",model);
                }
                var booking = new Booking
                {
                    RoomId = model.RoomId,
                    BookedBy = model.BookedBy,
                    BookingDate = model.BookingDate,
                    StartTime = model.StartTime,
                    EndTime = model.EndTime,
                    Purpose = model.Purpose
                };

                var result = await _bookingService.CreateBooking(booking);
                if (!result.Success)
                {
                    var messages = result.message.Split(Environment.NewLine);

                    foreach (var message in messages)
                    {
                        ModelState.AddModelError("", message);
                    }
                    model.Rooms = await _roomService.GetAllRooms();
                    return View("Create", model);
                }
                return RedirectToAction("MyBookings", "Booking", new
                {
                    bookedBy = booking.BookedBy
                });
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Unable to create booking");
                ViewBag.Rooms = await _roomService.GetAllRooms();
                return View("Create", model);
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

                    return View(new List<MyBookingsViewModel>());
                }
                var myBookings = await _bookingService.GetMyBookings(bookedBy);
                return View(myBookings);
            }
            catch(Exception)
            {
                ModelState.AddModelError("", "Unable to load your bookings.");

                return View(new List<MyBookingsViewModel>());
            }
        }
        [HttpGet]
        public async Task<IActionResult> CancelBooking(int bookingId)
        {
            var booking = await _bookingService.GetBookingById(bookingId);

            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
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
                return RedirectToAction("MyBookings", "Booking");
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Unable to cancel booking.");

                return RedirectToAction("Index", "Booking");
            }
        }
    }
}
