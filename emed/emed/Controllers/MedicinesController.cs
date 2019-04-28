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
    public class MedicinesController : Controller
    {
        private DB53Entities db = new DB53Entities();

        // GET: Medicines
        public ActionResult Index()
        {
            var medicines = db.Medicines.Include(m => m.Category).Include(m => m.Supplier);
            return View(medicines.ToList());
        }

        // GET: Medicines/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicine medicine = db.Medicines.Find(id);
            if (medicine == null)
            {
                return HttpNotFound();
            }
            return View(medicine);
        }

        // GET: Medicines/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "Category_Id", "Category_Name");
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "CompanyName");
            return View();
        }

        // POST: Medicines/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Medicine_Id,Name,Description,ExpiryDate,CategoryID,SupplierID")] Medicine medicine)
        {
            Medicine medicines = db.Medicines.FirstOrDefault(u => u.Name == medicine.Name);

            if (ModelState.IsValid)
            {
                if (medicines == null)
                {
                    db.Medicines.Add(medicine);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("Name","Medicine with same name is Alredy Present.");
                    ViewBag.CategoryID = new SelectList(db.Categories, "Category_Id", "Category_Name", medicine.CategoryID);
                    ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "CompanyName", medicine.SupplierID);
                    return View(medicine);

                }
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "Category_Id", "Category_Name", medicine.CategoryID);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "CompanyName", medicine.SupplierID);
            return View(medicine);
        }

        // GET: Medicines/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicine medicine = db.Medicines.Find(id);
            if (medicine == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "Category_Id", "Category_Name", medicine.CategoryID);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "CompanyName", medicine.SupplierID);
            return View(medicine);
        }

        // POST: Medicines/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Medicine_Id,Name,Description,ExpiryDate,CategoryID,SupplierID")] Medicine medicine)
        {
            if (ModelState.IsValid)
            {
                db.Entry(medicine).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "Category_Id", "Category_Name", medicine.CategoryID);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "CompanyName", medicine.SupplierID);
            return View(medicine);
        }

        // GET: Medicines/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Medicine medicine = db.Medicines.Find(id);
            if (medicine == null)
            {
                return HttpNotFound();
            }
            return View(medicine);
        }

        // POST: Medicines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            Medicine medicine = db.Medicines.Find(id);
            MedicinePotency medicinePotency = db.MedicinePotencies.FirstOrDefault(u => u.Medicine_Id == medicine.Medicine_Id);
            if(medicinePotency == null)
            {
                db.Medicines.Remove(medicine);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            ModelState.AddModelError("Name","Medicineis present in Medicine Potency.");
            ViewBag.CategoryID = new SelectList(db.Categories, "Category_Id", "Category_Name", medicine.CategoryID);
            ViewBag.SupplierID = new SelectList(db.Suppliers, "SupplierID", "CompanyName", medicine.SupplierID);
            return View(medicine);
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
