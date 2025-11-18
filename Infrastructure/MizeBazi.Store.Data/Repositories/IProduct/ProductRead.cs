using Dapper;
using Microsoft.Data.SqlClient;
using MizeBazi.Store.Application;
using MizeBazi.Store.Application.Interfaces;
using MizeBazi.Store.Common.Helper;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Data.Repositories;

public class ProductRead : IProductRead
{
    public async Task<GetProductResult> GetAsync(
        long? id = null,
        Guid? unicId = null,
        string sku = null
        , CancellationToken ct = default)
    {
        try
        {
            using (var connection = new SqlConnection(AppSetings.ReadConnection))
            {
                var p = new DynamicParameters();
                p.Add("@Id", id);
                p.Add("@UnicId", unicId);
                p.Add("@sku", sku);

                var query = p.CreateQuery(
                    "sp.GetProduct",
                    cancellationToken: ct
                );

                var result = await connection.QueryAsync<BaseProductResult>(query);
                var baseProductResult = new GetProductResult
                {
                    data = result.FirstOrDefault()
                };
                return baseProductResult;
            }
        }
        catch (Exception ex)
        {
            throw new DbException($"Category Get Exception {ex.Message}");
        }
    }

    public async Task<ListProductResult> ListAsync(ListProductQuery model, CancellationToken ct = default)
    {
        try
        {
            using (var connection = new SqlConnection(AppSetings.ReadConnection))
            {
                var p = new DynamicParameters();
                p.Add("@Name", model.Name);
                p.Add("@Description", model.Description);
                p.Add("@MaxPrice", model.MaxPrice);
                p.Add("@MinPrice", model.MinPrice);
                p.Add("@BrandId", model.BrandId);
                p.Add("@CategoryId", model.CategoryId);
                p.Add("@HasDiscount", model.HasDiscount);
                p.Add("@HasQuantity", model.HasQuantity);
                p.Add("@IsPublished", model.IsPublished);
                p.Add("@IsDeleted", model.IsDeleted);
                p.Add("@PageSize", model.PageSize);
                p.Add("@PageIndex", model.PageIndex);
    
                var query = p.CreateQuery(
                    "sp.ListProduct",
                    cancellationToken: ct
                );

                var result = await connection.QueryAsync<BaseProductResult>(query);
                var baseProductResult = new ListProductResult
                {
                    data = result
                };
                return baseProductResult;
            }
        }
        catch (Exception ex)
        {
            throw new DbException($"Category Get Exception {ex.Message}");
        }
    }

    public async Task<ListProductUserResult> ListAsync(ListProductUserQuery model, CancellationToken ct = default)
    {
        try
        {
            using (var connection = new SqlConnection(AppSetings.ReadConnection))
            {
                var p = new DynamicParameters();
                p.Add("@Name", model.Name);
                p.Add("@Description", null);
                p.Add("@MaxPrice", model.MaxPrice);
                p.Add("@MinPrice", model.MinPrice);
                p.Add("@BrandId", model.BrandId);
                p.Add("@CategoryId", model.CategoryId);
                p.Add("@HasDiscount", null);
                p.Add("@HasQuantity", null);
                p.Add("@IsPublished", null);
                p.Add("@IsDeleted", model.IsDeleted);
                p.Add("@PageSize", model.PageSize);
                p.Add("@PageIndex", model.PageIndex);

                var query = p.CreateQuery(
                    "sp.ListProduct",
                    cancellationToken: ct
                );

                var result = await connection.QueryAsync<BaseUserProductResult>(query);
                var baseProductResult = new ListProductUserResult
                {
                    data = result
                };
                return baseProductResult;
            }
        }
        catch (Exception ex)
        {
            throw new DbException($"Category Get Exception {ex.Message}");
        }
    }

}
