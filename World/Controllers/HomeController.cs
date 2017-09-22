using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using World.Services;
using World.ViewModels;

namespace World.Controllers
{
    public class HomeController: Controller
    {
        private IMailService _mailSevice;
        private IConfigurationRoot _config;

        public HomeController(IMailService  mailService, IConfigurationRoot config)
        {
            _mailSevice = mailService;
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public ContactViewModel Test()
        {
            return null;
        }

        public IActionResult Contact()
        {
            //throw new Exception("My Friendly Exception");

            return View();
        }

        [HttpPost]
        public IActionResult  Contact(ContactViewModel model)
        {
            if (model.Email.Contains("@abc.com"))
            {
                //ModelState.AddModelError("Email", "'abc.com' domain is not supported"); // to show infront of  'email' control
                ModelState.AddModelError("", "'abc.com' domain is not supported");      //to show  in summary
            }

            if (ModelState.IsValid)
            {
                //_mailSevice.SendMessage("test@test.com", model.Email, "Message Subject", model.Message);
                _mailSevice.SendMessage(_config["MailSettings:ToAddress"], model.Email, "Message Subject", model.Message);

                ViewBag.MessageSent = "Message Sent";
            }

            return View();
        }
    }
}
