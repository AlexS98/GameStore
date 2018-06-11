using Microsoft.Owin.Security;
using StoreDomain.Abstract;
using StoreDomain.Entities;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using WebUI.Models.ViewModels;

[assembly: InternalsVisibleTo("StoreDomain")]
namespace WebUI.Controllers
{
    class AccountController : Controller
    {
        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;
        private readonly IGenericRepository<User> userRepository;
        private readonly IGenericRepository<Admin> adminRepository;

        public AccountController(IGenericRepository<User> users, IGenericRepository<Admin> admins)
        {
            userRepository = users;
            adminRepository = admins;
        }

        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.UserName = AuthenticationManager.User.Identity.Name;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserLogin(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                IPerson user = null;
                user.Role = UserRole.Outsider;
                user = userRepository.Get().First(u => u.Nickname == model.Nickname &&
                        u.Password == model.Password);

                if (user.Role == UserRole.Outsider)
                {
                    ModelState.AddModelError("", @"Wrong login or password.");
                }
                else
                {
                    LoginClaims(user);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminLogin(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                IPerson admin = null;
                admin.Role = UserRole.Outsider;
                admin = adminRepository.Get().First(u => u.Nickname == model.Nickname &&
                        u.Password == model.Password);

                if (admin.Role == UserRole.Outsider)
                {
                    ModelState.AddModelError("", @"Wrong login or password.");
                }
                else
                {
                    LoginClaims(admin);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
        }

        private void LoginClaims(IPerson user)
        {
            ClaimsIdentity claim = new ClaimsIdentity("ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
            claim.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.GetPersonId().ToString(),
                ClaimValueTypes.String));
            claim.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, user.Nickname, ClaimValueTypes.String));
            claim.AddClaim(new Claim(
                "http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                "OWIN Provider", ClaimValueTypes.String));
            claim.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.ToString(), ClaimValueTypes.String));
            AuthenticationManager.SignOut();
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true }, claim);

        }

        public ActionResult Logout()
        {
            ViewBag.UserName = AuthenticationManager.User.Identity.Name;
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.UserName = AuthenticationManager.User.Identity.Name;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel registerModel)
        {
            ViewBag.UserName = AuthenticationManager.User.Identity.Name;
            userRepository.Create((User)registerModel.ToIPerson());
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult RegisterAdmin()
        {
            ViewBag.UserName = AuthenticationManager.User.Identity.Name;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterAdmin(RegisterViewModel registerModel)
        {
            ViewBag.UserName = AuthenticationManager.User.Identity.Name;
            adminRepository.Create((Admin)registerModel.ToIPerson());
            return RedirectToAction("Login", "Account");
        }
    }
}