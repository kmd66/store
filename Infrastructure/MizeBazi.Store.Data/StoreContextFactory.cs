using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;

namespace MizeBazi.Store.Data;
public class StoreContextFactory : IDesignTimeDbContextFactory<StoreContext>
{
    public StoreContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<StoreContext>();

        //optionsBuilder.UseSqlServer(
        //    "Server=.;Database=MizeBaziStore;Trusted_Connection=True;Encrypt=False");

        return new StoreContext(optionsBuilder.Options);
    }
}