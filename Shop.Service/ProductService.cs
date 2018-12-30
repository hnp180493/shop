using Shop.Common;
using Shop.Data.Infrastructure;
using Shop.Data.Repositories;
using Shop.Model.Models;
using System.Collections.Generic;
using System.Linq;
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
        IEnumerable<Product> GetLatest(int top);
        IEnumerable<Product> GetHot(int top);

        IEnumerable<Product> GetListProductByCategoryPaging(int categoryId, int page, int pageSize, out int totalRow);
    }

    public class ProductService : IProductService
    {
        private IProductRepository _repository;
        private IProductTagRepository _productTagRepository;
        private ITagRepository _tagRepository;
        private IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository repository, IUnitOfWork unitOfWork, IProductTagRepository productTagRepository, ITagRepository tagRepository)
        {
            _repository = repository;
            _productTagRepository = productTagRepository;
            _tagRepository = tagRepository;
            _unitOfWork = unitOfWork;
        }

        public Product Add(Product product)
        {
            var result = _repository.Add(product);
            _unitOfWork.Commit();
            if (!string.IsNullOrEmpty(product.Tags))
            {
                var listTags = product.Tags.Split(',');
                foreach (var tag in listTags)
                {
                    var tagId = StringHelper.ToUnsignString(tag);
                    if (_tagRepository.Count(x => x.ID == tag) == 0)
                    {
                        Tag tagAdd = new Tag
                        {
                            ID = tagId,
                            Name = tag,
                            Type = CommonConstants.ProductTag,
                        };
                        _tagRepository.Add(tagAdd);
                    }
                    ProductTag productTag = new ProductTag
                    {
                        ProductID = product.ID,
                        TagID = tagId
                    };
                    _productTagRepository.Add(productTag);
                }
            }
            return product;
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

        public IEnumerable<Product> GetHot(int top)
        {
            return _repository.GetMulti(x=>x.Status && x.HotFlag == true).OrderByDescending(x=>x.CreatedDate).Take(top);
        }

        public IEnumerable<Product> GetLatest(int top)
        {
            return _repository.GetMulti(x => x.Status).OrderByDescending(x => x.CreatedDate).Take(top);
        }

        public IEnumerable<Product> GetListProductByCategoryPaging(int categoryId, int page, int pageSize, out int totalRow)
        {
            var products = _repository.GetMulti(x => x.Status && x.CategoryID == categoryId);
            totalRow = products.Count();
            return products.Skip((page - 1)*pageSize).Take(pageSize);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Product product)
        {
            _repository.Update(product);
            if (string.IsNullOrEmpty(product.Tags))
            {
                var listTags = product.Tags.Split(',');
                foreach (var tag in listTags)
                {
                    var tagId = StringHelper.ToUnsignString(tag);
                    if (_tagRepository.Count(x => x.ID == tag) == 0)
                    {
                        Tag tagAdd = new Tag
                        {
                            ID = tagId,
                            Name = tag,
                            Type = CommonConstants.ProductTag,
                        };
                        _tagRepository.Add(tagAdd);
                    }
                    _productTagRepository.DeleteMulti(x => x.ProductID == product.ID);
                    ProductTag productTag = new ProductTag
                    {
                        ProductID = product.ID,
                        TagID = tagId
                    };
                    _productTagRepository.Add(productTag);
                }
            }
        }
    }
}