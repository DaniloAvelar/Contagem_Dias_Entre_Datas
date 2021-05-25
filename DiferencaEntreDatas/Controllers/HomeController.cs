using DiferencaEntreDatas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DiferencaEntreDatas.Controllers
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
            ViewBag.Message = "Seja Bem Vindo(a)!";
            return View();
        }

        [HttpPost]
        public IActionResult Index(DateTime inicial, DateTime final)
        {

            if (inicial.ToString() != "" && final.ToString() != "")
            {
                //calcula anos
                int Anos = new DateTime(final.Subtract(inicial).Ticks).Year - 1;
                DateTime AnosTranscorridos = inicial.AddYears(Anos);

                //calcula meses
                int Meses = 0;

                for (int i = 1; i <= 12; i++)
                {
                    if (AnosTranscorridos.AddMonths(i) == final)
                    {
                        Meses = i;
                        break;
                    }
                    else if (AnosTranscorridos.AddMonths(i) >= final)
                    {
                        Meses = i - 1;
                        break;
                    }

                }

                //calcula dias
                int Dias = final.Subtract(AnosTranscorridos.AddMonths(Meses)).Days;
                //    //atribuindo os valores
                var tdAno = Anos + "  ano(s)  " + Meses + "  mes(es) " + Dias + " dia(s)";

                var Resultado = tdAno;

                ViewBag.Message = "O Resultado entre ambas as datas são:";
                ViewBag.result = Resultado;
            }

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
    }
}
