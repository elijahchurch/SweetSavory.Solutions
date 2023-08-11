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
    public class FlavorsController : Controller
    {
        private readonly SweetSavoryContext _db;
        public FlavorsController(SweetSavoryContext db)
        {
            _db = db;
        }
        
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Title = "Add Flavor";
            return View();
        }
        [HttpPost]
        public ActionResult Create(Flavor flavor)
        {
            if (!ModelState.IsValid)
            {
                return View(flavor);
            }
            else
            {
                _db.Flavors.Add(flavor);
                _db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
        }
    }


}