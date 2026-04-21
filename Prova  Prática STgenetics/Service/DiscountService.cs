using Prova__Prática_STgenetics.entity;

namespace Prova__Prática_STgenetics;

public class DiscountService
{
    private readonly List<IDiscount> _discounts;

    public DiscountService()
    {
        _discounts = new List<IDiscount>
        {
            new FullComboDiscount(),
            new SandwichAndSodaDiscount(),
            new SandwichAndFriesDiscount()
        };
    }
    
    public decimal CalculateBestDiscount(IEnumerable<Product> items)
    {
        if (items == null || !items.Any()) return 0;
        
        var applicableStrategy = _discounts.FirstOrDefault(s => s.IsMatch(items));
        
        return applicableStrategy?.CalculateDiscount(items) ?? 0m;
    }
}