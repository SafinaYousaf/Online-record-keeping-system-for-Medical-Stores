using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using emed.Models;

namespace emed.Controllers
{
    public class PeopleController : Controller
    {
        SqlConnection con = new SqlConnection(@"Data Source=SAVIRAYOUSAF;Initial Catalog=DB53;Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework");
        private DB53Entities db = new DB53Entities();

        // GET: People
        public ActionResult Index()
        {
            //show all person to admin tyhat are staff or admin but not customer
            //var people = db.People.Include(p => p.Login).Include(p => p.Staff).Where(u => u.UserType != "Customer");
            var people = db.People.Include(p => p.Login).Include(p => p.Staff);
            return View(people.ToList());
        }

        // GET: People/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // GET: People/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.Logins, "Login_Id", "Email");
            ViewBag.Id = new SelectList(db.Staffs, "Staff_Id", "Designation");
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,Contact,Email,Address,Country,DateOfBirth,Gender,Discriminator")] Person person)
        {
            string pass = "123";
            Login newuser = new Login();
            newuser.Login_Id = person.Id;
            newuser.Password = pass;
            newuser.Email = person.Email;
            newuser.Discriminator = "Admin";
            if (ModelState.IsValid)
            {
                db.People.Add(person);
                db.Logins.Add(newuser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.Logins, "Login_Id", "Email", person.Id);
            ViewBag.Id = new SelectList(db.Staffs, "Staff_Id", "Designation", person.Id);
            return View(person);
        }

        // GET: People/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Logins, "Login_Id", "Email", person.Id);
            ViewBag.Id = new SelectList(db.Staffs, "Staff_Id", "Designation", person.Id);
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,Contact,Email,Address,Country,DateOfBirth,Gender,Discriminator")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Logins, "Login_Id", "Email", person.Id);
            ViewBag.Id = new SelectList(db.Staffs, "Staff_Id", "Designation", person.Id);
            return View(person);
        }

        // GET: People/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.People.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = db.People.Find(id);
            Login log = db.Logins.Find(id);
            Staff staffuser = db.Staffs.Find(id);
            if(staffuser != null)
            {
                db.Staffs.Remove(staffuser);

            }
            if(log != null)
            {
                db.Logins.Remove(log);

            }
            
            
            db.People.Remove(person);
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

        //MINE
        public ActionResult About()
        {
            
            return View();
        }
        public ActionResult Sip()
        {
            return View();
        }
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp([Bind(Include = "FirstName,LastName,Contact,Email,Address,Country,DateOfBirth,Gender,Discriminator,Password,ConfirmPassword ")] User userrr)
        {
            using (DB53Entities db = new DB53Entities())
            {
                Person persuser = db.People.FirstOrDefault(u => u.Email == (userrr.Email));
                if(userrr.Discriminator == "Admin")
                {
                    Login oldlog = db.Logins.FirstOrDefault(u => u.Discriminator == userrr.Discriminator);
                    if (oldlog != null)
                    {
                        ModelState.AddModelError("Discriminator", "Admin is alredy present. There can be only one Admin.");
                        return View(userrr);
                    }
                }
                if (persuser == null)
                {

                    Person pers = new Person();
                    pers.Address = userrr.Address;
                    pers.Contact = userrr.Contact;
                    pers.Country = userrr.Country;
                    pers.DateOfBirth = userrr.DateOfBirth;
                    pers.Email = userrr.Email;
                    pers.FirstName = userrr.FirstName;
                    pers.LastName = userrr.LastName;
                    pers.Gender = userrr.Gender;
                    
                    db.People.Add(pers);
                    db.SaveChanges();
                    Person user = db.People.FirstOrDefault(newuser => newuser.Email == (userrr.Email));

                    Login newlog = new Login();
                    newlog.Login_Id = user.Id;
                    newlog.Email = userrr.Email;
                    newlog.Password = userrr.Password;
                    newlog.Discriminator = userrr.Discriminator;
                    db.Logins.Add(newlog);
                    db.SaveChanges();
                    return RedirectToAction("Index", "People");
                }
                else
                    return View();
            }
        }
            //Login
            public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login entity)
        {
            using (DB53Entities db = new DB53Entities())
            {
                //fetche the data of user on the basis of inserted email
                Login user = db.Logins.FirstOrDefault(u => u.Email == (entity.Email));
                
                //if entered email is not bound to any user then object will be null
                if (user == null)
                {
                    TempData["ErrorMSG"] = "object not found";
                    
                    return View(entity);

                }
                //if we are here then it mean we sucsessfuly retrived the data
                //now we compare password
                int a = entity.Password.Count();
                if (user.Password.Substring(0, a) != entity.Password)
                {
                    //TempData["ErrorMSG"] = "Password not matched";
                    ModelState.AddModelError("Password", "Password doesnot match.");
                    return View(entity);

                }

                else
                {


                    if (user.Password.Substring(0, a) == entity.Password)
                    {
                        if(user.Discriminator.Substring(0,5) == "Admin")
                            return RedirectToAction("Admindashboard", "People");
                        else if(user.Discriminator.Substring(0, 5) == "Staff")
                        {
                            Staff staffuser = db.Staffs.FirstOrDefault(u => u.Staff_Id == (user.Login_Id));
                            if (staffuser != null)
                            {
                                
                                    return RedirectToAction("StaffAbout", "Staffs");
                            }

                        }
                        else
                            return RedirectToAction("Admindashboard", "People");



                    }
                    return View();
                }
                
            }
        }
        public ActionResult ForgotPW()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPW(ForgotPW entity)
        {
            if (ModelState.IsValid)
            {
                Login user = db.Logins.FirstOrDefault(u => u.Email == (entity.Email));
                if (user != null)
                {
                    if (entity.Password == entity.ConfirmPassword)
                    {
                        SqlCommand cmd = new SqlCommand("UPDATE [Login] SET [Password] = " + entity.Password + " WHERE Login_Id = " + user.Login_Id + "", con);
                        if (con.State == ConnectionState.Closed)
                            con.Open();
                        cmd.ExecuteNonQuery();
                        
                        return RedirectToAction("Login", "People");
                    }
                    else
                    {
                        ViewBag.Error = TempData["error"];
                        return View();
                    }

                }
                ModelState.AddModelError("Email", "Email is not bounded to any Account.");
                return View(entity);
            }
            return View(entity);
        }
        public ActionResult AddCustomer()
        {
            ViewBag.Medicine_Id = new SelectList(db.Medicines, "Medicine_Id", "Name");
            ViewBag.Potency_Id = new SelectList(db.Potencies, "Potency_Id", "Potency_mg");
            ViewBag.Staff_Id = new SelectList(db.People, "Id", "FirstName");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCustomer([Bind(Include = "Customer_Name,Contact,Address,Medicine_Id,NoOfItem,Date,Staff_Id,Potency_Id")] User userrr)
        {
            if (ModelState.IsValid)
            {
                
                MedicinePotency medpot = db.MedicinePotencies.FirstOrDefault(u => u.Medicine_Id == userrr.Medicine_Id && u.Potency_Id == userrr.Medicine_Id);
                if(medpot.NoOfItem >= userrr.NoOfItem)
                {
                    
                    if (medpot != null)
                    {
                        Customer cus = new Customer();
                        cus.Customer_Name = userrr.Customer_Name;
                        cus.Address = userrr.Address;
                        cus.Added_On = userrr.Date;
                        cus.Contact = userrr.ContactNO;
                        db.Customers.Add(cus);

                        db.SaveChanges();
                        Customer customer = db.Customers.FirstOrDefault(y => y.Added_On == userrr.Date);
                        if (User != null)
                        {
                            Sale sal = new Sale();

                            sal.Customer_Id = customer.Customer_Id;
                            sal.MedPot_Id = medpot.MedPot_Id;
                            sal.NoOfItem = userrr.NoOfItem;
                            sal.Date = userrr.Date;
                            sal.Staff_Id = userrr.Staff_Id;
                            db.Sales.Add(sal);
                            db.SaveChanges();
                            medpot.NoOfItem = (medpot.NoOfItem) - (userrr.NoOfItem);
                            db.Entry(medpot).State = EntityState.Modified;
                            db.SaveChanges();
                            return View();
                        }
                    }
                    

                }
                ModelState.AddModelError("NoOfItem","Enough Medicine is not Present");
                ViewBag.Medicine_Id = new SelectList(db.Medicines, "Medicine_Id", "Name");
                ViewBag.Potency_Id = new SelectList(db.Potencies, "Potency_Id", "Potency_mg");
                ViewBag.Staff_Id = new SelectList(db.People, "Id", "FirstName");
                return View();
                

                
            }
            ViewBag.Medicine_Id = new SelectList(db.Medicines, "Medicine_Id", "Name");
            ViewBag.Potency_Id = new SelectList(db.Potencies, "Potency_Id", "Potency_mg");
            ViewBag.Staff_Id = new SelectList(db.People, "Id", "FirstName");
            return View();
        }
        public ActionResult ListCustomer()
        {
            var Customer = db.Customers.Include(p => p.Sales);
            return View(Customer.ToList());
        }
        //people

        public ActionResult PersonStaff()
        {
            return View();
        }
        public ActionResult Home()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }

        //staff
        public ActionResult StaffAbout()
        {
            return View();
        }
        public ActionResult Admindashboard()
        {
            return View();
        }
    }
}
