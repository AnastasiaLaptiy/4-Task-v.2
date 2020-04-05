﻿using System.Collections.Generic;
using System.Linq;
using System.Web;
using Task4v2.Models;

namespace Task4v2.Managers
{
    public class ExtProductSessionManager
    {
        private SessionManager sessionManager = new SessionManager();

        public List<ExtProductModel> GetOrCreateExtProductList(HttpContextBase httpContext)
        {
            var context = sessionManager.CreateHttpContext(httpContext);
            var products = context.HttpContext.Session["ExtProductList"] as List<ExtProductModel>;
            if (products == null)
            {
                products = new List<ExtProductModel>();
            }
            context.HttpContext.Session["ExtProductList"] = products;
            return products;
        }

        public List<ExtProductModel> AddProductToList(ExtProductModel extProduct, HttpContextBase context)
        {
            var extnProducts = GetOrCreateExtProductList(context);
            extProduct.Id = extnProducts.Count > 0 ? extnProducts.Max(x => x.Id) + 1 : 1; 
            extnProducts.Add(extProduct);
            return extnProducts;
        }
    }
}