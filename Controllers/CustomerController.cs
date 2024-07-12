using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoleMates.Models;

namespace SoleMates.Controllers
{
    
    public class CustomerController : Controller
    {
        private readonly myContext _context;

        public CustomerController(myContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Category> categories = _context.tbl_category.ToList();
            ViewData["category"] = categories;

            List<Product> products = _context.tbl_product.ToList();
            ViewData["product"] = products;

            ViewBag.checkSession = HttpContext.Session.GetString("customerSession");
            return View();
        }

        public IActionResult customerLogin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult customerLogin(string customer_email, string customer_password)
        {
            var customer = _context.tbl_customer.FirstOrDefault(c => c.customer_email == customer_email);
            if (customer != null && customer.customer_password == customer_password)
            {
                HttpContext.Session.SetString("customerSession", customer.customer_id.ToString());
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.message = "Incorrect Username or Password";
                return View();
            }
        }

        public IActionResult customerRegistration()
        {
            return View();
        }

        [HttpPost]
        public IActionResult customerRegistration(Customer customer)
        {
            _context.tbl_customer.Add(customer);
            _context.SaveChanges();
            return RedirectToAction("customerLogin");
        }
        public IActionResult customerLogout()
        {
            HttpContext.Session.Remove("customerSession");
            return RedirectToAction("Index");
        }
        public IActionResult customerProfile()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("customerSession")))
            {
                return RedirectToAction("customerLogin");
            }
            else
            {
                List<Category> categories = _context.tbl_category.ToList();
                ViewData["category"] = categories;
                var customerId = HttpContext.Session.GetString("customerSession");
                var row = _context.tbl_customer.Where(a => a.customer_id == int.Parse(customerId)).ToList();
                return View(row);               
            }          
        }
        [HttpPost]
        public IActionResult updateCustomerProfile(Customer customer)
        {
            _context.tbl_customer.Update(customer);
            _context.SaveChanges();
            return RedirectToAction("customerProfile");
        }
        public IActionResult feedback()
        {
            string isLogin = HttpContext.Session.GetString("customerSession");
            if (isLogin == null)
            {
                return RedirectToAction("customerLogin");
            }
            else 
            {
                List<Category> categories = _context.tbl_category.ToList();
                ViewData["category"] = categories;
                return View();
            }
           
        }
        [HttpPost]
        public IActionResult feedback(Feedback feedback)
        {
            TempData["message"] = "Thank You For Your Feedback";
            _context.tbl_feedback.Add(feedback);
            _context.SaveChanges();
            return RedirectToAction("feedback");
        }
        public IActionResult fetchAllProducts()
        {
            List<Category> categories = _context.tbl_category.ToList();
            ViewData["category"] = categories;

            List<Product> products = _context.tbl_product.ToList();
            ViewData["product"] = products;

            return View();
        }
        public IActionResult ProductDetails(int id)
        {
            List<Category> categories = _context.tbl_category.ToList();
            ViewData["category"] = categories;

            var products = _context.tbl_product.Where(p=>p.product_id==id).ToList();

            return View(products);
        }
        public IActionResult AddToCart(int prod_id, Cart cart)
        {
            string isLogin = HttpContext.Session.GetString("customerSession");
            if (isLogin == null)
            {
                return RedirectToAction("customerLogin");
            }
            else
            {
                cart.prod_id = prod_id;
                cart.cust_id = int.Parse(isLogin);
                cart.product_quantity = 1;
                cart.cart_status = 0;
                _context.tbl_cart.Add(cart);
                _context.SaveChanges();
                TempData["message"] = "Product Successfully Added in Cart";
                return RedirectToAction("fetchAllProducts");
                 
            }
        }
        public IActionResult fetchCart()
        {
            List<Category> categories = _context.tbl_category.ToList();
            ViewData["category"] = categories;

            string customerId = HttpContext.Session.GetString("customerSession");

            if (customerId != null)
            {
                var cart = _context.tbl_cart.Where(c => c.cust_id == int.Parse(customerId)).Include(c => c.products).ToList();

                return View(cart);
            }
            else
            {
                return RedirectToAction("customerLogin");
            }
           
        }
        public IActionResult removeProduct(int id)
        {
            var product = _context.tbl_cart.Find(id);
            _context.tbl_cart.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("fetchCart");
        }
        public IActionResult checkoutProduct()
        {
            
            return View();
        }
        public IActionResult AboutUs()
        {
            List<Category> categories = _context.tbl_category.ToList();
            ViewData["category"] = categories;
                var teamMembers = new List<AboutUs>
            {
                new AboutUs { Name = "Alice Johnson", Position = "CEO", Description = "Alice is the visionary behind our company.", ImageUrl = "/images/alice.jpg" },
                new AboutUs { Name = "Bob Smith", Position = "CTO", Description = "Bob leads our technology division.", ImageUrl = "/images/bob.jpg" },
                new AboutUs { Name = "Carol White", Position = "CFO", Description = "Carol manages our finances.", ImageUrl = "/images/carol.jpg" },
                new AboutUs { Name = "David Brown", Position = "COO", Description = "David oversees our operations.", ImageUrl = "/images/david.jpg" },
                new AboutUs { Name = "Eve Black", Position = "CMO", Description = "Eve is in charge of our marketing.", ImageUrl = "/images/eve.jpg" }
            };

            return View(teamMembers);
        }
        public IActionResult ProductsByCategory(int categoryId)
        {
            var products = _context.tbl_product.Where(p => p.cat_id == categoryId).ToList();
            var categories = _context.tbl_category.ToList();
            ViewData["category"] = categories;
            return View("Index", products);
        }


    }
}
