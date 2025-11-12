using MizeBazi.Store.Common.Shared;
using MizeBazi.Store.Domain;

namespace MizeBazi.Store.Application.Interfaces;

public interface IBrandRead
{
    Task<Result<DbBrand>> GetAsync(long id);
}