using Microsoft.AspNetCore.Mvc;
using Prova__Prática_STgenetics.entity;
using Prova__Prática_STgenetics.repository;

namespace Prova__Prática_STgenetics.controller;

[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderRepository _repository;
    private readonly IProductRepository _productRepository;
    
    public OrderController(IOrderRepository repository, IProductRepository productRepository)
    {
        _repository = repository;
        _productRepository = productRepository;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] OrderRequestDto request, [FromServices] DiscountService discountService)
    {
        var order = new Order();

        try
        {
            var allProducts = await _productRepository.GetAllAsync();

            foreach (var productId in request.ProductIds)
            {
                var product = allProducts.FirstOrDefault(p => p.Id == productId);
                if (product == null)
                    return NotFound(new { error = $"Produto com ID {productId} não encontrado no cardápio." });

                order.AddItem(product);
            }

            order.CalculateTotals(discountService);
            await _repository.AddAsync(order);

            return CreatedAtAction(nameof(GetById), new { id = order.Id }, order);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orders = await _repository.GetAllAsync();
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var order = await _repository.GetByIdAsync(id);
        return Ok(order);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] OrderRequestDto request, [FromServices] DiscountService discountService)
    {
        var order = await _repository.GetByIdAsync(id);
        if (order == null) 
            return NotFound(new { error = "Pedido não encontrado." });

        try
        {
            order.ClearItems(); 

            var allProducts = await _productRepository.GetAllAsync();
            foreach (var productId in request.ProductIds)
            {
                var product = allProducts.FirstOrDefault(p => p.Id == productId);
                if (product == null) 
                    return NotFound(new { error = $"Produto com ID {productId} não encontrado." });

                order.AddItem(product);
            }

            order.CalculateTotals(discountService);
            await _repository.UpdateAsync(order);

            return Ok(order);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var order = await _repository.GetByIdAsync(id);
        if (order == null) 
            return NotFound(new { error = "Pedido não encontrado." });

        await _repository.DeleteAsync(id);
        return NoContent(); 
    }
    
}