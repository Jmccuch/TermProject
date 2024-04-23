﻿using Microsoft.AspNetCore.Mvc;
using TermProjectAPI;

namespace TermProject.Controllers
{
    public class ProfileController : Controller
    {

        API api = new API();

        public IActionResult Index(string username)
        {
            

           System.Diagnostics.Debug.WriteLine("CLICKED ON PROFILE: " + username);

            User user = api.GetUserInfo(username);

            return View(user);
        }
        public IActionResult RedirectLogOut()
        {
            return View("~/Views/Home/Index.cshtml");
        }
    }
}
