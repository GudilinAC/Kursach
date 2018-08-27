using Kyrsach.Data;
using Kyrsach.Models;
using Kyrsach.Models.InstructionViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Kyrsach.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var rnd = new Random();
            HomeViewModel model = new HomeViewModel()
            {
                Random = CreateTitleViewModel(_context.Instructions.OrderByDescending(x => rnd.Next()).Take(5).ToList()),
                Newest = CreateTitleViewModel(_context.Instructions.OrderByDescending(x => x.UpdateDate).Take(5).ToList())
            };
            return View(model);
        }

        public List<InstructionTitleViewModel> CreateTitleViewModel(List<Instruction> list)
        {
            List<InstructionTitleViewModel> resultList = new List<InstructionTitleViewModel>();
            list.ForEach(item => resultList.Add(new InstructionTitleViewModel
            {
                Id = item.Id,
                Name = item.Name,
                Image = item.Image
            }));
            return resultList;
        }

        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );
            return LocalRedirect(returnUrl);
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
