using MizeBazi.Store.Application.Interfaces;
using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Application;

public class EditCategoryCommandHandler(
    ICategoryWrite dataSource
    ) : CommandBase<EditeCategoryCommand, Result>
{

    public override Task<Result> Handle(EditeCategoryCommand command)
        => dataSource.EditeAsync(command);
}