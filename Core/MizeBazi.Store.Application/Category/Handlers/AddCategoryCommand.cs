using MizeBazi.Store.Application.Interfaces;
using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Application;

public class AddCategoryCommandHandler(
    ICategoryWrite dataSource
    ) : CommandBase<AddCategoryCommand, Result>
{

    public override Task<Result> Handle(AddCategoryCommand request)
        => dataSource.AddAsync(request);

}