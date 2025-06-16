using System;
using System.Collections.Generic;
using AdvancedReqnRollTest.Enums;
using OpenQA.Selenium;

namespace AdvancedReqnRollTest.Pages;

public class CommonPage : BasePage
{
    public CommonPage(IWebDriver driver) : base(driver)
    {
        
    }

    private string TabNameIdValue => "cv_Tab_{0}";
    private static string NewTempLinkXpath => "//a[text()='New']";
    private static string RNCertButtonXpath => "//li[@title='RN']";
    private static string ERSpecButtonXpath => "//li[@title='ER']";
    private static string TempSaveButtonXpath => "//input[@id='saveBtn']";
    
    private readonly Dictionary<string, string> _buttonXpaths = new()
    {
        { "New Temp link", NewTempLinkXpath },
        { "RN Cert", RNCertButtonXpath },
        { "ER Spec", ERSpecButtonXpath },
        {"Temp Save", TempSaveButtonXpath}
    };
    
    public void NavigateToTab(string tabName)
    {
        Click(GetBy(LocatorType.Id, TabNameIdValue, tabName));
    }

    public void ClickButtonXpath(string buttonName)
    {
        if (_buttonXpaths.TryGetValue(buttonName, out var xpath))
        {
            Click(GetBy(LocatorType.XPath, xpath));
        }
        else
        {
            throw new ArgumentException($"No XPath found for button: {buttonName}");
        }
    }
    
    public string ResolveDynamicValue(string value)
    {
        return value.Equals("<unique_text>", StringComparison.OrdinalIgnoreCase)
            ? $"Auto_{Guid.NewGuid().ToString("N")[..6]}"
            : value;
    }
}