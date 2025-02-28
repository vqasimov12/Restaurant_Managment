using DAL.SqlServer.Context;
using Dapper;
using Domain.Entites;
using Repository.Repositories;

namespace DAL.SqlServer.Infrastructure;
public class SqlProductRepository(string connectionString, AppDbContext context) : BaseSqlRepository(connectionString), IProductRepository
{
    private readonly AppDbContext _context = context;

    public async Task AddAsync(Product product)
    {
        var sql = @"INSERT INTO Products([Name],[CreatedBy],[CategoryId],[Description],[Price],[Stock])
                    VALUES(@Name , @CreatedBy,@CategoryId,@Description,@Price,@Stock); SELECT SCOPE_IDENTITY()";

        using var conn = OpenConnection();
        var generatedId = await conn.ExecuteScalarAsync<int>(sql, product);
        product.Id = generatedId;
    }

    public IQueryable<Product> GetAll() =>
        _context.Products.OrderByDescending(p => p.CreatedDate).Where(p => p.IsDeleted == false);

    public async Task<Product> GetByIdAsync(int id)
    {
        var sql = @"SELECT *
                    FROM Products AS p 
                    WHERE p.Id=@id AND p.IsDeleted=0";
        using var conn = OpenConnection();
        return await conn.QueryFirstOrDefaultAsync<Product>(sql, new { id });
    }

    public async Task<IEnumerable<Product>> GetByNameAsync(string name)
    {
        var sql = @"DECLARE @searchText NVARCHAR(max)
                    SET @searchText = '%'+ @name + '%'
                    SELECT p.* 
                    FROM Products p
                    WHERE p.[Name] Like @searchText and p.IsDeleted=0";
        using var conn = OpenConnection();
        return await conn.QueryAsync<Product>(sql, name);
    }

    public async Task<bool> Remove(int id, int deletedBy)
    {
        var checkSql = "SELECT Id FROM Products WHERE Id=@id AND IsDeleted=0";

        var sql = @"UPDATE Products
                    SET IsDeleted=1,
                    DeletedBy= @deletedBy,
                    DeletedDate = GETDATE()
                    Where Id=@id";

        using var conn = OpenConnection();
        using var transaction = conn.BeginTransaction();

        var productId = await conn.ExecuteScalarAsync<int?>(checkSql, new { id }, transaction);
        if (!productId.HasValue)
            return false;

        var affectedRow = await conn.ExecuteAsync(sql, new { id, deletedBy }, transaction);
        transaction.Commit();

        return affectedRow > 0;
    }

    public async Task Update(Product product)
    {
        var sql = @"UPDATE Products
                    SET Name = @Name,
                    Description = @Description,
                    Price = @Price,
                    Stock = @Stock,
                    CategoryId = @CategoryId,
                    UpdatedBy= @UpdatedBy,
                    UpdatedDate = GETDATE()
                    WHERE Id = @Id";

        using var conn = OpenConnection();
        await conn.QueryAsync<Product>(sql, product);
    }
}
