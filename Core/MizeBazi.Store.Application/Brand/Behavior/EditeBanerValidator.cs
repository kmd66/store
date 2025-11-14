using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Helper;
using MizeBazi.Store.Common.Shared;
using MizeBazi.Store.Domain;

namespace MizeBazi.Store.Application;

public class EditeBanerValidator(IAppLogger<EditeBanerValidator> logger) : IBehaviorHandler<EditeBanerCommand>
{
    public Task Handle(EditeBanerCommand command)
    {
        var error = new List<string>();

        if (command.Id == 0)
            error.Add(BanerConstants.ValidatError_Id);
        if (command.Name.IsNullOrEmpty())
            error.Add(BanerConstants.ValidatError_Name);
        if (command.Description.IsNullOrEmpty())
            error.Add(BanerConstants.ValidatError_Description);
        if (command.LogoUrl.IsNullOrEmpty())
            error.Add(BanerConstants.ValidatError_LogoUrl);

        if(error.Count > 0)
        {
            string errorMessages = error.AppJoin();
            logger.LogWarning($"Exception message: {error.AppJoin()}");
            throw new ValidatorException($"Brand Add Exception: {errorMessages}");
        }

        return Task.CompletedTask;
    }

    public Task Handle(EditeBanerCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
} 