using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MvcLab4.Identity;
using MvcLab4.Models;

namespace DatabaseScaffoldApplication.Controllers
{
  public class AuthController : Controller
  {
    //User ile alakalı yönetim işlemlerinden sorumlu sınıf.
    private UserManager<ApplicationUser> _userManager;
    //Sign in - sign-out işlemlerinden sorumlu sınıfımız.
    private SignInManager<ApplicationUser> _singInManager;
    //Role ait yönetim işlemlerinden sorumlu sınıf.
    private RoleManager<ApplicationRole> _roleManager;

    public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager)
    {
      _userManager = userManager;
      _singInManager = signInManager;
      _roleManager = roleManager;
    }

    public IActionResult Index()
    {
      return View();
    }

    public async Task<IActionResult> CreateUser()
    {
      var user = new ApplicationUser { UserName = "melike.civelek", Email = "melike@test.com" };
      var result1 = await _userManager.CreateAsync(user, "Melike1234*");

      var result2 = await _roleManager.CreateAsync(new ApplicationRole { Name = "Admin", Description = "Admin" });

      var result3 = await _userManager.AddToRoleAsync(user, "Admin");

      if (result3.Succeeded && result1.Succeeded && result2.Succeeded)
      {
        ViewBag.Message = "User Kaydı başarılı";
      }

      return View();
    }

    [HttpGet]
    public async Task<IActionResult> Login()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginInput model)
    {
      // email göre user var mı kontrolü
      var user = await _userManager.FindByEmailAsync(model.Email);
 

      if (user != null)
      {
        // parola kontrolü
        var passwordConfirmed = await _userManager.CheckPasswordAsync(user, model.Password);

        if (passwordConfirmed)
        {
          // login ol cookie oluştur
          await _singInManager.SignInAsync(user, model.RememberMe);

          return RedirectToAction("AuthenticatedUserView", "Home");
        }
      
      }

      return View();
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> LogOut()
    {
      await _singInManager.SignOutAsync();

      return Redirect("/");
    }


    [Authorize]
    [HttpGet]
    public async Task<IActionResult> AccessDenied()
    {
      // Sayfaya erişim yetkiniz yok
      return View();
    }
  }
}