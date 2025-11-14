using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Helper;
using MizeBazi.Store.Common.Shared;
using MizeBazi.Store.Domain;

namespace MizeBazi.Store.Application;

public class AddBanerValidator(IAppLogger<AddBanerValidator> logger) : IBehaviorHandler<AddBanerCommand>
{
    public Task Handle(AddBanerCommand command)
    {
        var error = new List<string>();

        if (command.Id > 0)
            error.Add(BanerConstants.ValidatError_Id);
        if (command.Name.IsNullOrEmpty())
            error.Add(BanerConstants.ValidatError_Name);
        if (command.Description.IsNullOrEmpty())
            error.Add(BanerConstants.ValidatError_Description);
        if (command.LogoUrl.IsNullOrEmpty())
            error.Add(BanerConstants.ValidatError_LogoUrl);

        if (error.Count > 0)
        {
            string errorMessages = string.Join(":,:", error);
            logger.LogWarning($"Exception message: {errorMessages}");
            throw new ValidatorException($"Brand Add Exception: {errorMessages}");
        }

        return Task.CompletedTask;
    }

    public Task Handle(AddBanerCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
} 