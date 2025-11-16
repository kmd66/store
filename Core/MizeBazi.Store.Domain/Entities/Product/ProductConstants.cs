namespace MizeBazi.Store.Domain
{
    public class ProductConstants
    {
        public const string ValidatError_Id = "command.Id > 0";
        public const string ValidatError_Name = "command.Name.IsNullOrEmpty()";
        public const string ValidatError_Description = "command.Description.IsNullOrEmpty()";
        public const string ValidatError_ImageUrl = "command.LogoUrl.IsNullOrEmpty()";
    }
}
