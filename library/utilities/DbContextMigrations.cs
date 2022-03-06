using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using library.datacenter;
using Microsoft.EntityFrameworkCore;

namespace library.utilities
{
  public static class DbContextMigrations
  {
    public static void InitialMigrate(IApplicationBuilder app)
    {
      using(var serviceScope = app.ApplicationServices.CreateScope())
      {
        RunMigration(serviceScope.ServiceProvider.GetService<APIDbContext>());
      }
    }

    private static void RunMigration(APIDbContext context)
    {
      System.Console.WriteLine("Applying Migrations...");
      context.Database.Migrate();
      System.Console.WriteLine("Done Migrations");
    }

  }
}