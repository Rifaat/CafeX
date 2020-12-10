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
    public class OrderTotalsController : Controller
    {
        private CafeXContext db = new CafeXContext();

        // GET: OrderTotals
        public ActionResult Index()
        {
            return View(db.OrderTotals.ToList());
        }

        // GET: OrderTotals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderTotal orderTotal = db.OrderTotals.Find(id);
            if (orderTotal == null)
            {
                return HttpNotFound();
            }
            return View(orderTotal);
        }

        // GET: OrderTotals/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OrderTotals/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Total,Tip,ServiceChargeAmt,TotalDue")] OrderTotal orderTotal)
        {
            if (ModelState.IsValid)
            {
                db.OrderTotals.Add(orderTotal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(orderTotal);
        }

        // GET: OrderTotals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderTotal orderTotal = db.OrderTotals.Find(id);
            if (orderTotal == null)
            {
                return HttpNotFound();
            }
            return View(orderTotal);
        }

        // POST: OrderTotals/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Total,Tip,ServiceChargeAmt,TotalDue")] OrderTotal orderTotal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderTotal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(orderTotal);
        }

        // GET: OrderTotals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderTotal orderTotal = db.OrderTotals.Find(id);
            if (orderTotal == null)
            {
                return HttpNotFound();
            }
            return View(orderTotal);
        }

        // POST: OrderTotals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderTotal orderTotal = db.OrderTotals.Find(id);
            db.OrderTotals.Remove(orderTotal);
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
