using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechSupport.Models;

namespace TechSupport.Controllers
{
    public class StatesController : Controller
    {
        private TechSupportEntities context = new TechSupportEntities();
        // GET: States
        public ActionResult StatesList()
        {
            List<State> states = context.States.ToList();

            return View(states);
        }

        public ActionResult SearchState(string id)
        {
            List<State> states = context.States
                .Where(s => s.StateName.Contains(id) || s.StateCode.Contains(id)).ToList();

            return View("StatesList", states);
        }

        public ActionResult CreateState(string id = "")
        {
            State state;

            if (string.IsNullOrWhiteSpace(id))
            {
                state = new State();
                ViewBag.Title = "Create State";
            }
            else
            {
                state = context.States.FirstOrDefault(s => s.StateCode == id);
                ViewBag.Title = "Edit State";
            }

            return View(state);
        }

        [HttpPost]
        public ActionResult CreateState(State state)
        {
            context.States.AddOrUpdate(state);
            context.SaveChanges();

            return View("StatesList", context.States.ToList());

        }

        public ActionResult DeleteState(string id)
        {
            State state = context.States.FirstOrDefault(s => s.StateCode == id);

            context.States.Remove(state);
            context.SaveChanges();

            return View("StatesList", context.States.ToList());
        }
    }
}