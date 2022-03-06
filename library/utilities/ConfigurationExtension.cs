
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace library.utilities
{
  public static class ConfigurationExtension
  {
    private const string AppSettingFileName = "appsettings.json";
    private const string ConnectionTemplate = "UnitTesting";
    private const string RequiredKeyworkInConnectionTemp = "Test";

    /// <summary>
    /// 
    /// </summary>
    /// <param name="callingAssembly"></param>
    /// <param name="dlmode"></param>
    /// <returns></returns>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static string GetAssemblyTopLevelDirectory(Assembly callingAssembly = null, bool dlmode = false)
    {
      string strReturnDirectory = string.Empty;
      string strBinDir = $"{Path.DirectorySeparatorChar}bin{Path.DirectorySeparatorChar}"; //Never use /bin/
      string strPathToManipulate = (callingAssembly ?? Assembly.GetCallingAssembly()).Location;

      if(dlmode)
      {
        System.Console.WriteLine("static DbContextHelper.GetAssemblyTopLevelDirectory");
        System.Console.WriteLine($"\tCalling Assembly Location: {strPathToManipulate}");
      }

      int indexOfBinStart = strPathToManipulate.IndexOf(strBinDir, System.StringComparison.OrdinalIgnoreCase);

      if(indexOfBinStart <= 0)
      {
        throw new System.Exception($"Did not find '{strBinDir}' in the assembly. Do you need to provide the callingAssembly parameter?");
      }

      strReturnDirectory = strPathToManipulate.Substring(0, indexOfBinStart);

      if(dlmode)
      {
        System.Console.WriteLine($"\tCalling Assembly Location /bin/ index start: {indexOfBinStart}");
        System.Console.WriteLine($"\tCalling Assembly Location Return Path Before index start: {strReturnDirectory}");
      }

      return strReturnDirectory;
    }

   /// <summary>
   /// 
   /// </summary>
   /// <param name="relativeAssemblyOfCallingClass"></param>
   /// <param name="settingFileName"></param>
   /// <param name="dlmode"></param>
   /// <returns></returns>
    public static IConfigurationRoot GetConfiguration(string relativeAssemblyOfCallingClass, string settingFileName=AppSettingFileName, bool dlmode=false)
    {
      string strCallingProjectPath = GetAssemblyTopLevelDirectory(Assembly.GetCallingAssembly(), dlmode);
      if(dlmode)
      {
        System.Console.WriteLine($"static DbContextHelper.GetConfiguration");
        System.Console.WriteLine($"Calling Project Path from GetCallingAssemly(): {strCallingProjectPath}");
      }

      string[] arrRelCallingClass = relativeAssemblyOfCallingClass.Split(',');

      if(dlmode)
      {
        System.Console.WriteLine($"Relative Assembly Of Calling Class: {relativeAssemblyOfCallingClass} \n\tonly interested in --> {arrRelCallingClass[0]}");
      }

      //string strProjectPathOfInterest = Path.GetFullPath(strCallingProjectPath + Path.DirectorySeparatorChar + arrRelCallingClass[0] + ".csproj");

      ConfigurationBuilder builder = new ConfigurationBuilder();
      builder.SetBasePath(strCallingProjectPath).AddJsonFile(settingFileName, optional: true);
      
      return builder.Build();
    }
  
    /// <summary>
    /// 
    /// </summary>
    /// <param name="testClass"></param>
    /// <param name="optionalMethodName"></param>
    /// <param name="seperator"></param>
    /// <param name="dlmode"></param>
    /// <returns></returns>
    public static string GetUniqueDbConnectionString(this object testClass, string optionalMethodName = null, char seperator='_', bool dlmode=false, bool extension=false)
    {
      if(dlmode)
      {
        System.Console.WriteLine("static DbContextHelper.GetUniqueDbConnectionString");
      }

      string strRawConnectionString = GetConfiguration(Assembly.GetAssembly(testClass.GetType()).ToString(), AppSettingFileName, dlmode).GetConnectionString(ConnectionTemplate);

      if(dlmode)
      {
        System.Console.WriteLine($"\tRaw Connection String: {strRawConnectionString}");
      }

      SqlConnectionStringBuilder builderSb = new SqlConnectionStringBuilder(strRawConnectionString);

      if(extension)
      {
        string strUniqueDbExtension = $"{seperator}{testClass.GetType().Name}";
        builderSb.InitialCatalog += strUniqueDbExtension; 
      }
      
      if(dlmode)
      {
        System.Console.WriteLine($"\tFinal Connection String: {builderSb.ConnectionString}");
      }

      return builderSb.ToString();
    }
  
  }
}