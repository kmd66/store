using Dapper;
using Microsoft.Data.SqlClient;
using MizeBazi.Store.Application.Interfaces;
using MizeBazi.Store.Common.Helper;
using MizeBazi.Store.Common.Shared;
using MizeBazi.Store.Domain;

namespace MizeBazi.Store.Data.Repositories;

public class ProductCategoryRepository : IProductCategoryRepository
{
    public async Task<Result> EditeAsync(ProductCategory model, CancellationToken ct = default)
    {
        try
        {
            using (var connection = new SqlConnection(AppSetings.ReadConnection))
            {
                var p = new DynamicParameters();
                p.Add("@ProductId", model.ProductId);
                p.Add("@Json", model.CategoryIds.ToJson());

                var query = p.CreateQuery(
                    "sp.EditeProductCategory",
                    cancellationToken: ct
                );

                var result = await connection.QueryAsync<DbProductCategory>(query);

                return Result<IEnumerable<DbProductCategory>>.Successful(data: result);
            }
        }
        catch (Exception ex)
        {
            throw new DbException($"Category Get Exception {ex.Message}");
        }
    }

    public async Task<Result<IEnumerable<long>>> GetCategoryIdsAsync(ProductCategory model, CancellationToken ct = default)
    {
        try
        {
            using (var connection = new SqlConnection(AppSetings.ReadConnection))
            {
                var t = model.CategoryIds.ToJson();
                var p = new DynamicParameters();
                p.Add("@Json", model.CategoryIds.ToJson());

                var query = p.CreateQuery(
                    "sp.GetCategoryIds",
                    cancellationToken: ct
                );

                var result = await connection.QueryAsync<long>(query);

                return Result<IEnumerable<long>>.Successful(data: result);
            }
        }
        catch (Exception ex)
        {
            throw new DbException($"Category Get Exception {ex.Message}");
        }
    }

    public async Task<Result<IEnumerable<DbProductCategory>>> ListByIdAsync(long productId, CancellationToken ct = default)
    {
        try
        {
            using (var connection = new SqlConnection(AppSetings.ReadConnection))
            {
                var p = new DynamicParameters();
                p.Add("@Id", productId);
                var query = p.CreateQuery(
                    "sp.LsitProductCategory",
                    cancellationToken: ct
                );

                var result = await connection.QueryAsync<DbProductCategory>(query);

                return Result<IEnumerable<DbProductCategory>>.Successful(data: result);
            }
        }
        catch (Exception ex)
        {
            throw new DbException($"Category Get Exception {ex.Message}");
        }
    }
}

