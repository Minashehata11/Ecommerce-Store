using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Store.Data.Context;
using Store.Data.Entities.IdentityEntities;
using Store.Rebository;

namespace Store.Api.Helper
{
    public class ApplySeeding
    {
        public  static async Task ApplySeedingAsync(WebApplication app)
        {
            using(var scope=app.Services.CreateScope())
            {
                var services=scope.ServiceProvider;
                var loggerfFactory=services.GetRequiredService<ILoggerFactory>();
                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();
                    var userManger = services.GetRequiredService<UserManager<ApplicationUser>>();
                    await context.Database.MigrateAsync();
                    await ApplicationContextSeed.seedAsync(context, loggerfFactory);
                    await AppIdentityContextSeed.SeedUserAsync(userManger);
                }

                catch (Exception ex)
                {
                    var logger=loggerfFactory.CreateLogger<ApplySeeding>();
                    logger.LogError(ex.Message);
                }
            }
        }
    }
}
