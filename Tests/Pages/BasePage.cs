using System;
using AdvancedReqnRollTest.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

public abstract class BasePage
{
    protected readonly IWebDriver Driver;
    private readonly int _defaultTimeoutInSeconds = 10;

    public BasePage(IWebDriver driver)
    {
        Driver = driver ?? throw new ArgumentNullException(nameof(driver));
    }

    #region Element Find Helpers

    protected IWebElement WaitAndFind(By by, int timeoutSeconds = 10)
    {
        var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutSeconds));
        return wait.Until(driver => driver.FindElement(by));
    }

    protected void WaitUntilVisible(By by, int timeoutSeconds = 10)
    {
        var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeoutSeconds));
        wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(by));
    }

    #endregion

    #region Actions

    protected void Click(By by)
    {
        try
        {
            WaitAndFind(by).Click();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Click Error] Locator: {by}. Error: {ex.Message}");
            throw;
        }
    }

    protected void EnterText(By by, string text)
    {
        try
        {
            var element = WaitAndFind(by);
            element.Clear();
            element.SendKeys(text);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[EnterText Error] Locator: {by}, Text: {text}. Error: {ex.Message}");
            throw;
        }
    }

    #endregion

    #region Locator Builder

    protected By GetBy(LocatorType type, string locator)
    {
        return type switch
        {
            LocatorType.Id => By.Id(locator),
            LocatorType.Name => By.Name(locator),
            LocatorType.XPath => By.XPath(locator),
            LocatorType.Css => By.CssSelector(locator),
            LocatorType.Class => By.ClassName(locator),
            LocatorType.Tag => By.TagName(locator),
            LocatorType.LinkText => By.LinkText(locator),
            LocatorType.PartialLinkText => By.PartialLinkText(locator),
            _ => throw new ArgumentException($"Unsupported locator type: {type}")
        };
    }

    #endregion
}
