using Hangfire;
using Hangfire.Mongo;
using Hangfire.Mongo.Migration.Strategies;
using Hangfire.Mongo.Migration.Strategies.Backup;

namespace XpChallenge.Portfolio.Api.Configurations.Extensions
{
    public static class HangfireConfigurationExtension
    {
        public static void AddHangfire(this IServiceCollection services, IConfiguration configuration)
        {
            var mongoSettings = configuration.GetSection(nameof(MongoConfiguration)).Get<MongoConfiguration>();

            if (mongoSettings != null && !string.IsNullOrEmpty(mongoSettings.ConnectionString))
            {
                var options = new MongoStorageOptions
                {
                    MigrationOptions = new MongoMigrationOptions
                    {
                        MigrationStrategy = new DropMongoMigrationStrategy(),
                        BackupStrategy = new NoneMongoBackupStrategy()
                    }
                };

                var connectionString = string.Format(mongoSettings.ConnectionString,
                    mongoSettings.DatabaseName);

                services.AddHangfire(configuration => configuration
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseMongoStorage(connectionString, options));

                services.AddHangfireServer();
            }
        }
    }
}
