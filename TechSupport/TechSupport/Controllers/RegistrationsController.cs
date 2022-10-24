using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechSupport.Models;

namespace TechSupport.Controllers
{
    public class RegistrationsController : Controller
    {
        private TechSupportEntities context = new TechSupportEntities();
        // GET: Registrations
        public ActionResult RegistrationsList()
        {
            List<Registration> registrations = context.Registrations.ToList();
            return View(registrations);
        }

        public ActionResult SearchRegistration(string id)
        {
            List<Registration> registrations = context.Registrations.Where
                (r => r.Customer.Name.Contains(id) ||
                 r.Product.Name.Contains(id) ||
                 r.ProductCode.Contains(id)).ToList();


            return View("RegistrationsList", registrations);
        }

        public ActionResult CreateRegistration()
        {
            Registration registration = new Registration();

            return View(registration);
        }

        [HttpPost]
        public ActionResult CreateRegistration(Registration registration)
        {
            registration.Customer = context.Customers.FirstOrDefault(c => c.CustomerID == registration.CustomerID);
            registration.Product = context.Products.FirstOrDefault(p => p.ProductCode == registration.ProductCode);

            context.Registrations.AddOrUpdate(registration);
            context.SaveChanges();

            return View("RegistrationsList", context.Registrations.ToList());
        }

        public ActionResult DeleteRegistration(Registration registration)
        {
            Registration regToDelete = context.Registrations
                .FirstOrDefault(r => r.CustomerID == registration.CustomerID &&
                    r.ProductCode == registration.ProductCode &&
                    r.RegistrationDate == registration.RegistrationDate);

            context.Registrations.Remove(regToDelete);
            context.SaveChanges();

            return View("RegistrationsList", context.Registrations.ToList());
        }
    }
}