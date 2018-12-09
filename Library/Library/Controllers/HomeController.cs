using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using Microsoft.VisualStudio.Web.CodeGeneration.Utils;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Library.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = "Librarian")]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

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
        [AllowAnonymous]
        [HttpGet]
        public IActionResult ValidateEmail(string email)
        {
            if (LibraryModel.getLib().Library.LibraryCard.Where(p => p.Email == email).Count() == 0)
                return Json("true");
            else
                return Json(string.Format("an account for address {0} already exists.", email));
        }
        [AllowAnonymous]
        [HttpGet]
        public IActionResult ValidateKey(string SecretKey)
        {
            if (SecretKey == "KH_2286879_Library_key" || SecretKey == "XXX" )
                return Json("true");
            else
                return Json(string.Format("Невернай ключ!"));
        }
    }
}
