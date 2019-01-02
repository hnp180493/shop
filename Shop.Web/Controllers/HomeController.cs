using AutoMapper;
using Shop.Model.Models;
using Shop.Service;
using Shop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Web.Controllers
{
    public class HomeController : Controller
    {
        IProductCategoryService _productCategoryService;
        IProductService _productService;
        ICommonService _commonService;
        public HomeController(
            IProductCategoryService productCategoryService, 
            ICommonService commonService,
            IProductService productService)
        {
            _productCategoryService = productCategoryService;
            _commonService = commonService;
            _productService = productService;
        }

        public ActionResult Index()
        {
            ViewBag.Time = DateTime.Now;
            var slideModel = _commonService.GetSlides();
            var slideView = Mapper.Map<IEnumerable<SlideViewModel>>(slideModel);
            var latestProductModel = _productService.GetLatest(3);
            var latestProductView = Mapper.Map<IEnumerable<ProductViewModel>>(latestProductModel);
            var hotProductModel = _productService.GetHot(3);
            var hotProductView = Mapper.Map<IEnumerable<ProductViewModel>>(hotProductModel);
            var homeModel = new HomeViewModel();
            homeModel.Slides = slideView;
            homeModel.LastestProducts = latestProductView;
            homeModel.HotProducts = hotProductView;
            return View(homeModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [ChildActionOnly]
        [OutputCache(Duration =3600)]
        public ActionResult Footer()
        {
            return PartialView();
        }
        [ChildActionOnly]
        public ActionResult Header()
        {
            return PartialView();
        }
        [ChildActionOnly]
        [OutputCache(Duration =3600)]
        public ActionResult Category()
        {
            var categories = _productCategoryService.GetAll();
            var result = Mapper.Map<IEnumerable<ProductCategoryViewModel>>(categories);
            return PartialView(result);
        }
    }
}