﻿using StoreDomain.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;

[assembly: InternalsVisibleToAttribute("StoreDomain")]
namespace WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly EFDbContext _db = new EFDbContext();
        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        [HttpGet]
        public ActionResult Login()
        {
            ViewBag.UserName = AuthenticationManager.User.Identity.Name;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(IPerson model)
        {
            if (ModelState.IsValid)
            {
                IPerson user = null;
                Role role = null;
                if (!model.Manager)
                {
                    user = await _db.Inventors.FirstOrDefaultAsync(u => u.FullName == model.Name &&
                    u.Password == model.Password);
                }
                else
                {
                    Register register = await _db.Registers.FirstOrDefaultAsync(u => u.FullName == model.Name &&
                    u.Password == model.Password);
                    if (register != null)
                    {
                        role = await _db.Roles.FirstOrDefaultAsync(u => u.RoleId == register.RoleId);
                        user = register;
                    }
                }

                if (user == null)
                {
                    ModelState.AddModelError("", @"Wrong login or password.");
                }
                else
                {
                    ClaimsIdentity claim = new ClaimsIdentity("ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                    claim.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.GetPersonId().ToString(),
                        ClaimValueTypes.String));
                    claim.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, user.FullName, ClaimValueTypes.String));
                    claim.AddClaim(new Claim(
                        "http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                        "OWIN Provider", ClaimValueTypes.String));
                    claim.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, (role != null) ? role.UserRole :
                        "Registred user", ClaimValueTypes.String));
                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = true }, claim);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);
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
        public ActionResult Register(RegisterModel registerModel)
        {
            ViewBag.UserName = AuthenticationManager.User.Identity.Name;
            var inventors = new GenericRepository<Inventor>();
            inventors.Create(registerModel.ToInventor());
            return RedirectToAction("Login", "Account");
        }
    }
}