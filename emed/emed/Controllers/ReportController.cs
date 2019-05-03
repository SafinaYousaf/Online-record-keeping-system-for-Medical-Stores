using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using emed.Models;
using CrystalDecisions.CrystalReports.Engine;


namespace emed.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AllSales()
        {
            emed.Models.DB53Entities db = new emed.Models.DB53Entities();
            //CrMVCApp.Models.Customer c;
            //var c = (from b in db.AllSales select b).ToList();
            var c = db.AllSales.ToList();

            AllSales rpt = new AllSales();
            rpt.Load();
            rpt.SetDataSource(c);
            Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "C:\\Users\\M.ALI\\Documents\\ProjectAreports\\AllSalesReport.pdf");

        }
        public ActionResult AllSalesAction()
        {
            return AllSales();

        }
        
        public ActionResult MedicineInfo()
        {
            emed.Models.DB53Entities db = new emed.Models.DB53Entities();
            //CrMVCApp.Models.Customer c;
            var c = (from b in db.MedicineInfoes select b).ToList();

            MedicineInfo rpt = new MedicineInfo();
            rpt.Load();
            rpt.SetDataSource(c);
            Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "C:\\Users\\M.ALI\\Documents\\ProjectAreports\\MedicineInfoReport.pdf");

        }
        public ActionResult MedicineInfoAction()
        {
            return MedicineInfo();

        }

       
        

        public ActionResult StaffInfo()
        {
            emed.Models.DB53Entities db = new emed.Models.DB53Entities();
            //CrMVCApp.Models.Customer c;
            var e = db.StaffInfoes.ToList();

            StaffInfo rpt = new StaffInfo();
            rpt.Load();
            rpt.SetDataSource(e);
            Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "C:\\Users\\M.ALI\\Documents\\ProjectAreports\\StaffCountReport.pdf");

        }
        public ActionResult StaffInfoAction()
        {
            return StaffInfo();

        }


        //number of items present in stock
        public ActionResult NoOfItem()
        {
            emed.Models.DB53Entities db = new emed.Models.DB53Entities();
            //CrMVCApp.Models.Customer c;
            var e = db.NoOfItemsInstocks.ToList();

            NoOfItemsInstock rpt = new NoOfItemsInstock();
            rpt.Load();
            rpt.SetDataSource(e);
            Stream s = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            return File(s, "C:\\Users\\M.ALI\\Documents\\ProjectAreports\\StaffCountReport.pdf");

        }
        public ActionResult NoOfItemAction()
        {
            return NoOfItem();

        }

        


    }
}