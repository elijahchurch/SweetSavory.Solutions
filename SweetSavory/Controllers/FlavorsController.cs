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

        public ActionResult Details (int id)
        {
        Flavor flavorModel = _db.Flavors
                    .Include(entry => entry.JoinTable)
                    .ThenInclude(entry => entry.Treat)
                    .FirstOrDefault(entry => entry.FlavorId == id);
        ViewBag.Title = flavorModel.Name;
        return View(flavorModel);
        }

        [HttpPost]
        public ActionResult DeleteJoin(int joinId)
        {
            TreatFlavor joinEntry = _db.TreatFlavors.FirstOrDefault(joinEntry => joinEntry.TreatFlavorId == joinId);
            _db.TreatFlavors.Remove(joinEntry);
            _db.SaveChanges();
            return RedirectToAction("Details", new {id = joinEntry.FlavorId});
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Edit Flavor";
            Flavor flavorModel = _db.Flavors.FirstOrDefault(entry => entry.FlavorId == id);
            return View(flavorModel);
        }

        [HttpPost]
        public ActionResult Edit(Flavor flavor)
        {
            if (!ModelState.IsValid)
            {
                return View(flavor);
            }
            else
            {
            _db.Flavors.Update(flavor);
            _db.SaveChanges();
            return RedirectToAction("Details", new {id = flavor.FlavorId});
            }
        }

        [Authorize]
        public ActionResult Delete(int id)
        {
        ViewBag.Title = "Delete Flavor";
        Flavor flavorModel = _db.Flavors.FirstOrDefault(entry => entry.FlavorId == id);
        return View(flavorModel);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirm(int id)
        {
            Flavor targetFlavor = _db.Flavors.FirstOrDefault(entry => entry.FlavorId == id);
            _db.Flavors.Remove(targetFlavor);
            _db.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

    }


}