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
    public class OutboxDocumentController : Controller
    {
        private DocumentFlowContext db = new DocumentFlowContext();

        // GET: OutboxDocument
        public ActionResult Index()
        {
            //return View(db.OutboxDocumentModels.Where(o => o.LeadResolutionLogin.Contains(User.Identity.Name)).ToList());
            return View(db.OutboxDocumentModels.ToList());

        }

        // GET: OutboxDocument/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OutboxDocumentModel outboxDocumentModel = db.OutboxDocumentModels.Find(id);
            if (outboxDocumentModel == null)
            {
                return HttpNotFound();
            }
            return View(outboxDocumentModel);
        }

        // GET: OutboxDocument/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OutboxDocument/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,DocIndex,Description,Author,LeadResolution,LeadResolutionLogin,SaveTime,DocumentFile,NoteToDocument,Status")] HttpPostedFileBase upload, OutboxDocumentModel outboxDocumentModel)
        {
            if (ModelState.IsValid)
            {
                db.OutboxDocumentModels.Add(outboxDocumentModel);
                //Upload(upload);

                if (upload != null)
                {
                    // получаем имя файла
                    //string fileName = LeadResolutionName(incomingDocumentModel) + System.IO.Path.GetFileName(upload.FileName);
                    // получаем расширение файла
                    string fileName = LeadResolutionName(outboxDocumentModel) + DocIndexName(outboxDocumentModel) + System.IO.Path.GetExtension(upload.FileName);
                    // сохраняем файл в папку IncomingDocuments.Files в проекте
                    upload.SaveAs(Server.MapPath("~/OutboxDocuments.Files/" + fileName));
                    //определяем название файла для сохранения и последующей загрузки. Переопределение (определение) поля DocumentFile
                    outboxDocumentModel.DocumentFile = fileName;
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(outboxDocumentModel);
        }

        /// <summary>
        /// получаем данные поля LeadResolution. Кто назначен для обработки документа
        /// </summary>
        /// <param name="outboxDocumentModel"></param>
        /// <returns></returns>
        public string LeadResolutionName(OutboxDocumentModel outboxDocumentModel)
        {
            return outboxDocumentModel.LeadResolution;
        }

        /// <summary>
        /// получаем данные поля DocIndexName
        /// </summary>
        /// <param name="outboxDocumentModel"></param>
        /// <returns></returns>
        public string DocIndexName(OutboxDocumentModel outboxDocumentModel)
        {
            return outboxDocumentModel.DocIndex;
        }


        // GET: OutboxDocument/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OutboxDocumentModel outboxDocumentModel = db.OutboxDocumentModels.Find(id);
            if (outboxDocumentModel == null)
            {
                return HttpNotFound();
            }
            return View(outboxDocumentModel);
        }

        // POST: OutboxDocument/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Date,DocIndex,Description,Author,LeadResolution,LeadResolutionLogin,SaveTime,DocumentFile,NoteToDocument,Status")] OutboxDocumentModel outboxDocumentModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(outboxDocumentModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(outboxDocumentModel);
        }

        // GET: OutboxDocument/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OutboxDocumentModel outboxDocumentModel = db.OutboxDocumentModels.Find(id);
            if (outboxDocumentModel == null)
            {
                return HttpNotFound();
            }
            return View(outboxDocumentModel);
        }

        // POST: OutboxDocument/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OutboxDocumentModel outboxDocumentModel = db.OutboxDocumentModels.Find(id);
            db.OutboxDocumentModels.Remove(outboxDocumentModel);
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
