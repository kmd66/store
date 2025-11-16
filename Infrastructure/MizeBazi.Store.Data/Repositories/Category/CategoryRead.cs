using Dapper;
using Microsoft.Data.SqlClient;
using MizeBazi.Store.Application;
using MizeBazi.Store.Application.Interfaces;
using MizeBazi.Store.Common.Helper;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Data.Repositories;

public class CategoryRead : ICategoryRead
{
    public async Task<Result<GetCategoryResult>> GetAsync(GetCategoryQuery model, CancellationToken cancellationToken = default)
    {
        try
        {
            using (var connection = new SqlConnection(AppSetings.ReadConnection))
            {
                var p = new DynamicParameters();
                p.Add("@Id", model.Id);
                p.Add("@UnicId", model.UnicId);

                var query = p.CreateQuery(
                    "sp.GetCategory",
                    cancellationToken: cancellationToken
                );

                var result = await connection.QueryAsync<GetCategoryResult>(query);

                return Result<GetCategoryResult>.Successful(data: result.FirstOrDefault());
            }
        }
        catch (Exception ex)
        {
            throw new DbException($"Category Get Exception {ex.Message}");
        }
    }


    public async Task<Result<IEnumerable<ListCategoryResult>>> ListAsync(ListCategoryQuery model, CancellationToken cancellationToken = default)
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
                    "sp.LsitCategory",
                    cancellationToken: cancellationToken
                );

                var result = await connection.QueryAsync<ListCategoryResult>(query);

                return Result<IEnumerable<ListCategoryResult>>.Successful(data: result);
            }
        }
        catch (Exception ex)
        {
            throw new DbException($"Category List Exception {ex.Message}");
        }
    }
}
