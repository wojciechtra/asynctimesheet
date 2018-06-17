using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Timesheet.Web.Controllers
{
    public class ChatsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}