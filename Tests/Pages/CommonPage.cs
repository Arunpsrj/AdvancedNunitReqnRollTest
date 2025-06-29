using System;
using System.Collections.Generic;
using System.Linq;
using AdvancedReqnRollTest.Enums;
using AdvancedReqnRollTest.Models;
using OpenQA.Selenium;

namespace AdvancedReqnRollTest.Pages;

public class CommonPage : BasePage
{
    public CommonPage(IWebDriver driver) : base(driver)
    {
        
    }

    private string TabNameIdValue => "cv_Tab_{0}";
    private string NewTempLinkXpath => "//a[text()='New']";
    private Dictionary<string, string> _buttonXpaths;
    private static readonly Random _random = new();
    
    private Dictionary<string, string> ButtonXpaths => _buttonXpaths ??= new()
    {
        { "New Temp link", NewTempLinkXpath },
        { "RN Cert", RNCertButtonXpath },
        { "ER Spec", ERSpecButtonXpath },
        { "Temp Save", SaveButtonXpath },
        { "New Client link", NewClientLinkXpath },
        { "Client Save", SaveButtonXpath }
    };
    
    public void NavigateToTab(string tabName)
    {
        Click(GetBy(LocatorType.Id, TabNameIdValue, tabName));
    }

    public void ClickButtonXpath(string buttonName)
    {
        if (ButtonXpaths.TryGetValue(buttonName, out var xpath))
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
            ? GenerateRandomWord(7)
            : value;
    }

    private string GenerateRandomWord(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        return new string(Enumerable.Range(0, length)
            .Select(_ => chars[_random.Next(chars.Length)]).ToArray());
    }

    public void SelectFromDropDown(string dropDownValue, string dropDownName)
    {
        if (string.IsNullOrWhiteSpace(dropDownName))
            throw new ArgumentException("Dropdown name cannot be null or empty.", nameof(dropDownName));

        By locator = dropDownName switch
        {
            "Status" => GetBy(LocatorType.Name, StatusDropDownName),
            "HomeRegion" => GetBy(LocatorType.Id, HomeRegionDropDownId),
            "Region" => GetBy(LocatorType.Id, RegionDropDownId),
            _ => throw new ArgumentOutOfRangeException(nameof(dropDownName), $"Dropdown '{dropDownName}' is not supported.")
        };

        SelectDropdownByText(locator, dropDownValue);
    }
    
    public void FillAddressFor(string type, AddressModel addressModel)
    {
        Dictionary<string, string> fieldMap = type switch
        {
            "temp-permanent" or "client" => new()
            {
                { "address", AddressLineId },
                { "city", CityId },
                { "state", StateId },
                { "zip", ZipId }
            },
            "temp-temporary" => new()
            {
                { "address", TemporaryAddressLineId },
                { "city", TemporaryCityId },
                { "state", TemporaryStateId },
                { "zip", TemporaryZipId }
            },
            _ => throw new ArgumentException($"Unsupported address type: {type}")
        };

        EnterText(GetBy(LocatorType.Id, fieldMap["address"]), addressModel.Address);
        EnterText(GetBy(LocatorType.Id, fieldMap["city"]), addressModel.City);
        EnterText(GetBy(LocatorType.Id, fieldMap["state"]), addressModel.State);
        EnterText(GetBy(LocatorType.Id, fieldMap["zip"]), addressModel.Zip);
    }
    
    public void EnterTextForField(string fieldLabel, string value)
    {
        var locator = fieldLabel switch
        {
            "temp first name" => GetBy(LocatorType.Name, TempFirstNameId),
            "temp last name" => GetBy(LocatorType.Name, TempLastNameId),
            "client name" => GetBy(LocatorType.Id,ClientNameId),
            // Add more field mappings as needed
            _ => throw new ArgumentException($"No mapping defined for: {fieldLabel}")
        };
        EnterText(locator, value);
    }
}