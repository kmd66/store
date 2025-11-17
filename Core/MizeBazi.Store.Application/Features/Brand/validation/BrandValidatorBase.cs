using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Helper;
using MizeBazi.Store.Domain;

namespace MizeBazi.Store.Application;

public abstract class BrandValidatorBase<T> : BehaviorBase<T>
{
    protected List<string> ValidateCommon(BaseBrandRecordModel command)
    {
        var errors = new List<string>();

        if (command.Name.IsNullOrEmpty())
            errors.Add(BrandConstants.ValidatError_Name);
        if (command.Description.IsNullOrEmpty())
            errors.Add(BrandConstants.ValidatError_Description);
        if (command.LogoUrl.IsNullOrEmpty())
            errors.Add(BrandConstants.ValidatError_LogoUrl);

        return errors;
    }
}