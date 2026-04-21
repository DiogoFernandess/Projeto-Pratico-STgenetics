namespace Prova__Prática_STgenetics.entity;

public class Order
{
    public Guid Id { get; private set; }
    private readonly List<Product> _items = new();
    public IReadOnlyCollection<Product> Items => _items.AsReadOnly();

    public decimal Subtotal => _items.Sum(i => i.Price);
    public decimal Discount { get; private set; }
    public decimal Total => Subtotal - Discount;

    public void AddItem(Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product), "O produto não pode ser nulo.");
        }

        if (_items.Any(i => i.Category == product.Category))
        {
            throw new InvalidOperationException($"O pedido já contém um item da categoria {product.Category}. Item duplicado.");
        }

        _items.Add(product);
    }
    
    public void CalculateTotals(DiscountService discountService)
    {
        Discount = discountService.CalculateBestDiscount(_items);
    }
    
    public void ClearItems()
    {
        _items.Clear();
    }
}