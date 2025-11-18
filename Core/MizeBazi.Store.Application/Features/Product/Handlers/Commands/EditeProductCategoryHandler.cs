using MizeBazi.Store.Application.Interfaces;
using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Helper;
using MizeBazi.Store.Common.Shared;
using MizeBazi.Store.Domain;

namespace MizeBazi.Store.Application;

public class EditeProductCategoryHandler(
    IProductCategoryRepository dataSource,
    IProductRead productRead
    ) : CommandBase<EditeProductCategoryCommand, Result>
{
    public override async Task<Result> Handle(EditeProductCategoryCommand command, CancellationToken ct)
    {
        var model = ProductFactory.Create(command.Id, command.Categories);
        
        var productQuery = command.JsonMapObject<GetProductByIdQuery>();
        productQuery.Check(BrandConstants.ValidatError_Id);

        var product = await productRead.GetAsync(productQuery.Id, productQuery.UnicId, null, ct);
        if (!product.success)
            return Result.Failure(message: product.message);
        if (product.data == null)
            return Result.Failure(message: ProductConstants.Error_ProductDataNull);

        var categorys = await dataSource.GetCategoryIdsAsync(model, ct);
        if (!categorys.success)
            return Result.Failure(message: categorys.message);

        //var ids = categorys.data.ToList().Select(x => x.ProductId).ToList();
        if (!model.CategoryIdsEqual(categorys.data.ToList()))
            return Result.Failure(message: ProductConstants.Error_CategoryIdsEqual);


        return await dataSource.EditeAsync(model, ct);
    }
}