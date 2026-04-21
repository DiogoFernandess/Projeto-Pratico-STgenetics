using Microsoft.AspNetCore.Mvc;
using Moq;
using Prova__Prática_STgenetics.controller;
using Prova__Prática_STgenetics.entity;
using Prova__Prática_STgenetics.enums;
using Prova__Prática_STgenetics.repository;
using Xunit;

namespace Prova__Prática_STgenetics.Tests;

public class CreateOrderTests
{
    [Fact]
        public async Task CreateOrder_WithValidProducts_ShouldReturn201Created()
        {
           
            var mockOrderRepo = new Mock<IOrderRepository>();
            var mockProductRepo = new Mock<IProductRepository>();
            var discountService = new DiscountService();
            
            var fakeProductId = Guid.NewGuid();
            var fakeProduct = new Product("X Burger", 5.00m, ProductCategoryEnum.Sandwich);
            
            typeof(Product).GetProperty("Id").SetValue(fakeProduct, fakeProductId);

            var fakeProductList = new List<Product> { fakeProduct };
            
            mockProductRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(fakeProductList);
            
            var controller = new OrderController(mockOrderRepo.Object, mockProductRepo.Object);
            
            var request = new OrderRequestDto
            {
                ProductIds = new List<Guid> { fakeProductId }
            };
            
            var result = await controller.CreateOrder(request, discountService);
            
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            
            Assert.Equal("GetById", createdResult.ActionName);
            
            mockOrderRepo.Verify(repo => repo.AddAsync(It.IsAny<Order>()), Times.Once);
        }
}