using System;
using System.Collections.Generic;
using System.Linq;
using AdvancedReqnRollTest.Enums;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace AdvancedReqnRollTest.Pages;

public abstract class BasePage
{
    protected readonly IWebDriver Driver;
    protected readonly Actions Actions;
    private string _parentWindowHandle;
    private readonly int _defaultTimeoutInSeconds = 10;

    public BasePage(IWebDriver driver)
    {
        Driver = driver ?? throw new ArgumentNullException(nameof(driver));
    }
    
    protected string RNCertButtonXpath => "//li[@title='RN']";
    protected string CertPartialXpath => "//li[@title='{0}']";
    protected string CertTextBoxId => "certstxt";
    protected static string ERSpecButtonXpath => "//li[@title='ER']";
    protected string SpecPartialXpath => "//li[@title='{0}']";
    protected string SpecTextBoxId => "specstxt";
    protected string SaveButtonXpath => "//input[@id='saveBtn']";
    protected string StatusDropDownName => "status";
    protected static string HomeRegionDropDownId => "HomeRegion";
    protected string RegionDropDownId => "region";
    protected string AddressLineId => "address";
    protected string CityId => "city";
    protected string StateId => "state";
    protected string ZipId => "zip";
    protected string TemporaryAddressLineId => "address_2";
    protected string TemporaryCityId => "city_2";
    protected string TemporaryStateId => "state_2";
    protected string TemporaryZipId => "zip_2";
    protected string NewClientLinkXpath => "//a[text()='New']";
    protected string TempFirstNameId => "firstname";
    protected string TempLastNameId => "lastname";
    protected string ClientNameId => "clientname";
    
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
    
    protected void SelectDropdownByValue(By by, string value)
    {
        try
        {
            var dropdown = GetSelectElement(by);
            dropdown.SelectByValue(value);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[DropdownByValue Error] Locator: {by}, Value: {value}. Error: {ex.Message}");
            throw;
        }
    }

    protected void SelectDropdownByText(By by, string text)
    {
        try
        {
            var dropdown = GetSelectElement(by);
            dropdown.SelectByText(text);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[DropdownByText Error] Locator: {by}, Text: {text}. Error: {ex.Message}");
            throw;
        }
    }

    private SelectElement GetSelectElement(By by)
    {
        var element = WaitAndFind(by);
        return new SelectElement(element);
    }

    protected void DoubleClick(By by)
    {
        try
        {
            var element = WaitAndFind(by);
            Actions.DoubleClick(element).Perform();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[DoubleClick Error] Locator: {by}. Error: {ex.Message}");
            throw;
        }
    }

    protected void RightClick(By by)
    {
        try
        {
            var element = WaitAndFind(by);
            Actions.ContextClick(element).Perform();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[RightClick Error] Locator: {by}. Error: {ex.Message}");
            throw;
        }
    }

    protected void HoverOverElement(By by)
    {
        try
        {
            var element = WaitAndFind(by);
            Actions.MoveToElement(element).Perform();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Hover Error] Locator: {by}. Error: {ex.Message}");
            throw;
        }
    }

    protected void JavaScriptClick(By by)
    {
        try
        {
            var element = WaitAndFind(by);
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].click();", element);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[JavaScriptClick Error] Locator: {by}. Error: {ex.Message}");
            throw;
        }
    }

    protected void ScrollIntoView(By by)
    {
        try
        {
            var element = WaitAndFind(by);
            ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[ScrollIntoView Error] Locator: {by}. Error: {ex.Message}");
            throw;
        }
    }
    
    protected By GetBy(LocatorType type, string xpathTemplate, params object[] args)
    {
        string finalLocator = string.Format(xpathTemplate, args);
        return type switch
        {
            LocatorType.Id => By.Id(finalLocator),
            LocatorType.Name => By.Name(finalLocator),
            LocatorType.XPath => By.XPath(finalLocator),
            LocatorType.Css => By.CssSelector(finalLocator),
            LocatorType.Class => By.ClassName(finalLocator),
            LocatorType.Tag => By.TagName(finalLocator),
            LocatorType.LinkText => By.LinkText(finalLocator),
            LocatorType.PartialLinkText => By.PartialLinkText(finalLocator),
            _ => throw new ArgumentException($"Unsupported locator type: {type}")
        };
    }
    
    protected void StoreParentWindow()
    {
        _parentWindowHandle = Driver.CurrentWindowHandle;
    }

    protected void SwitchToParentWindow()
    {
        if (string.IsNullOrEmpty(_parentWindowHandle))
            throw new InvalidOperationException("Parent window not stored. Call StoreParentWindow() first.");

        Driver.SwitchTo().Window(_parentWindowHandle);
    }

    protected List<string> GetChildWindowHandles()
    {
        return Driver.WindowHandles
            .Where(handle => handle != _parentWindowHandle)
            .ToList();
    }

    protected void SwitchToChildWindowByIndex(int index)
    {
        var children = GetChildWindowHandles();
        if (index < 0 || index >= children.Count)
            throw new ArgumentOutOfRangeException(nameof(index), "Invalid child window index.");

        Driver.SwitchTo().Window(children[index]);
    }

    protected void SwitchToWindowByTitle(string partialTitle)
    {
        foreach (var handle in Driver.WindowHandles)
        {
            Driver.SwitchTo().Window(handle);
            if (Driver.Title.Contains(partialTitle, StringComparison.OrdinalIgnoreCase))
                return;
        }

        throw new InvalidOperationException($"No window with title containing '{partialTitle}' found.");
    }

    protected void SwitchToWindowByUrl(string partialUrl)
    {
        foreach (var handle in Driver.WindowHandles)
        {
            Driver.SwitchTo().Window(handle);
            if (Driver.Url.Contains(partialUrl, StringComparison.OrdinalIgnoreCase))
                return;
        }

        throw new InvalidOperationException($"No window with URL containing '{partialUrl}' found.");
    }

    protected void CloseAllChildWindows()
    {
        var children = GetChildWindowHandles();
        foreach (var child in children)
        {
            Driver.SwitchTo().Window(child);
            Driver.Close();
        }
        SwitchToParentWindow();
    }
}