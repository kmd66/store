using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Helper;
using MizeBazi.Store.Common.Shared;
using MizeBazi.Store.Domain;

namespace MizeBazi.Store.Application;

public class EditeBrandValidator(IAppLogger<EditeBrandValidator> logger) 
    : BrandValidatorBase<EditeBrandCommand>
{
    public override Task Handle(EditeBrandCommand command)
    {
        var errors = ValidateCommon(command);

        if (command.Id == 0)
            errors.Add(BrandConstants.ValidatError_Id);

        if(errors.Count > 0)
        {
            string errorMessages = errors.AppJoin();
            logger.LogWarning($"Exception Edite message: {errorMessages}");
            throw new ValidatorException($"Brand Edite Exception: {errorMessages}");
        }

        return Task.CompletedTask;
    }
} 