using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ShoppingCart.Services.Adapters.Interfaces;
using ShoppingCart.Services.DTOs.Product;
using ShoppingCart.Services.DTOs.ShoppingCart;
using ShoppingCart.Services.DTOs.ShoppingCartItem;
using ShoppingCart.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace ShoppingCart.UnitTest.Shopping_Cart
{
    public class ShoppingCartServiceTest
    {
        private readonly Mock<IShoppingCartServiceAdapter> _mockRepo;
        private readonly Mock<IProductsServiceAdapter> _mockRepoProducts;
        private readonly ShoppingCartController _controller;
        private readonly ProductController _controllerProducts;

        public ShoppingCartServiceTest()
        {
            _mockRepo = new Mock<IShoppingCartServiceAdapter>();
            _mockRepoProducts = new Mock<IProductsServiceAdapter>();
            _controller = new ShoppingCartController(_mockRepo.Object, _mockRepoProducts.Object);
            _controllerProducts = new ProductController( _mockRepoProducts.Object);         

        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            var cartID = "9b3df90a-1007-4778-b614-91df990e0c95";
            var mockHttpContext = MockHttpSession.setSessionKey(cartID);
            _controller.ControllerContext.HttpContext = mockHttpContext.Object;

            // Act
            var okResult = _controller.Get();


            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsAllItems()
        {
            var cartID = "9b3df90a-1007-4778-b614-91df990e0c95";
            var mockHttpContext = MockHttpSession.setSessionKey(cartID);
            _controller.ControllerContext.HttpContext = mockHttpContext.Object;
            // Act
            var okResult = _controller.Get().Result;
            // Assert
            var items = Assert.IsType<List<ShoppingCartDto>>(okResult.Value);
            Assert.Equal(3, items.Count);
        }


        [Fact]
        public void Remove_NotExistingGuidPassed_ReturnsNotFoundResponse()
        {
            var cartID = "9b3df90a-1007-4778-b614-91df990e0c95";
            string id = "e3656e5b-23df-4ced-a7e4-4b066666f54a";
            var mockHttpContext = MockHttpSession.setSessionKey(cartID);
            _controller.ControllerContext.HttpContext = mockHttpContext.Object;
            // Act
            var badResponse = _controller.DeleteItemFromSC(id);
            // Assert
            Assert.IsType<NotFoundResult>(badResponse);
        }
        [Fact]
        public void Remove_ExistingGuidPassed_ReturnsOkResult()
        {
            var cartID = "9b3df90a-1007-4778-b614-91df990e0c95";
            string id = "e3656e5b-23df-4ced-a7e4-4b066839f54a";
            var mockHttpContext = MockHttpSession.setSessionKey(cartID);
            _controller.ControllerContext.HttpContext = mockHttpContext.Object;
            // Act
            var okResponse = _controller.DeleteItemFromSC(id);
            // Assert
            Assert.IsType<OkResult>(okResponse);
        }

        [Fact]
        public void Getroducts_WhenCalled_ReturnsAllItems()
        {
            // Act
            var okResult = _controllerProducts.Get().Result;
            // Assert
            var items = Assert.IsType<List<ProductDto>>(okResult.Value);
            Assert.Equal(6, items.Count);
        }

        [Fact]
        public void GetById_ExistingGuidPassed_ReturnsOkResult()
        {
            // Arrange
            string id = "e3656e5b-23df-4ced-a7e4-4b066839f54a";
            // Act
            var okResult = _controllerProducts.Get(id);
            // Assert
            Assert.IsType<OkObjectResult>(okResult.Result);
        }

        [Fact]
        public void Clear_ExistingGuidPassed()
        {
            var cartID = "9b3df90a-1007-4778-b614-91df990e0c95";
            var mockHttpContext = MockHttpSession.setSessionKey(cartID);
            _controller.ControllerContext.HttpContext = mockHttpContext.Object;
            // Act
            _controller.ClearCart();
            var okResult = _controller.Get();
            var items = Assert.IsType<List<ShoppingCartItemDto>>(okResult.Result);
            // Assert
            Assert.Empty(items);
        }

        [Fact]
        public void AddToCart_ExistingGuidPassed()
        {
            var cartID = "17b37a88-c502-45a8-84be-b48ecc205a3e";
            string id = "e3656e5b-23df-4ced-a7e4-4b066839f54a";

            var mockHttpContext = MockHttpSession.setSessionKey(cartID);
            _controller.ControllerContext.HttpContext = mockHttpContext.Object;
            // Act
            _controller.AddtoCart(id,1);
            var okResult = _controller.Get();
            var items = Assert.IsType<List<ShoppingCartItemDto>>(okResult.Result);
            // Assert
            Assert.Equal(2, items.Count);
        }

    }
}
