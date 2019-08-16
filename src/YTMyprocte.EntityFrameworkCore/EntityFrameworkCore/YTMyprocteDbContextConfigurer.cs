using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace YTMyprocte.EntityFrameworkCore
{
    public static class YTMyprocteDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<YTMyprocteDbContext> builder, string connectionString)
        {
            builder.UseMySql(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<YTMyprocteDbContext> builder, DbConnection connection)
        {
            builder.UseMySql(connection);
        }
    }
}
