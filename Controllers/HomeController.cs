using Login_user.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace Login_user.Controllers
{
    public class HomeController : Controller
    {

        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        private readonly NmcSchoolContext context;
        public HomeController(NmcSchoolContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        // register user
        public IActionResult Register()
        {
            List<SelectListItem> gender = new()
            {
                new SelectListItem
                {
                    Value="male",Text="male"
                },
                new SelectListItem
                {
                    Value="female",
                    Text="female"
                }
                
            };
            ViewBag.gender=gender;
            return View();
        }

        // get register 
        [HttpPost]
        public async Task< IActionResult> Register(UserLogin user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await context.UserLogins.AddAsync(user);
                    await context.SaveChangesAsync();
                    TempData["dataAdd"] = "Data add success..";
                    return RedirectToAction("login","home");

                }
                else
                {
                    TempData["dataFailed"] = "Data allredy added..";
                }
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
            return View();
        }

        public enum gender
        {
            male,
            female
        }

        // Dashbord
        public IActionResult Dashbord()
        {
            if(HttpContext.Session.GetString("userSession")== null)
            {
                return RedirectToAction("login");
            }

            return View();
        }

        // Logout section
        public IActionResult Logout()
        {
            if (HttpContext.Session.GetString("userSession") != null)
            {
                HttpContext.Session.Remove("userSession");
                RedirectToAction("Login","home");
            }
            if(HttpContext.Session.GetString("userSession")== null)
            {
                return RedirectToAction("login");
            }

            return View();
        }

        // view section 
        public IActionResult Login()
        {
            if(HttpContext.Session.GetString("userSession") !=null)
            {
                return RedirectToAction("dashbord","home");
            }

            return View();
        }

        // GET LONGIN  SECTION
        [HttpPost]
        public IActionResult Login(UserLogin user)
        {
            var myuser=context.UserLogins.Where(x =>
            x.Gmail==user.Gmail && x.Password==user.Password).FirstOrDefault();
            if(myuser != null)
            {
                HttpContext.Session.SetString("userSession", myuser.Gmail);
                return RedirectToAction("dashbord", "home");
            }
            if(myuser == null)
            {
                TempData["error"] = "invalid user";
                RedirectToAction("login");
            }

            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
