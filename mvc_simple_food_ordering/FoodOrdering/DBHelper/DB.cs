using FoodOrdering.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodOrdering.DBHelper
{
    public sealed class DB // this should be a singleton
    {
        private static List<Tuple<string, string>> foodStore;
        private static List<Report> Orders;

        private static DB instance = null;
        public static DB GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DB();
                    DB.Init();    
                }
                return instance;
            }
        }

        public static void Init()
        {
            foodStore = new List<Tuple<string, string>>();
            Orders = new List<Report>();
            foodStore.Add(new Tuple<string, string>("PizzaHut", "Pizza:15"));
            foodStore.Add(new Tuple<string, string>("PizzaHut", "Hamburger:6"));
            foodStore.Add(new Tuple<string, string>("PizzaHut", "Spaghetti:8"));
            foodStore.Add(new Tuple<string, string>("PizzaHut", "Chicken wings:6"));
            foodStore.Add(new Tuple<string, string>("PizzaHut", "Salad:4"));

            foodStore.Add(new Tuple<string, string>("McDonalds", "Hamburger:2"));
            foodStore.Add(new Tuple<string, string>("McDonalds", "Salad:3"));

            foodStore.Add(new Tuple<string, string>("BurgerKing", "Hamburger:4"));
            foodStore.Add(new Tuple<string, string>("BurgerKing", "Spaghetti:8"));
            foodStore.Add(new Tuple<string, string>("BurgerKing", "Chicken wings:5"));
            foodStore.Add(new Tuple<string, string>("BurgerKing", "Salad:5"));

            foodStore.Add(new Tuple<string, string>("KFC", "Hamburger:6"));
            foodStore.Add(new Tuple<string, string>("KFC", "Chicken wings:6"));
            foodStore.Add(new Tuple<string, string>("KFC", "Salad:4"));

        }

        public  List<Tuple<string, string>> GetFoodTypesAndPrices(string foodProvider)
        {
            var resList = new List<Tuple<string, string>>();
            foreach (var item in foodStore) // here we could use Linq
            {
                if (item.Item1 == foodProvider)
                    resList.Add(item);
            }

            return resList;
        }

        public  bool SaveOrder(Order order)
        {
            var report = new Report();
            report.EmployeeName = order.EmployeeName;
            report.FoodProvider = order.FoodProvider;
            report.FoodType = order.FoodType.Split(':')[0];
            report.Price = order.FoodType.Split(':')[1];
            Orders.Add(report);

            return true; // for now, we return true, because we don't have a real DB
        }

        public List<Report> GetOrderList()
        {
            return Orders;
        }
    }
}