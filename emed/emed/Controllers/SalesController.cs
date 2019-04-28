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
    public class SalesController : Controller
    {
        private DB53Entities db = new DB53Entities();

        // GET: Sales
        public ActionResult Index()
        {
            var sales = db.Sales.Include(s => s.Customer).Include(s => s.MedicinePotency).Include(s => s.Staff);
            return View(sales.ToList());
        }

        // GET: Sales/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // GET: Sales/Create
        public ActionResult Create()
        {
            ViewBag.Customer_Id = new SelectList(db.Customers, "Customer_Id", "Customer_Name");
            //ViewBag.MedPot_Id = new SelectList(db.MedicinePotencies, "MedPot_Id", "MedPot_Id");
            ViewBag.Medicine_Id = new SelectList(db.Medicines, "Medicine_Id", "Name");
            ViewBag.Potency_Id = new SelectList(db.Potencies, "Potency_Id", "Potency_mg");
            ViewBag.Staff_Id = new SelectList(db.People, "Id", "FirstName");
            return View();
        }

        // POST: Sales/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Sold_Id,NoOfItem,Date,Staff_Id,Customer_Id,Medicine_Id,Potency_Id")] SalesObjectclass salesobj)
        {
            Sale sale = new Sale();
            MedicinePotency medpot = db.MedicinePotencies.FirstOrDefault(u => u.Medicine_Id == salesobj.Medicine_Id && u.Potency_Id == salesobj.Potency_Id);
            if (medpot != null)
            {
                if (medpot.NoOfItem >= salesobj.NoOfItem)
                {
                    if (ModelState.IsValid)
                    {
                        sale.MedPot_Id = medpot.MedPot_Id;
                        sale.NoOfItem = salesobj.NoOfItem;
                        sale.Staff_Id = salesobj.Staff_Id;
                        sale.Date = salesobj.Date;
                        sale.Customer_Id = salesobj.Customer_Id;


                        db.Sales.Add(sale);
                        db.SaveChanges();
                        medpot.NoOfItem = (medpot.NoOfItem) - (salesobj.NoOfItem);
                        db.Entry(medpot).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    ModelState.AddModelError("NoOfItem","Medicine is out of stock.");
                    ViewBag.Customer_Id = new SelectList(db.Customers, "Customer_Id", "Customer_Name", salesobj.Customer_Id);
                    ViewBag.Medicine_Id = new SelectList(db.Medicines, "Medicine_Id", "Name", salesobj.Medicine_Id);
                    ViewBag.Potency_Id = new SelectList(db.Potencies, "Potency_Id", "Potency_mg", salesobj.Potency_Id);
                    ViewBag.Staff_Id = new SelectList(db.People, "Id", "FirstName", salesobj.Staff_Id);
                    return View(salesobj);

                }
            }
            ViewBag.Customer_Id = new SelectList(db.Customers, "Customer_Id", "Customer_Name", salesobj.Customer_Id);
            ViewBag.Medicine_Id = new SelectList(db.Medicines, "Medicine_Id", "Name", salesobj.Medicine_Id);
            ViewBag.Potency_Id = new SelectList(db.Potencies, "Potency_Id", "Potency_mg", salesobj.Potency_Id);
            ViewBag.Staff_Id = new SelectList(db.People, "Id", "FirstName", salesobj.Staff_Id);
            return View(salesobj);
        }

        // GET: Sales/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SalesObjectclass salesobj = new SalesObjectclass();
            
            Sale sale = db.Sales.Find(id);

            salesobj.Sold_Id = sale.Sold_Id;
            MedicinePotency medpot = db.MedicinePotencies.FirstOrDefault(mp => mp.MedPot_Id == sale.MedPot_Id);
            salesobj.Medicine_Id = medpot.Medicine_Id;
            salesobj.Potency_Id = medpot.Potency_Id;
            salesobj.NoOfItem = sale.NoOfItem;
            salesobj.Staff_Id = sale.Staff_Id;
            salesobj.Date = sale.Date;
            salesobj.Customer_Id = sale.Customer_Id;
            if (sale == null)
            {
                return HttpNotFound();
            }
            ViewBag.Customer_Id = new SelectList(db.Customers, "Customer_Id", "Customer_Name", sale.Customer_Id);
            ViewBag.MedPot_Id = new SelectList(db.MedicinePotencies, "MedPot_Id", "MedPot_Id", sale.MedPot_Id);
            ViewBag.Medicine_Id = new SelectList(db.Medicines, "Medicine_Id", "Name", sale.MedicinePotency.Medicine.Medicine_Id);
            ViewBag.Potency_Id = new SelectList(db.Potencies, "Potency_Id", "Potency_mg", sale.MedicinePotency.Potency.Potency_Id);
            //ViewBag.Staff_Id = new SelectList(db.Staffs, "Id", "Name", sale.Staff.Person.Id);
            ViewBag.Staff_Id = new SelectList(db.People, "Id", "FirstName", sale.Staff.Person.Id);
            return View(salesobj);
        }

        // POST: Sales/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Sold_Id,NoOfItem,Date,Staff_Id,Customer_Id,Medicine_Id,Potency_Id")] SalesObjectclass salesobj)
        {
            Sale sale = db.Sales.FirstOrDefault(u => u.Customer_Id == salesobj.Customer_Id);
            
            MedicinePotency medpot = db.MedicinePotencies.FirstOrDefault(u => u.Medicine_Id == salesobj.Medicine_Id && u.Potency_Id == salesobj.Potency_Id);
            if (medpot != null)
            {
                if (medpot.NoOfItem >= salesobj.NoOfItem)
                {
                    if (ModelState.IsValid)
                    {
                        sale.MedPot_Id = medpot.MedPot_Id;
                        sale.NoOfItem = salesobj.NoOfItem;
                        sale.Staff_Id = salesobj.Staff_Id;
                        sale.Date = salesobj.Date;
                        sale.Customer_Id = salesobj.Customer_Id;
                        if (salesobj.NoOfItem <= sale.NoOfItem)
                        {
                            int diff = sale.NoOfItem - salesobj.NoOfItem;
                            medpot.NoOfItem = (medpot.NoOfItem) + (diff);
                            db.Entry(sale).State = EntityState.Modified;
                            db.Entry(medpot).State = EntityState.Modified;
                            db.SaveChanges();

                        }

                        else
                        {
                            db.Sales.Add(sale);
                            db.SaveChanges();
                            medpot.NoOfItem = (medpot.NoOfItem) - (salesobj.NoOfItem);
                            db.Entry(medpot).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                        return RedirectToAction("Index");

                    }
                }
                else
                {
                    ModelState.AddModelError("NoOfItem", "Medicine is out of stock.");
                    ViewBag.Customer_Id = new SelectList(db.Customers, "Customer_Id", "Customer_Name", salesobj.Customer_Id);
                    ViewBag.Medicine_Id = new SelectList(db.Medicines, "Medicine_Id", "Name", salesobj.Medicine_Id);
                    ViewBag.Potency_Id = new SelectList(db.Potencies, "Potency_Id", "Potency_mg", salesobj.Potency_Id);
                    ViewBag.Staff_Id = new SelectList(db.People, "Id", "FirstName", salesobj.Staff_Id);
                    return View(salesobj);

                }
            }
            ViewBag.Customer_Id = new SelectList(db.Customers, "Customer_Id", "Customer_Name", salesobj.Customer_Id);
            ViewBag.Medicine_Id = new SelectList(db.Medicines, "Medicine_Id", "Name", salesobj.Medicine_Id);
            ViewBag.Potency_Id = new SelectList(db.Potencies, "Potency_Id", "Potency_mg", salesobj.Potency_Id);
            ViewBag.Staff_Id = new SelectList(db.People, "Id", "FirstName", salesobj.Staff_Id);
            return View(salesobj);
            /* if (ModelState.IsValid)
             {
                 Sale sale = new Sale();
                 db.Entry(sale).State = EntityState.Modified;
                 db.SaveChanges();
                 return RedirectToAction("Index");
             }
             ViewBag.Customer_Id = new SelectList(db.Customers, "Customer_Id", "Customer_Name", sale.Customer_Id);
             ViewBag.MedPot_Id = new SelectList(db.MedicinePotencies, "MedPot_Id", "MedPot_Id", sale.MedPot_Id);
             ViewBag.Staff_Id = new SelectList(db.Staffs, "Staff_Id", "Designation", sale.Staff_Id);
             return View(sale);*/
        }

        // GET: Sales/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sale sale = db.Sales.Find(id);
            if (sale == null)
            {
                return HttpNotFound();
            }
            return View(sale);
        }

        // POST: Sales/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sale sale = db.Sales.Find(id);
            db.Sales.Remove(sale);
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
