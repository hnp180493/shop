using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shop.Data.Infrastructure;
using Shop.Data.Repositories;
using Shop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Test.RepositoryTest
{
    [TestClass]
    public class ProductCategoryRepositoryTest
    {
        IDbFactory _dbFactory;
        IUnitOfWork _unitOfWork;
        IProductCategoryRepository _productCategoryRepository;

        [TestInitialize]
        public void Init()
        {
            _dbFactory = new DbFactory();
            _unitOfWork = new UnitOfWork(_dbFactory);
            _productCategoryRepository = new ProductCategoryRepository(_dbFactory);
        }

        [TestMethod]
        public void ProductCategoryRepository_Create()
        {
            ProductCategory product = new ProductCategory()
            {
                Name = "Product categrory 1",
                Alias = "Alias",
                Status = true
            };

            var result = _productCategoryRepository.Add(product);
            _unitOfWork.Commit();

            Assert.AreEqual(1, result.ID);
        }

    }
}
