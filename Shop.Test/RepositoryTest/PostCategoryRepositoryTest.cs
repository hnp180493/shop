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
    public class PostCategoryRepositoryTest
    {
        IDbFactory dbFactory;
        IUnitOfWork unitOfWork;
        IPostCategoryRepository postCategoryRepository;

        [TestInitialize]
        public void Initialize()
        {
            dbFactory = new DbFactory();
            unitOfWork = new UnitOfWork(dbFactory);
            postCategoryRepository = new PostCategoryRepository(dbFactory);
        }

        [TestMethod]
        public void PostCategoryRepository_Create()
        {
            PostCategory postCategory = new PostCategory();
            postCategory.Name = "Test1";
            postCategory.Alias = "Test Alias1";
            postCategory.Status = true;

            var result = postCategoryRepository.Add(postCategory);
            unitOfWork.Commit();
            Assert.AreEqual(1, result.ID);
        }

        [TestMethod]
        public void PostCategoryRepository_GetAll()
        {
            var result = postCategoryRepository.GetAll();
            var count = result.Count();
            Assert.IsTrue(count > 0);
        }
    }
}
