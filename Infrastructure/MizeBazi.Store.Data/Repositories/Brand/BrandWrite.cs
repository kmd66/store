using Microsoft.EntityFrameworkCore;
using MizeBazi.Store.Application;
using MizeBazi.Store.Application.Interfaces;
using MizeBazi.Store.Common.Helper;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Data.Repositories;

public class BrandWrite(StoreContext context) : IBrandWrite
{
    readonly StoreContext _context = context;

    public async Task<Result> AddAsync(AddBrandCommand model, CancellationToken cancellationToken = default)
    {
        try
        {
            var brand = model.JsonMapObject<Entities.Brand>();
            brand.UnicId = Guid.NewGuid();
            brand.Date = DateTime.UtcNow;
            brand.IsDeleted = false;
            brand.DeletedDate = null;
            
            _context.Brands.Add(brand);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Successful();
        }
        catch (Exception ex)
        {
            throw new DbException($"Brand Add Exception {ex.Message}");
        }
    }

    public async Task<Result> EditeAsync(EditeBrandCommand model, CancellationToken cancellationToken = default)
    {
        try
        {
            var ett = await _context.Brands.FirstOrDefaultAsync(x =>
                x.Id == model.Id, 
                cancellationToken
            );

            if (ett == null)
                return Result.Successful();

            ett.Name = model.Name;
            ett.Description = model.Description;
            ett.LogoUrl = model.LogoUrl;
            _context.Update(ett);
            await _context.SaveChangesAsync();
            return Result.Successful();

        }
        catch (Exception ex)
        {
            throw new DbException($"Brand Edite Exception {ex.Message}");
        }
    }
}

