using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Helper;
using MizeBazi.Store.Common.Shared;
using MizeBazi.Store.Domain;

namespace MizeBazi.Store.Application;

public class AddCategoryValidator(IAppLogger<AddCategoryValidator> logger)
    : CategoryValidatorBase<AddCategoryCommand>
{
    public override Task Handle(AddCategoryCommand command)
    {
        var errors = ValidateCommon(command);

        if (command.Id > 0)
            errors.Add(CategoryConstants.ValidatError_Id);

        if (errors.Count > 0)
        {
            string errorMessages = errors.AppJoin();
            logger.LogWarning($"Exception Add message: {errorMessages}");
            throw new ValidatorException($"Brand Add Exception: {errorMessages}");
        }

        return Task.CompletedTask;
    }
} 