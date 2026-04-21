using Microsoft.EntityFrameworkCore;
using Prova__Prática_STgenetics.entity;

namespace Prova__Prática_STgenetics.repository;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _context.Products.ToListAsync();
    }
}