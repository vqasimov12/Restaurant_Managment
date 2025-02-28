using Domain.Entites;

namespace Repository.Repositories;

public interface IProductRepository
{
    Task AddAsync(Product product);
    Task Update(Product product);
    Task<bool> Remove(int id, int deletedBy);
    IQueryable<Product> GetAll();
    Task<Product> GetByIdAsync(int id);
    Task<IEnumerable<Product>> GetByNameAsync(string name);
}
