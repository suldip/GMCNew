using Microsoft.AspNetCore.Mvc;
using global::GMC.Models.GMC;
using global::GMC.Interface.GMC;
using global::GMC.Models.GMC.BusinessLogic;

namespace GMC.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILoginBL _loginBL;
        private readonly IUserRegistrationBL _registrationBL;
        private readonly global::GMC.DAL.Repository.GMC.MasterDAL _masterDal;

        public AccountController(ILoginBL loginBL, IUserRegistrationBL registrationBL, global::GMC.DAL.Repository.GMC.MasterDAL masterDal)
        {
            _loginBL = loginBL;
            _registrationBL = registrationBL;
            _masterDal = masterDal;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                bool isValid = await _loginBL.ValidateUser(model);
                if (isValid)
                {
                    HttpContext.Session.SetString("UserName", model.Username);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "Invalid Username or Password";
                }
            }
            return View(model);
        }
        
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult UserRegistration()
        {
            ViewBag.Roles = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_masterDal.GetUserRoles(), "RoleName", "RoleName");
            return View();
        }

        [HttpPost]
        public IActionResult UserRegistration(UserRegistrationModel model)
        {
            ViewBag.Roles = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(_masterDal.GetUserRoles(), "RoleName", "RoleName");
            if (ModelState.IsValid)
            {
                // Inject the standard framework authenticated user into createdby
                var sessionUser = HttpContext.Session.GetString("UserName");
                model.createdby = !string.IsNullOrEmpty(sessionUser) ? sessionUser : "System";

                bool isSuccess = _registrationBL.RegisterUser(model);
                if (isSuccess)
                {
                    ViewBag.Success = "User registered successfully!";
                    ModelState.Clear();
                    return View();
                }
                else
                {
                    ViewBag.Error = "An error occurred during registration.";
                }
            }
            return View(model);
        }
    }
}
