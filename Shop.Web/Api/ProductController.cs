using AutoMapper;
using Shop.Model.Models;
using Shop.Service;
using Shop.Web.Infrastructure.Core;
using Shop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace Shop.Web.Api
{
    [RoutePrefix("api/product")]
    public class ProductController : ApiControllerBase
    {
        private IProductService _productService;

        public ProductController(IErrorService errorService, IProductService productService)
            : base(errorService)
        {
            this._productService = productService;
        }

        [Route("getall")]
        public HttpResponseMessage Get(HttpRequestMessage request, string keyword, int page, int pageSize)
        {
            return CreateHttpResponse(request, () =>
            {
                var data = _productService.GetAll(keyword);
                int totalRows = data.Count();
                var queryData = data.Skip((page) * pageSize).Take(pageSize);
                var mapData = Mapper.Map<List<ProductViewModel>>(queryData);

                var pagination = new Pagination<ProductViewModel>
                {
                    Page = page,
                    Items = mapData,
                    TotalCount = totalRows,
                    TotalPages = (int)Math.Ceiling((decimal)totalRows / pageSize),
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
                var products = _productService.GetAll();

                var productsView = Mapper.Map<List<ProductViewModel>>(products);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, productsView);

                return response;
            });
        }

        [Route("getbyid")]
        public HttpResponseMessage Get(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                var product = _productService.GetById(id);

                var productView = Mapper.Map<ProductViewModel>(product);

                HttpResponseMessage response = request.CreateResponse(HttpStatusCode.OK, productView);

                return response;
            });
        }

        [Route("create")]
        public HttpResponseMessage Post(HttpRequestMessage request, ProductViewModel categoryVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    var category = Mapper.Map<Product>(categoryVm);
                    var result = _productService.Add(category);
                    _productService.Save();
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
        public HttpResponseMessage Put(HttpRequestMessage request, ProductViewModel productVm)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    var category = Mapper.Map<Product>(productVm);
                    _productService.Update(category);
                    _productService.Save();
                    var responseData = Mapper.Map<ProductViewModel>(category);
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
                    var oldData = _productService.Delete(id);
                    _productService.Save();
                    response = request.CreateResponse(HttpStatusCode.OK, oldData);
                }
                else
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                return response;
            });
        }

        [Route("deletemulti")]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage request, string listIdJson)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    var listId = new JavaScriptSerializer().Deserialize<List<int>>(listIdJson);
                    foreach (var id in listId)
                    {
                        _productService.Delete(id);
                    }

                    _productService.Save();
                    response = request.CreateResponse(HttpStatusCode.OK, listId.Count);
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