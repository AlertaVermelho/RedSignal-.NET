using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RedSignal.Data;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        // Conexão com o Oracle — substitua pelos dados reais
        optionsBuilder.UseOracle("User Id=RM555698;Password=fiap25;Data Source=localhost:1521/br.com.fiap.oracle;");

        return new AppDbContext(optionsBuilder.Options);
    }
}
