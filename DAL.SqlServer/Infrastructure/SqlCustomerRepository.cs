using DAL.SqlServer.Context;
using Dapper;
using Domain.Entites;
using Repository.Repositories;

namespace DAL.SqlServer.Infrastructure;

public class SqlCustomerRepository(string connectionString, AppDbContext context) : BaseSqlRepository(connectionString), ICustomerRepository
{
    public async Task AddAsync(Customer customer)
    {
        var sql = @"INSERT INTO Customers([Name],[Email],[Phone],[Address],[Surname]) 
                    VALUES(@Name,@Email,@Phone,@Address,@Surname); SELECT SCOPE_IDENTITY()";

        using var conn = OpenConnection();
        var generatedId = await conn.ExecuteScalarAsync<int>(sql, customer);
        customer.Id = generatedId;
    }


    public IQueryable<Customer> GetAll() =>
        context.Customers.Where(c => c.IsDeleted == false).OrderByDescending(c => c.CreatedDate);

    public async Task<Customer> GetByIdAsync(int id)
    {
        var sql = "SELECT * FROM Customers WHERE Id=@id AND IsDeleted=0";

        using var conn = OpenConnection();
        return await conn.QueryFirstOrDefaultAsync<Customer>(sql, new { id });
    }

    public Task<IEnumerable<Customer>> GetByNameAsync(string name)
    {
        var sql = @"DECLARE @searchText NVARCHAR(max)
                    SET @searchText = '%'+ @name + '%'
                    SELECT c.* 
                    FROM Customers c
                    WHERE c.[Name] ike @searchText and c.IsDeleted=0";
        using var conn = OpenConnection();
        return conn.QueryAsync<Customer>(sql, name);
    }

    public async Task<bool> Remove(int id, int deletedBy)
    {

        var checkSql = "SELECT Id FROM Customers WHERE Id=@id AND IsDeleted=0";
        using var conn = OpenConnection();
        using var transaction = conn.BeginTransaction();
        var customerId = await conn.ExecuteScalarAsync<int?>(checkSql, new { id }, transaction);
        if (!customerId.HasValue)
            return false;

        var sql = @"UPDATE Customers
                    SET IsDeleted=1,
                    DeletedBy= @deletedBy,
                    DeletedDate = GETDATE()
                    Where Id=@id";
        var affectedRow = await conn.ExecuteAsync(sql, new { id, deletedBy }, transaction);
        transaction.Commit();
        return affectedRow > 0;

    }

    public async Task Update(Customer customer)
    {
        var sql = @"UPDATE Customers
                    SET Name = @Name,
                    Email = @Email,
                    Phone = @Phone,
                    Address = @Address,
                    Surname = @Surname
                    WHERE Id = @Id";
        using var conn = OpenConnection();
        await conn.ExecuteAsync(sql, customer);
    }
}
