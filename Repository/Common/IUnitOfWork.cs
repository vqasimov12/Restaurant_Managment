using Repository.Repositories;

namespace Repository.Common;

public interface IUnitOfWork
{
    public ICategoryRepository CategoryRepository { get;}
    public IUserRepository UserRepository { get;}
    public IProductRepository ProductRepository { get; }
    public ICustomerRepository CustomerRepository { get; }
    Task<int> SaveChanges();
}