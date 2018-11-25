using Shop.Data;
using System;

namespace TeduShop.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        ShopDbContext Init();
    }
}