// DependencyConfig.cs
using Btec_Website;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


public static class DependencyConfig
{
    public static void ConfigureServices(IServiceCollection services)
    {
        // Register your DbContext
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer("Server=QUANG-SILLY-GOO;Database=BTEC;Integrated Security=true;TrustServerCertificate=true;"));
        // Add other services as needed
        // ...
    }
}
