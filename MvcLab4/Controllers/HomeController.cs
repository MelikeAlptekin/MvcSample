using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcLab4.Models;
using System.Diagnostics;

namespace MvcLab4.Controllers
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

    [Authorize]
    public IActionResult AuthenticatedUserView()
    {
      return View();
    }

    //role based authorization
    [Authorize(Roles = "Admin")]
    public IActionResult AdminView()
    {
      return View();
    }

    //policy base authorization
    [Authorize(Policy = "AdminOrManager")]
    public IActionResult AdminOrManagerView()
    {
      return View();
    }

    //requirement policy based authorization
    [Authorize(Policy = "DomainCheck")]
    public IActionResult SpesificDomainView()
    {
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