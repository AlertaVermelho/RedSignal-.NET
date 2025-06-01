using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RedSignal.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        // Conexão com o Oracle — substitua pelos dados reais
        optionsBuilder.UseOracle("User Id=xxxxxx;Password=xxxxxx;Data Source=localhost:1521/br.com.fiap.oracle;");

        return new AppDbContext(optionsBuilder.Options);
    }
}
