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
    public class PostCategoryServiceTest
    {
        private Mock<IPostCategoryRepository> _mockPCRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private IPostCategoryService _postCategoryService;

        [TestInitialize]
        public void Init()
        {
            _mockPCRepository = new Mock<IPostCategoryRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _postCategoryService = new PostCategoryService(_mockPCRepository.Object, _mockUnitOfWork.Object);
        }

        [TestMethod]
        public void PostCategoryService_Create()
        {
            PostCategory postCategory = new PostCategory
            {
                Name = "Test service",
                Alias = "Test Alias",
                Status = true
            };

            _mockPCRepository
                .Setup(x => x.Add(postCategory))
                .Returns((PostCategory p) =>
                {
                    p.ID = 1;
                    return p;
                });

            var result = _postCategoryService.Add(postCategory);
            Assert.AreEqual(1, result.ID);
        }

        [TestMethod]
        public void PostCategoryService_GetAll()
        {
            _mockPCRepository
                .Setup(x => x.GetAll(null))
                .Returns(new List<PostCategory>()
                {
                    new PostCategory(){Name="Test 1", Alias = "", Status = true},
                    new PostCategory(){Name="Test 2", Alias = "", Status = true}
                });
            var result = _postCategoryService.GetAll().ToList();

            Assert.AreEqual(2, result.Count);
        }
    }
}
