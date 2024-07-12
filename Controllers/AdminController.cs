using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoleMates.Models;
using Microsoft.AspNetCore.Http;

namespace SoleMates.Controllers
{
    public class AdminController : Controller
    {
        private readonly myContext _context;
        private readonly IWebHostEnvironment _env;

        public AdminController(myContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        private bool IsAdminLoggedIn()
        {
            return HttpContext.Session.GetString("admin_session") != null;
        }

        private void EnsureAdminLoggedIn()
        {
            if (!IsAdminLoggedIn())
            {
                RedirectToAction("Login").ExecuteResult(ControllerContext);
            }
        }

        public IActionResult Index()
        {
            if (IsAdminLoggedIn())
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string adminEmail, string adminPassword)
        {
            var row = _context.tbl_admin.FirstOrDefault(a => a.admin_email == adminEmail);
            if (row != null && row.admin_password == adminPassword)
            {
                HttpContext.Session.SetString("admin_session", row.admin_id.ToString());
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.message = "Incorrect Username or Password";
                return View();
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("admin_session");
            return RedirectToAction("Login");
        }

        public IActionResult Profile()
        {
            EnsureAdminLoggedIn();

            var adminId = HttpContext.Session.GetString("admin_session");
            var admin = _context.tbl_admin.Where(a => a.admin_id == int.Parse(adminId)).ToList();
            return View(admin);
        }

        [HttpPost]
        public IActionResult Profile(Admin admin)
        {
            _context.tbl_admin.Update(admin);
            _context.SaveChanges();
            return RedirectToAction("Profile");
        }

        [HttpPost]
        public IActionResult ChangeProfileImage(IFormFile admin_image, Admin admin)
        {
            string imagePath = Path.Combine(_env.WebRootPath, "admin_image", admin_image.FileName);
            using (FileStream fs = new FileStream(imagePath, FileMode.Create))
            {
                admin_image.CopyTo(fs);
            }
            admin.admin_image = admin_image.FileName;
            _context.tbl_admin.Update(admin);
            _context.SaveChanges();
            return RedirectToAction("Profile");
        }

        public IActionResult fetchCustomer()
        {
            
            EnsureAdminLoggedIn();
            return View(_context.tbl_customer.ToList());
        }

        public IActionResult customerDetails(int id)
        {
            EnsureAdminLoggedIn();
            return View(_context.tbl_customer.FirstOrDefault(c => c.customer_id == id));
        }

        public IActionResult updateCustomer(int id)
        {
            EnsureAdminLoggedIn();
            return View(_context.tbl_customer.Find(id));
        }

        [HttpPost]
        public IActionResult updateCustomer(Customer customer)
        {
            EnsureAdminLoggedIn();
            _context.tbl_customer.Update(customer);
            _context.SaveChanges();
            return RedirectToAction("fetchCustomer");
        }

        public IActionResult deletePermission(int id)
        {
            EnsureAdminLoggedIn();
            return View(_context.tbl_customer.FirstOrDefault(c => c.customer_id == id));
        }

        public IActionResult deleteCustomer(int id)
        {
            EnsureAdminLoggedIn();
            var customer = _context.tbl_customer.Find(id);
            _context.tbl_customer.Remove(customer);
            _context.SaveChanges();
            return RedirectToAction("fetchCustomer");
        }

        public IActionResult fetchCategory()
        {
            
            EnsureAdminLoggedIn();
            return View(_context.tbl_category.ToList());
        }

        public IActionResult addCategory()
        {
            EnsureAdminLoggedIn();
            return View();
        }

        [HttpPost]
        public IActionResult addCategory(Category cat)
        {
            EnsureAdminLoggedIn();
            _context.tbl_category.Add(cat);
            _context.SaveChanges();
            return RedirectToAction("fetchCategory");
        }

        public IActionResult updateCategory(int id)
        {
            EnsureAdminLoggedIn();
            var category = _context.tbl_category.Find(id);
            return View(category);
        }

        [HttpPost]
        public IActionResult updateCategory(Category cat)
        {
            EnsureAdminLoggedIn();
            _context.tbl_category.Update(cat);
            _context.SaveChanges();
            return RedirectToAction("fetchCategory");
        }

        public IActionResult deletePermissionCategory(int id)
        {
            EnsureAdminLoggedIn();
            return View(_context.tbl_category.FirstOrDefault(c => c.category_id == id));
        }

        public IActionResult deleteCategory(int id)
        {
            EnsureAdminLoggedIn();
            var category = _context.tbl_category.Find(id);
            _context.tbl_category.Remove(category);
            _context.SaveChanges();
            return RedirectToAction("fetchCategory");
        }

        public IActionResult fetchProduct()
        {
            
            EnsureAdminLoggedIn();
            return View(_context.tbl_product.ToList());
        }

        public IActionResult addProduct()
        {
            EnsureAdminLoggedIn();
            List<Category> categories = _context.tbl_category.ToList();
            ViewData["category"] = categories;
            return View();
        }

        [HttpPost]
        public IActionResult addProduct(Product prod, IFormFile product_image)
        {
            EnsureAdminLoggedIn();
            string imgName = Path.GetFileName(product_image.FileName);
            string imgPath = Path.Combine(_env.WebRootPath, "product_images", imgName);
            using (FileStream fs = new FileStream(imgPath, FileMode.Create))
            {
                product_image.CopyTo(fs);
            }
            prod.product_image = imgName;
            _context.tbl_product.Add(prod);
            _context.SaveChanges();
            return RedirectToAction("fetchProduct");
        }

        public IActionResult productDetails(int id)
        {
            EnsureAdminLoggedIn();
            return View(_context.tbl_product.Include(p => p.Category).FirstOrDefault(p => p.product_id == id));
        }

        public IActionResult deletePermissionProduct(int id)
        {
            EnsureAdminLoggedIn();
            return View(_context.tbl_product.FirstOrDefault(p => p.product_id == id));
        }

        public IActionResult deleteProduct(int id)
        {
            EnsureAdminLoggedIn();
            var product = _context.tbl_product.Find(id);
            _context.tbl_product.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("fetchProduct");
        }

        public IActionResult updateProduct(int id)
        {
            EnsureAdminLoggedIn();
            List<Category> categories = _context.tbl_category.ToList();
            ViewData["category"] = categories;

            var product = _context.tbl_product.Find(id);
            ViewBag.selectedCategoryId = product.cat_id;
            return View(product);
        }

        [HttpPost]
        public IActionResult updateProduct(Product prod)
        {
            EnsureAdminLoggedIn();
            _context.tbl_product.Update(prod);
            _context.SaveChanges();
            return RedirectToAction("fetchProduct");
        }

        public IActionResult ChangeProductImage(IFormFile product_image, Product product)
        {
            EnsureAdminLoggedIn();
            string imagePath = Path.Combine(_env.WebRootPath, "product_images", product_image.FileName);
            using (FileStream fs = new FileStream(imagePath, FileMode.Create))
            {
                product_image.CopyTo(fs);
            }
            product.product_image = product_image.FileName;
            _context.tbl_product.Update(product);
            _context.SaveChanges();
            return RedirectToAction("fetchProduct");
        }

        public IActionResult fetchFeedback()
        {
            
            EnsureAdminLoggedIn();
            return View(_context.tbl_feedback.ToList());
        }

        public IActionResult deletePermissionFeedback(int id)
        {
            EnsureAdminLoggedIn();
            return View(_context.tbl_feedback.FirstOrDefault(f => f.feedback_id == id));
        }

        public IActionResult deleteFeedback(int id)
        {
            EnsureAdminLoggedIn();
            var feedback = _context.tbl_feedback.Find(id);
            _context.tbl_feedback.Remove(feedback);
            _context.SaveChanges();
            return RedirectToAction("fetchFeedback");
        }

        public IActionResult fetchCart()
        {
            
            EnsureAdminLoggedIn();
            var cart = _context.tbl_cart.Include(c => c.products).Include(c => c.customers).ToList();
            return View(cart);
        }

        public IActionResult deletePermissionCart(int id)
        {
            EnsureAdminLoggedIn();
            return View(_context.tbl_cart.FirstOrDefault(c => c.cart_id == id));
        }

        public IActionResult deleteCart(int id)
        {
            EnsureAdminLoggedIn();
            var cart = _context.tbl_cart.Find(id);
            _context.tbl_cart.Remove(cart);
            _context.SaveChanges();
            return RedirectToAction("fetchCart");
        }

        public IActionResult updateCart(int id)
        {
            EnsureAdminLoggedIn();
            var cart = _context.tbl_cart.Find(id);
            return View(cart);
        }

        [HttpPost]
        public IActionResult updateCart(int cart_status, Cart cart)
        {
            EnsureAdminLoggedIn();
            cart.cart_status = cart_status;
            _context.tbl_cart.Update(cart);
            _context.SaveChanges();
            return RedirectToAction("fetchCart");
        }
    }
}
