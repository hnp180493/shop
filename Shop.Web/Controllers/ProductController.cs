using AutoMapper;
using Shop.Common;
using Shop.Service;
using Shop.Web.Infrastructure.Core;
using Shop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Shop.Web.Controllers
{
    public class ProductController : Controller
    {
        IProductService _productService;
        IProductCategoryService _productCategoryService;
        public ProductController(IProductService productService, IProductCategoryService productCategoryService)
        {
            _productService = productService;
            _productCategoryService = productCategoryService;
        }
        // GET: Product
        public ActionResult Detail(int id)
        {
            var productModel = _productService.GetById(id);
            var productView = Mapper.Map<ProductViewModel>(productModel);
            var relatedProduct = _productService.GetRelatedProduct(id, 7);
            ViewBag.RelatedProduct = Mapper.Map<IEnumerable<ProductViewModel>>(relatedProduct);

            ViewBag.MoreImages = new JavaScriptSerializer().Deserialize<List<string>>(productView.MoreImages);
            return View(productView);
        }

        public ActionResult Category(int id, int page = 1, string sort="")
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int maxPage = int.Parse(ConfigHelper.GetByKey("MaxPage"));
            int totalRows;
            var categoryModel = _productCategoryService.GetById(id);
            ViewBag.Category = Mapper.Map<ProductCategoryViewModel>(categoryModel);
            ViewBag.Sort = sort;
            var productModel = _productService.GetListProductByCategoryPaging(id, page, pageSize, sort, out totalRows);
            var productView = Mapper.Map<IEnumerable<ProductViewModel>>(productModel);
            var pagination = new Pagination<ProductViewModel>
            {
                Items = productView,
                TotalCount = totalRows,
                MaxPage = maxPage,
                Page = page,
                TotalPages = (int)Math.Ceiling((decimal)(totalRows / pageSize))
            };
            return View(pagination);
        }

        public ActionResult Search(string name, int page = 1, string sort = "")
        {
            int pageSize = int.Parse(ConfigHelper.GetByKey("PageSize"));
            int maxPage = int.Parse(ConfigHelper.GetByKey("MaxPage"));
            int totalRows;
            ViewBag.Keyword = name;
            ViewBag.Sort = sort;
            var productModel = _productService.GetListProductByName(name, page, pageSize, sort, out totalRows);
            var productView = Mapper.Map<IEnumerable<ProductViewModel>>(productModel);
            var pagination = new Pagination<ProductViewModel>
            {
                Items = productView,
                TotalCount = totalRows,
                MaxPage = maxPage,
                Page = page,
                TotalPages = (int)Math.Ceiling((double)totalRows/pageSize)
            };
            return View(pagination);
        }

        public JsonResult GetListProductByName(string name)
        {
            var data = _productService.GetListProductByName(name);
            return Json(new
            {
                data = data
            }, JsonRequestBehavior.AllowGet);
        }
    }
}