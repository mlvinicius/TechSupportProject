using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechSupport.Models;

namespace TechSupport.Controllers
{
    public class IncidentController : Controller
    {
        TechSupportEntities context = new TechSupportEntities();
        // GET: Incident
        public ActionResult IncidentsList()
        {
            List<Incident> incidents = context.Incidents.ToList();
            return View(incidents);
        }

        public ActionResult SearchIncident(string id, int incidentType = 1, int type = 0)
        {
            List<Incident> incidents = context.Incidents.ToList();

            switch (type)
            {
                case 1:
                    incidents = incidents.Where(i => i.CustomerID.ToString().Contains(id)).ToList();
                    break;
                case 2:
                    incidents = incidents.Where(i => i.ProductCode.ToLower().Contains(id.ToLower())).ToList();
                    break;
                case 3:
                    incidents = incidents.Where(i => i.Title.ToLower().Contains(id.ToLower())).ToList();
                    break;
                case 4:
                    incidents = incidents.Where(i => i.Description.ToLower().Contains(id.ToLower())).ToList();
                    break;
                default:
                    break;
            }

            if (incidentType == 2)
            {
                incidents = incidents.Where(i => i.DateClosed == null).ToList();
            }

            return View("IncidentsList", incidents);
        }

        public ActionResult ChooseCustomer()
        {
            return PartialView(context.Customers.ToList());
        }

        public ActionResult ChooseCustomerSearch(string id)
        {
            var customers = context.Customers.Where(c => c.Name.Contains(id)).ToList();

            return PartialView("ChooseCustomer", customers);
        }

        public ActionResult ChooseTechnician()
        {
            return PartialView(context.Technicians.ToList());
        }

        public ActionResult ChooseTechnicianSearch(string id)
        {
            var technicians = context.Technicians.Where(p => p.Name.Contains(id)).ToList();

            return PartialView("ChooseTechnician", technicians);
        }

        public ActionResult ChooseProduct()
        {
            return PartialView(context.Products.ToList());
        }

        public ActionResult ChooseProductSearch(string id)
        {
            var products = context.Products.Where(p => p.Name.Contains(id) || p.ProductCode.Contains(id)).ToList();

            return PartialView("ChooseProduct", products);
        }

        public ActionResult SelectProduct(string prodCode)
        {
            Product prod = context.Products.FirstOrDefault(p => p.ProductCode == prodCode);

            return Json(prod.Name, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SelectCustomer(int custId)
        {
            Customer cust = context.Customers.FirstOrDefault(c => c.CustomerID == custId);

            return Json(cust.Name, JsonRequestBehavior.AllowGet);
        }

        public ActionResult SelectTechnician(int techId)
        {
            Technician tech = context.Technicians.FirstOrDefault(t => t.TechID == techId);

            return Json(tech.Name, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CreateIncident()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateIncident(Incident incident)
        {
            incident.Customer = context.Customers.FirstOrDefault(c => c.CustomerID == incident.CustomerID);
            incident.Technician = context.Technicians.FirstOrDefault(t => t.TechID == incident.TechID);
            incident.Product = context.Products.FirstOrDefault(p => p.ProductCode == incident.ProductCode);

            context.Incidents.AddOrUpdate(incident);
            context.SaveChanges();

            return RedirectToAction("IncidentsList");
        }

        public ActionResult DeleteIncident(int id)
        {
            Incident incident = context.Incidents.FirstOrDefault(i => i.IncidentID == id);
            context.Incidents.Remove(incident);
            context.SaveChanges();

            return View("IncidentsList", context.Incidents.ToList());
        }

        public ActionResult EditIncident(int? id)
        {
            if (id == null)
            {
                return View("IncidentsList", context.Incidents.ToList());
            }

            List<SelectListItem> techs = context.Technicians.Select(t =>
                new SelectListItem { Text = t.Name, Value = t.TechID.ToString() }).ToList();

            SelectList list = new SelectList(techs, "Value", "Text");

            ViewBag.Techs = list;

            Incident incident = context.Incidents.FirstOrDefault(i => i.IncidentID == id);

            return PartialView(incident);

        }

        [HttpPost]
        public ActionResult EditIncident(Incident incident)
        {
            incident.Customer = context.Customers.FirstOrDefault(c => c.CustomerID == incident.CustomerID);
            incident.Technician = context.Technicians.FirstOrDefault(t => t.TechID == incident.TechID);
            incident.Product = context.Products.FirstOrDefault(p => p.ProductCode == incident.ProductCode);

            context.Incidents.AddOrUpdate(incident);
            context.SaveChanges();

            return View("IncidentsList", context.Incidents.ToList());
        }
    }
}