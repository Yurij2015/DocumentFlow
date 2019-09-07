using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DocumentFlow.Models;

namespace DocumentFlow.Controllers
{
    public class IncomingDocumentController : Controller
    {
        private DocumentFlowContext db = new DocumentFlowContext();

        // GET: IncomingDocument
        public ActionResult Index()
        {
            return View(db.IncomingDocuments.ToList());
        }

        // GET: IncomingDocument/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncomingDocumentModel incomingDocumentModel = db.IncomingDocuments.Find(id);
            if (incomingDocumentModel == null)
            {
                return HttpNotFound();
            }
            return View(incomingDocumentModel);
        }

        // GET: IncomingDocument/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: IncomingDocument/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,DocIndex,Description,Author,LeadResolution,SaveTime,DocumentFile,Status")] HttpPostedFileBase upload, IncomingDocumentModel incomingDocumentModel)
        {
            if (ModelState.IsValid)
            {
                db.IncomingDocuments.Add(incomingDocumentModel);

                Upload(upload);

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(incomingDocumentModel);
        }

        public void Upload(HttpPostedFileBase upload)
        {
            if (upload != null)
            {
                // получаем имя файла
                string fileName = System.IO.Path.GetFileName(upload.FileName);
                // сохраняем файл в папку IncomingDocuments.Files в проекте
                upload.SaveAs(Server.MapPath("~/IncomingDocuments.Files/" + fileName));
            }
        }


        // GET: IncomingDocument/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncomingDocumentModel incomingDocumentModel = db.IncomingDocuments.Find(id);
            if (incomingDocumentModel == null)
            {
                return HttpNotFound();
            }
            return View(incomingDocumentModel);
        }

        // POST: IncomingDocument/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,DocIndex,Description,Author,LeadResolution,SaveTime,DocumentFile,Status")] IncomingDocumentModel incomingDocumentModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(incomingDocumentModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(incomingDocumentModel);
        }

        // GET: IncomingDocument/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IncomingDocumentModel incomingDocumentModel = db.IncomingDocuments.Find(id);
            if (incomingDocumentModel == null)
            {
                return HttpNotFound();
            }
            return View(incomingDocumentModel);
        }

        // POST: IncomingDocument/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            IncomingDocumentModel incomingDocumentModel = db.IncomingDocuments.Find(id);
            db.IncomingDocuments.Remove(incomingDocumentModel);
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
