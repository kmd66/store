using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Domain;

public class DbBrand : SoftDeleteEntity
{
    public string Name { get; set; }

    public string Description { get; set; }

    public string LogoUrl { get; set; }

}

