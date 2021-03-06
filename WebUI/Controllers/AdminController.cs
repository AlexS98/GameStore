﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return RedirectToAction("AdminLogin", "Account");
        }

        [Authorize(Roles = "Admin,Moderator")]
        public ActionResult Games()
        {
            return View();
        }


    }
}