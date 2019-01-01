using AutoMapper;
using Shop.Model.Models;
using Shop.Web.Models;

namespace Shop.Web.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.CreateMap<Post, PostViewModel>();
            Mapper.CreateMap<PostCategory, PostCategoryViewModel>();
            Mapper.CreateMap<Tag, TagViewModel>();
            Mapper.CreateMap<ProductCategory, ProductCategoryViewModel>();
            Mapper.CreateMap<Product, ProductViewModel>();
            Mapper.CreateMap<ProductViewModel, Product>();
            Mapper.CreateMap<ProductCategoryViewModel, ProductCategory>();
            Mapper.CreateMap<SlideViewModel, Slide>();
            Mapper.CreateMap<Slide, SlideViewModel>();
            Mapper.CreateMap<Page, PageViewModel>();
        }
    }
}