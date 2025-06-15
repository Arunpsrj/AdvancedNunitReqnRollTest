namespace AdvancedReqnRollTest.Config;

using OpenQA.Selenium;
using Reqnroll.BoDi;

public static class ReqnrollServiceRegistration
{
    public static IWebDriver RegisterServices(IObjectContainer container)
    {
        // Register config
        var settings = ConfigurationHelper.GetSettings<AppSettings>();
        container.RegisterInstanceAs(settings);

        // Register WebDriverManager
        container.RegisterTypeAs<WebDriverManager, IWebDriverManager>();
        var driverManager = container.Resolve<IWebDriverManager>();

        // Init WebDriver
        var driver = driverManager.InitDriver();
        container.RegisterInstanceAs<IWebDriver>(driver);

        // Register POMs or additional dependencies
        container.RegisterTypeAs<LoginPage, LoginPage>();

        return driver;
    }
}
