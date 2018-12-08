using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClassicGarage.DAL;
using ClassicGarage.Models;

namespace ClassicGarage.Controllers
{
    public class CarController : Controller
    {
        private GarageContext db = new GarageContext();

        //GET:Car/autanaaukcji
        public ActionResult Autanaaukcji()
        {
            var market = from d in db.Market where d.Active.Equals(true) select d.CarId;
            var owner = from o in db.Owner where o.Email.Equals(User.Identity.Name)select o.ID;
            var auto = from c in db.Cars where market.Contains(c.ID)&&owner.Contains(c.OwnerID) select c;
            return View(auto.ToList());
        }

        // GET: Car
        public ActionResult Index()
        {
            string query = "SELECT * FROM OwnerModels WHERE Email = @email";

            try
            {
                OwnerModels owner = db.Owner.SqlQuery(query, new SqlParameter("@email", User.Identity.Name)).FirstOrDefault();
                Console.Out.WriteLine(User.Identity.Name);
                ViewBag.OwnerId = owner.ID;
                ViewBag.exist = true;
                var cars = db.Cars.Include(c => c.Owner).Where(c=>c.OwnerID ==owner.ID);
                return View(cars.ToList());
            }
            catch (Exception e)
            {
                ViewBag.exist = false;
                return View();
            }
            
            
     
           
        }



        // GET: Car/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarModels carModels = db.Cars.Find(id);
            if (carModels == null)
            {
                return HttpNotFound();
            }
            return View(carModels);
        }

        // GET: Car/Create
        public ActionResult Create()
        {
            string query = "SELECT * FROM OwnerModels WHERE Email = @email";

            try
            {
                OwnerModels owner = db.Owner.SqlQuery(query, new SqlParameter("@email", User.Identity.Name)).FirstOrDefault();
                Console.Out.WriteLine(User.Identity.Name);
                ViewBag.OwnerId = owner.ID;
                ViewBag.exist = true;
            }
            catch (Exception e)
            {
                ViewBag.exist = false;
            }
            return View();
        }

        // POST: Car/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Brand,Model,Year,Vin,SerialNo,Photo,PurchaseDate,PurchasePrice,Budget,OwnerID")] CarModels carModels)
        {
            if (ModelState.IsValid)
            {

                carModels.SaleDate = carModels.PurchaseDate;
                ClassicGarage.Models.MarketModel market=new MarketModel();
             
               
                db.Cars.Add(carModels);
             
                db.SaveChanges();
                db.Market.Add(market);
                var id =( from d in db.Cars where d.Vin.Equals(carModels.Vin) select d.ID).First();
                market.CarId = id ;
                market.Active = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            
            return View(carModels);
        }

        // GET: Car/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarModels carModels = db.Cars.Find(id);
            if (carModels == null)
            {
                return HttpNotFound();
            }
            ViewBag.OwnerID = new SelectList(db.Owner, "ID", "Name", carModels.OwnerID);
            return View(carModels);
        }

        // POST: Car/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Brand,Model,Year,Vin,SerialNo,Photo,PurchaseDate,SaleDate,PurchasePrice,SalePrice,Budget,OwnerID")] CarModels carModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerID = new SelectList(db.Owner, "ID", "Name", carModels.OwnerID);
            return View(carModels);
        }

        // GET: Car/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarModels carModels = db.Cars.Find(id);
            if (carModels == null)
            {
                return HttpNotFound();
            }
            return View(carModels);
        }

        // POST: Car/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CarModels carModels = db.Cars.Find(id);
            db.Cars.Remove(carModels);
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
