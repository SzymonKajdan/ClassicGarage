using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClassicGarage.DAL;
using ClassicGarage.Models;

namespace ClassicGarage.Controllers
{
    public class PartController : Controller
    {
        private GarageContext db = new GarageContext();

        // GET: Part
        public ActionResult Index()
        {
            return View(db.Parts.ToList());
        }

        // GET: Part/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartModels partModels = db.Parts.Find(id);
            if (partModels == null)
            {
                return HttpNotFound();
            }
            return View(partModels);
        }

        // GET: Part/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Part/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CarID,Name,CatNo,PurchasePrice,PurchaseSale,PurchaseDate,SaleDate")] PartModels partModels)
        {
            if (ModelState.IsValid)
            {
                db.Parts.Add(partModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(partModels);
        }

        // GET: Part/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartModels partModels = db.Parts.Find(id);
            if (partModels == null)
            {
                return HttpNotFound();
            }
            return View(partModels);
        }

        // POST: Part/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CarID,Name,CatNo,PurchasePrice,PurchaseSale,PurchaseDate,SaleDate")] PartModels partModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(partModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(partModels);
        }

        // GET: Part/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PartModels partModels = db.Parts.Find(id);
            if (partModels == null)
            {
                return HttpNotFound();
            }
            return View(partModels);
        }

        // POST: Part/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PartModels partModels = db.Parts.Find(id);
            db.Parts.Remove(partModels);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
