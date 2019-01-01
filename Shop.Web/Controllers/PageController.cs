using AutoMapper;
using Shop.Service;
using Shop.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Shop.Web.Controllers
{
    public class PageController : Controller
    {
        IPageService _pageService;
        public PageController(IPageService pageService)
        {
            _pageService = pageService;
        }
        // GET: Page
        public ActionResult Index(string alias)
        {
            var pageModel = _pageService.GetByAlias(alias);
            var pageView = Mapper.Map<PageViewModel>(pageModel);
            return View(pageView);
        }
    }
}