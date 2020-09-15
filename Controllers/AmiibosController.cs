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
using PagedList; 

namespace AmiiboTracker.Controllers
{
    public class AmiibosController : Controller
    {
        private AmiiboContext db = new AmiiboContext();

        // GET: Amiibos


        [HttpGet]
        public ActionResult Index(string searchString, string sortOrder, int? page, string currentFilter)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var amiibos1 = from s in db.Amiiboes
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                amiibos1 = amiibos1.Where(s => s.Character.Contains(searchString));
            }

            else
            {
                searchString = currentFilter;
            }



            switch (sortOrder)
            {
                case "name_desc":
                    amiibos1 = amiibos1.OrderBy(s => s.PK);
                    break;
                case "Date":
                    amiibos1 = amiibos1.OrderBy(s => s.PK);
                    break;
                case "date_desc":
                    amiibos1 = amiibos1.OrderBy(s => s.PK);
                    break;
                default:  // Name ascending 
                    amiibos1 = amiibos1.OrderBy(s => s.PK);
                    break;
            }




            int pageSize = 10;
            int pageNumber = (page ?? 1);
            ViewBag.itemCount = amiibos1.Count(); 
            return View(amiibos1.ToPagedList(pageNumber, pageSize));

            //return View(db.Amiiboes.ToList());
        }



        // GET: Amiibos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Amiibo amiibo = db.Amiiboes.Find(id);
            if (amiibo == null)
            {
                return HttpNotFound();
            }
            return View(amiibo);
        }

        // GET: Amiibos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Amiibos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PK,AmiiboSeries,Character,GameSeries,Head,Image,Name,ReleaseAU,ReleaseEU,ReleaseJP,ReleaseNA,Tail,Type")] Amiibo amiibo)
        {
            if (ModelState.IsValid)
            {
                db.Amiiboes.Add(amiibo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(amiibo);
        }

        // GET: Amiibos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Amiibo amiibo = db.Amiiboes.Find(id);
            if (amiibo == null)
            {
                return HttpNotFound();
            }
            return View(amiibo);
        }

        // POST: Amiibos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PK,AmiiboSeries,Character,GameSeries,Head,Image,Name,ReleaseAU,ReleaseEU,ReleaseJP,ReleaseNA,Tail,Type")] Amiibo amiibo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(amiibo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(amiibo);
        }

        // GET: Amiibos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Amiibo amiibo = db.Amiiboes.Find(id);
            if (amiibo == null)
            {
                return HttpNotFound();
            }
            return View(amiibo);
        }

        // POST: Amiibos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Amiibo amiibo = db.Amiiboes.Find(id);
            db.Amiiboes.Remove(amiibo);
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
