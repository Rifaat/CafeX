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
    public class ItemsController : Controller
    {
        private CafeXContext db = new CafeXContext();

        // GET: Items
        public ActionResult Index()
        {
            // Populate Items List...
            if (!db.Items.Any())
                PopulateItems();

            //
            return View(db.Items.ToList());
        }

        // -----------------------------------------------------------------
        // TODO: RAB NOTE...
        // Helper function to populate items first time around, could use migration but have some issues with my VS & nuget pkges.
        // mainly db migration and api not liken' me even as admin, laptop due for format new SSD is waiting... 
        // -----------------------------------------------------------------
        // to build DB OBJs:
        //  Tools > NuGet Package Manager > Package Manager Console
        //  Run the following commands:
        //      Add-Migration InitialCreate
        //      Update-Database
        //
        // load-migration InitialCreate
        // Update-Database
        // -----------------------------------------------------------------
        public void PopulateItems()
        {
            // Add items to the list.
            db.Items.Add(new Item() { Name = "Cola - Cold", Price = 0.50, Tip = false });
            db.Items.Add(new Item() { Name = "Coffee - Hot", Price = 1.00, Tip = false });
            db.Items.Add(new Item() { Name = "Cheese Sandwich - Cold", Price = 2.00, Tip = true });
            db.Items.Add(new Item() { Name = "Steak Sandwich - Hot", Price = 4.50, Tip = true });
            db.SaveChanges();
        }


        // GET: Items/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // GET: Items/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Items/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Price,Tip")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(item);
        }

        // GET: Items/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Price,Tip")] Item item)
        {
            if (ModelState.IsValid)
            {
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        // GET: Items/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Item item = db.Items.Find(id);
            if (item == null)
            {
                return HttpNotFound();
            }
            return View(item);
        }

        // POST: Items/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Item item = db.Items.Find(id);
            db.Items.Remove(item);
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
