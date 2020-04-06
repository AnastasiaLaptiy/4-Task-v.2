﻿using System.Web.Mvc;
using Task4v2.Managers;
using Task4v2.Models;

namespace Task4v2.Controllers
{
    public class ProductController : Controller
    {
        private ProductSessionManager sessionManager = new ProductSessionManager();

        public ActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateProduct(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                sessionManager.AddProduct(product, HttpContext);
                return RedirectToAction("ProductList");
            }
            else
            {
                return View();
            }
        }

        public ActionResult ProductList()
        {
            var products = sessionManager.GetOrCreateProductList(HttpContext);
            if (products.Count == 0)
            {
                return View("EmptyList");
            }
            return View(products);
        }

        public ActionResult DetailsProduct(int id)
        {
            return View(sessionManager.DetailsProduct(id, HttpContext));
        }

        public ActionResult EditProduct(int id)
        {
            return View(sessionManager.DetailsProduct(id, HttpContext));
        }

        [HttpPost]
        public ActionResult EditProduct(ProductModel product)
        {
            if (ModelState.IsValid)
            {
                sessionManager.EditProduct(product, HttpContext);
                return RedirectToAction("ProductList");
            }
            return View();
        }

        public ActionResult DeleteProduct(ProductModel product)
        {
            sessionManager.DeleteProduct(product, HttpContext);
            return RedirectToAction("ProductList");
        }
    }
}