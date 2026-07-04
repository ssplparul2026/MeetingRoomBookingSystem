using Microsoft.AspNetCore.Mvc;

namespace MeetingRoomBooking.Controllers
{
    public class BookingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
