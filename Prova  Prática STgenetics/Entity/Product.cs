using Prova__Prática_STgenetics.enums;

namespace Prova__Prática_STgenetics.entity;

public class Product
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public ProductCategoryEnum CategoryEnum { get; private set; }
    
    public Product(string name, decimal price, ProductCategoryEnum categoryEnum)
    {
        Id = Guid.NewGuid();
        Name = name;
        Price = price;
        CategoryEnum = categoryEnum;
    }
}