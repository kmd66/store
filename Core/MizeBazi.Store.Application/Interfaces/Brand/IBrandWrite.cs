using MizeBazi.Store.Common.Shared;
using MizeBazi.Store.Domain;

namespace MizeBazi.Store.Application.Interfaces;

public interface IBrandWrite
{
    Task<Result> AddAsync(DbBrand brand);
}
