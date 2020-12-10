using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeX.Models
{
    // 
    // POCO (plain old cls object) business object to hold an Order TEMPLATE???.
    // Items are static, Order is generated first time then needs regenerate on change, could/should be done dynamic off course
    // but for time being and as per requierement that is sufficient...
    public class Order
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Qty { get; set; }
        public double Price { get; set; }
        public double Total { get; set; }
        public bool Tip { get; set; }
    }
}