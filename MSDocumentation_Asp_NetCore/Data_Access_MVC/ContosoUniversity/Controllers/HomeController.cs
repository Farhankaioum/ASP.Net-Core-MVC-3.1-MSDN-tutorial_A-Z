using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ContosoUniversity.Models;
using ContosoUniversity.Data;
using ContosoUniversity.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ContosoUniversity.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SchoolContext _context;

        public HomeController(ILogger<HomeController> logger, SchoolContext context)
        {
            _logger = logger;
            this._context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        // about page for how many student and enrollments
        public async Task<ActionResult> About()
        {
            var data = from student in _context.Students
                       group student by student.EnrollmentDate into dataGroup
                       select new EnrollmentDateGroupViewModel
                       {
                           EnrollmentDate = dataGroup.Key,
                           StudentCount = dataGroup.Count()
                       };

            return View(await data.AsNoTracking().ToListAsync());
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
    }
}
