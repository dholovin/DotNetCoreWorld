using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using World.Models;
using World.Services;
using World.ViewModels;

namespace World.Controllers
{
    public class HomeController: Controller
    {
        //private WorldContext _context;      //Before Implementing Repository pattern abstraction
        private readonly IWorldRepository _repository;
        private readonly IMailService _mailSevice;
        private readonly IConfigurationRoot _config;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IMailService  mailService, IConfigurationRoot config, IWorldRepository repository, ILogger<HomeController> logger)
        {
            _mailSevice = mailService;
            _config = config;
            _repository = repository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
            {
                var trips = _repository.GetAllTrips();
                
                //return View(trips);
                return View();
            }
            catch(Exception ex)
            {
                _logger.LogError($"Failed to get Trips on Index page: {ex.Message}");
                return Redirect("/Error");
            }
        }

        public IActionResult About()
        {
            return View();
        }

        public ContactViewModel TestNull()
        {
            var spike = new ContactViewModel() { Name = "Test" };
            return spike;

            //return null;
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
