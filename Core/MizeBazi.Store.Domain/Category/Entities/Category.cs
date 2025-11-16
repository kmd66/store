using MizeBazi.Store.Common.Helper;
using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Domain;

public class Category : DbCategory
{
    public Category() { }
    public static Category CreateFromBaseModel(BaseEntityRecord model) => model.JsonMapObject<Category>();

    public Category(string name, string description, string imageUrl)
    {
        Name = name;
        Description = description;
        ImageUrl = imageUrl;
    }

    public override void Confirm(Common.Shared.EventType t)
    {
        base.Confirm(t);
    }
}

