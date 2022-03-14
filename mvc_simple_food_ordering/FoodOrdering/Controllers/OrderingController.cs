using FoodOrdering.DBHelper;
using FoodOrdering.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FoodOrdering.Controllers
{
    public class OrderingController : Controller
    {
        private DB _db;

        // GET: Ordering
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetFoodList(string Provider)
        {
            if (Provider == null)
                throw new Exception("Null parameters");
            var retfood = DB.GetInstance.GetFoodTypesAndPrices(Provider);
            return Json(retfood, JsonRequestBehavior.AllowGet);
        }

        [HttpPost] //attribute to get posted values from HTML Form
        public ActionResult MakeOrder(Order order)
        {
            if(order == null)
                throw new Exception("Null parameters");
            order.FoodType = order.FoodType.Replace(" - price: $", ":");
            DB.GetInstance.SaveOrder(order);
            return RedirectToAction("Index","Reporting");
        }

    }
}