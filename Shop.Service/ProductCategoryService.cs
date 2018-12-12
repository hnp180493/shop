using Shop.Data.Infrastructure;
using Shop.Data.Repositories;
using Shop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Service
{
    public interface IProductCategoryService
    {
        ProductCategory Add(ProductCategory productCategory);
    }
    public class ProductCategoryService : IProductCategoryService
    {
        IProductCategoryRepository _repository;
        IUnitOfWork _unitOfWork;

        public ProductCategoryService(IProductCategoryRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }


        public ProductCategory Add(ProductCategory productCategory)
        {
            return _repository.Add(productCategory);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
