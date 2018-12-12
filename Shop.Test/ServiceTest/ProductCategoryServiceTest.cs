using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Shop.Data.Infrastructure;
using Shop.Data.Repositories;
using Shop.Model.Models;
using Shop.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Test.ServiceTest
{
    [TestClass]
    public class ProductCategoryServiceTest
    {
        IProductCategoryService _productCategoryService;
        Mock<IProductCategoryRepository> _mockRepository;
        Mock<IUnitOfWork> _mockUnitOfWork;

        [TestInitialize]
        public void Init()
        {
            _mockRepository = new Mock<IProductCategoryRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _productCategoryService = new ProductCategoryService(_mockRepository.Object, _mockUnitOfWork.Object);
        }

        [TestMethod]
        public void Create()
        {
            ProductCategory product = new ProductCategory()
            {
                Name = "Test Product Category 1",
                Alias = "Test",
                Status = true
            };

            _mockRepository
                .Setup(x => x.Add(product))
                .Returns((ProductCategory p) =>
                {
                    p.ID = 1;
                    return p;
                });

            var result = _productCategoryService.Add(product);

            Assert.AreEqual(1, result.ID);
        }
    }
}
