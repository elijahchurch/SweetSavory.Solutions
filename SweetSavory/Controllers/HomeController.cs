using Microsoft.AspNetCore.Mvc;
using SweetSavory.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace SweetSavory.Controllers
{
    public class HomeController : Controller
    {
        private readonly SweetSavoryContext _db;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(UserManager<ApplicationUser> userManager, SweetSavoryContext db)
        {
            _userManager = userManager;
            _db = db;
        }

        [HttpGet("/")]
        public ActionResult Index()
        {
            List<Treat> treats = _db.Treats.ToList();
            List<Flavor> flavors = _db.Flavors.ToList();
            ViewBag.Treats = treats;
            ViewBag.Flavors = flavors;
            ViewBag.Title = "Sweet and Savory";
            return View();
        }
    }
}