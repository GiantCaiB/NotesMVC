using System;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BLL;
using BOL;
using NLog;

namespace UI.Controllers
{
    public class CustomersController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private CustomerService service;

        public CustomersController(CustomerService service)
        {
            this.service = service;
        }

        public CustomersController()
        {
            service = new CustomerService();
        }

        // GET: Customers
        public ActionResult Index(string searchString)
        {
            logger.Log(LogLevel.Debug, "GET: Customer - Index request is received with searchString:{0}", searchString);
            var customers = service.Search(searchString);
            return View(customers);
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            logger.Log(LogLevel.Debug, "GET: Customer - Details request is received.");
            if (id == null)
            {
                logger.Log(LogLevel.Debug, "Null Id!");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = service.GetById(id);
            if (customer == null)
            {
                logger.Log(LogLevel.Debug, "Cannot find the customer with id:{0}", id);
                return HttpNotFound();
            }
            logger.Log(LogLevel.Debug, "Found the customer with id:{0}", id);
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            logger.Log(LogLevel.Debug, "GET: Customer - Create request is received.");
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,username,first_name,last_name,phone_number,date_of_birth")] Customer customer)
        {
            logger.Log(LogLevel.Debug, "POST: Customer - Create request is received with {0}.",customer.ToString());
            CheckUsernameUnique(customer.username);
            CheckIsAustralianNumber(customer.phone_number);
            CheckAge(customer.date_of_birth);
            if (ModelState.IsValid)
            {
                service.Insert(customer);
                logger.Log(LogLevel.Info, "{0} is added into DB.", customer.ToString());
                return RedirectToAction("Index");
            }
            logger.Log(LogLevel.Debug, "Failed to create {0}.", customer.ToString());
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            logger.Log(LogLevel.Debug, "GET: Customer - Edit request is received wtih id:{0}.",id);
            if (id == null)
            {
                logger.Log(LogLevel.Debug, "Null Id!");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = service.GetById(id);
            if (customer == null)
            {
                logger.Log(LogLevel.Debug, "Cannot find the customer with id:{0}", id);
                return HttpNotFound();
            }
            logger.Log(LogLevel.Debug, "Found the customer with id:{0}", id);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,username,first_name,last_name,phone_number,date_of_birth")] Customer customer)
        {
            logger.Log(LogLevel.Debug, "POST: Customer - Edit request is received with {0}.", customer.ToString());
            Customer existingCustomer = service.GetById(customer.id);
            if (existingCustomer == null)
            {
                logger.Log(LogLevel.Debug, "Cannot find the {0}.", customer.ToString());
                return HttpNotFound();
            }
            if(existingCustomer.username != customer.username)
            {
                CheckUsernameUnique(customer.username);
            }
            CheckIsAustralianNumber(customer.phone_number);
            CheckAge(customer.date_of_birth);
            if (ModelState.IsValid)
            {
                service.Update(customer);
                logger.Log(LogLevel.Info, "{0} is updated into DB.", customer.ToString());
                return RedirectToAction("Index");
            }
            logger.Log(LogLevel.Debug, "Fail to update {0} into DB.", customer.ToString());
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            logger.Log(LogLevel.Debug, "GET: Customer - Delete request is received wtih id:{0}.", id);
            if (id == null)
            {
                logger.Log(LogLevel.Debug, "Null Id!");
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = service.GetById(id);
            if (customer == null)
            {
                logger.Log(LogLevel.Debug, "Cannot find the customer with id:{0}", id);
                return HttpNotFound();
            }
            logger.Log(LogLevel.Debug, "Found the customer with id:{0}", id);
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            service.Delete(id);
            logger.Log(LogLevel.Info, "The customer with id:{0} has been deleted from DB.", id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            service.Dispose(disposing);
        }

        private void CheckUsernameUnique(string username)
        {
            if (!service.CheckUsernameUnique(username))
            {
                logger.Log(LogLevel.Debug, "Username is not unique.");
                ModelState.AddModelError("username", "Username already exists");
            }
        }
        private void CheckIsAustralianNumber(string phoneNumber)
        {
            if (!service.customerValidator.CheckIsAustralianNumber(phoneNumber))
            {
                logger.Log(LogLevel.Debug, "Phone number is not valid.");
                ModelState.AddModelError("phone_number", "Only valid Australian landline or mobile numbers");
            }
        }
        private void CheckAge(DateTime dateOfBirth)
        {
            if (!service.customerValidator.CheckAgeIsValid(dateOfBirth))
            {
                logger.Log(LogLevel.Debug, "Date of birth is not valid.");
                ModelState.AddModelError("date_of_birth", "Age must between 0 and 110");
            }
        }
    }
}
