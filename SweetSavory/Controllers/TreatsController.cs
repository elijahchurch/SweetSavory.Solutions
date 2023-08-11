using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using SweetSavory.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace SweetSavory.Controllers
{
    public class TreatsController : Controller
    {
        private readonly SweetSavoryContext _db;
        public TreatsController(SweetSavoryContext db)
        {
            _db = db;
        }
        
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Title = "Add Treat";
            return View();
        }
        [HttpPost]
        public ActionResult Create(Treat treat)
        {
            if (!ModelState.IsValid)
            {
                return View(treat);
            }
            else
            {
                _db.Treats.Add(treat);
                _db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
        }
    }


}