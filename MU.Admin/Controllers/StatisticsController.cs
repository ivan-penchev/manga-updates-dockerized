using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MU.Admin.Controllers
{
    public class StatisticsController : BaseControllerClass
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
