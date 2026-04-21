using Prova__Prática_STgenetics.entity;

namespace Prova__Prática_STgenetics;

public interface IDiscount
{
    // Verifica se o pedido atende os critérios de promoção
    bool IsMatch(IEnumerable<Product> items);
    
    // Retorna o valor do desconto
    decimal CalculateDiscount(IEnumerable<Product> items);
}