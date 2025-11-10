using MizeBazi.Store.Common.Shared;

namespace MizeBazi.Store.Data.Entities;

public class DbCategory : SoftDeleteEntity
{
    public string Name { get; set; }

    public string Description { get; set; }

    public string ImageUrl { get; set; }

}