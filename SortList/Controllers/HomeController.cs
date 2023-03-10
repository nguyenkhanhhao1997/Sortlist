using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SortList.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SortList.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Sort([FromQuery] string input)
        {
            var list = input.Split(',').ToList();

            List<int> inputValue = new List<int>();
            foreach (var item in list)
            {
                if (int.TryParse(item, out int result))
                {
                    inputValue.Add(result);
                }
            }

            var resultList = Sort(inputValue);

            return Ok(string.Join(", ", resultList));
        }

        public List<int> Sort(List<int> listNum)
        {
            List<int> sortingList = new List<int>();
            var orderByDes = listNum.OrderByDescending(x=>x);
            var firstList = orderByDes.Take(10).ToList();
            var secondList = orderByDes.Skip(10).Take(10).ToList();
            var lastList = orderByDes.Skip(20).Take(10).ToList();
            sortingList.AddRange(secondList);
            sortingList.AddRange(firstList);
            sortingList.AddRange(lastList);
            return sortingList;
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
