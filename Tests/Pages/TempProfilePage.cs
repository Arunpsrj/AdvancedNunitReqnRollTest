using System;
using AdvancedReqnRollTest.Enums;
using OpenQA.Selenium;

namespace AdvancedReqnRollTest.Pages;

public class TempProfilePage : BasePage
{
    public TempProfilePage(IWebDriver driver) : base(driver)
    {
        
    }

    private string TempFirstNameId => "firstname";
    private string TempLastNameId => "lastname";
    
    public void EnterTextForField(string fieldLabel, string value)
    {
        var locator = fieldLabel switch
        {
            "temp first name" => GetBy(LocatorType.Name, TempFirstNameId),
            "temp last name" => GetBy(LocatorType.Name, TempLastNameId),
            // Add more field mappings as needed
            _ => throw new ArgumentException($"No mapping defined for: {fieldLabel}")
        };

        EnterText(locator, value);
    }
}