using CategoriasMvc.Models;
using CategoriasMvc.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CategoriasMvc.Controllers;

public class CategoriasController : Controller
{
    private readonly ICategoriaService _categoriaService;

    public CategoriasController(ICategoriaService categoriaService)
    {
        _categoriaService = categoriaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoriaViewModel>>> Index()
    {
        var result = await _categoriaService.GetCategorias();

        if (result == null)
        {
            return View("Error");
        }

        return View(result);
    }

    [HttpGet]
    public IActionResult CriarNovaCategoria()
    {
        return View();
    }

    [HttpPost]
    public async Task<ActionResult<CategoriaViewModel>> CriarNovaCategoria(CategoriaViewModel categoriaVM)
    {
        if (ModelState.IsValid)
        {
            var result = await _categoriaService.CriarCategoria(categoriaVM);

            if (result != null)
            {
                return RedirectToAction(nameof(Index));
            }
        }
        ViewBag.Erro = "Erro ao criar Categoria";
        return View(categoriaVM);
    }
}