using Microsoft.AspNetCore.Mvc;

namespace MeetingRoomBooking.Controllers
{
    public class RoomController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
