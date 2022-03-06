using library.entities;
using library.utilities;
using Microsoft.EntityFrameworkCore;

namespace library.datacenter
{
  /// <summary>
  /// 
  /// </summary>
  public class APIDbContext: DbContext
  {

    private string ConnectionString { get; set; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public APIDbContext(DbContextOptions<APIDbContext> options): base(options)
    {
      System.Console.WriteLine("APIDbContext.Constructor(DbContextOptions<>)");
    }

    /// <summary>
    /// For InitialMigration
    /// </summary>
    public APIDbContext()
    {
      System.Console.WriteLine("APIDbContext.DefaultConstructor");
      ConnectionString = this.MigrationConnectionString(null, dlmode: true);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="optionsBuilder"></param>
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      System.Console.WriteLine("APIDbContext.OnConfiguring");
      if(!optionsBuilder.IsConfigured && !string.IsNullOrEmpty(ConnectionString))
      {
        optionsBuilder
          .UseSqlServer(ConnectionString, providerOptions => providerOptions.CommandTimeout(1200))
          .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
      }
    }

    public virtual DbSet<Tasks> Tasks { get; set; }
    public virtual DbSet<Members> Members { get; set; }
  }
}