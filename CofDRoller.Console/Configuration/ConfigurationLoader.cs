using Microsoft.Extensions.Configuration;

namespace CofdRoller.Console;
public static class ConfigurationLoader
{
    public static Config Load()
    {
        var Configuration = LoadFromJsonFile("config");

        var config = new Config();

        return config;
    }

    public static IConfigurationRoot LoadFromJsonFile(string fileName, bool optional = false)
    {
        return new ConfigurationBuilder().AddJsonFile(fileName + ".json", optional).AddJsonFile(fileName + "-local.json", optional: true).AddJsonFile(fileName + "-" + Environment.MachineName + ".json", optional: true)
            .Build();
    }
}