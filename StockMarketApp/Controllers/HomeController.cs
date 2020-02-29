using StockMarketApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StockMarketApp.Controllers
{
    public class HomeController : Controller
    {
        StockMarketContext db = new StockMarketContext();

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Orders = db.Orders;
            ViewBag.CustomerOrders = db.CustomerOrders;
            ViewBag.SellerOrders = db.SellerOrders;

            return View();
        }

        [HttpPost]
        public RedirectResult Buy(CustomerOrder customerOrder)
        {
            customerOrder.DateTimeCustomer = DateTime.Now;
            foreach (SellerOrder sellerOrder in db.SellerOrders.OrderBy(s => s.Price))
            {
                if (customerOrder.Count > 0)
                {
                    if (sellerOrder.Price <= customerOrder.Price)
                    {
                        if (sellerOrder.Count >= customerOrder.Count)
                        {
                            sellerOrder.Count -= customerOrder.Count;
                            db.Orders.Add(new Order
                            {
                                Count = customerOrder.Count,
                                DateTimeCompleted = customerOrder.DateTimeCustomer,
                                DateTimeCustomer = customerOrder.DateTimeCustomer,
                                DateTimeSeller = sellerOrder.DateTimeSeller,
                                EmailCustomer = customerOrder.Email,
                                EmailSeller = sellerOrder.Email,
                                Price = sellerOrder.Price
                            });
                            customerOrder.Count = 0;
                        }
                        else
                        {
                            customerOrder.Count -= sellerOrder.Count;

                            db.Orders.Add(new Order
                            {
                                Count = sellerOrder.Count,
                                DateTimeCompleted = customerOrder.DateTimeCustomer,
                                DateTimeCustomer = customerOrder.DateTimeCustomer,
                                DateTimeSeller = sellerOrder.DateTimeSeller,
                                EmailCustomer = customerOrder.Email,
                                EmailSeller = sellerOrder.Email,
                                Price = sellerOrder.Price
                            });

                            sellerOrder.Count = 0;
                        }
                    }
                }
            }
            db.SaveChanges();

            db.SellerOrders.RemoveRange(db.SellerOrders.Where(s => s.Count == 0).ToList());

            if (customerOrder.Count > 0)
            {
                db.CustomerOrders.Add(customerOrder);
            }

            db.SaveChanges();           

            return RedirectPermanent("/Home/Index");
        }

        [HttpPost]
        public RedirectResult Sell(SellerOrder sellerOrder)
        {
            sellerOrder.DateTimeSeller = DateTime.Now;

            foreach (CustomerOrder customerOrder in db.CustomerOrders)
            {
                if (sellerOrder.Count > 0)
                {
                    if (sellerOrder.Price <= customerOrder.Price)
                    {
                        if (sellerOrder.Count <= customerOrder.Count)
                        {
                            customerOrder.Count -= sellerOrder.Count;
                            db.Orders.Add(new Order
                            {
                                Count = sellerOrder.Count,
                                DateTimeCompleted = sellerOrder.DateTimeSeller,
                                DateTimeCustomer = customerOrder.DateTimeCustomer,
                                DateTimeSeller = sellerOrder.DateTimeSeller,
                                EmailCustomer = customerOrder.Email,
                                EmailSeller = sellerOrder.Email,
                                Price = customerOrder.Price
                            });
                            sellerOrder.Count = 0;
                        }
                        else
                        {
                            sellerOrder.Count -= customerOrder.Count;

                            db.Orders.Add(new Order
                            {
                                Count = customerOrder.Count,
                                DateTimeCompleted = sellerOrder.DateTimeSeller,
                                DateTimeCustomer = customerOrder.DateTimeCustomer,
                                DateTimeSeller = sellerOrder.DateTimeSeller,
                                EmailCustomer = customerOrder.Email,
                                EmailSeller = sellerOrder.Email,
                                Price = customerOrder.Price
                            });

                            customerOrder.Count = 0;
                        }
                    }
                }
            }
            db.SaveChanges();

            db.CustomerOrders.RemoveRange(db.CustomerOrders.Where(s => s.Count == 0).ToList());
            if (sellerOrder.Count > 0)
            {
                db.SellerOrders.Add(sellerOrder);
            }
            db.SaveChanges();

            return RedirectPermanent("/Home/Index");
        }




    }
}