using Microsoft.AspNetCore.Mvc;

namespace hostel_management.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Rooms()
        {
            return View();
        }  
       
      
    } 
}
