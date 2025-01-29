using Microsoft.EntityFrameworkCore;
using System;
using TTB.Assignment.API.Repositories;

namespace TTB.Assignment.API.Extensions
{
    public static class DbExtension
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();

            using AppDbContext dbContext =
                scope.ServiceProvider.GetRequiredService<AppDbContext>();

            dbContext.Database.Migrate();
        }
    }
}
