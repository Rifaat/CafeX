using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeX.Models
{
    // 
    // POCO (plain old cls object) business object to hold an Order Totals ONLY.
    // data stored is enought to produce the final result of an order and some stats at later...
    public class OrderTotal
    {
        public int Id { get; set; }
        public decimal Total { get; set; }
        public bool Tip { get; set; }
        public decimal ServiceChargeAmt { get; set; }
        public decimal TotalDue { get; set; }
    }
}