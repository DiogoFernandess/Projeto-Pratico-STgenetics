using Microsoft.AspNetCore.Mvc;
using Moq;
using Prova__Prática_STgenetics.controller;
using Prova__Prática_STgenetics.entity;
using Prova__Prática_STgenetics.enums;
using Prova__Prática_STgenetics.repository;
using Xunit;

namespace Prova__Prática_STgenetics.Tests;

public class DuplicateItemsTests
{
    [Fact]
        public async Task CreateOrder_WithDuplicateCategory_ShouldReturn400BadRequest()
        {
            var mockOrderRepo = new Mock<IOrderRepository>();
            var mockProductRepo = new Mock<IProductRepository>();
            var discountService = new DiscountService();

            var fakeBurgerId = Guid.NewGuid();
            var burger = new Product("X Burger", 5.00m, ProductCategoryEnum.Sandwich);
            typeof(Product).GetProperty("Id").SetValue(burger, fakeBurgerId);

            var fakeBaconId = Guid.NewGuid();
            var baconBurger = new Product("X Bacon", 7.00m, ProductCategoryEnum.Sandwich);
            typeof(Product).GetProperty("Id").SetValue(baconBurger, fakeBaconId);

            var fakeProductList = new List<Product> { burger, baconBurger };
            mockProductRepo.Setup(repo => repo.GetAllAsync()).ReturnsAsync(fakeProductList);

            var controller = new OrderController(mockOrderRepo.Object, mockProductRepo.Object);

            var request = new OrderRequestDto
            {
                ProductIds = new List<Guid> { fakeBurgerId, fakeBaconId }
            };

            var result = await controller.CreateOrder(request, discountService);
            
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            
            var errorObject = badRequestResult.Value;
            
            var errorMessage = errorObject?.GetType().GetProperty("error")?.GetValue(errorObject, null);

            Assert.Equal("O pedido já contém um item da categoria Sandwich. Item duplicado.", errorMessage);
            
            mockOrderRepo.Verify(repo => repo.AddAsync(It.IsAny<Order>()), Times.Never);
        }
        
        
}