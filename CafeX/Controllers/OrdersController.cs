using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CafeX.Models;

namespace CafeX.Controllers
{
    public class OrdersController : Controller
    {
        private CafeXContext db = new CafeXContext();

        // GET: Orders
        public ActionResult Index()
        {
            // Populate Items List if NOT Exist...
            if (!db.Orders.Any())
                PopulateOrder();

            //
            return View(db.Orders.ToList());
        }
        // -----------------------------------------------------------------
        // TODO: RAB NOTE...
        // Helper function to populate Static Order with items first time around... 
        // -----------------------------------------------------------------
        public void PopulateOrder()
        {
            // Add items to Order list.
            var allItems = db.Items.ToList();
            //
            foreach (var item in allItems)
            {
                // CHK: "Id,Name,Qty,Price,Total,Tip"
                db.Orders.Add(new Order() { Name = item.Name, Qty = 0, Price = item.Price, Total = 0, Tip = item.Tip });
            }
            //
            db.SaveChanges();
        }


        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(FormCollection frmCol)
        {
            string[] ids = frmCol["Item.Id"].Split(',');
            string[] q = frmCol["Item.Qty"].Split(',');

            double total = 0;
            bool tip = false;
            for (int i = 0; i < ids.Length; i++)
            {
                Models.Order order = db.Orders.Find(Convert.ToInt32(ids[i]));
                total += Convert.ToInt16(q[i]) * order.Price;
                // if item orderd and h
                if (order.Tip && Convert.ToInt16(q[i]) > 0)
                    tip = true;
            }

            OrderTotal ot = new OrderTotal();
            ot.Total = Convert.ToDecimal(total);

            if (tip)
                total = total * 1.1;

            ot.TotalDue = Convert.ToDecimal(total);
            ot.ServiceChargeAmt = ot.TotalDue - ot.Total;
            //db.OrderTotal.Add(ot);
            //db.SaveChanges();
            //
            // Display Totals...
            //  RedirectToAction("Index", "OrderTotal");
            ViewData["Total"] = ot.Total;
            ViewData["ServiceChargeAmt"] = ot.ServiceChargeAmt;
            ViewData["TotalDue"] = ot.TotalDue;


            // 
            // db.Orders.Add(new Order() { Name = item.Name, Qty = 0, Price = item.Price, Total = 0, Tip = item.Tip });
            // CHK: "Id,Total,Tip,ServiceChargeAmt,TotalDue")
            db.OrderTotals.Add(ot);
            //
            db.SaveChanges();

            return View("OrderSummary");//
        }


        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Qty,Price,Total,Tip")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
