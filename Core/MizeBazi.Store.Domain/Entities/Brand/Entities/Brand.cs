using MizeBazi.Store.Common.Helper;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Domain;

public class Brand : DbBrand
{
    public Brand() { }
    public static Brand CreateFromBaseModel(BaseEntityRecord model) => model.JsonMapObject<Brand>();

    public Brand(string name, string description, string logoUrl)
    {
        Name = name;
        Description = description;
        LogoUrl = logoUrl;
    }
}

