using System;
using Microsoft.EntityFrameworkCore;
using library.utilities;

namespace test.utilities.contextconfiguration
{
  public static class DbContextExtension
  {
    public static DbContextOptions<T> CreateUniqueClassOptions<T>(this object callingClass, bool dlmode =false) where T : DbContext
    {
        return CreateOptionsWithDatabaseName<T>(callingClass, null, dlmode).Options;
    }

    private static DbContextOptionsBuilder<T> CreateOptionsWithDatabaseName<T>(object callingClass, string callingMember=null, bool dlmode=false) where T : DbContext
    {
      string strConnectionString = callingClass.GetUniqueDbConnectionString(callingMember,'_', dlmode, extension: true);
      DbContextOptionsBuilder<T> builder = new DbContextOptionsBuilder<T>();
      builder.UseSqlServer(strConnectionString);
      return builder;
    }
  }
}             