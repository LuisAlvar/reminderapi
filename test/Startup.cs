using System;
using Xunit;
using Xunit.Abstractions;
using test.attributes;
using test.utilities.contextconfiguration;
using library.datacenter;
using Microsoft.EntityFrameworkCore;

namespace test
{ 
    public class Startup
    {
        private readonly ITestOutputHelper output;

        public Startup(ITestOutputHelper output)
        {
          var options = this.CreateUniqueClassOptions<APIDbContext>(dlmode: true);
          APIDbContext context = new APIDbContext(options);
          context.Database.Migrate();
          
          this.output = output;
        }

        [Fact]
        public void SetupMigration()
        {
          output.WriteLine("Running First");
        }
    }
}