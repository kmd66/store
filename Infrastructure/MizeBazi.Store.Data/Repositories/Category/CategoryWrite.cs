using Microsoft.EntityFrameworkCore;
using MizeBazi.Store.Application;
using MizeBazi.Store.Application.Interfaces;
using MizeBazi.Store.Common.Helper;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Data.Repositories;

public class CategoryWrite(StoreContext context) : ICategoryWrite
{
    readonly StoreContext _context = context;

    public async Task<Result> AddAsync(AddCategoryCommand model, CancellationToken cancellationToken = default)
    {
        try
        {
            var Category = model.JsonMapObject<Entities.Category>();
            Category.UnicId = Guid.NewGuid();
            Category.Date = DateTime.UtcNow;
            Category.IsDeleted = false;
            Category.DeletedDate = null;
            
            _context.Categorys.Add(Category);
            await _context.SaveChangesAsync(cancellationToken);

            return Result.Successful();
        }
        catch (Exception ex)
        {
            throw new DbException($"Category Add Exception {ex.Message}");
        }
    }

    public async Task<Result> EditeAsync(EditeCategoryCommand model, CancellationToken cancellationToken = default)
    {
        try
        {
            var ett = await _context.Categorys.FirstOrDefaultAsync(x =>
                x.Id == model.Id, 
                cancellationToken
            );

            if (ett == null)
                return Result.Successful();

            ett.Name = model.Name;
            ett.Description = model.Description;
            ett.ImageUrl = model.ImageUrl;
            _context.Update(ett);
            await _context.SaveChangesAsync();
            return Result.Successful();

        }
        catch (Exception ex)
        {
            throw new DbException($"Category Edite Exception {ex.Message}");
        }
    }
}

