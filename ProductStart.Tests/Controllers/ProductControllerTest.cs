using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;


using System.Web.Http.Results;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Routing;
using AutoMoq;
using NUnit.Framework;

using Moq;
using ClassLibrary1;
using ProductStart.Controllers;
using ProductStart.Factories;
using ProductStart.Providers;
using ProductStart.Repository;
using ProductStart.Tests.Utility;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace ProductStart.Tests.Controllers
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class ProductControllerTest
    {

        private Mock<IProductProvider> _provider;
        private Mock<IProductRepository> _repository;

        private ProductsController _controller;


        [TestInitialize]
        public void Initialize()
        {

            _provider = new Mock<IProductProvider>();
            _repository= new Mock<IProductRepository>();

            _controller = new ProductsController(_provider.Object);

            _controller.Request = new HttpRequestMessage();
            _controller.Configuration = new HttpConfiguration();
        }

        [TestMethod]
        public void ProductController_Get_ShouldReturn()
        {
            //Arrange
            var productList = TestUtility.CreateProductList();
            _provider.Setup(
                    p => p.GetAll()
                )
                .Returns(productList);
            //Act
            var results = _controller.Get();

            //Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(3, results.Count());

        }

        [TestMethod]
        public void ProductController_Get_ById_ShouldReturnTheCorrectProduct()
        {
            //Arrange
            var id = 1;
            var name = "name";
            var category = ProductCategory.Groceries;

            var description = "description";
            var product = ProductFactory.CreateProduct(id, name, category, description);

            _provider.Setup(
                    p => p.Get(id)
                )
                .Returns(product);
            _controller.Request = new HttpRequestMessage();
            _controller.Configuration = new HttpConfiguration();

            // Act
            var response = _controller.Get(id) as OkNegotiatedContentResult<Product>;

            var result = response.Content;
            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Id, id);
            Assert.AreEqual(result.Name, name);
            Assert.AreEqual(result.Category, category);
            Assert.AreEqual(result.Description, description);

        }

        [TestMethod]
        public void ProductController_Get_ById_ShouldReturn_NotFound()
        {
            //Arrange
            var otherId = 10;
            _provider.Setup(
                    p => p.Get(otherId)
                )
                .Returns((Product) null);

            // Act
            var actionResult = _controller.Get(otherId);
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod]
        public void ProductController_PostProduct_ShouldReturn()
        {
            //Arrange
            var id = 1;
            var name = "name";
            var category = ProductCategory.Groceries;

            var description = "description";
            var product = ProductFactory.CreateProduct(id, name, category, description);

            _provider.Setup(
                    p => p.Add(product)
                )
                .Returns(product);


            _controller.Request = new HttpRequestMessage
            {
                RequestUri = new Uri("http://localhost/api/products")
            };


            //_controller.Request = new HttpRequestMessage();
            _controller.Configuration = new HttpConfiguration();

            _controller.Configuration = new HttpConfiguration();
            _controller.Configuration.Routes.MapHttpRoute(
                name: "Product",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional });

            _controller.RequestContext.RouteData = new HttpRouteData(
                route: new HttpRoute(),
                values: new HttpRouteValueDictionary { { "controller", "Product" } });


            //Act
            var response = _controller.PostProduct(product);
            //Assert

            Product result;
            Assert.IsTrue(response.TryGetContentValue<Product>(out result));
            Assert.AreEqual(product.Id, result.Id);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ProductController_PutProduct_ForExistingProduct_SholdReturnGoodProduct()
        {
            //Arrange
            var id = 1;
            var name = "name";
            var category = ProductCategory.Groceries;
            var description = "description";
            var product = ProductFactory.CreateProduct(id, name, category, description);


            var newName = "new name";
            var newCategory = ProductCategory.Hardware;

            var newDescription = "new description";

            var newProduct= new Product {Id = id, Name = newName, Category = newCategory, Description = newDescription};
            

            _provider.Setup(
                    p => p.Update(id, product)
                )
                .Returns(newProduct);
            //Act

            var response = _controller.PutProduct(id, product) as NegotiatedContentResult<Product>;

            var result = response.Content;

            //Assert
            Assert.AreEqual(HttpStatusCode.Accepted, response.StatusCode);

            Assert.IsNotNull(result);
            Assert.AreEqual(newProduct.Id, result.Id);
            Assert.AreEqual(newProduct.Name, result.Name);
            Assert.AreEqual(newProduct.Category, result.Category);
            Assert.AreEqual(newProduct.Description, result.Description);
        }


        [TestMethod]
        public void ProductController_PutProduct_ForNotExistingProduct_SholdReturnNot()
        {
            //Arrange
            var id = 1;
            var name = "name";
            var category = ProductCategory.Groceries;
            var description = "description";
            var product = ProductFactory.CreateProduct(id, name, category, description);


            var newName = "new name";
            var newCategory = ProductCategory.Hardware;

            var newDescription = "new description";

            var newProduct = new Product { Id = id, Name = newName, Category = newCategory, Description = newDescription };

            var otherId = 10;


            _provider.Setup(
                    p => p.Update(otherId, product)
                )
                .Returns((Product) null);
            //Act

            var response = _controller.PutProduct(id, product);


            //Assert
            Assert.IsNotNull(response);
            Assert.IsInstanceOfType(response, typeof(NotFoundResult));
        }


    }
}