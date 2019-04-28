using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using emed.Models;

namespace emed.Controllers
{
    public class MedicinePotenciesController : Controller
    {
        private DB53Entities db = new DB53Entities();

        // GET: MedicinePotencies
        public ActionResult Index()
        {
            var medicinePotencies = db.MedicinePotencies.Include(m => m.Medicine).Include(m => m.Potency);
            return View(medicinePotencies.ToList());
        }

        // GET: MedicinePotencies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicinePotency medicinePotency = db.MedicinePotencies.Find(id);
            if (medicinePotency == null)
            {
                return HttpNotFound();
            }
            return View(medicinePotency);
        }

        // GET: MedicinePotencies/Create
        public ActionResult Create()
        {
            ViewBag.Medicine_Id = new SelectList(db.Medicines, "Medicine_Id", "Name");
            ViewBag.Potency_Id = new SelectList(db.Potencies, "Potency_Id", "Potency_mg");
            
            return View();
        }

        // POST: MedicinePotencies/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MedPot_Id,Medicine_Id,Potency_Id,Price,ExpiryDate,NoOfItem")] MedicinePotency medicinePotency)
        {
            if (ModelState.IsValid)
            {
                MedicinePotency meddpot = db.MedicinePotencies.FirstOrDefault(u => u.Medicine_Id == (medicinePotency.Medicine_Id) && u.Potency_Id == medicinePotency.Potency_Id);

                if (meddpot == null)
                {
                    db.MedicinePotencies.Add(medicinePotency);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Medicine_Id","Medicine with given potency is alredy present in Record.");
                    ViewBag.Medicine_Id = new SelectList(db.Medicines, "Medicine_Id", "Name", medicinePotency.Medicine_Id);
                    ViewBag.Potency_Id = new SelectList(db.Potencies, "Potency_Id", "Potency_Id", medicinePotency.Potency_Id);
                    return View(medicinePotency);
                }
            }

            ViewBag.Medicine_Id = new SelectList(db.Medicines, "Medicine_Id", "Name", medicinePotency.Medicine_Id);
            ViewBag.Potency_Id = new SelectList(db.Potencies, "Potency_Id", "Potency_Id", medicinePotency.Potency_Id);
            return View(medicinePotency);
        }

        // GET: MedicinePotencies/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicinePotency medicinePotency = db.MedicinePotencies.Find(id);
            if (medicinePotency == null)
            {
                return HttpNotFound();
            }
            ViewBag.Medicine_Id = new SelectList(db.Medicines, "Medicine_Id", "Name", medicinePotency.Medicine_Id);
            ViewBag.Potency_Id = new SelectList(db.Potencies, "Potency_Id", "Potency_mg", medicinePotency.Potency_Id);
            return View(medicinePotency);
        }

        // POST: MedicinePotencies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MedPot_Id,Medicine_Id,Potency_Id,Price,ExpiryDate,NoOfItem")] MedicinePotency medicinePotency)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicinePotency).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Medicine_Id = new SelectList(db.Medicines, "Medicine_Id", "Name", medicinePotency.Medicine_Id);
            ViewBag.Potency_Id = new SelectList(db.Potencies, "Potency_Id", "Potency_Id", medicinePotency.Potency_Id);
            return View(medicinePotency);
        }

        // GET: MedicinePotencies/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MedicinePotency medicinePotency = db.MedicinePotencies.Find(id);
            if (medicinePotency == null)
            {
                return HttpNotFound();
            }
            return View(medicinePotency);
        }

        // POST: MedicinePotencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            MedicinePotency medicinePotency = db.MedicinePotencies.Find(id);
            Sale sale = db.Sales.FirstOrDefault(u => u.MedPot_Id == medicinePotency.MedPot_Id);
            if(sale == null)
            {
                db.MedicinePotencies.Remove(medicinePotency);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            else
            {
                ModelState.AddModelError("Medicine_Id", "It is bounded with Sales.");
                ViewBag.Medicine_Id = new SelectList(db.Medicines, "Medicine_Id", "Name", medicinePotency.Medicine_Id);
                ViewBag.Potency_Id = new SelectList(db.Potencies, "Potency_Id", "Potency_Id", medicinePotency.Potency_Id);
                return View(medicinePotency);

            }
            
            
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }


        //potency
        //get
        public ActionResult AddPotency()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddPotency([Bind(Include = "Potency_Id,Potency_mg")] Potency potency)
        {
            Potency persuser = db.Potencies.FirstOrDefault(u => u.Potency_mg == (potency.Potency_mg));
            if (persuser == null)
            {
                db.Potencies.Add(potency);
                db.SaveChanges();
                return RedirectToAction("ListPotencies");
            }
            else
                ModelState.AddModelError("Potency_mg", "Potency Can not be added as it already exists.");
            return View();
        }

        public ActionResult ListPotencies()
        {
            return View(db.Potencies.ToList());
        }
        // GET: Potencies/Delete/5
        public ActionResult DeletePotency(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Potency potency = db.Potencies.Find(id);
            if (potency == null)
            {
                return HttpNotFound();
            }

            return View(potency);
        }

        // POST: Potencies/Delete/5
        [HttpPost, ActionName("DeletePotency")]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePotencyConfirmed(int id)
        {
            Potency potency = db.Potencies.Find(id);
            MedicinePotency meddd = db.MedicinePotencies.FirstOrDefault(u => u.Potency_Id == potency.Potency_Id);
            if (meddd != null)
            {
                ModelState.AddModelError("Potency_mg", "This Potency can not be Deleted Because some medicines has this potency.");
                return View();

            }

            else
            {
                db.Potencies.Remove(potency);
                db.SaveChanges();
                return RedirectToAction("ListPotencies");
            }
        }
    }
}
