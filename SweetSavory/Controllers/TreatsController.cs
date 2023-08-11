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

        public ActionResult Details(int id)
        {
            Treat treatModel = _db.Treats
                        .Include(entry => entry.JoinTable)
                        .ThenInclude(entry => entry.Flavor)
                        .FirstOrDefault(entry => entry.TreatId == id);
            ViewBag.Title = treatModel.Name;
            ViewBag.FlavorId = new SelectList(_db.Flavors, "FlavorId", "Name");
            return View(treatModel);
        }

        [HttpPost]
        public ActionResult AddFlavor(Treat treat, int flavorId)
        {
            #nullable enable
            TreatFlavor? joinEntry = _db.TreatFlavors.FirstOrDefault(entry  =>(entry.TreatId == treat.TreatId && entry.FlavorId == flavorId));
            #nullable disable
            if (joinEntry == null && flavorId != 0)
            {
                _db.TreatFlavors.Add(new TreatFlavor() { TreatId = treat.TreatId, FlavorId = flavorId });
                _db.SaveChanges();
            }
            return RedirectToAction("Details", new {id = treat.TreatId});
        }

        [HttpPost]
        public ActionResult DeleteJoin(int joinId)
        {
            TreatFlavor joinEntry = _db.TreatFlavors.FirstOrDefault(joinEntry => joinEntry.TreatFlavorId == joinId);
            _db.TreatFlavors.Remove(joinEntry);
            _db.SaveChanges();
            return RedirectToAction("Details", new {id = joinEntry.TreatId});
        }

    }


}