using Prova__Prática_STgenetics.entity;

namespace Prova__Prática_STgenetics.repository;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();
}