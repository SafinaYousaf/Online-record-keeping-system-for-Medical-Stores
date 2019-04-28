using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace emed.Models
{
    public class SalesObjectclass
    {
        public int Sold_Id { get; set; }
        public int NoOfItem { get; set; }
        public System.DateTime Date { get; set; }
        public int Staff_Id { get; set; }
        public int Customer_Id { get; set; }
        public int Medicine_Id { get; set; }
        public int Potency_Id { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual MedicinePotency MedicinePotency { get; set; }
        public virtual Staff Staff { get; set; }
    }
}