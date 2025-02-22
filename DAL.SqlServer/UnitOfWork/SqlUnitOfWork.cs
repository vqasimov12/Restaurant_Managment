using DAL.SqlServer.Context;
using DAL.SqlServer.Infrastructure;
using Repository.Common;
using Repository.Repositories;

namespace DAL.SqlServer.UnitOfWork;

public class SqlUnitOfWork(string connectionString, AppDbContext context) : IUnitOfWork
{
    private readonly string _connectionString = connectionString;
    private readonly AppDbContext _context = context;

    public SqlCategoryRepository _sqlCategoryRepository;
    public SqlUserRepository _sqlUserRepository;
    public SqlProductRepository _sqlProductRepository;
    public SqlCustomerRepository _sqlCustomerRepository;

    public ICategoryRepository CategoryRepository => _sqlCategoryRepository ?? new SqlCategoryRepository(_connectionString, _context);
    public IUserRepository UserRepository => _sqlUserRepository ?? new SqlUserRepository(_context);

    public IProductRepository ProductRepository => _sqlProductRepository ?? new SqlProductRepository(connectionString, _context);

    public ICustomerRepository CustomerRepository => _sqlCustomerRepository ?? new SqlCustomerRepository(connectionString, _context);

    public async Task<int> SaveChanges() => await _context.SaveChangesAsync();
}