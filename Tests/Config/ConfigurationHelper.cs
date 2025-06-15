using System;
using System.IO;
using Microsoft.Extensions.Configuration;

public static class ConfigurationHelper
{
    private static readonly IConfigurationRoot Configuration;

    static ConfigurationHelper()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        Configuration = builder.Build()
            ?? throw new InvalidOperationException("Failed to build configuration");
    }

    public static T GetSettings<T>() where T : class
    {
        var section = Configuration.Get<T>(options =>
        {
            options.ErrorOnUnknownConfiguration = true;
            options.BindNonPublicProperties = false;
        });

        if (section == null)
        {
            throw new InvalidOperationException($"Failed to bind configuration to type {typeof(T).Name}. Please ensure all required properties are set in appsettings.json");
        }

        return section;
    }

    public static string GetValue(string key)
    {
        ArgumentNullException.ThrowIfNull(key);
        return Configuration[key];
    }
}
