 namespace library.utilities
 {
   public static class DbConfiguration
   {
     public static string MigrationConnectionString(this object callingClass, string callingMember=null, bool dlmode = false, bool extensions = false)
     {
       return callingClass.GetUniqueDbConnectionString(callingMember, '_', dlmode, extensions);
     }
   }
 }