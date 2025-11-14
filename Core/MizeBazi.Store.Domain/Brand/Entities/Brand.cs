
using MizeBazi.Store.Common.Helper;

namespace MizeBazi.Store.Domain;

public class Brand : DbBrand
{
    public Brand() { }
    public static Brand CreateFromBaseModel(DbBrand model) => model.JsonMapObject<Brand>();

    public Brand(string name, string description, string logoUrl)
    {
        Name = name;
        Description = description;
        LogoUrl = logoUrl;
    }

    public override void Confirm(Common.Shared.EventType t)
    {
        base.Confirm(t);
    }
}

