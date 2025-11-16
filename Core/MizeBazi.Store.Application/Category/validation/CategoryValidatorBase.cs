using MizeBazi.Store.Common.Abstractions;
using MizeBazi.Store.Common.Helper;
using MizeBazi.Store.Domain;

namespace MizeBazi.Store.Application;

public abstract class CategoryValidatorBase<T> : BehaviorBase<T>
{
    protected List<string> ValidateCommon(BaseCategoryRecordModel command)
    {
        var errors = new List<string>();

        if (command.Name.IsNullOrEmpty())
            errors.Add(CategoryConstants.ValidatError_Name);
        if (command.Description.IsNullOrEmpty())
            errors.Add(CategoryConstants.ValidatError_Description);
        if (command.ImageUrl.IsNullOrEmpty())
            errors.Add(CategoryConstants.ValidatError_ImageUrl);

        return errors;
    }
}