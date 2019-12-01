using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BLL;
using BOL;
using NLog;

namespace UI.Controllers
{
    public class CallNotesController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private CallNoteService service;
        public CallNotesController(CallNoteService service)
        {
            this.service = service;
        }
        public CallNotesController()
        {
            service = new CallNoteService();
        }
        // GET: CallNotes
        public ActionResult Index(int? customerId)
        {
            logger.Log(LogLevel.Debug, "GET: CallNote - Index request is received with customer id:{0}", customerId);
            var callNotes = service.GetByCustomerId(customerId);
            ViewBag.CustomerId = customerId;
            return View(callNotes.ToList());
        }
        // GET: CallNotes/Details/5 
        public ActionResult Details(int? id)
        {
            logger.Log(LogLevel.Debug, "GET: CallNote - Details request is received.");
            if (id == null)
            {
                logger.Log(LogLevel.Debug, "Null Id!");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CallNote callNote = service.GetById(id);
            if (callNote == null)
            {
                logger.Log(LogLevel.Debug, "Cannot find the call note with id:{0}", id);
                return HttpNotFound();
            }
            logger.Log(LogLevel.Debug, "Found the call note with id:{0}", id);
            return View(callNote);
        }
        // GET: CallNotes/Create
        public ActionResult Create(int? customerId, int? parentId)
        {
            logger.Log(LogLevel.Debug, "GET: CallNote - Create request is received.");
            var callNote = service.InitialiseCallNote(customerId, parentId);
            return View(callNote);
        }
        // POST: CallNotes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,parent_id,customer_id,text_content,date_created")] CallNote callNote)
        {
            logger.Log(LogLevel.Debug, "POST: CallNote - Create request is received with {0}.", callNote.ToString());
            if (ModelState.IsValid)
            {
                service.Insert(callNote);
                logger.Log(LogLevel.Info, "{0} is added into DB.", callNote.ToString());
                return RedirectToAction("Index", new { customerId = callNote.customer_id });
            }
            logger.Log(LogLevel.Debug, "Failed to create {0}.", callNote.ToString());
            return View(callNote);
        }
        // GET: CallNotes/Edit/5
        public ActionResult Edit(int? id)
        {
            logger.Log(LogLevel.Debug, "GET: CallNote - Edit request is received wtih id:{0}.", id);
            if (id == null)
            {
                logger.Log(LogLevel.Debug, "Null Id!");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CallNote callNote = service.GetById(id);
            if (callNote == null)
            {
                logger.Log(LogLevel.Debug, "Cannot find the call note with id:{0}", id);
                return HttpNotFound();
            }
            ViewBag.customer_id = callNote.customer_id;
            ViewBag.parent_id = callNote.parent_id;
            logger.Log(LogLevel.Debug, "Found the call note with id:{0}", id);
            return View(callNote);
        }
        // POST: CallNotes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,parent_id,customer_id,text_content,date_created")] CallNote callNote)
        {
            logger.Log(LogLevel.Debug, "POST: CallNote - Edit request is received with {0}.", callNote.ToString());
            CallNote existingCallNote = service.GetById(callNote.id);
            if (callNote == null)
            {
                logger.Log(LogLevel.Debug, "Cannot find the {0}.", callNote.ToString());
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                service.Update(callNote);
                logger.Log(LogLevel.Info, "{0} is updated into DB.", callNote.ToString());
                return RedirectToAction("Index", new { customerId = callNote.customer_id });
            }
            ViewBag.customer_id = existingCallNote.customer_id;
            ViewBag.parent_id = existingCallNote.parent_id;
            logger.Log(LogLevel.Debug, "Fail to update {0} into DB.", callNote.ToString());
            return View(callNote);
        }
        // GET: CallNotes/Delete/5
        public ActionResult Delete(int? id)
        {
            logger.Log(LogLevel.Debug, "GET: CallNote - Delete request is received wtih id:{0}.", id);
            if (id == null)
            {
                logger.Log(LogLevel.Debug, "Null Id!");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CallNote callNote = service.GetById(id);
            if (callNote == null)
            {
                logger.Log(LogLevel.Debug, "Cannot find the call note with id:{0}", id);
                return HttpNotFound();
            }
            logger.Log(LogLevel.Debug, "Found the call note with id:{0}", id);
            return View(callNote);
        }
        // POST: CallNotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var ownerId = (service.GetById(id)).customer_id;
            service.Delete(id);
            logger.Log(LogLevel.Info, "The call note with id:{0} has been deleted from DB.", id);
            return RedirectToAction( "Index", new { customerId = ownerId });
        }
        protected override void Dispose(bool disposing)
        {
            service.Dispose(disposing);
        }
    }
}
