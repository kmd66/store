using MizeBazi.Store.Application.Interfaces;
using MizeBazi.Store.Common.Shared;
using MizeBazi.Store.Domain;

namespace MizeBazi.Store.Data.Repositories;

public class BrandWrite : IBrandWrite
{
    public Task<Result> AddAsync(DbBrand brand)
    {
        throw new NotImplementedException();
    }
}

