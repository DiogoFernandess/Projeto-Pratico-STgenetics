using Prova__Prática_STgenetics.entity;
using Prova__Prática_STgenetics.enums;
using Xunit;

namespace Prova__Prática_STgenetics.Tests;

public class OrderDiscountTests
{
    [Fact] 
    public void CalculateTotal_WithFullCombo_Apply20PercentDiscount()
    {
        var discountService = new DiscountService();
        var order = new Order();
            
        order.AddItem(new Product("X Burger", 5.00m, ProductCategoryEnum.Sandwich));
        order.AddItem(new Product("Batata", 2.00m, ProductCategoryEnum.Fries));
        order.AddItem(new Product("Refri", 2.50m, ProductCategoryEnum.Soda));
        
        order.CalculateTotals(discountService);
        
        Assert.Equal(9.50m, order.Subtotal);
        Assert.Equal(1.90m, order.Discount);
        Assert.Equal(7.60m, order.Total);
    }
    
    [Fact] 
    public void CalculateTotal_WithSandwichAndSoda_Apply15PercentDiscount()
    {
        var discountService = new DiscountService();
        var order = new Order();
            
        order.AddItem(new Product("X Burger", 5.00m, ProductCategoryEnum.Sandwich));
        order.AddItem(new Product("Refri", 2.50m, ProductCategoryEnum.Soda));
        
        order.CalculateTotals(discountService);
        
        Assert.Equal(7.50m, order.Subtotal);
        Assert.Equal(1.90m, order.Discount);
        Assert.Equal(7.60m, order.Total);
    }
    
    [Fact] 
    public void CalculateTotal_WithSandwichAndFires_Apply10PercentDiscount()
    {
        var discountService = new DiscountService();
        var order = new Order();
            
        order.AddItem(new Product("X Burger", 5.00m, ProductCategoryEnum.Sandwich));
        order.AddItem(new Product("Batata", 2.00m, ProductCategoryEnum.Fries));
        
        order.CalculateTotals(discountService);
        
        Assert.Equal(7.00m, order.Subtotal);
        Assert.Equal(0.70m, order.Discount);
        Assert.Equal(6.30m, order.Total);
    }
    
}
