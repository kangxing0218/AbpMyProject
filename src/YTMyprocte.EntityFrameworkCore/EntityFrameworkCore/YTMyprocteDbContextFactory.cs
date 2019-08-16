using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using YTMyprocte.Configuration;
using YTMyprocte.Web;

namespace YTMyprocte.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class YTMyprocteDbContextFactory : IDesignTimeDbContextFactory<YTMyprocteDbContext>
    {
        public YTMyprocteDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<YTMyprocteDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            YTMyprocteDbContextConfigurer.Configure(builder, configuration.GetConnectionString(YTMyprocteConsts.ConnectionStringName));

            return new YTMyprocteDbContext(builder.Options);
        }
    }
}
