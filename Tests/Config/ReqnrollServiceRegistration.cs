using AdvancedReqnRollTest.Drivers;
using AdvancedReqnRollTest.Interfaces;
using AdvancedReqnRollTest.Models;

namespace AdvancedReqnRollTest.Config;

using OpenQA.Selenium;
using Reqnroll.BoDi;

public static class ReqnrollServiceRegistration
{
    public static IWebDriver RegisterServices(IObjectContainer container)
    {
        var settings = ConfigurationHelper.GetSettings<AppSettings>();
        container.RegisterInstanceAs(settings);

        // Register WebDriverManager
        container.RegisterTypeAs<WebDriverManager, IWebDriverManager>();
        var driverManager = container.Resolve<IWebDriverManager>();

        // Init WebDriver
        var driver = driverManager.InitDriver();
        container.RegisterInstanceAs<IWebDriver>(driver);

        // ✅ Pass AppSettings to PageObjectManager
        IPageObjectManager pageManager = new PageObjectManager(driver, settings);
        container.RegisterInstanceAs<IPageObjectManager>(pageManager);

        return driver;
    }
}
