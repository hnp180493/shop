using Shop.Data.Repositories;
using Shop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Service
{
    public interface ICommonService
    {
        IEnumerable<Slide> GetSlides();
    }
    public class CommonService : ICommonService
    {
        ISlideRepository _slideRepository;
        public CommonService(ISlideRepository slideRepository)
        {
            _slideRepository = slideRepository;
        }
        public IEnumerable<Slide> GetSlides()
        {
            return _slideRepository.GetMulti(x => x.Status);
        }
    }
}
