using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult OAuth2Starter()
        {
            return Redirect("http://10.0.2.2/MultiPurposeAuthSite/Account/OAuthAuthorize?client_id=40319c0100f94ff3aab3004c8bdb5e52&response_type=token&scope=profile%20email%20phone%20address%20userid&state=xAk2xv64R0");
            //return View();
        }

        public IActionResult OAuth2Redirect()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
