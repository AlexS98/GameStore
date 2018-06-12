using Microsoft.Owin.Security;
using GameStore.StoreDomain.Abstract;
using GameStore.StoreDomain.Entities;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using WebUI.Models.ViewModels;

namespace GameStore.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;
        private readonly IGenericRepository<User> userRepository;
        private readonly IGenericRepository<Admin> adminRepository;
        private readonly IGenericRepository<UserCabinet> cabinetRepository;

        public AccountController(IGenericRepository<User> users, IGenericRepository<Admin> admins,
            IGenericRepository<UserCabinet> cabinets)
        {
            userRepository = users;
            adminRepository = admins;
            cabinetRepository = cabinets;
        }

        [HttpGet]
        public ActionResult UserLogin()
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
                IPerson user = new User()
                {
                    Role = UserRole.Outsider
                };
                user = userRepository.Get().FirstOrDefault(u => u.Nickname == model.Nickname &&
                        u.Password == model.Password);

                if (user == null || user.Role == UserRole.Outsider)
                {
                    ModelState.AddModelError("", @"Wrong login or password.");
                }
                else
                {
                    LoginClaims(user);
                    if (cabinetRepository.GetWithInclude(x => x.User).FirstOrDefault(x => x.User == user) == null)
                        return RedirectToAction("CreateCabinet", "Home", new { id = user.GetPersonId() });
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
        public ActionResult RegisterUser()
        {
            ViewBag.UserName = AuthenticationManager.User.Identity.Name;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterUser(PersonViewModel registerModel)
        {
            ViewBag.UserName = AuthenticationManager.User.Identity.Name;
            registerModel.Role = UserRole.User;
            if (ModelState.IsValid)
            {
                userRepository.Create((User)registerModel.ToIPerson());
                return RedirectToAction("UserLogin", "Account");
            }
            return View(registerModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminLogin(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                IPerson admin = new Admin()
                {
                    Role = UserRole.Outsider
                };
                admin = adminRepository.Get().FirstOrDefault(u => u.Nickname == model.Nickname &&
                        u.Password == model.Password);

                if (admin.Role == UserRole.Outsider)
                {
                    ModelState.AddModelError("", @"Wrong login or password.");
                }
                else
                {
                    LoginClaims(admin);
                    return RedirectToAction("Games", "Admin");
                }
            }
            return View(model);
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
        public ActionResult RegisterAdmin(PersonViewModel registerModel)
        {
            ViewBag.UserName = AuthenticationManager.User.Identity.Name;
            registerModel.Role = UserRole.Admin;
            adminRepository.Create((Admin)registerModel.ToIPerson());
            return RedirectToAction("AdminLogin", "Account");
        }
    }
}