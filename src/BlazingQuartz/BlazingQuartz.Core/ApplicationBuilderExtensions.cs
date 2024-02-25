using BlazingQuartz.Core.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BlazingQuartz.Core
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseBlazingQuartz(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var options = scope.ServiceProvider.GetRequiredService<IOptions<BlazingQuartzCoreOptions>>().Value;
                if (options.AutoMigrateDb)
                {
                    var db = scope.ServiceProvider.GetRequiredService<BlazingQuartzDbContext>();
                    db.Database.Migrate();
                }
            }

            return app;
        }
    }
}
