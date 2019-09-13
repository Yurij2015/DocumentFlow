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
            if (User.Identity.Name == "ivanovivan@mail.ru" || User.Identity.Name == "petrovoleg@mail.ru")
            {
                return View(db.IncomingDocuments.Where(o => o.LeadResolutionLogin.Contains(User.Identity.Name)).ToList());
            }
            else
            {
                return View(db.IncomingDocuments.ToList());
            }

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
        [Authorize(Roles = "docmaker")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: IncomingDocument/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Date,DocIndex,Description,Author,LeadResolution,SaveTime,NoteToDocument,DocumentFile,Status")] HttpPostedFileBase upload, IncomingDocumentModel incomingDocumentModel)
        {
            if (ModelState.IsValid)
            {
                db.IncomingDocuments.Add(incomingDocumentModel);

                //Upload(upload);

                if (upload != null)
                {
                    // получаем имя файла
                    //string fileName = LeadResolutionName(incomingDocumentModel) + System.IO.Path.GetFileName(upload.FileName);
                    // получаем расширение файла
                    string llogin = (LeadResolutionLogin(incomingDocumentModel));
                    if (llogin == "ivanovivan@mail.ru")
                    {
                        incomingDocumentModel.LeadResolution = "Иванов И. И.";
                    }
                    if (llogin == "petrovoleg@mail.ru")
                    {
                        incomingDocumentModel.LeadResolution = "Петров О. И.";
                    }
                    string fileName = LeadResolutionName(incomingDocumentModel) + DocIndexName(incomingDocumentModel) + System.IO.Path.GetExtension(upload.FileName);
                    // сохраняем файл в папку IncomingDocuments.Files в проекте
                    upload.SaveAs(Server.MapPath("~/IncomingDocuments.Files/" + fileName));
                    //определяем название файла для сохранения и последующей загрузки. Переопределение (определение) поля DocumentFile
                    incomingDocumentModel.DocumentFile = fileName;
                }

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(incomingDocumentModel);
        }

        /// <summary>
        /// получаем данные поля LeadResolutionLogin. Логин того, кто назначен для обработки документа
        /// </summary>
        /// <param name="incomingDocumentModel"></param>
        /// <returns></returns>
        public string LeadResolutionLogin(IncomingDocumentModel incomingDocumentModel)
        {
            return incomingDocumentModel.LeadResolutionLogin;
        }

        /// <summary>
        /// получаем данные поля LeadResolution. Кто назначен для обработки документа
        /// </summary>
        /// <param name="incomingDocumentModel"></param>
        /// <returns></returns>
        public string LeadResolutionName(IncomingDocumentModel incomingDocumentModel)
        {
            return incomingDocumentModel.LeadResolution;
        }

        /// <summary>
        /// получаем данные поля DocIndexName
        /// </summary>
        /// <param name="incomingDocumentModel"></param>
        /// <returns></returns>
        public string DocIndexName(IncomingDocumentModel incomingDocumentModel)
        {
            return incomingDocumentModel.DocIndex;
        }


        // GET: IncomingDocument/Edit/5
        [Authorize(Roles = CustomRoles.Docmaker + "," + CustomRoles.Viewer + "," + CustomRoles.Implementer)]
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
        public ActionResult Edit([Bind(Include = "Id,Date,DocIndex,Description,Author,LeadResolution,LeadResolutionLogin,NoteToDocument, SaveTime,DocumentFile,Status")] IncomingDocumentModel incomingDocumentModel)
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
        [Authorize(Roles = "docmaker")]
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
