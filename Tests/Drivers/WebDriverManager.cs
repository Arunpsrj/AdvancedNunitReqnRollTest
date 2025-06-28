using System;
using System.Collections.Generic;
using System.IO;
using AdvancedReqnRollTest.Enums;
using AdvancedReqnRollTest.Interfaces;
using AdvancedReqnRollTest.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Remote;

namespace AdvancedReqnRollTest.Drivers;

public class WebDriverManager : IWebDriverManager
{
    private readonly AppSettings _settings;

    public WebDriverManager(AppSettings settings)
    {
        _settings = settings ?? throw new ArgumentNullException(nameof(settings));
    }

    public IWebDriver InitDriver()
    {
        if (!Enum.TryParse(_settings.Browser, true, out BrowserType browserType))
        {
            throw new NotSupportedException($"Browser '{_settings.Browser}' is not supported.");
        }

        return _settings.RunOn?.ToLower() switch
        {
            "browserstack" => InitRemoteDriver(browserType),
            _ => InitLocalDriver(browserType)
        };
    }

    private IWebDriver InitLocalDriver(BrowserType browserType)
    {
        IWebDriver driver = browserType switch
        {
            BrowserType.Chrome => new ChromeDriver(GetSafeChromeOptions()),
            BrowserType.Edge => new EdgeDriver(), // Consider similar logic if using Edge in CI
            _ => throw new NotSupportedException($"Browser '{browserType}' is not supported.")
        };

        driver.Manage().Window.Maximize();
        return driver;
    }
    
    private ChromeOptions GetSafeChromeOptions()
    {
        var options = new ChromeOptions();

        // Check if running in CI (like GitHub Actions, Azure Pipelines, etc.)
        var isCiEnvironment = Environment.GetEnvironmentVariable("CI")?.ToLower() == "true";

        if (isCiEnvironment)
        {
            options.AddArgument("--headless=new"); // Only use headless in CI
            options.AddArgument("--no-sandbox");
            options.AddArgument("--disable-dev-shm-usage");

            // Avoid "user data dir is already in use" error
            string tempUserDataDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            options.AddArgument($"--user-data-dir={tempUserDataDir}");

            options.AddArgument("--disable-gpu");
        }

        return options;
    }
    
    private IWebDriver InitRemoteDriver(BrowserType browserType)
    {
        if (string.IsNullOrWhiteSpace(_settings.BrowserStack?.User) ||
            string.IsNullOrWhiteSpace(_settings.BrowserStack?.Key))
        {
            throw new InvalidOperationException("BrowserStack credentials are missing in config.");
        }

        ChromeOptions options = new()
        {
            BrowserVersion = "latest",
            PlatformName = "Windows 10"
        };

        var bstackOptions = new Dictionary<string, object>
        {
            ["os"] = "Windows",
            ["osVersion"] = "10",
            ["userName"] = _settings.BrowserStack.User,
            ["accessKey"] = _settings.BrowserStack.Key,
            ["buildName"] = "Build 1.0",
            ["sessionName"] = "ReqnRoll Test",
            ["resolution"] = "1920x1080"
        };

        options.AddAdditionalOption("bstack:options", bstackOptions);

        return new RemoteWebDriver(
            new Uri("https://hub-cloud.browserstack.com/wd/hub/"),
            options.ToCapabilities(),
            TimeSpan.FromSeconds(60));
    }
}