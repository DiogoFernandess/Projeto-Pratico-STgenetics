using Prova__Prática_STgenetics.entity;

namespace Prova__Prática_STgenetics.repository;

public interface IOrderRepository
{
    Task<Order> GetByIdAsync(Guid id);
    Task<IEnumerable<Order>> GetAllAsync();
    Task AddAsync(Order order);
    Task UpdateAsync(Order order);
    Task DeleteAsync(Guid id);
}