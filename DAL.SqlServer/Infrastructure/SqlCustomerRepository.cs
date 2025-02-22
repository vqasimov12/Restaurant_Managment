using DAL.SqlServer.Context;
using Domain.Entites;
using Repository.Repositories;

namespace DAL.SqlServer.Infrastructure;

public class SqlCustomerRepository(string connectionString, AppDbContext context) : BaseSqlRepository(connectionString), ICustomerRepository
{
    public Task AddAsync(Customer customer)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Customer> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Customer> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Customer>> GetByNameAsync(string name)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Remove(int id, int deletedBy)
    {
        throw new NotImplementedException();
    }

    public Task Update(Customer customer)
    {
        throw new NotImplementedException();
    }
}
