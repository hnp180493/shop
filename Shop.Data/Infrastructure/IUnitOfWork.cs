namespace Shop.Data.Infrastructure
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}