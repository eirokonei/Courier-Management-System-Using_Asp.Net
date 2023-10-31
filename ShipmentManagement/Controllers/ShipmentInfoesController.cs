using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShipmentManagement.Models;
using Rotativa;

namespace ShipmentManagement.Controllers
{
    public class ShipmentInfoesController : Controller
    {
        private CourierEntities db = new CourierEntities();

        // GET: ShipmentInfoes
        public ActionResult Index()
        {
            return View(db.ShipmentInfoes.ToList());
        }

        [HttpPost]
        public ActionResult Index(string searching)
        {
            if (Guid.TryParse(searching, out Guid searchingGuid))
            {
                return View(db.ShipmentInfoes.Where(x => x.ConsignmentNo == searchingGuid).ToList());
            }
            else if (searching == null)
            {
                return View(db.ShipmentInfoes.ToList());
            }
			else
			{
                var emptyList = new List<ShipmentInfo>(); 
                ViewData["NotFoundMessage"] = "Not Found"; 
                return View(emptyList);
            }
           
        }

        public ActionResult AdminData()
        {
            return View(db.ShipmentInfoes.ToList());
        }

        public ActionResult PrintReport()
        {
            
            if (Session["Username"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var report = new ActionAsPdf("AdminData");
            return report;
        }

        // GET: ShipmentInfoes/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShipmentInfo shipmentInfo = db.ShipmentInfoes.Find(id);
            if (shipmentInfo == null)
            {
                return HttpNotFound();
            }
            return View(shipmentInfo);
        }

        // GET: ShipmentInfoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShipmentInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ConsignmentNo,ShipperName,ShipperContact,ShipperAddress,RecipientName,RecipientContact,RecipientAddress,ProcessedBranch,PickupBranch,DOPlacingOrder,ExpectedDeliveryDate,Status")] ShipmentInfo shipmentInfo)
        {
            if (ModelState.IsValid)
            {
                shipmentInfo.DOPlacingOrder = DateTime.Now;
                shipmentInfo.ConsignmentNo = Guid.NewGuid();
                db.ShipmentInfoes.Add(shipmentInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(shipmentInfo);
        }

        // GET: ShipmentInfoes/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShipmentInfo shipmentInfo = db.ShipmentInfoes.Find(id);
            if (shipmentInfo == null)
            {
                return HttpNotFound();
            }
            return View(shipmentInfo);
        }

        // POST: ShipmentInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       

        public ActionResult Edit([Bind(Include = "ConsignmentNo,ShipperName,ShipperContact,ShipperAddress,RecipientName,RecipientContact,RecipientAddress,ProcessedBranch,PickupBranch,Status")] ShipmentInfo shipmentInfo)
        {
            if (ModelState.IsValid)
            {
                // Load the existing entity from the database, including the DOPlacingOrder and ExpectedDeliveryDate
                var existingShipmentInfo = db.ShipmentInfoes.Find(shipmentInfo.ConsignmentNo);

                if (existingShipmentInfo != null)
                {
                    // Set the properties you want to modify
                    db.Entry(existingShipmentInfo).CurrentValues.SetValues(shipmentInfo);

                    // Exclude the DOPlacingOrder and ExpectedDeliveryDate from being modified
                    db.Entry(existingShipmentInfo).Property(x => x.DOPlacingOrder).IsModified = false;
                    db.Entry(existingShipmentInfo).Property(x => x.ExpectedDeliveryDate).IsModified = false;

                    // Save the changes
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                else
                {
                    // Handle the case where the entity with the specified ConsignmentNo doesn't exist
                    // Return an error view or take appropriate action
                }
            }
            return View(shipmentInfo);
        }


        // GET: ShipmentInfoes/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShipmentInfo shipmentInfo = db.ShipmentInfoes.Find(id);
            if (shipmentInfo == null)
            {
                return HttpNotFound();
            }
            return View(shipmentInfo);
        }

        // POST: ShipmentInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            ShipmentInfo shipmentInfo = db.ShipmentInfoes.Find(id);
            db.ShipmentInfoes.Remove(shipmentInfo);
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
