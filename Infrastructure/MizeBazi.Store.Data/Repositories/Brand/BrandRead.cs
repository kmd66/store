using Dapper;
using Microsoft.Data.SqlClient;
using MizeBazi.Store.Application;
using MizeBazi.Store.Application.Interfaces;
using MizeBazi.Store.Common.Helper;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Data.Repositories;

public class BrandRead : IBrandRead
{
    public async Task<Result<BaseBanerRecordModel>> GetAsync(GetBanerQuery model, CancellationToken cancellationToken = default)
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

                var result = await connection.QueryAsync<BaseBanerRecordModel>(query);

                return Result<BaseBanerRecordModel>.Successful(data: result.FirstOrDefault());
            }
        }
        catch (Exception ex)
        {
            throw new DbException($"Brand Add Exception {ex.Message}");
        }
    }


    public async Task<Result<IEnumerable<ListBanerResult>>> ListAsync(ListBanerQuery model, CancellationToken cancellationToken = default)
    {
        try
        {
            using (var connection = new SqlConnection(AppSetings.ReadConnection))
            {
                var p = new DynamicParameters();
                p.Add("@Name", model.Name.ToDbValue());
                p.Add("@IsDeleted", model.IsDeleted.ToDbValue());
                p.Add("@PageSize", model.PageSize);
                p.Add("@PageIndex", model.PageIndex);

                var query = p.CreateQuery(
                    "sp.LsitBrand",
                    cancellationToken: cancellationToken
                );

                var result = await connection.QueryAsync<ListBanerResult>(query);

                return Result<IEnumerable<ListBanerResult>>.Successful(data: result);
            }
        }
        catch (Exception ex)
        {
            throw new DbException($"Brand Add Exception {ex.Message}");
        }
    }
}
