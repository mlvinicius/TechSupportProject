using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TechSupport.Models;

namespace TechSupport.Controllers
{
    public class CustomersController : Controller
    {
        TechSupportEntities context = new TechSupportEntities();

        public ActionResult CustomersList()
        {
            List<Customer> customers = context.Customers.ToList();
            return View(customers);
        }

        public ActionResult SearchCustomers(string id)
        {
            List<Customer> customers = context.Customers
                .Where(c => c.Name.Contains(id)).ToList();

            return View("CustomersList", customers);
        }

        public ActionResult UpsertCustomer(int id = 0)
        {
            Customer customer;

            if (id == 0)
            {
                customer = new Customer();
                ViewBag.Title = "Create Customer";
            }
            else
            {
                customer = context.Customers.FirstOrDefault(c => c.CustomerID == id);
                ViewBag.Title = "Edit Customer";
            }

            List<SelectListItem> states = context.States.Select(s =>
                new SelectListItem { Text = s.StateName, Value = s.StateCode }).ToList();

            SelectList list = new SelectList(states, "Value", "Text");

            ViewBag.States = list;

            return View(customer);
        }

        [HttpPost]
        public ActionResult UpsertCustomer(Customer customer)
        {
            State custState = context.States.FirstOrDefault(s => s.StateCode == customer.State);
            customer.State1 = custState;

            context.Customers.AddOrUpdate(customer);
            context.SaveChanges();

            return View("CustomersList", context.Customers.ToList());
        }

        public ActionResult DeleteCustomer(int id)
        {
            Customer cust = context.Customers.FirstOrDefault(c => c.CustomerID == id);
            try
            {
                context.Customers.Remove(cust);

                context.SaveChanges();
            }
            catch (Exception)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("Toast", cust);
            }



            return View("CustomersList", context.Customers.ToList());
        }

        public ActionResult ForceDelete(int id)
        {
            Customer cust = context.Customers.FirstOrDefault(c => c.CustomerID == id);
            List<Incident> incidents = context.Incidents.Where(i => i.CustomerID == id).ToList();
            List<Registration> registrations = context.Registrations.Where(i => i.CustomerID == id).ToList();

            foreach (var item in incidents)
            {
                context.Incidents.Remove(item);
            }
            foreach (var item in registrations)
            {
                context.Registrations.Remove(item);
            }

            context.Customers.Remove(cust);

            context.SaveChanges();

            return View("CustomersList", context.Customers.ToList());
        }
    }
}