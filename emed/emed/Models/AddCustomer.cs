using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace emed.Models
{
    public class AddCustomer
    {
        public int Customer_Id { get; set; }
        public string Customer_Name { get; set; }
        public int Contact { get; set; }
        public string Address { get; set; }
        public int Sold_Id { get; set; }
        public int Medicine_Id { get; set; }
        public int NoOfItem { get; set; }
        public System.DateTime Date { get; set; }
        public int Staff_Id { get; set; }
        public int Potency_Id { get; set; }

    }

}