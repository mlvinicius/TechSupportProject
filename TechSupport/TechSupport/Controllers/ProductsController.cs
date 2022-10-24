using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TechSupport.Models;

namespace TechSupport.Controllers
{
    public class ProductsController : Controller
    {
        private TechSupportEntities context = new TechSupportEntities();
        // GET: Products
        public ActionResult ProductsList()
        {
            List<Product> products = context.Products.ToList();
            return View(products);
        }

        public ActionResult SearchProducts(string id)
        {
            if(string.IsNullOrWhiteSpace(id))
            {
                return View("ProductsList", context.Products.ToList());
            }
            List<Product> products = context.Products.Where(p => p.Name.ToLower().Contains(id.ToLower())
                || p.ProductCode.ToLower().Contains(id.ToLower())).ToList();

            return View("ProductsList", products);
        }

        public ActionResult CreateProduct()
        {
            ViewBag.Title = "Create Product";
            return View();
        }

        [HttpPost]
        public ActionResult CreateProduct(Product product)
        {
            context.Products.Add(product);
            context.SaveChanges();

            ViewBag.Title = "Create Product";
            return View("ProductsList", context.Products.ToList());
        }

        [HttpGet]
        public ActionResult EditProduct(string id)
        {
            Product product = context.Products.FirstOrDefault(p => p.ProductCode.ToLower() == id.ToLower());

            ViewBag.Title = "Edit Product";
            return View("CreateProduct", product);
        }

        [HttpPost]
        public ActionResult EditProduct(Product product)
        {
            context.Products.AddOrUpdate(product);
            context.SaveChanges();

            return View("ProductsList", context.Products.ToList());
        }

        public ActionResult DeleteProduct(string id)
        {
            Product product = context.Products.FirstOrDefault(p => p.ProductCode == id);

            context.Products.Remove(product);
            context.SaveChanges();

            return View("ProductsList", context.Products.ToList());
        }
    }
}