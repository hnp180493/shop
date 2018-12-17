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
    public interface IProductService
    {
        Product Add(Product Product);
        void Update(Product Product);
        IEnumerable<Product> GetAll();
        Product GetById(int id);
        IEnumerable<Product> GetAll(string keyword);
        void Save();
        Product Delete(int id);
    }
    public class ProductService: IProductService
    {
        IProductRepository _repository;
        IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }


        public Product Add(Product product)
        {
            var result = _repository.Add(product);
            _unitOfWork.Commit();
            if (string.IsNullOrEmpty(product.Tags))
            {
                var listTags = product.Tags.Split(',');
                foreach(var tag in listTags)
                {
                    //var tagId = StringHelper
                    //ProductTag productTag = new ProductTag
                    //{

                    //}
                    _repository.Add(product);
                }
            }
            return _repository.Add(product);
        }

        public Product Delete(int id)
        {
            return _repository.Delete(id);
        }

        public IEnumerable<Product> GetAll()
        {
            return _repository.GetAll();
        }

        public IEnumerable<Product> GetAll(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return _repository.GetAll();
            }
            return _repository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
        }

        public Product GetById(int id)
        {
            return _repository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Product Product)
        {
            _repository.Update(Product);
        }
    }
}
