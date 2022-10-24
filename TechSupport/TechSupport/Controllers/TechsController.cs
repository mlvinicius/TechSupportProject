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
    public class TechsController : Controller
    {
        private TechSupportEntities context = new TechSupportEntities();

        // GET: Techs
        public ActionResult TechsList()
        {
            List<Technician> technicians = context.Technicians.ToList();

            return View(technicians);
        }

        public ActionResult SearchTechs(string id)
        {
            List<Technician> technicians;

            if (string.IsNullOrWhiteSpace(id))
            {
                technicians = context.Technicians.ToList();
            }
            else
            {
                technicians = context.Technicians.Where(t => t.Name.ToLower().Contains(id.ToLower())).ToList();
            }

            return View("TechsList", technicians);
        }

        public ActionResult RegisterTech()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RegisterTech(Technician tech)
        {
            try
            {
                context.Technicians.Add(tech);
                context.SaveChanges();
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }


            return RedirectToAction("TechsList");
        }

        public ActionResult DeleteTechnician(int id)
        {
            Technician tech = context.Technicians.FirstOrDefault(t => t.TechID == id);

            if (tech.Incidents == null)
            {
                context.Technicians.Remove(tech);
                context.SaveChanges();
            }
            else
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return PartialView("Toast", tech);
            }

            return RedirectToAction("TechsList");
        }

        public ActionResult EditTechnician(int? id)
        {
            Technician tech = context.Technicians.FirstOrDefault(t => t.TechID == id);

            return View(tech);
        }

        [HttpPost]
        public ActionResult EditTechnician(Technician tech)
        {
            try
            {
                context.Technicians.AddOrUpdate(tech);
                context.SaveChanges();

                return RedirectToAction("TechsList");
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult SafeDelete(int id)
        {
            Technician tech = context.Technicians.FirstOrDefault(t => t.TechID == id);

            List<Incident> incidents = context.Incidents.Where(i => i.TechID == id).ToList();

            foreach (var item in incidents)
            {
                item.Technician = null;
                item.TechID = null;
            }

            context.Technicians.Remove(tech);
            context.SaveChanges();

            return View("TechsList", context.Technicians.ToList());
        }

        public ActionResult ForceDelete(int id)
        {
            Technician tech = context.Technicians.FirstOrDefault(t => t.TechID == id);

            context.Technicians.Remove(tech);
            context.SaveChanges();

            return View("TechsList", context.Technicians.ToList());
        }
    }
}