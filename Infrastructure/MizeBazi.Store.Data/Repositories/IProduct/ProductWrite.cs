using Microsoft.EntityFrameworkCore;
using MizeBazi.Store.Application;
using MizeBazi.Store.Application.Interfaces;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Data.Repositories;

public class ProductWrite(StoreContext context) : IProductWrite
{
    readonly StoreContext _context = context;

    public async Task<Result<long>> AddAsync(Domain.Product model, CancellationToken ct = default)
    {
        await using var transaction = await context.Database.BeginTransactionAsync();
        try
        {
            var ett = new Entities.Product();
            ett.DomainProductMap(model);

            _context.Products.Add(ett);
            await _context.SaveChangesAsync(ct);

            List<Entities.ProductCategory> list = new();
            foreach (var id in model.Categories.CategoryIds)
            {
                list.Add(new Entities.ProductCategory
                {
                    ProductId = ett.Id,
                    CategoryId = id
                });
            }
            _context.ProductCategorys.AddRange(list);

            await _context.SaveChangesAsync(ct);
            await transaction.CommitAsync();
            return Result<long>.Successful(data: ett.Id);
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            throw new DbException($"Product EditePublish Exception {ex.Message}");
        }
    }

    public async Task<Result> EditeAsync(Domain.Product model, CancellationToken ct = default)
    {
        try
        {
            var ett = await _context.Products.FirstOrDefaultAsync(x =>
                x.Id == model.Id,
                ct
            );
            if (ett == null)
                throw new DbException($"ett == null");

            ett.DomainProductMap(model);

            _context.Products.Update(ett);
            await _context.SaveChangesAsync(ct);

            return Result.Successful();
        }
        catch (Exception ex)
        {
            throw new DbException($"Product EditePublish Exception {ex.Message}");
        }
    }

    public async Task<Result> EditePublishAsync(PublishProductCommand model, CancellationToken ct = default)
    {
        try
        {
            var ett = await _context.Products.FirstOrDefaultAsync(x =>
                x.Id == model.Id,
                ct
            );
            if (ett == null)
                return Result.Successful();

            ett.IsPublished = model.State;
            _context.Products.Update(ett);
            await _context.SaveChangesAsync(ct);

            return Result.Successful();
        }
        catch (Exception ex)
        {
            throw new DbException($"Product EditePublish Exception {ex.Message}");
        }
    }

    public async Task<Result> DeleteAsync(DeleteProductCommand model, CancellationToken ct = default)
    {
        try
        {
            var ett = await _context.Products.FirstOrDefaultAsync(x =>
                x.Id == model.Id,
                ct
            );
            if (ett == null)
                return Result.Successful();
            if(model.State)
                ett.DeletedDate = DateTime.UtcNow;
            else
                ett.DeletedDate = null;
            ett.IsDeleted = model.State;
            _context.Products.Update(ett);
            await _context.SaveChangesAsync(ct);

            return Result.Successful();
        }
        catch (Exception ex)
        {
            throw new DbException($"Product Delete Exception {ex.Message}");
        }
    }
}

