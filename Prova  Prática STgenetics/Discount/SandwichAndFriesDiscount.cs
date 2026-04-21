using Prova__Prática_STgenetics.entity;
using Prova__Prática_STgenetics.enums;

namespace Prova__Prática_STgenetics;

public class SandwichAndFriesDiscount : IDiscount
{
    public bool IsMatch(IEnumerable<Product> items)
    {
        return items.Any(i => i.CategoryEnum == ProductCategoryEnum.Sandwich) &&
               !items.Any(i => i.CategoryEnum == ProductCategoryEnum.Soda) &&
               items.Any(i => i.CategoryEnum == ProductCategoryEnum.Fries);
    }

    public decimal CalculateDiscount(IEnumerable<Product> items)
    {
        var subtotal = items.Sum(i => i.Price);
        return subtotal * 0.10m;
    }
} 