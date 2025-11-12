using MizeBazi.Store.Application.Interfaces;
using MizeBazi.Store.Common.Shared;
using MizeBazi.Store.Domain;

namespace MizeBazi.Store.Data.Repositories;

public class BrandRead : IBrandRead
{
    public Task<Result<DbBrand>> GetAsync(long id)
    {
        throw new NotImplementedException();
    }
}
