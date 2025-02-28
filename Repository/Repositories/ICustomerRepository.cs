using Domain.Entites;

namespace Repository.Repositories;

public interface ICustomerRepository
{
    Task AddAsync(Customer customer);
    Task Update(Customer customer);
    Task<bool> Remove(int id, int deletedBy);
    IQueryable<Customer> GetAll();
    Task<Customer> GetByIdAsync(int id);
    Task<IEnumerable<Customer>> GetByNameAsync(string name);
}
