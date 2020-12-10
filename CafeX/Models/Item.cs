using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CafeX.Models
{
    //
    // POCO (plain old cls object) business object to hold an item.
    // Tip is a boolean indicating this item quilifies order for a 10% "service charge" or "tip".
    //
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public bool Tip { get; set; }
    }
}