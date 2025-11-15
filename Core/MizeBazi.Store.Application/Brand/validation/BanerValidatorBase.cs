using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Helper;
using MizeBazi.Store.Domain;

namespace MizeBazi.Store.Application;

public abstract class BanerValidatorBase<T> : BehaviorBase<T>
{
    protected List<string> ValidateCommon(BaseBanerRecordModel command)
    {
        var errors = new List<string>();

        if (command.Name.IsNullOrEmpty())
            errors.Add(BanerConstants.ValidatError_Name);
        if (command.Description.IsNullOrEmpty())
            errors.Add(BanerConstants.ValidatError_Description);
        if (command.LogoUrl.IsNullOrEmpty())
            errors.Add(BanerConstants.ValidatError_LogoUrl);

        return errors;
    }
}