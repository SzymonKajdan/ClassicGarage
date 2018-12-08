using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClassicGarage.DAL;
using ClassicGarage.Models;

namespace ClassicGarage.Controllers
{
    public class MarketController : Controller
    {
        private GarageContext db = new GarageContext();

        // GET: Market
        public ActionResult Index()
        {
            
            var market = from d in db.Market where d.Active.Equals(true) select d.CarId;
            var cars = from c in db.Cars where market.Contains(c.ID) select c;


            return View(cars.ToList());
        }

        // GET: Market/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MarketModel marketModel = db.Market.Find(id);
            if (marketModel == null)
            {
                return HttpNotFound();
            }
            return View(marketModel);
        }

        // GET: Market/Create
        public ActionResult Create()
        {
            IList<int> cars = new List<int>();
            var market = from d in db.Market where d.Active.Equals(false) select d.CarId;
            var owner = from o in db.Owner where o.Email.Equals(User.Identity.Name) select o.ID;
   
            var cars1 = from c in db.Cars where owner.Contains(c.OwnerID) && market.Contains(c.ID)  select c;
            
            //cars= cars1.ToList();
            Debug.WriteLine("something");
            foreach (var car in cars1)
            {
                Debug.WriteLine("car"+car.Model+" "+car.ID);
            }
          Debug.WriteLine("something");
            ViewData["samochody"] = cars1.ToList();
            return View();
        }

        // POST: Market/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,CarId,Active")] MarketModel marketModel)
        {
            if (ModelState.IsValid)
            {
                 MarketModel m = (from d in db.Market where d.CarId.Equals(marketModel.CarId) select d).First();
                Debug.Write(m.ID);
                m.Active = true;
                db.Entry(m).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(marketModel);
            // tutaj musze zrobic zeby byla edycja ceny dodanych
        }

        // GET: Market/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MarketModel marketModel = db.Market.Find(id);
            if (marketModel == null)
            {
                return HttpNotFound();
            }
            return View(marketModel);
        }

        // POST: Market/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,CarId,Active")] MarketModel marketModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(marketModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(marketModel);
        }

        // GET: Market/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MarketModel marketModel = db.Market.Find(id);
            if (marketModel == null)
            {
                return HttpNotFound();
            }
            return View(marketModel);
        }

        // POST: Market/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MarketModel marketModel = db.Market.Find(id);
            db.Market.Remove(marketModel);
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
