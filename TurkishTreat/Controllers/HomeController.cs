using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TurkishTreat.Data;
using TurkishTreat.Models;
using TurkishTreat.Services;
using TurkishTreat.ViewModel;

namespace TurkishTreat.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMailService _mailService;
        private readonly IProductOrderRepository _repository;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IMailService mailService, IProductOrderRepository repository , ILogger<HomeController> logger)
        {
            _mailService = mailService;
            _repository = repository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Shop()
        {
            var results = _repository.GetAllProducts().ToList();
            return View(results);
        }

        [HttpGet("contact")]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                _mailService.SendMessage(model.Email, model.Subject, model.Message);
                ViewBag.UserMessage("Mail Sent!");
            }
            return View();
        }

        [HttpGet("privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
