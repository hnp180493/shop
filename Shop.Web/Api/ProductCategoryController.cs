using AutoMapper;
using Shop.Data.Infrastructure;
using Shop.Model.Models;
using Shop.Service;
using Shop.Web.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Shop.Web.Api
{
    [RoutePrefix("api/productCategory")]
    public class ProductCategoryController : ApiControllerBase
    {
        IProductCategoryService _productCategoryService;
        public ProductCategoryController(IErrorService errorService, 
            IProductCategoryService productCategoryService) 
            :base(errorService)
        {
            this._productCategoryService = productCategoryService;
        }

        [Route("getall")]
        public HttpResponseMessage Get(HttpRequestMessage request, string keyword, int page, int pageSize)
        {
            return CreateHttpResponse(request, () =>
            {
                int totalRows = 0;
                var products = _productCategoryService.GetAll(keyword);
                totalRows = products.Count();
                var query = products.Skip(page * pageSize).Take(pageSize);

                var productsView = Mapper.Map<List<ProductCategoryViewModel>>(query);
                var pagination = new Pagination<ProductCategoryViewModel>()
                {
                    Page = page,
                    Items = productsView,
                    TotalCount = totalRows,
                    TotalPages= (int)Math.Ceiling((decimal)totalRows/pageSize),
                };
                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, pagination);

                return response;
            });
        }

        [Route("getParents")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                var products = _productCategoryService.GetAll();

                var productsView = Mapper.Map<List<ProductCategoryViewModel>>(products);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, productsView);

                return response;
            });
        }

        [Route("getbyid")]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var product = _productCategoryService.GetById(id);

                var productView = Mapper.Map<ProductCategoryViewModel>(product);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, productView);

                return response;
            });
        }

        [Route("create")]
        public HttpResponseMessage Post(HttpRequestMessage request, ProductCategoryViewModel categoryVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response= null;
                if (ModelState.IsValid)
                {
                    var category = Mapper.Map<ProductCategory>(categoryVm);
                    var result = _productCategoryService.Add(category);
                    _productCategoryService.Save();
                    response = request.CreateResponse(HttpStatusCode.Created, result);
                }
                else
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                return response;

            });
        }

        [Route("update")]
        public HttpResponseMessage Put(HttpRequestMessage request, ProductCategoryViewModel categoryVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    var category = Mapper.Map<ProductCategory>(categoryVm);
                    _productCategoryService.Update(category);
                    _productCategoryService.Save();
                    var responseData = Mapper.Map<ProductCategoryViewModel>(category);
                    response = request.CreateResponse(HttpStatusCode.Created, responseData);
                }
                else
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                return response;

            });
        }

        [Route("delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    var oldData = _productCategoryService.Delete(id);
                    _productCategoryService.Save();
                    response = request.CreateResponse(HttpStatusCode.OK, oldData);
                }
                else
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                return response;

            });
        }
    }
}