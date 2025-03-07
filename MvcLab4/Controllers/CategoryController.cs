using Microsoft.AspNetCore.Mvc;
using MvcLab4.Entities;
using MvcLab4.Repository;

namespace MvcLab4.Controllers
{
  public class CategoryController : Controller
  {
    private ICategoryRepository _repository;

    public CategoryController(ICategoryRepository repository)
    {
      _repository = repository;
    }

    public IActionResult Index()
    {
     
      _repository.Create(new Category { CategoryName = "kategori Örnek" });
      int result = _repository.Save();

      if (result > 0)
      {
        ViewBag.Message = "Kayıt Başarılı";
      }

      return View();
    }
  }
}