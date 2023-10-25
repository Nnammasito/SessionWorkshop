using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SessionWorkshop.Models;

namespace SessionWorkshopr.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }
    [HttpGet]
    [Route("")]
    public IActionResult Index()
    {
        HttpContext.Session.Clear();
        return View();
    }

    [HttpGet]
    [Route("Calculadora")]
    public IActionResult Calculadora()
    {
        ViewBag.Resultado = 22;
        return View();
    }

    [HttpPost]
    [Route("Calculadora")]
    public IActionResult Calculadora(int resultado, string operacion)
    {
        if (operacion == null)
        {
            return RedirectToAction("Calculadora");
        }

        switch (operacion)
        {
            case "suma":
                resultado++;
                break;
            case "resta":
                if (resultado > 0)
                {
                    resultado--;
                }
                else
                {
                    resultado = 0;
                }
                break;
            case "multiplicacion":
                resultado = resultado * 2;
                break;
            case "ramdom":
                Random random = new Random();
                resultado = random.Next(0, 11);
                break;
        }
        ViewBag.Resultado = resultado;
        return View();
    }

    [HttpPost]
    [Route("Login")]
    public IActionResult Login(string Name)
    {
        if (Name == null)
        {
            return RedirectToAction("Index");
        }
        HttpContext.Session.SetString("Name", Name);
        return RedirectToAction("Calculadora");
    }

    [HttpGet]
    [Route("Privacy")]
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