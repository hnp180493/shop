using Shop.Data.Infrastructure;
using Shop.Data.Repositories;
using Shop.Model.Models;

namespace Shop.Service
{
    public interface IPageService
    {
        Page GetByAlias(string alias);
    }

    public class PageService : IPageService
    {
        IPageRepository _pageRepository;
        public PageService(IPageRepository pageRepository)
        {
            _pageRepository = pageRepository;
        }
        public Page GetByAlias(string alias)
        {
            return _pageRepository.GetSingleByCondition(x => x.Alias == alias);
        }
    }
}