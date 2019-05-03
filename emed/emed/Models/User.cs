using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace emed.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public Nullable<System.DateTime> DateOfBirth { get; set; }
        public string Gender { get; set; }
        public string Password { get; set; }
        public string Discriminator { get; set; }
        public string ConfirmPassword { get; set; }
        public int Customer_Id { get; set; }
        public string Customer_Name { get; set; }
        public int ContactNO { get; set; }
        //public string Address { get; set; }
        public int Sold_Id { get; set; }
        public int Medicine_Id { get; set; }
        public int NoOfItem { get; set; }
        public System.DateTime Date { get; set; }
        public int Staff_Id { get; set; }
        public int Potency_Id { get; set; }

    }
}