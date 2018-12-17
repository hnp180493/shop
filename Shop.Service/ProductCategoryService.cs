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
        void Update(ProductCategory productCategory);
        IEnumerable<ProductCategory> GetAll();
        ProductCategory GetById(int id);
        IEnumerable<ProductCategory> GetAll(string keyword);
        void Save();
        ProductCategory Delete(int id);
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

        public ProductCategory Delete(int id)
        {
            return _repository.Delete(id);
        }

        public IEnumerable<ProductCategory> GetAll()
        {
            return _repository.GetAll();
        }

        public IEnumerable<ProductCategory> GetAll(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return _repository.GetAll();
            }
            return _repository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
        }

        public ProductCategory GetById(int id)
        {
            return _repository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(ProductCategory productCategory)
        {
            _repository.Update(productCategory);
        }
    }
}
