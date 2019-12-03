using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Share.Data;

public static class ApplicationBuilderExtension
{

    public static IApplicationBuilder DatabaseMigration(this IApplicationBuilder app)
    {
        using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
        {
            serviceScope.ServiceProvider.GetService<ShareDbContext>().Database.Migrate();
        }
        return app;

    }
}

