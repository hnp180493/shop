using Shop.Data.Infrastructure;
using Shop.Model.Models;
using System.Collections.Generic;
using System.Linq;
namespace Shop.Data.Repositories
{
    public interface IProductTagRepository : IRepository<ProductTag>
    {
        IEnumerable<Product> GetListProductByTag(string tagId);
    }

    public class ProductTagRepository : RepositoryBase<ProductTag>, IProductTagRepository
    {
        public ProductTagRepository(IDbFactory dbFactory) : base(dbFactory)
        {
        }

        public IEnumerable<Product> GetListProductByTag(string tagId)
        {
            var query = from p in DbContext.Products
                        join pt in DbContext.ProductTags
                        on p.ID equals pt.ProductID
                        where pt.TagID == tagId
                        select p;
            return query;
        }
    }
}