using MizeBazi.Store.Application.Interfaces;
using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Helper;
using MizeBazi.Store.Common.Shared;
using MizeBazi.Store.Domain;

namespace MizeBazi.Store.Application;

public class EditeProductCategoryHandler(
    IProductCategoryRepositorys dataSource,
    IProductRead productRead
    ) : CommandBase<EditeProductCategoryCommand, Result>
{
    public override async Task<Result> Handle(EditeProductCategoryCommand command, CancellationToken ct)
    {
        var model = ProductFactory.Create(command.id, command.categories);
        var productQuery = command.JsonMapObject<GetProductByIdQuery>();

        var product = await productRead.GetAsync(productQuery, ct);
        if (!product.success)
            return Result.Failure(message: product.message);
        if (product.data == null)
            return Result.Failure(message: ProductConstants.Error_ProductDataNull);

        var categorys = await dataSource.ListByIdAsync(command.id, ct);
        if (!categorys.success)
            return Result.Failure(message: categorys.message);

        var ids = categorys.data.ToList().Select(x => x.ProductId).ToList();
        if (!model.CategoryIdsEqual(ids))
            return Result.Failure(message: ProductConstants.Error_CategoryIdsEqual);


        return await dataSource.EditeAsync(model, ct);
    }
}