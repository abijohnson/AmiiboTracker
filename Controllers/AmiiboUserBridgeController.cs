using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AmiiboTracker.DAL;
using AmiiboTracker.Models;

namespace AmiiboTracker.Controllers
{
    public class AmiiboUserBridgeController : Controller
    {
        private AmiiboContext db = new AmiiboContext();

        private AmiiboUserBridge bridgeTable = new AmiiboUserBridge();

        // GET: AmiiboUserBridge

        public void AddToWishList(int amiiboID)
        {
            if (!ControllerContext.IsChildAction)
            {
                var userID = db.AspNetUsers.FirstOrDefault(x => x.Email == System.Web.HttpContext.Current.User.Identity.Name).Id;
                Amiibo amiibo2Save = db.Amiiboes.FirstOrDefault(y => y.PK == amiiboID);

                var sameRecord = db.AmiiboUserBridges.Any(x => x.IsWishList == true && x.UserID == userID && x.AmiiboID == amiibo2Save.PK) ;

                if (sameRecord == true )
                {
                    var sameRecord2 = db.AmiiboUserBridges.Where(x => x.IsWishList == true && x.UserID == userID && x.AmiiboID == amiibo2Save.PK).FirstOrDefault();

                    db.AmiiboUserBridges.Remove(sameRecord2);

                    db.SaveChanges();
                }

            }
 
        }

        public void RemoveFromWishList(int amiiboID)
        {
            if (!ControllerContext.IsChildAction)
            {
                var userID = db.AspNetUsers.FirstOrDefault(x => x.Email == System.Web.HttpContext.Current.User.Identity.Name).Id;
                Amiibo amiibo2Save = db.Amiiboes.FirstOrDefault(y => y.PK == amiiboID);

                bridgeTable.AmiiboID = amiibo2Save.PK;
                bridgeTable.UserID = userID;

                bridgeTable.IsWishList = true;

                db.AmiiboUserBridges.Remove(bridgeTable);

                db.SaveChanges();
            }

        }

        [Authorize]
        public ActionResult Index()
        {
            var amiiboUserBridges = db.AmiiboUserBridges.Include(a => a.Amiibo).Include(a => a.AspNetUser);
            return View(amiiboUserBridges.ToList());
        }

        // GET: AmiiboUserBridge/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmiiboUserBridge amiiboUserBridge = db.AmiiboUserBridges.Find(id);
            if (amiiboUserBridge == null)
            {
                return HttpNotFound();
            }
            return View(amiiboUserBridge);
        }

        // GET: AmiiboUserBridge/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.AmiiboID = new SelectList(db.Amiiboes, "PK", "AmiiboSeries");
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email");
            return View();
        }

        // POST: AmiiboUserBridge/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PK,UserID,AmiiboID,IsWishList")] AmiiboUserBridge amiiboUserBridge)
        {
            if (ModelState.IsValid)
            {
                db.AmiiboUserBridges.Add(amiiboUserBridge);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AmiiboID = new SelectList(db.Amiiboes, "PK", "AmiiboSeries", amiiboUserBridge.AmiiboID);
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email", amiiboUserBridge.UserID);
            return View(amiiboUserBridge);
        }

        // GET: AmiiboUserBridge/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmiiboUserBridge amiiboUserBridge = db.AmiiboUserBridges.Find(id);
            if (amiiboUserBridge == null)
            {
                return HttpNotFound();
            }
            ViewBag.AmiiboID = new SelectList(db.Amiiboes, "PK", "AmiiboSeries", amiiboUserBridge.AmiiboID);
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email", amiiboUserBridge.UserID);
            return View(amiiboUserBridge);
        }

        // POST: AmiiboUserBridge/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PK,UserID,AmiiboID,IsWishList")] AmiiboUserBridge amiiboUserBridge)
        {
            if (ModelState.IsValid)
            {
                db.Entry(amiiboUserBridge).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AmiiboID = new SelectList(db.Amiiboes, "PK", "AmiiboSeries", amiiboUserBridge.AmiiboID);
            ViewBag.UserID = new SelectList(db.AspNetUsers, "Id", "Email", amiiboUserBridge.UserID);
            return View(amiiboUserBridge);
        }

        // GET: AmiiboUserBridge/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AmiiboUserBridge amiiboUserBridge = db.AmiiboUserBridges.Find(id);
            if (amiiboUserBridge == null)
            {
                return HttpNotFound();
            }
            return View(amiiboUserBridge);
        }

        // POST: AmiiboUserBridge/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AmiiboUserBridge amiiboUserBridge = db.AmiiboUserBridges.Find(id);
            db.AmiiboUserBridges.Remove(amiiboUserBridge);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
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
