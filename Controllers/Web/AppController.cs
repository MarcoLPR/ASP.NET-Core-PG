using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ASP.NET_Core_PG.Models;
using ASP.NET_Core_PG.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace ASP.NET_Core_PG.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;
        private IConfigurationRoot _config;
        private ITravelRepository _repository;
        private ILogger<AppController> _logger;

        public AppController(IMailService mailService,
            IConfigurationRoot config,
            ITravelRepository repository,
            ILogger<AppController> logger)
        {
            _mailService = mailService;
            _config = config;
            _repository = repository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Database()
        {
            try
            {
                var data = _repository.GetAllTrips();
                return View(data);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Failed to get trips in Database page: {ex.Message}");
                return Redirect("/error");
            }
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (model.Email.Contains("aol.com"))
            {
                ModelState.AddModelError("Email", "We don't support AOL addresses");
            }
            if (ModelState.IsValid)
            {
                _mailService.SendMail(_config["MailSettings: ToAddress"], model.Email, "From WebSite", model.Message);

                ModelState.Clear();
                ViewBag.UserMessage = "Message Sent";            }
            return View();
        }
    }
}