using AutoMapper;
using Shop.Service;
using Shop.Web.Infrastructure.Core;
using Shop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Shop.Web.Api
{
    [RoutePrefix("api/product")]
    public class ProductController : ApiControllerBase
    {
        IProductService _productService;
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
                var queryData = data.Skip((page - 1) * pageSize).Take(pageSize);
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
    }
}