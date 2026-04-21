using Prova__Prática_STgenetics.repository;

namespace Prova__Prática_STgenetics.controller;

using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

//Consulta de Cardápio
[ApiController]
[Route("api/[controller]")]
public class MenuController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    
    public MenuController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetMenu()
    {
        var menu = await _productRepository.GetAllAsync();
        
        if (menu == null)
        {
            return NotFound(new { error = "Cardápio não encontrado." });
        }

        return Ok(menu);
    }
}