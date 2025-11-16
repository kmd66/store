using Dapper;
using Microsoft.Data.SqlClient;
using MizeBazi.Store.Application;
using MizeBazi.Store.Application.Interfaces;
using MizeBazi.Store.Common.Helper;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Data.Repositories;

public class BrandRead : IBrandRead
{
    public async Task<Result<GetBrandResult>> GetAsync(GetBrandQuery model, CancellationToken cancellationToken = default)
    {
        try
        {
            using (var connection = new SqlConnection(AppSetings.ReadConnection))
            {
                var p = new DynamicParameters();
                p.Add("@Id", model.Id);
                p.Add("@UnicId", model.UnicId);

                var query = p.CreateQuery(
                    "sp.GetBrand",
                    cancellationToken: cancellationToken
                );

                var result = await connection.QueryAsync<GetBrandResult>(query);

                return Result<GetBrandResult>.Successful(data: result.FirstOrDefault());
            }
        }
        catch (Exception ex)
        {
            throw new DbException($"Brand Get Exception {ex.Message}");
        }
    }


    public async Task<Result<IEnumerable<ListBrandResult>>> ListAsync(ListBrandQuery model, CancellationToken cancellationToken = default)
    {
        try
        {
            using (var connection = new SqlConnection(AppSetings.ReadConnection))
            {
                var p = new DynamicParameters();
                p.Add("@Name", model.Name.ToDbValue());
                p.Add("@IsDeleted", model.IsDeleted);
                p.Add("@PageSize", model.PageSize);
                p.Add("@PageIndex", model.PageIndex);

                var query = p.CreateQuery(
                    "sp.LsitBrand",
                    cancellationToken: cancellationToken
                );

                var result = await connection.QueryAsync<ListBrandResult>(query);

                return Result<IEnumerable<ListBrandResult>>.Successful(data: result);
            }
        }
        catch (Exception ex)
        {
            throw new DbException($"Brand List Exception {ex.Message}");
        }
    }
}
