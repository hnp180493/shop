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
        IEnumerable<Product> GetAll(string keyword);
    }
    public class ProductService: IProductService
    {
        IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public IEnumerable<Product> GetAll(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                return _productRepository.GetAll();
            }
            return _productRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword));
        }
    }
}
